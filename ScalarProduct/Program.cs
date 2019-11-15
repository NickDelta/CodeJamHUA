using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ScalarProduct
{
    class Program
    {
        static void Main(string[] args)
        {

            var file = new StreamReader(@"C:\Users\Delta\source\repos\CodeJamHUA\ScalarProduct\large.in");

            var output = new StreamWriter(@"C:\Users\Delta\source\repos\CodeJamHUA\ScalarProduct\large_out.txt");
            output.AutoFlush = true;

            int cases = int.Parse(file.ReadLine());

            for (int i = 1; i <= cases; i++)
                output.WriteLine("Case #{0}: {1}", i,Calculate(file));


            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();

        }

        public static long Calculate(StreamReader file)
        {
            int size = int.Parse(file.ReadLine());

            long[] a = Array.ConvertAll(file.ReadLine().Split(), long.Parse);
            long[] b = Array.ConvertAll(file.ReadLine().Split(), long.Parse);

            Array.Sort(a, new AscendingIntComparer());
            Array.Sort(b, new DescendingIntComparer());

            long sum = 0;
            for (int j = 0; j < size; j++)
                sum += a[j] * b[j];

            return sum;
        }
    }

    public class DescendingIntComparer : IComparer<long>
    {
        public int Compare(long x, long y)
        {
            return y.CompareTo(x);
        }
    }
    public class AscendingIntComparer : IComparer<long>
    {
        public int Compare(long x, long y)
        {
            return x.CompareTo(y);
        }
    }
}
