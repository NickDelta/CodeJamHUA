using System;
using System.Collections.Generic;
using System.Threading;
using System.Globalization;
using static Google1B2009.Tokenization;
using static Google1B2009.Parser;

namespace Google1B2009
{
    public static class Program
    {
        static void Main(string[] args)
        {
            //We need the US Locale for the numbers to be in the format X.XXXXXXX
            var thread = new Thread(Execute);
            thread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US");
            thread.Start();

        }

        private static void Execute()
        {
            System.IO.StreamReader file = new System.IO.StreamReader(@"C:\Users\Delta\Desktop\small.in");

            System.IO.StreamWriter output = new System.IO.StreamWriter(@"C:\Users\Delta\Desktop\out.txt");
            output.AutoFlush = true; //Used to flush the Stream buffer automatically (C# Only)

            int cases = int.Parse(file.ReadLine());

            //For each case
            for (int i = 1; i <= cases; i++)
            {

                int lines = int.Parse(file.ReadLine());

                var text = LineParser(file, lines);

                var tokenQueue = TokenizeString(text); //Tokenize the tree text
                var root = Parse(tokenQueue); //Parse the tree of the case

                output.WriteLine("Case #{0}:", i ); //Write the case number in the output file

                Console.WriteLine("Case #{0}'s Binary Tree:", i);
                root.Print(); //Extra: Print to the console the tree graphically ( Not neccessary )

                lines = int.Parse(file.ReadLine());
                text = LineParser(file, lines);
                tokenQueue = TokenizeString(text); //Tokenize the Amimals + their features


                for (int animals = 0; animals < lines; animals++)
                {
                    string animal = tokenQueue.Dequeue();
                    int f = int.Parse(tokenQueue.Dequeue());

                    HashSet<string> features = new HashSet<string>();
                    for (int j = 0; j < f; j++)
                    {
                        features.Add(tokenQueue.Dequeue());
                    }
                    output.WriteLine("{0:F7}", root.CalcProb(features)); //Write the results in the output file
                }              

            }


            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }


    }
}
