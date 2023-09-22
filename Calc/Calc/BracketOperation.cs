namespace Calc
{
    public class BracketOperation
    {
        public char OpenSymbol { get; private set; }
        public char CloseSymbol { get; private set; }
        public BracketOperation(char openSymbol, char closeSymbol)
        {
            OpenSymbol = openSymbol;
            CloseSymbol = closeSymbol;
        }
    }
}
