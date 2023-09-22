namespace Calc
{
    public class UnaryOperation
    {
        public delegate object OperationInvoker(object arg1);
        public new OperationInvoker Invoker { get; set; }
        public char Symbol { get; set; }
        public UnaryOperation(char symbol, OperationInvoker invoker)
        {
            Symbol = symbol;
            Invoker = invoker;
        }
    }
}
