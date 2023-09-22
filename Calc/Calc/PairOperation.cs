namespace Calc
{
    public class PairOperation
    {
        public delegate object OperationInvoker(object arg1, object arg2);
        public char Symbol { get; set; }
        public int Priority { get; set; }
        public OperationInvoker Invoker { get; set; }
        public PairOperation(char symbol, int priority, OperationInvoker invoker)
        {
            Symbol = symbol;
            Invoker = invoker;
            Priority = priority;
        }
    }
}
