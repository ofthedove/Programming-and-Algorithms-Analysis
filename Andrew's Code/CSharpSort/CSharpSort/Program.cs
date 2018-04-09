using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsertionSort
{
    class Program
    {
        enum sortAlgos { NotYetSet, Insertion, Bubble, Merge };

        static void Main(string[] args)
        {
            sortAlgos sortAlgo;
            string inputPath;
            if (ParseCmdArgs(args, out sortAlgo, out inputPath) == false)
            {
                return;
            }

            List<float> data = ReadInputFile(inputPath);

            switch (sortAlgo)
            {
                case sortAlgos.Insertion:
                    SortInsertion(data);
                    break;
                case sortAlgos.Bubble:
                    SortBubble(data);
                    break;
                case sortAlgos.Merge:
                    SortMerge(data);
                    break;
            }
        }

        static bool ParseCmdArgs(string[] args, out sortAlgos sortAlgo, out string inputPath)
        {
            sortAlgo = sortAlgos.NotYetSet;
            inputPath = "";

            if (args.Length < 2)
            {
                System.Console.WriteLine("Requires two arguments, only " + args.Length + " provided");
                System.Console.WriteLine("Correct Usage: Sort [insertion/bubble/merge] [C:\\Path\\To\\Input.file]");
                return false;
            }

            string argAlgo = args[0];
            string argPath = args[1];

            switch (argAlgo)
            {
                case "insertion":
                    sortAlgo = sortAlgos.Insertion;
                    break;
                case "bubble":
                    sortAlgo = sortAlgos.Bubble;
                    break;
                case "merge":
                    sortAlgo = sortAlgos.Merge;
                    break;
                default:
                    System.Console.WriteLine(argAlgo + " is not a valid sorting algorithm.");
                    System.Console.WriteLine("Correct Usage: Sort [insertion/bubble/merge] [C:\\Path\\To\\Input.file]");
                    return false;
            }

            if (File.Exists(argPath) == false)
            {
                System.Console.WriteLine("Specified input path invalid or does not exist.");
                System.Console.WriteLine("Correct Usage: Sort [insertion/bubble/merge] [C:\\Path\\To\\Input.file]");
                return false;
            }
            inputPath = argPath;

            return true;
        }

        static List<float> ReadInputFile(string inputPath)
        {
            List<float> data = new List<float>();

            try
            {
                using (FileStream fs = File.Open(inputPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                using (BufferedStream bs = new BufferedStream(fs))
                using (StreamReader sr = new StreamReader(bs))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        float item;
                        if (float.TryParse(line, out item))
                        {
                            data.Add(item);
                        }
                        else
                        {
                            System.Console.WriteLine("Error parsing input file at line " + line.ToString());
                            System.Console.WriteLine("Input file must contain one float per line");
                        }
                    }
                }
            }
            catch (Exception e)
            {
                System.Console.WriteLine("Exception thrown when attempting to read input file");
                System.Console.WriteLine("Exception message: " + e.Message);
                return null;
            }

            return data;
        }

        private static void SortInsertion(List<float> data)
        {
            throw new NotImplementedException();
        }

        private static void SortBubble(List<float> data)
        {
            throw new NotImplementedException();
        }

        private static void SortMerge(List<float> data)
        {
            throw new NotImplementedException();
        }
    }
}
