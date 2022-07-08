namespace Task13.Models
{
    public class Paydesk
    {
        #region Events
        public static event Func<int, string, string> PriorityCalculatorFunction;
        public static event Action<string> OnServedCustomerEvent;
        #endregion

        private int _numberOfUsersNorma;

        #region Properties
        public string Profile { get; private set; }
        public int Number { get; }
        public int NumberOfUsersNorma
        {
            get => _numberOfUsersNorma;
            private set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("Normal number of users for paydesk can't be less or equal to 0!");
                }
                _numberOfUsersNorma = value;
            }
        }
        public Coordinates Coordinates { get; }
        public bool IsWorking { get; private set; }
        public int Count
        {
            get
            {
                lock (_peopleQueue)
                {
                    return _peopleQueue.Count;
                }
            }
        }
        #endregion

        private PriorityQueue<Person, string> _peopleQueue;
        public Paydesk(int number, int xCoordinate, int yCoordinate, bool isWorking, int numberOfUsersNorma, string profile = "all")
        {
            IsWorking = isWorking;
            Number = number;
            Profile = profile;
            NumberOfUsersNorma = numberOfUsersNorma;
            Coordinates = new Coordinates(xCoordinate, yCoordinate);
            _peopleQueue = new PriorityQueue<Person, string>(new StringComparer());
            if (IsWorking)//Якщо каса відкрита, запускаю процесс обслуговування клієнтів
            {
                var _ = OpenPaydeskAsync();
            }
        }
        public async Task EnqueueAsync(Person person)
        {
            await Task.Run(() =>
            {
                var personPriority = PriorityCalculatorFunction?.Invoke(person.Age, person.Status);
                if (personPriority == null)
                {
                    personPriority = person.Status + (200 - person.Age).ToString();//Дефолтний калькулятор пріоритету людини
                }
                lock (_peopleQueue)
                {
                    _peopleQueue.Enqueue(person, personPriority);
                }
            });
        }
        private async Task ServeCustomersAsync()
        {
            await Task.Run(() =>
            {
                while (IsWorking)
                {
                    Person? customer = null;
                    lock (_peopleQueue)
                    {
                        _peopleQueue.TryDequeue(out customer, out string _);
                    }
                    if (customer != null &&
                    (Profile == "all" || customer.Status.Equals(Profile)))
                    {
                        Thread.Sleep(customer.AvailableTime);
                        OnServedCustomerEvent?.Invoke(
                            $"Paydesk number: {Number}\n" +
                            $"Date and time of service: {DateTime.Now}\n" +
                            $"Customer information:\n{customer}");
                    }
                }
            });
        }
        public async Task OpenPaydeskAsync()
        {
            IsWorking = true;
            try
            {
                await ServeCustomersAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message} Paydesk number {Number}");
            }
        }
        public PriorityQueue<Person, string> ClosePaydesk()
        {
            IsWorking = false;
            return _peopleQueue;
        }
        public PriorityQueue<Person, string> RepurposePaydesk(string profile)
        {
            Profile = profile;
            SplitQueueByPeopleStatus(profile, out var repurposedQueue, out var otherCustomers);
            lock (_peopleQueue)
            {
                _peopleQueue = repurposedQueue;
            }
            return otherCustomers;
        }
        public IEnumerable<Person> GetControlPassengersGroup(string status)
        {
            var controlPassengersGroup = new List<Person>();
            SplitQueueByPeopleStatus(status, out var repurposedQueue, out var otherCustomers);
            lock (_peopleQueue)
            {
                _peopleQueue = otherCustomers;
            }
            while (repurposedQueue.TryDequeue(out var person, out string _))
            {
                controlPassengersGroup.Add(person);
            }
            return controlPassengersGroup;
        }
        private void SplitQueueByPeopleStatus(string status,
            out PriorityQueue<Person, string> repurposedQueue,
            out PriorityQueue<Person, string> otherCustomers)
        {
            repurposedQueue = new PriorityQueue<Person, string>(new StringComparer());
            otherCustomers = new PriorityQueue<Person, string>(new StringComparer());
            lock (_peopleQueue)
            {
                while (_peopleQueue.Count > 0)
                {
                    _peopleQueue.TryDequeue(out var person, out var priority);
                    if (status.Equals("all") || person.Status.Equals(status))
                    {
                        repurposedQueue.Enqueue(person, priority);
                    }
                    else
                    {
                        otherCustomers.Enqueue(person, priority);
                    }
                }
            }
        }
    }
}
