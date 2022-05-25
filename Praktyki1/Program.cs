using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Praktyki1
{
    public class Program
    {
        static void Main(string[] args)
        {
            Stack<double> myStack = new Stack<double>(); // tworzymy stos potrzebyny do przerowadzania operacji w notacji polskiej 
            while(true) // nieskończona pętla 
            {
                foreach (double d in myStack) 
                    Console.Write(d + " "); // wypisywanie elemnetów znajdujących się na stosie 
                Console.Write(">");
                var input=Console.ReadLine(); // wczytywanie danych od użytkownika 
                myStack=CalculateRPN(input.ToLower(), myStack); //przypisanie stosu po przeprowadzonej operacji 
                
            }
        }
        static Stack<double> CalculateRPN(string rpn,Stack<double> stack) // funkcja odpowiedzialana za przerowadzanie operacji
        {
            string[] rpnTokens = rpn.Split(' '); //dzieli input na części 
            foreach (string token in rpnTokens) // dla każdej z części sprawdzamy za co odpowiada
            {
                if (double.TryParse(token, out double number)) // jeśli jest sama liczba dodajemy do stosu 
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
                                    if(number !=0)
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
                                {
                                    var first = stack.Pop();
                                    var second = stack.Pop();
                                    if(second != 0)
                                    stack.Push(first %second);
                                }
                                    
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
                        case "sin":
                            {
                                if(stack.Count > 0)
                                stack.Push(Math.Sin(stack.Pop()));
                                break;
                            }
                        case "cos":
                            {
                                if (stack.Count > 0)
                                    stack.Push(Math.Cos(stack.Pop()));
                                break;
                            }
                        default:
                            Console.WriteLine("Not Implemented");
                            break;
                    }
                }
            }

            return stack;
        }

    }
}
