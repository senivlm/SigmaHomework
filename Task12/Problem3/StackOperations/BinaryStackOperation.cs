namespace Task12.Problem3.StackOperations
{
    public class BinaryStackOperation : StackOperation
    {
        public Func<double, double, double> OnOperationAction { get; private set; }
        public BinaryStackOperation(string stringRepresentation, int priority, Func<double, double, double> onOperationAction) :
            base(stringRepresentation, priority)
        {
            OnOperationAction = onOperationAction;
        }
        public override void ExecuteOperation(Stack<double> variables)
        {
            double value2 = variables.Pop();
            double value1 = variables.Pop();

            double? operationResult = OnOperationAction?.Invoke(value1, value2);
            if (operationResult == null)
            {
                throw new ArgumentNullException($"Result of operation \"{StringRepresentation}\" was null!");
            }
            variables.Push((double)operationResult);
        }
    }
}
