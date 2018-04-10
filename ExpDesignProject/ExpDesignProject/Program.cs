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
        enum Programmers { Andrew, Cody };
        enum Langs { Java, CSharp, Python };
        enum sortAlgos { NotYetSet, Insertion, Bubble, Merge, Selection };

        const string inputPath = "\"C:\\Users\\andre\\classes\\IE 563 Experimental Design\\Project\\Source\\numbers10k.txt\"";
        const string andrewCodePath = "\"C:\\Users\\andre\\classes\\IE 563 Experimental Design\\Project\\Source\\Andrew's Code\\";
        const string codyCodePath = "\"C:\\Users\\andre\\classes\\IE 563 Experimental Design\\Project\\Source\\Cody's Code\\";

        static void Main(string[] args)
        {
            for (int i = 0; i < 5; i++)
            {
                System.Console.WriteLine("##### Iteration " + i);
                System.Console.WriteLine("## Insertion");

                System.Console.WriteLine("- Andrew");
                System.Console.Write("Java  \t");
                System.Console.WriteLine(RunSort(Programmers.Andrew, Langs.Java, sortAlgos.Insertion, inputPath));
                System.Console.Write("CSharp\t");
                System.Console.WriteLine(RunSort(Programmers.Andrew, Langs.CSharp, sortAlgos.Insertion, inputPath));
                System.Console.Write("Python\t");
                System.Console.WriteLine(RunSort(Programmers.Andrew, Langs.Python, sortAlgos.Insertion, inputPath));

                System.Console.WriteLine("- Cody");
                System.Console.Write("Java  \t");
                System.Console.WriteLine(RunSort(Programmers.Cody, Langs.Java, sortAlgos.Insertion, inputPath));
                System.Console.Write("CSharp\t");
                System.Console.WriteLine(RunSort(Programmers.Cody, Langs.CSharp, sortAlgos.Insertion, inputPath));
                System.Console.Write("Python\t");
                System.Console.WriteLine(RunSort(Programmers.Cody, Langs.Python, sortAlgos.Insertion, inputPath));

                System.Console.WriteLine("## Bubble");

                System.Console.WriteLine("- Andrew");
                System.Console.Write("Java  \t");
                System.Console.WriteLine(RunSort(Programmers.Andrew, Langs.Java, sortAlgos.Bubble, inputPath));
                System.Console.Write("CSharp\t");
                System.Console.WriteLine(RunSort(Programmers.Andrew, Langs.CSharp, sortAlgos.Bubble, inputPath));
                System.Console.Write("Python\t");
                System.Console.WriteLine(RunSort(Programmers.Andrew, Langs.Python, sortAlgos.Bubble, inputPath));

                System.Console.WriteLine("- Cody");
                System.Console.Write("Java  \t");
                System.Console.WriteLine(RunSort(Programmers.Cody, Langs.Java, sortAlgos.Bubble, inputPath));
                System.Console.Write("CSharp\t");
                System.Console.WriteLine(RunSort(Programmers.Cody, Langs.CSharp, sortAlgos.Bubble, inputPath));
                System.Console.Write("Python\t");
                System.Console.WriteLine(RunSort(Programmers.Cody, Langs.Python, sortAlgos.Bubble, inputPath));

                System.Console.WriteLine("## Selection");

                System.Console.WriteLine("- Andrew");
                System.Console.Write("Java  \t");
                System.Console.WriteLine(RunSort(Programmers.Andrew, Langs.Java, sortAlgos.Selection, inputPath));
                System.Console.Write("CSharp\t");
                System.Console.WriteLine(RunSort(Programmers.Andrew, Langs.CSharp, sortAlgos.Selection, inputPath));
                System.Console.Write("Python\t");
                System.Console.WriteLine(RunSort(Programmers.Andrew, Langs.Python, sortAlgos.Selection, inputPath));

                System.Console.WriteLine("- Cody");
                System.Console.Write("Java  \t");
                System.Console.WriteLine(RunSort(Programmers.Cody, Langs.Java, sortAlgos.Selection, inputPath));
                System.Console.Write("CSharp\t");
                System.Console.WriteLine(RunSort(Programmers.Cody, Langs.CSharp, sortAlgos.Selection, inputPath));
                System.Console.Write("Python\t");
                System.Console.WriteLine(RunSort(Programmers.Cody, Langs.Python, sortAlgos.Selection, inputPath));
            }

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
    }
}
