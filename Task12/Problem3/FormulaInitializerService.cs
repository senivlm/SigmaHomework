namespace Task12.Problem3
{
    public class FormulaInitializerService
    {
        private FormulaCalculatorBuilder _formulaCalculator = new FormulaCalculatorBuilder();
        public FormulaCalculator InitializeFromConsole()
        {
            _formulaCalculator.ResetFormula();

            while (true)
            {
                Console.WriteLine($"Your formula: {_formulaCalculator.Formula}\n{new string('-', 50)}");
                Console.WriteLine(
                    "1) Add operation;\n" +
                    "2) Add number;\n" +
                    "3) Add opening bracket;\n" +
                    "4) Add closing bracket;\n" +
                    "5) Get formula;");
                Console.Write("Choose number: ");
                var userChoise = Console.ReadLine();
                if (userChoise != null && int.TryParse(userChoise, out int number) && number > 0 && number < 6)
                {
                    try
                    {
                        switch (number)
                        {
                            case 1:
                                Console.WriteLine("Available operations: ");
                                Console.WriteLine(string.Join(", ", FormulaCalculator.GetAvailableOperations()));
                                Console.Write("Write an operation you want to add to formula: ");
                                _formulaCalculator.AddOperation(Console.ReadLine());
                                break;
                            case 2:
                                Console.Write("Write a number you want to add to formula: ");
                                _formulaCalculator.AddNumber(Console.ReadLine());
                                break;
                            case 3:
                                _formulaCalculator.AddOpeningBracket();
                                break;
                            case 4:
                                _formulaCalculator.AddClosingBracket();
                                break;
                            case 5:
                                Console.WriteLine($"Console initialized formula: {_formulaCalculator.Formula}");
                                return _formulaCalculator.CreateFormulaCalculator();
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadKey();
                    }
                }
                Console.Clear();
            }
        }
    }
}
