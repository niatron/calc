﻿using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Calc
{
    public class Calculator
    {
        private double GetDoubleFromString(string expression, ref int startPos)
        {
            var result = string.Empty;
            var wasSeparator = false;

            while (startPos < expression.Length)
            {
                var c = expression[startPos];
                if (Constants.IsDigitSeparator(c))
                    if (wasSeparator)
                        throw new IncorrectExpressionException($"Incorrect number {result}");
                    else
                    {
                        result += c;
                        wasSeparator = true;
                    }
                else if (Char.IsDigit(c))
                    result += c;
                else
                    break;
                startPos++;
            }
            if (double.TryParse(result, out double value))
                return value;
            else
                throw
                    new IncorrectExpressionException($"Can't get number from string '{result}'");
        }
        public Calculator(PairOperation[] binaryOperations, UnaryOperation[] unaryOperations, BracketOperation[] bracketOperations)
        {
            this.pairOperations = new Dictionary<char, PairOperation>();
            this.unaryOperations = new Dictionary<char, UnaryOperation>();
            this.bracketOperationsByOpenSymbol = new Dictionary<char, BracketOperation>();
            this.bracketOperationsByCloseSymbol = new Dictionary<char, BracketOperation>();
            foreach (var op in binaryOperations)
                pairOperations.Add(op.Symbol, op);
            foreach (var op in unaryOperations)
                this.unaryOperations.Add(op.Symbol, op);
            foreach (var op in bracketOperations)
            {
                this.bracketOperationsByOpenSymbol.Add(op.OpenSymbol, op);
                this.bracketOperationsByCloseSymbol.Add(op.CloseSymbol, op);
            }
        }
        public static Calculator GetStandart()
        {
            var pairOperations = new PairOperation[]
            {
                new PairOperation('+', 1, (a, b) => (double)a + (double)b),
                new PairOperation('-', 1, (a, b) => (double)a - (double)b),
                new PairOperation('*', 2, (a, b) => (double)a * (double)b),
                new PairOperation('/', 2, (a, b) => (double)a / (double)b)
            };
            var unaryOperations = new UnaryOperation[]
            {
                new UnaryOperation('+', (a) => a),
                new UnaryOperation('-', (a) => -(double)a)
            };
            var bracketOperations = new BracketOperation[]
            {
                new BracketOperation('(',')')
            };
            return new Calculator(pairOperations, unaryOperations, bracketOperations);
        }
        Dictionary<char, PairOperation> pairOperations;
        Dictionary<char, UnaryOperation> unaryOperations;
        Dictionary<char, BracketOperation> bracketOperationsByOpenSymbol;
        Dictionary<char, BracketOperation> bracketOperationsByCloseSymbol;
        private List<object> ToPostfix(string expression)
        {
            var postfixList = new List<object>();
            var operationStack = new Stack<object>();
            var currentOerationWillUnary = true;

            for (int i = 0; i < expression.Length; i++)
            {
                var c = expression[i];
                if (Constants.IsSpace(c))
                {
                    continue;
                }
                else if (Char.IsDigit(c))
                {
                    var value = GetDoubleFromString(expression, ref i);
                    i--;
                    postfixList.Add(value);
                    currentOerationWillUnary = false;
                }
                else if (bracketOperationsByOpenSymbol.ContainsKey(c))
                {
                    operationStack.Push(bracketOperationsByOpenSymbol[c]);
                    currentOerationWillUnary = true;
                }
                else if (bracketOperationsByCloseSymbol.ContainsKey(c))
                {
                    while (operationStack.Count > 0 && operationStack.Peek() is not BracketOperation)
                        postfixList.Add(operationStack.Pop());
                    if (operationStack.Count == 0 || operationStack.Peek() is not BracketOperation @barcket || barcket.CloseSymbol != c)
                        throw new IncorrectExpressionException($"Incorrect '{c}' at position {i + 1}");
                    operationStack.Pop();
                    currentOerationWillUnary = false;
                }
                else if (currentOerationWillUnary && unaryOperations.ContainsKey(c))
                {
                    operationStack.Push(unaryOperations[c]);
                }
                else if (pairOperations.ContainsKey(c))
                {
                    while (operationStack.Count > 0 && operationStack.Peek() is PairOperation @operation && operation.Priority >= pairOperations[c].Priority)
                        postfixList.Add(operationStack.Pop());
                    operationStack.Push(pairOperations[c]);
                    currentOerationWillUnary = true;
                }
                else
                {
                    throw new IncorrectExpressionException($"Unknown symbol '{c}'");
                }
            }
            foreach (var op in operationStack)
                if (op is BracketOperation)
                    throw new IncorrectExpressionException($"Incorrect bracket");
                else
                    postfixList.Add(op);

            return postfixList;
        }
        public double Calc(string expression) => Calc(ToPostfix(expression));
        
        static double Calc(List<object> postfixList)
        {
            var argumentStack = new Stack<double>();

            foreach (var exp in postfixList)
            {
                if (exp is double @expDouble)
                {
                    argumentStack.Push(@expDouble);
                }
                else if (exp is UnaryOperation @unaryOperation)
                {
                    if (argumentStack.Count < 1)
                        throw new IncorrectExpressionException($"Incorrect argument count");
                    var arg1 = argumentStack.Pop();
                    argumentStack.Push((double)@unaryOperation.Invoker.Invoke(arg1));
                }
                else if (exp is PairOperation @operation)
                {
                    if (argumentStack.Count < 2)
                        throw new IncorrectExpressionException($"Incorrect argument count");
                    var arg2 = argumentStack.Pop();
                    var arg1 = argumentStack.Pop();

                    argumentStack.Push((double)operation.Invoker.Invoke(arg1, arg2));
                }
            }
            if(argumentStack.Count != 1)
                throw new IncorrectExpressionException($"Incorrect argument count");

            return argumentStack.Pop();
        }
    }
}
