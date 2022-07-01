namespace Task12.Problem3.StackOperations
{
    public class UnaryStackOperation : StackOperation
    {
        public Func<double, double> OnOperationAction { get; private set; }
        public UnaryStackOperation(string stringRepresentation, int priority, Func<double, double> onOperationAction) :
            base(stringRepresentation, priority)
        {
            OnOperationAction = onOperationAction;
        }
        public override void ExecuteOperation(Stack<double> variables)
        {
            double value = variables.Pop();

            double? operationResult = OnOperationAction?.Invoke(value);
            if (operationResult == null)
            {
                throw new ArgumentNullException($"Result of operation \"{StringRepresentation}\" was null!");
            }
            variables.Push((double)operationResult);
        }
    }
}
