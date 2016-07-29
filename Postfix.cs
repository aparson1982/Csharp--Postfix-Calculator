
using System;
using System.Text;
using System.Collections;

namespace ConsoleApplication7
{
    class Postfix
    {
        //Main
        static void Main(string[] args)
        {
            Console.WriteLine("Enter a postfix expression separated by spaces:  \n");

            string line = Console.ReadLine();

            Stack postStack = new Stack();

            while (String.IsNullOrEmpty(line) == false)
            {
                LineReader input = new LineReader(line);
                int DoubleCounts = 0;
                int OperatorCounts = 0;

                while (input.isAdjItem())  //loops through each item in the input
                {
                    double answer = 0;

                    if (input.isDouble())//if its a number it pushes onto the stack
                    {
                        double d = input.adjDouble();
                        postStack.Push(d);

                        DoubleCounts++;  //keeps track of the number of operands
                    }
                    else //else if its not a number it pops the operands off the stack and solves 
                    {
                        string words = input.adjItem();
                        string[] symbols = words.Split(' ');
                        foreach (string symbol in symbols)
                        {
                            char charVal = System.Convert.ToChar(symbol);
                            double operand1 = ((Double)postStack.Pop());
                            double operand2 = ((Double)postStack.Pop());
                            answer = Solve(charVal, operand1, operand2);

                            postStack.Push((answer));  //pushes the answer on the stack to keep track of current value

                            OperatorCounts++;  //keeps count of the number of operators

                        }
                    }
                }
                if (OperatorCounts == DoubleCounts - 1)  //ensures the postfix form is balanced
                {
                    Console.WriteLine("= " + postStack.Peek());
                    Console.WriteLine("");
                }
                else
                {
                    Console.WriteLine("Expression is invalid. \n");
                }

                line = Console.ReadLine();

            }

        }

        public static double Solve(char operation, double operand1, double operand2)
        {

            double answer = 0;
            switch (operation)
            {
                case '+':
                    answer = operand1 + operand2;
                    break;
                case '-':
                    answer = operand2 - operand1;
                    break;
                case '*':
                    answer = operand1 * operand2;
                    break;
                case '/':
                    answer = operand2 / operand1;
                    break;
            }
            return answer;
        }
    }

    class LineReader : System.IO.StringReader  //derived class LineReader
    {
        string presentLine;

        public LineReader(string source) : base(source)
        {
            readAdjLine();
        }
        private void readAdjLine()
        {

            char adjChar;
            int adjItem;

            StringBuilder theString = new StringBuilder();

            do
            {
                adjItem = this.Read();
                if (adjItem < 0)
                {
                    break;
                }

                adjChar = (char)adjItem;

                if (char.IsWhiteSpace(adjChar))
                {
                    break;
                }

                theString.Append(adjChar);
            } while (true);

            while (char.IsWhiteSpace((char)this.Peek()) && (this.Peek() >= 0))
            {
                this.Read();
            }
            if (theString.Length > 0)
            {
                presentLine = theString.ToString();
            }
            else
            {
                presentLine = null;
            }

        }

        public bool isDouble()
        {
            double sample;

            if (presentLine == null)
            {
                return false;
            }

            return double.TryParse(presentLine, out sample);
        }

        public double adjDouble()
        {
            try
            {
                return double.Parse(presentLine);
            }
            finally
            {
                readAdjLine();
            }
        }

        public bool isAdjItem()
        {
            return presentLine != null;
        }

        public string adjItem()
        {
            try
            {
                return presentLine.ToString();
            }
            finally
            {
                readAdjLine();
            }
        }
    }
}
