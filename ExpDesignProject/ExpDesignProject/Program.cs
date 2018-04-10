using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpDesignProject
{
    class Program
    {
        enum sortAlgos { Insertion, Bubble, Selection, Merge, NotYetSet };
        enum Programmers { Andrew, Cody };
        enum Langs { Java, CSharp, Python };

        const string inputPath = "\"C:\\Users\\andre\\classes\\IE 563 Experimental Design\\Project\\Source\\numbers10k.txt\"";
        const string andrewCodePath = "\"C:\\Users\\andre\\classes\\IE 563 Experimental Design\\Project\\Source\\Andrew's Code\\";
        const string codyCodePath = "\"C:\\Users\\andre\\classes\\IE 563 Experimental Design\\Project\\Source\\Cody's Code\\";

        const int numIterations = 3;

        static void Main(string[] args)
        {
            Random rand = new Random();

            int[,,] numTimesRun = new int[3, 2, 3];

            System.Console.WriteLine("algo\tprog\tlang\tresult");

            while (!Finished(numTimesRun, numIterations))
            {
                int algoNum;
                int progNum;
                int langNum;

                // Generate values randomly, ignoring them if they've already been done
                do
                {
                    algoNum = rand.Next(0, 3);
                    progNum = rand.Next(0, 2);
                    langNum = rand.Next(0, 3);
                } while (ComboDone(numTimesRun, numIterations, algoNum, progNum, langNum));

                numTimesRun[algoNum, progNum, langNum]++;

                sortAlgos algo = (sortAlgos)algoNum;
                Programmers prog = (Programmers)progNum;
                Langs lang = (Langs)langNum;

                long result = RunSort(prog, lang, algo, inputPath);

                System.Console.Write(algo.ToString() + "\t");
                System.Console.Write(prog.ToString() + "\t");
                System.Console.Write(lang.ToString() + "\t");
                System.Console.WriteLine(result);
            }

            System.Console.ReadKey();
            System.Console.ReadKey();
            System.Console.ReadKey();
        }

        private static long RunSort(Programmers programmer, Langs lang, sortAlgos algo, string pathToInput)
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            
            //String command = programPath + "\" " + NameFromAlgo(algo) + " " + pathToInput;

            Process process = new Process();
            process.StartInfo.FileName = GetProgramPath(programmer, lang);
            if (programmer == Programmers.Andrew) {
                process.StartInfo.Arguments = GetOptionalFirstArg(programmer, lang) + " " + NameFromAlgo(programmer, algo) + " " + pathToInput;
            } else
            {
                process.StartInfo.Arguments = GetOptionalFirstArg(programmer, lang) + " " + pathToInput + " " + NameFromAlgo(programmer, algo);
            }
            process.Start();

            /*ProcessStartInfo cmdsi = new ProcessStartInfo("cmd.exe");
            cmdsi.Arguments = command;
            Process cmd = Process.Start(cmdsi);*/

            process.WaitForExit();

            watch.Stop();
            long elapsedMs = watch.ElapsedMilliseconds;

            return elapsedMs;
        }

        private static string GetProgramPath(Programmers programmer, Langs lang)
        {
            switch (lang)
            {
                case Langs.Java:
                    if (programmer == Programmers.Andrew)
                    {
                        return "java";
                    }
                    else
                    {
                        return "java";
                    }
                case Langs.CSharp:
                    if (programmer == Programmers.Andrew)
                    {
                        return andrewCodePath + "CSharpSort" + "\"";
                    }
                    else
                    {
                        return codyCodePath + "SortProject" + "\"";
                    }
                case Langs.Python:
                    if (programmer == Programmers.Andrew)
                    {
                        return "python";
                    }
                    else
                    {
                        return "python";
                    }
            }

            return "";
        }

        private static string GetOptionalFirstArg(Programmers programmer, Langs lang)
        {
            switch (lang)
            {
                case Langs.Java:
                    if (programmer == Programmers.Andrew)
                    {
                        return andrewCodePath + "Main" + "\"";
                    }
                    else
                    {
                        return codyCodePath + "Program" + "\"";
                    }
                case Langs.Python:
                    if (programmer == Programmers.Andrew)
                    {
                        return andrewCodePath + "Sort.py" + "\"";
                    }
                    else
                    {
                        return codyCodePath + "sort.py" + "\"";
                    }
            }

            return "";
        }

        private static string NameFromAlgo(Programmers programmer, sortAlgos algo)
        {
            if (programmer == Programmers.Andrew)
            {
                switch (algo)
                {
                    case sortAlgos.Insertion:
                        return "insertion";
                    case sortAlgos.Bubble:
                        return "bubble";
                    case sortAlgos.Merge:
                        return "merge";
                    case sortAlgos.Selection:
                        return "selection";
                }
            }
            else
            {
                switch (algo)
                {
                    case sortAlgos.Insertion:
                        return "1";
                    case sortAlgos.Bubble:
                        return "2";
                    case sortAlgos.Merge:
                        return "4";
                    case sortAlgos.Selection:
                        return "3";
                }
            }

            return "";
        }

        private static bool Finished(int[,,] arr, int num)
        {
            bool finished = true;

            for (int i = 0; i < arr.GetLength(0); i++)
            {
                for (int j = 0; j < arr.GetLength(1); j++)
                {
                    for (int k = 0; k < arr.GetLength(2); k++)
                    {
                        if (arr[i, j, k] != num)
                        {
                            finished = false;
                        }
                    }
                }
            }

            return finished;
        }

        private static bool ComboDone(int[,,] arr, int num, int algoNum, int progNum, int langNum)
        {
            if (arr[algoNum, progNum, langNum] >= num)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
