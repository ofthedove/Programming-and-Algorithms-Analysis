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
        enum sortAlgos { NotYetSet, Insertion, Bubble, Merge, Selection };

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
                    data = SortMerge(data);
                    break;
                case sortAlgos.Selection:
                    data = SortSelection(data);
                    break;
            }

            /*foreach (float item in data)
            {
                System.Console.WriteLine(item);
            }*/
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
                case "selection":
                    sortAlgo = sortAlgos.Selection;
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
            for (int i = 0; i < data.Count; i++)
            {
                for (int j = i; j > 0 && (data[j - 1] > data[j]); j--)
                {
                    float tmp = data[j];
                    data[j] = data[j - 1];
                    data[j - 1] = tmp;
                }
            }
        }

        private static void SortBubble(List<float> data)
        {
            while (true)
            {
                bool noSwaps = true;

                for (int i = 1; i < data.Count; i++)
                {
                    if (data[i - 1] > data[i])
                    {
                        float tmp = data[i];
                        data[i] = data[i - 1];
                        data[i - 1] = tmp;
                        noSwaps = false;
                    }
                }

                if (noSwaps)
                {
                    break;
                }
            }
        }

        private static List<float> SortMerge(List<float> data)
        {
            if (data.Count <= 1)
            {
                return data;
            }

            List<float> left = new List<float>();
            List<float> right = new List<float>();
            for (int i = 0; i < data.Count / 2; i++)
            {
                left.Add(data[i]);
            }
            for (int i = data.Count / 2; i < data.Count; i++)
            {
                right.Add(data[i]);
            }

            left = SortMerge(left);
            right = SortMerge(right);

            return Merge(left, right);
        }

        private static List<float> Merge(List<float> left, List<float> right)
        {
            List<float> result = new List<float>();

            while (left.Count > 0 && right.Count > 0)
            {
                if (left[0] < right[0])
                {
                    result.Add(left[0]);
                    left.RemoveAt(0);
                }
                else
                {
                    result.Add(right[0]);
                    right.RemoveAt(0);
                }
            }

            while (left.Count > 0)
            {
                result.Add(left[0]);
                left.RemoveAt(0);
            }
            while (right.Count > 0)
            {
                result.Add(right[0]);
                right.RemoveAt(0);
            }

            return result;
        }

        private static List<float> SortSelection(List<float> data)
        {
            List<float> sortedList = new List<float>();

            while (data.Count() > 0)
            {
                int minIndex = 0;
                float min = data[minIndex];
                for (int i = 1; i < data.Count(); i++)
                {
                    if (data[i] < min)
                    {
                        minIndex = i;
                        min = data[minIndex];
                    }
                }

                sortedList.Add(data[minIndex]);
                data.RemoveAt(minIndex);
            }

            return sortedList;
        }
    }
}
