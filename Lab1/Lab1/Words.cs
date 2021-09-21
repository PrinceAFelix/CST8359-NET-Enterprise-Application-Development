using System;
using System.IO;
using System.Threading;
using System.Collections.Generic;
using System.Linq;


namespace Lab1
{

    class Words
    {
  
        
        //No Arg Constructor
        public Words() { }

        //Read words from file
        public IList<string> readFile()
        {
            int counter = 0;

            IList<string> list = new List<string>();

            try
            {
                using (StreamReader sr = new StreamReader("Words.txt"))
                {

                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        list.Add(line);
                        counter++;
                    }
                }

            }
            catch (IOException ioe)
            {
                Console.WriteLine("Exception while reading file");
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception while reading file");
            }



            Console.WriteLine("Reading Words");



            Console.WriteLine("Reading Words Complete\nNumber of words found: " + counter + "\n\n");

            return list;

        }

        //Sort the words from file using bubble sort
        public IList<string> bubbleSortWords(IList<string> list)
        {

            int n = list.Count;
            int i = 0;

            String[] passList = new String[n];

            foreach (var w in list)
            {
                passList[i] = list[i];
                i++;
            }

            string temp;

            var time = System.Diagnostics.Stopwatch.StartNew();
            for (i = 0; i < n; i++)
            {
                for (int j = 0; j < n - i - 1; j++)
                {
                    if (passList[j].CompareTo(passList[j + 1]) > 0)
                    {
                        temp = passList[j];
                        passList[j] = passList[j + 1];
                        passList[j + 1] = temp;
                    }
                }
            }
            time.Stop();
            Console.WriteLine("Time elapsed: " + time.ElapsedMilliseconds + "ms\n\n");



            return passList;
        }

        //Sort the words from file using LINQ query or a Lambda expression
        public IList<string> linqLambaSortWords(IList<string> list)
        {

            String[] passList = new String[list.Count];

            for (int i = 0; i < list.Count; i++)
                passList[i] = list[i];

            var time = System.Diagnostics.Stopwatch.StartNew();

            var w = from words in passList
                    orderby words
                    select words;

            foreach (var n in w) { }

            time.Stop();

            Console.WriteLine("Time elapsed: " + time.ElapsedMilliseconds + "ms\n\n");


            return passList;
        }

        //Count the distinct words
        public void distinctCounter(IList<string> list)
        {

            int count = 0;

            count = list.Distinct().Count();

            Console.WriteLine("The number of distinct words is: " + count + "\n\n");

        }

        //Take first 10 words
        public void tenWords(IList<string> list)
        {
            var words = list.Take(10);

            foreach (var word in words)
                Console.WriteLine(word);

            Console.WriteLine("\n");
        }

        //Count the number of words that starts with letter 'j'
        public void jDisplayCount(IList<string> list)
        {
            int count = 0;
            var jWords = from x in list
                         where x.StartsWith('j')
                         select x;

            foreach (var word in jWords)
            {
                Console.WriteLine(word);
                count++;
            }

            Console.WriteLine("Number of words that start with 'j': " + count + "\n\n");

        }

        //Display and Count the words that ends with letter 'd'
        public void dDsiplayCount(IList<string> list)
        {
            int count = 0;
            var dWords = from x in list
                         where x.EndsWith('d')
                         select x;

            foreach (var word in dWords)
            {
                Console.WriteLine(word);
                count++;
            }

            Console.WriteLine("Number of words that end with 'd': " + count + "\n\n");

        }

        //Dislay and Count the words with more than 4 character
        public void greaterFour(IList<string> list)
        {
            int count = 0;
            var dWords = from x in list
                         where x.Length > 4
                         select x;

            foreach (var word in dWords)
            {
                Console.WriteLine(word);
                count++;
            }

            Console.WriteLine("Number of words longer than 4 characters: " + count + "\n\n");
        }

        //Dislay and Count the words with less than 3 character that starts with letter 'a'
        public void lessThree(IList<string> list)
        {


            int count = 0;

            var dWords = from x in list
                         where x.Length < 3
                         where x.StartsWith('a')
                         select x;

            foreach (var word in dWords)
            {
                Console.WriteLine(word);
                count++;
            }

            Console.WriteLine("Number of words longer less than 3 characters and start with 'a' " + count + "\n\n");
        }

    }
}
