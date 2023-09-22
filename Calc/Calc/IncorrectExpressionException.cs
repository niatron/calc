using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Calc
{
    public class IncorrectExpressionException : Exception
    {
        public IncorrectExpressionException() : base()
        {
        }

        public IncorrectExpressionException(string message) : base(message)
        {
        }

        public IncorrectExpressionException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected IncorrectExpressionException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
