using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Google1B2009
{
    public static class Parser
    {       
        public static Node Parse(Queue<string> tokens)
        {
            tokens.Dequeue();

            double weight = double.Parse(tokens.Dequeue());
          
            Node tree = new Node(weight,null);

            string s = tokens.Dequeue();
            if (s.Equals(")"))
                return tree;

            tree.Name = s;
            tree.LeftChild = Parse(tokens);
            tree.RightChild = Parse(tokens);
            tokens.Dequeue();
            return tree;

        
        }

        public static string LineParser(StreamReader file, int lines)
        {
            string text = String.Empty;
            for (int i = 0; i < lines; i++)
            {
                text += file.ReadLine() + "\n" ;
            }

            return text; //This function is like the multiple fgets() we make ;) C# makes it easy bro.
        }
    }
}
