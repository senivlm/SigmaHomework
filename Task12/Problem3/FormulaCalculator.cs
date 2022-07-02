using System.Text.RegularExpressions;
using Task12.Problem3.StackOperations;

namespace Task12.Problem3
{
    public class FormulaCalculator
    {
        private readonly string _formula;
        private static readonly Dictionary<string, StackOperation> _stackOperations = new List<StackOperation>()
        {
            new BinaryStackOperation("+", 1, (a,b) => a + b),
            new BinaryStackOperation("-", 1, (a,b) => a - b),
            new BinaryStackOperation("*", 2, (a,b) => a * b),
            new BinaryStackOperation("/", 2, (a,b) => a / b),
            new BinaryStackOperation("^", 3, (a,b) => Math.Pow(a,b)),
            new UnaryStackOperation("cos", 4, v => Math.Cos(v)),
            new UnaryStackOperation("sin", 4, v => Math.Sin(v))
        }.ToDictionary(so => so.StringRepresentation);//Спочатку стоврюю список, який перетворюю в словник, щоб ключ і
                                                      //StringRepresentation вказували на один і той же участок пам'яті
                                                      //(щоб не виділялась додаткова пам'ять)
        public FormulaCalculator(string formula)
        {
            _formula = Regex.Replace(formula, @"\s{2,}", " ").Trim();//Видаляю лишні пробіли
        }
        public static void AddOperation(StackOperation operation)
        {
            _stackOperations.Add(operation.StringRepresentation, operation);
        }
        public static void RemoveOperation(string stringRepresentation)
        {
            _stackOperations.Remove(stringRepresentation);
        }
        public static IEnumerable<string> GetAvailableOperations()
        {
            return _stackOperations.Keys;
        }
        public static bool ContainsOperation(string operation)
        {
            return _stackOperations.ContainsKey(operation);
        }
        public double Calculate()
        {
            var values = new Stack<double>();
            var operations = new Stack<string>();

            var partsOfFormula = _formula.Split(' ');//Ділю формулу на елементи
            foreach (var partOfFormula in partsOfFormula)
            {
                if (double.TryParse(partOfFormula, out double result))//Перевіряю чи є елемент числом
                {
                    values.Push(result);
                }
                else if (partOfFormula.Equals("("))//Якщо відкриваюча дужка, просто пушу в стек
                {
                    operations.Push(partOfFormula);
                }
                else if (partOfFormula.Equals(")"))//Якщо закриваюча дужка, виконую всі операції до зустрічі відкриваючої
                {
                    while (!operations.Peek().Equals("("))
                    {
                        _stackOperations[operations.Pop()].ExecuteOperation(values);
                    }
                    operations.Pop();
                }
                else
                {
                    var receivedOperation = _stackOperations[partOfFormula];//Отримую нову операцію, яка прийшла

                    //Якщо в стекі є якась операція і якщо пріоритет отриманої операції (не дужки) менший або рівний з
                    //пріорітетом операції в стекі, то виконую операцію зі стека
                    while (operations.Count != 0 &&
                        _stackOperations.ContainsKey(operations.Peek()) &&
                        receivedOperation.Priority <= _stackOperations[operations.Peek()].Priority)
                    {
                        _stackOperations[operations.Pop()].ExecuteOperation(values);
                    }
                    operations.Push(receivedOperation.StringRepresentation);
                }
            }

            while (operations.TryPop(out string? value))//Виконую операції, які залишились
            {
                _stackOperations[value].ExecuteOperation(values);
            }

            return values.Pop();
        }
    }
}
