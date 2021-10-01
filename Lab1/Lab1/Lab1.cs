using System;
using System.Collections.Generic;


namespace Lab1
{
    class Lab1
    {
        public void menu()
        {
            Console.Write("Hello World!!! My First C# App\n" +
                "Options\n" +
                "----------\n" +
                "1 - Import Words from File" +
                "\n2 - Bubble Sort words" +
                "\n3 - LINQ / Lambda sort words" +
                "\n4 - Count the Distinct Words" +
                "\n5 - Take the first 10 words" +
                "\n6 - Get the number of words that start with 'j' and display the count" +
                "\n7 - Get and display of words that end with 'd' and display the count" +
                "\n8 - Get and display of words that are greater than 4 characters long, and display the count" +
                "\n9 - Get and display of words that are less than 3 characters long and start with the letter 'a', and display the count" +
                "\nx – Exit" +
                "\n\nMake a selection: ");
        }

        void sleep() { }

        public static void Main(string[] args)
        {
            Lab1 l = new Lab1();
            Words w = new Words();

            IList<string> words = new List<string>();

            String userInput = "z";
            int loop = 1;



            do
            {
                try
                {

                    l.menu();
                    userInput = Console.ReadLine();

                    switch (userInput)
                    {
                        case "1":
                            Console.Clear();
                            words.Clear();
                            words = w.readFile();
                            break;
                        case "2":
                            Console.Clear();
                            w.bubbleSortWords(words);
                            break;
                        case "3":
                            Console.Clear();
                            w.linqLambaSortWords(words);
                            break;
                        case "4":
                            Console.Clear();
                            w.distinctCounter(words);
                            break;
                        case "5":
                            Console.Clear();
                            w.tenWords(words);
                            break;
                        case "6":
                            Console.Clear();
                            w.jDisplayCount(words);
                            break;
                        case "7":
                            Console.Clear();
                            w.dDsiplayCount(words);
                            break;
                        case "8":
                            Console.Clear();
                            w.greaterFour(words);
                            break;
                        case "9":
                            Console.Clear();
                            w.lessThree(words);
                            break;
                        case "x":
                            loop = 0;
                            break;
                        default:
                            Console.Clear();
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Invalid Input\n\n");
                            Console.ResetColor();
                            break;
                    }
                }
                catch (ArgumentException ae)
                {
                    Console.WriteLine("Argument Exception while reading menu");
                }




            } while (loop == 1);


        }
    }
}
