using System.Text;

namespace Task12.Problem3
{
    public class FormulaCalculatorBuilder
    {
        public StringBuilder Formula { get; }
        public FormulaCalculatorBuilder()
        {
            Formula = new StringBuilder();
        }
        public void AddOperation(string operation)
        {
            operation = operation.Trim();
            if (!FormulaCalculator.ContainsOperation(operation))
            {
                throw new InvalidOperationException($"Invalid operation \"{operation}\"!");
            }
            Formula.Append(operation + " ");
        }
        public void AddNumber(string number)
        {
            number = number.Trim().Replace('.', ',');
            if (!double.TryParse(number, out double _))
            {
                throw new InvalidCastException($"Invalid number \"{number}\"!");
            }
            Formula.Append(number + " ");
        }
        public void AddOpeningBracket()
        {
            Formula.Append("( ");
        }
        public void AddClosingBracket()
        {
            Formula.Append(") ");
        }
        public void ResetFormula()
        {
            Formula.Clear();
        }
        public FormulaCalculator CreateFormulaCalculator()
        {
            return new FormulaCalculator(Formula.ToString());
        }
    }
}
