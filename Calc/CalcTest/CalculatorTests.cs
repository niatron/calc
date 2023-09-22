using Microsoft.VisualStudio.TestTools.UnitTesting;
using Calc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calc.Tests
{
    [TestClass()]
    public class CalculatorTests
    {
        Calculator calculator;
        public CalculatorTests()
        {
            calculator = Calculator.GetStandart();
        }

        [TestMethod()]
        public void GetStandartTest()
        {
            var n = 10;
            for (double a = 0; a < n; a++)
                for (double b = 0; b < n; b++)
                    for (double c = 0; c < n; c++)
                        for (double d = 1; d < n; d++)
                        {
                            Assert.AreEqual(calculator.Calc($"{a} + {b} - {c} / {d}"), a + b - c / d, $"{a} + {b} - {c} / {d}");
                            Assert.AreEqual(calculator.Calc($"{a} + {b} * {c} / {d}"), a + b * c / d, $"{a} + {b} * {c} / {d}");
                            Assert.AreEqual(calculator.Calc($"+-((-{a}) + {b}) * {c} / {d}"), +-((-a) + b) * c / d, $"+-((-{a}) + {b}) * {c} / {d}");
                        }
        }

        [TestMethod()]
        [ExpectedException(typeof(IncorrectExpressionException))]
        public void IncorretctNumberTest() => calculator.Calc("123,0,0");
        
        [TestMethod()]
        [ExpectedException(typeof(IncorrectExpressionException))]
        public void IncorretctBracketCloseTest() => calculator.Calc("(9)0)");
        
        [TestMethod()]
        [ExpectedException(typeof(IncorrectExpressionException))]
        public void IncorretctBracketOpenTest() => calculator.Calc("((90)");
        
        [TestMethod()]
        [ExpectedException(typeof(IncorrectExpressionException))]
        public void UnknownSymbolTest() => calculator.Calc("90&");

        [TestMethod()]
        [ExpectedException(typeof(IncorrectExpressionException))]
        public void IncorrectArgumentTest() => calculator.Calc("90 90");

        [TestMethod()]
        [ExpectedException(typeof(IncorrectExpressionException))]
        public void IncorrectArgument2Test() => calculator.Calc("(90+90) (90 + 90)");

        [TestMethod()]
        [ExpectedException(typeof(IncorrectExpressionException))]
        public void IncorrectOperationTest() => calculator.Calc("90*/90");
    }
}