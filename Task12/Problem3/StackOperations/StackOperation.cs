namespace Task12.Problem3.StackOperations
{
    public abstract class StackOperation
    {
        public string StringRepresentation { get; private set; }
        public int Priority { get; private set; }
        public StackOperation(string stringRepresentation, int priority)
        {
            StringRepresentation = stringRepresentation;
            Priority = priority;
        }
        public abstract void ExecuteOperation(Stack<double> variables);
    }
}