namespace Calc
{
    internal class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                var calculator = Calculator.GetStandart();
                Console.WriteLine("Input expression:");
                var expression = Console.ReadLine();
                if (expression.ToLower() == "exit")
                    break;
                try
                {
                    var result = calculator.Calc(expression);
                    Console.WriteLine(result);
                }
                catch (IncorrectExpressionException iee)
                {
                    Console.WriteLine(iee.Message);
                }
                catch(DivideByZeroException dbze)
                {
                    Console.WriteLine(dbze.Message);
                }
            }
        }
    }

    
}