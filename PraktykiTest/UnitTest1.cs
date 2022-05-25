using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PraktykiTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void CanMultiplication()
        {
            Stack<double> myStack = new Stack<double>();
            myStack=CalculateRPN("2",myStack);
            myStack = CalculateRPN("3", myStack);
            myStack = CalculateRPN("*", myStack); 
            Assert.IsTrue(myStack.Pop() == 6);  
        }
        [TestMethod]
        public void CanClear()
        {
            Stack<double> myStack = new Stack<double>();
            myStack = CalculateRPN("2", myStack);
            myStack = CalculateRPN("clr", myStack);
            Assert.AreEqual(myStack.Count,0);
        }
        [TestMethod]
        public void CanAdd()
        {
            Stack<double> myStack = new Stack<double>();
            myStack = CalculateRPN("2", myStack);
            Assert.AreEqual(myStack.Pop(), 2);
        }
        [TestMethod]
        public void CanDuplicate()
        {
            Stack<double> myStack = new Stack<double>();
            myStack = CalculateRPN("2", myStack);
            myStack = CalculateRPN("dup", myStack);
            Assert.AreEqual(myStack.Pop(), 2);
            Assert.AreEqual(myStack.Pop(), 2);
        }
        [TestMethod]
        public void CanDrop()
        {
            Stack<double> myStack = new Stack<double>();
            myStack = CalculateRPN("2", myStack);
            myStack = CalculateRPN("3", myStack);
            myStack = CalculateRPN("swap", myStack);
            Assert.AreEqual(myStack.Pop(), 2);
            Assert.AreEqual(myStack.Pop(), 3);
        }
        public Stack<double> CalculateRPN(string rpn, Stack<double> stack) // funkcja odpowiedzialana za przerowadzanie operacji
        {
            string[] rpnTokens = rpn.Split(' '); //dzieli input na czêœci 

            foreach (string token in rpnTokens) // dla ka¿dej z czêœci sprawdzamy za co odpowiada
            {
                if (double.TryParse(token, out double number)) // jeœli jest sama liczba dodajemy do stosu 
                {
                    stack.Push(number);
                }
                else
                {
                    switch (token) // wybieramy odpowiednia operacje 
                    {
                        case "^":
                        case "pow":
                            {
                                if (stack.Count > 1)
                                {
                                    number = stack.Pop();
                                    stack.Push((double)Math.Pow((double)stack.Pop(), (double)number));
                                }
                                break;
                            }
                        case "ln":
                            {
                                if (stack.Count > 0)
                                    stack.Push((double)Math.Log((double)stack.Pop(), Math.E));
                                break;
                            }
                        case "sqrt":
                            {
                                if (stack.Count > 0)
                                    stack.Push((double)Math.Sqrt((double)stack.Pop()));
                                break;
                            }
                        case "*":
                            {
                                if (stack.Count > 1)
                                    stack.Push(stack.Pop() * stack.Pop());
                                break;
                            }
                        case "/":
                            {
                                if (stack.Count > 1)
                                {
                                    number = stack.Pop();
                                    stack.Push(stack.Pop() / number);
                                }

                                break;
                            }
                        case "+":
                            {
                                if (stack.Count > 1)
                                {
                                    if (stack.Count > 0)
                                        stack.Push(stack.Pop() + stack.Pop());
                                }

                                break;
                            }
                        case "-":
                            {
                                if (stack.Count > 1)
                                {
                                    number = stack.Pop();
                                    stack.Push(stack.Pop() - number);
                                }
                                break;
                            }
                        case "clr":
                            {
                                if (stack.Count > 0)
                                    stack.Clear();
                                break;
                            }
                        case "dup":
                            {
                                if (stack.Count > 0)
                                {
                                    double top = stack.Peek();
                                    stack.Push(top);
                                }
                                break;
                            }
                        case "%":
                            {
                                if (stack.Count > 0)
                                    stack.Push(stack.Pop() % stack.Pop());
                                break;
                            }
                        case "pi":
                            {

                                stack.Push(Math.PI);
                                break;
                            }
                        case "drop":
                            {
                                if (stack.Count > 0)
                                {
                                    var newStack = stack.ToList();
                                    stack.Clear();
                                    newStack.RemoveAt(newStack.Count - 1);
                                    for (int i = 0; i < newStack.Count; i++)
                                        stack.Push(newStack[i]);
                                }
                                break;
                            }
                        case "swap":
                            {
                                if (stack.Count > 1)
                                {
                                    var top = stack.Pop();
                                    var secondTop = stack.Pop();
                                    stack.Push(top);
                                    stack.Push(secondTop);
                                }
                                break;
                            }
                        case "rand":
                            {
                                Random rnd = new Random();
                                int num = rnd.Next();
                                stack.Push(num);
                                break;
                            }
                        case "e":
                            {
                                stack.Push(Math.E);
                                break;
                            }
                        default:
                            Console.WriteLine("Error");
                            break;
                    }
                }
            }

            return stack;
        }
    }
}