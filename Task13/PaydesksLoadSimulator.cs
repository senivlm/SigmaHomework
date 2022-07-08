using Task13.Models;

namespace Task13
{
    public class PaydesksLoadSimulator
    {
        public event Func<int, string, Task>? OnQueueOverflowAction;
        private bool _wasOverflowed = false;
        private List<Paydesk> _paydesks = new List<Paydesk>();
        public PaydesksLoadSimulator(
            List<Paydesk> paydesks,
            Func<int, string, Task>? onQueueOverflowAction = null,
            IEnumerable<Person>? customers = null,
            int timeInterval = 0)
        {
            //Навішую обробник
            OnQueueOverflowAction += onQueueOverflowAction;
            _paydesks = paydesks;
            if (customers != null)//Розприділяю користувачів по касам із певним часовим інтервалом
            {
                var _ = AddCustomersWithTimeIntervalAsync(customers, timeInterval);
            }
        }
        public async Task<IEnumerable<Person>> GetPassangersControlGroupAsync(string passengersStatus)
        {
            var passangersControlGroup = new List<Person>();

            try
            {
                await Task.Run(() =>
                {
                    //Отримую контрольну групу пасажирів із кожної каси паралельно
                    Parallel.ForEach(_paydesks, (paydesk) =>
                    {
                        lock (passangersControlGroup)
                        {
                            passangersControlGroup.AddRange(paydesk.GetControlPassengersGroup(passengersStatus));
                        }
                    });
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return passangersControlGroup;
        }
        public async Task RepurposePaydeskAsync(int paydeskNumber, string newProfile = "all")
        {
            var chosenPaydesk = _paydesks.First(pd => pd.Number == paydeskNumber);
            if (chosenPaydesk != null)
            {
                var customersToAdd = await Task.Run(() => chosenPaydesk.RepurposePaydesk(newProfile));
                while (customersToAdd.Count > 0)
                {
                    await StandAtPaydeskAsync(customersToAdd.Dequeue());
                }
            }
        }
        public async Task ClosePaydeskAsync(int paydeskNumber)
        {
            var chosenPaydesk = _paydesks.First(pd => pd.Number == paydeskNumber);
            if (chosenPaydesk != null && chosenPaydesk.IsWorking == true)
            {
                var remainingPeople = chosenPaydesk.ClosePaydesk();
                while (remainingPeople.TryDequeue(out var person, out string _))
                {
                    await StandAtPaydeskAsync(person);
                }
            }
        }
        public async Task OpenPaydeskAsync(int paydeskNumber)
        {
            var chosenPaydesk = _paydesks.First(pd => pd.Number == paydeskNumber);
            if (chosenPaydesk != null && chosenPaydesk.IsWorking == false)
            {
                await chosenPaydesk.OpenPaydeskAsync();
            }
        }
        public async Task StandAtPaydeskAsync(Person person)
        {
            var suitablePaydesks = _paydesks.Where(pd => pd.IsWorking &&
                (pd.Profile.Equals("all") || pd.Profile.Equals(person.Status)));

            if (suitablePaydesks.Any())//Перевіряю чи існують підходящі для клієнта каси
            {
                var chosenPaydesk = ChooseLeastCrowdedPaydesk(suitablePaydesks);//Обираю касу із найменшою завантаженістю

                if (chosenPaydesk != null)
                {
                    //Якщо каса із найменшою завантаженістю є касою із найбільшою завантаженістю, то обираю найближчу до користувача касу
                    if (suitablePaydesks.Count() != 1 && ReferenceEquals(chosenPaydesk, suitablePaydesks.MaxBy(pd => pd.Count)))
                    {
                        chosenPaydesk = ChooseClosestPaydesk(suitablePaydesks, person);
                    }
                    //При спробі переповнення якої-небудь каси активується подія
                    if (chosenPaydesk.Count > chosenPaydesk.NumberOfUsersNorma)
                    {
                        Task overflowAction;
                        //Якщо каса переповнюється не перший раз, очікую поки кількість відвідувачів не стане меншою за половину норми
                        if (_wasOverflowed)
                        {
                            overflowAction = Task.Run(() =>
                            {
                                while (chosenPaydesk.Count > chosenPaydesk.NumberOfUsersNorma / 2)
                                {
                                    Thread.Sleep(1000);
                                }
                            });
                        }
                        //Якщо перший раз, то викликаю подію
                        else
                        {
                            overflowAction = Task.Run(async () =>
                            {
                                _wasOverflowed = true;
                                var task = OnQueueOverflowAction?.Invoke(chosenPaydesk.Number, person.Status);
                                if (task != null)
                                {
                                    await task;
                                }
                            });

                        }
                        await overflowAction;
                    }
                    await chosenPaydesk!.EnqueueAsync(person);
                }
            }
        }
        public async Task AddCustomersWithTimeIntervalAsync(IEnumerable<Person> customers, int timeInterval = 0)
        {
            foreach (var customer in customers)
            {
                await StandAtPaydeskAsync(customer);
                Thread.Sleep(timeInterval);
            }
        }
        private Paydesk? ChooseLeastCrowdedPaydesk(IEnumerable<Paydesk> suitablePaydesks)
        {
            return suitablePaydesks.MinBy(pd => pd.Count);
        }
        private Paydesk? ChooseClosestPaydesk(IEnumerable<Paydesk> suitablePaydesks, Person person)
        {
            return suitablePaydesks
                .Select(pd => new
                {
                    Paydesk = pd,
                    Distance = Math.Sqrt(Math.Pow(pd.Coordinates.X - person.Coordinates.X, 2) + Math.Pow(pd.Coordinates.Y - person.Coordinates.Y, 2))
                })
                .MinBy(pd => pd.Distance)?.Paydesk;
        }
    }
}
