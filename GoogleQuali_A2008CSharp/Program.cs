using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoogleQuali_A2008CSharp
{
   public class Program
    {
        static void Main(string[] args)
        {
            var inputFile = new StreamReader(@"C:\Users\Delta\source\repos\CodeJamHUA\GoogleQuali_A2008CSharp\small.txt");

            var outputFile = new StreamWriter(@"C:\Users\Delta\source\repos\CodeJamHUA\GoogleQuali_A2008CSharp\out.txt");
            outputFile.AutoFlush = true;

            int casesCount = int.Parse(inputFile.ReadLine());

            for (int currCase = 1; currCase <= casesCount; currCase++)
            {
                int engineCount = int.Parse(inputFile.ReadLine());

                //Skip the engine names, not required in this solution.
                for (int j = 0; j < engineCount; j++) {inputFile.ReadLine();} 

                int queriesCount = int.Parse(inputFile.ReadLine());

                //This HashSet will store all the engines used before a switch is required.
                var usedEngines = new HashSet<string>(); 
                int switches = 0;

                for(int j = 0; j < queriesCount; j++)
                {
                    string currentQuery = inputFile.ReadLine();

                    //If the engine of the query is not on the HashSet, it is added
                    usedEngines.Add(currentQuery); 

                    //If the Hashet count equals to the number of available engines then
                    //all engines were used and a switch is required to handle the last one
                    if(usedEngines.Count == engineCount)
                    {
                        switches++;
                        usedEngines.Clear();
                        usedEngines.Add(currentQuery);

                    }
                }

                //Write the output to the output file
                outputFile.WriteLine("Case #{0}: {1}", currCase, switches);     
            }

            inputFile.Close();
            outputFile.Close();
            Console.ReadKey();

        }
    }
}
