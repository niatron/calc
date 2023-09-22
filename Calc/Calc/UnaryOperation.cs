namespace Calc
{
    public class UnaryOperation
    {
        public delegate object OperationInvoker(object arg1);
        public OperationInvoker Invoker { get; private set; }
        public char Symbol { get; private set; }
        public UnaryOperation(char symbol, OperationInvoker invoker)
        {
            Symbol = symbol;
            Invoker = invoker;
        }
    }
}
