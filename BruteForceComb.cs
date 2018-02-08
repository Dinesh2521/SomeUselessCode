using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SomeStupidLogic
{
    class Program
    {
        static void Main(string[] args)
        { 
            //string[] source = new string[5];
            List<string> source = new List<string>() { "a", "b", "c", "d", "e" };
            List<string> output = new List<string>();
            output.AddRange(source);
            
            output = allCombWitoutDup(source, source, output,  5);
            foreach(string str in output)
            {
                Console.Write(str + ", ");
            }
            /*
            output = allCombWithDup(source, source, output, 5);
            foreach (string str in output)
            {
                Console.Write(str + ", ");
            }*/
            Console.WriteLine();
            Console.WriteLine("No Patterns : " + output.Count);
            Console.Read();
        }

        private static List<string> allCombWitoutDup(List<string> source, List<string> target, List<string> output, int recStepCount)
        {
            //-1 because, single character pattern is not part of the loop
            if ((recStepCount-1) <= 0)
            {
                return output;
            }

            List<string> _tmpList = new List<string>(); 
            foreach(string pat1 in target)
            {
                foreach (string pat2 in source)
                {
                    // if you wanted to avoid duplicates 
                    if(!pat1.Contains(pat2))
                    {
                        _tmpList.Add(pat1 + pat2); 
                    }
                }
            }
            output.AddRange(_tmpList);
            return allCombWitoutDup(source, _tmpList, output, recStepCount - 1); 
        }

        private static List<string> allCombWithDup(List<string> source, List<string> target, List<string> output, int recStepCount)
        {
            //-1 because, single character pattern is not part of the loop
            if ((recStepCount - 1) <= 0)
            {
                return output;
            }

            List<string> _tmpList = new List<string>();
            foreach (string pat1 in target)
            {
                foreach (string pat2 in source)
                {
                   _tmpList.Add(pat1 + pat2);
                }
            }
            output.AddRange(_tmpList);
            return allCombWithDup(source, _tmpList, output, recStepCount - 1);
        }
    }
}
