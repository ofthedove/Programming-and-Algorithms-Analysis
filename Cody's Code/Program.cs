using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SortProject
{
    class Program
    {
        static List<float> readFile(string filename)
        {
            List<float> nums = new List<float>();
            try
            {
                string line;
                StreamReader file = new StreamReader(filename);
                while ((line = file.ReadLine()) != null)
                {
                    nums.Add(Convert.ToInt32(line));
                }
            }
            catch(Exception)
            {
                Console.WriteLine("Cannot open file " + filename);
            }
            return nums;
        }

        static void verify(List<float> nums)
        {
            float x = 0;
            bool first = true;
            foreach (float i in nums)
            {
                if (first)
                {
                    first = false;
                }
                else
                {
                    if (i < x)
                    {
                        Console.WriteLine("Not sorted");
                    }

                }
                Console.WriteLine(i.ToString());
                x = i;
            }
        }

        static List<float> merge(List<float> numbers, int left, int middle, int right)
        {
            int n1 = middle - left + 1;
            int n2 = right - middle;

            List<float> L = new List<float>();
            List<float> R = new List<float>();

            int i;
            int j;
            for(i = 0; i < n1; i++)
            {
                L.Add(numbers[left + i]);
            }

            for (j = 0; j < n2; j++)
            {
                R.Add(numbers[middle + 1 + j]);
            }

            i = 0;
            j = 0;
            int k = left;

            while(i < n1 && j < n2)
            {
                if(L[i] <= R[j])
                {
                    numbers[k] = L[i];
                    i++;
                }
                else
                {
                    numbers[k] = R[j];
                    j++;
                }
                k++;
            }

            while(i < n1)
            {
                numbers[k] = L[i];
                i++;
                k++;
            }

            while (j < n2)
            {
                numbers[k] = R[j];
                j++;
                k++;
            }

            return numbers;
        }

        //modified from https://www.geeksforgeeks.org/merge-sort/
        static List<float> merge_sort(List<float> numbers, int left, int right)
        {
            if(left < right)
            {
                //for some reason Visual studio treats all of these as ints
                //but it works
                int middle =(left + (right - 1)) / 2;
                merge_sort(numbers, left, middle);
                merge_sort(numbers, middle + 1, right);
                merge(numbers, left, middle, right);
            }
            return numbers;
        }

        static List<float> insertion_sort(List<float> numbers)
        {
            int i = 0;
            while (i < numbers.Count)
            {
                int j = i;
                while (j > 0 && numbers[j - 1] > numbers[j])
                {
                    float x = numbers[j - 1];
                    numbers[j - 1] = numbers[j];
                    numbers[j] = x;
                    j -= 1;
                }
                i += 1;
            }
            return numbers;
        }

        static List<float> bubble_sort(List<float> numbers)
        {
            bool swapped = true;
            while (swapped) {
                swapped = false;

                int i = 0;

                while (i < numbers.Count - 1)
                {
                    if (numbers[i] > numbers[i + 1])
                    {
                        float x = numbers[i + 1];
                        numbers[i + 1] = numbers[i];
                        numbers[i] = x;
                        swapped = true;
                    }
                    i += 1;
                }
            }
            return numbers;
        }

        static void Main(string[] args)
        {
            List<float> nums;
            if(args.Length > 0)
            {
                nums = readFile(args[0]);
            }
            else
            {
                nums = new List<float>{ 5, 8, 2, 10, 1, 13, 11 };
            }

            if(nums == null)
            {
                nums = new List<float> { 5, 8, 2, 10, 1, 13, 11 };
            }

            int algorithm = 1;
            if (args.Length > 1)
            {
                algorithm = Convert.ToInt32(args[1]);
            }
            switch (algorithm)
            {
                case 1:
                    insertion_sort(nums);
                    verify(nums);
                    break;
                case 2:
                    bubble_sort(nums);
                    verify(nums);
                    break;
                case 3:
                    merge_sort(nums, 0, nums.Count - 1);
                    verify(nums);
                    break;
                default:
                    Console.WriteLine("Invalid algorithm choice");
                    break;
            }
            /*
            insertion_sort(nums);
            verify(nums);
            List<float> nums2 = nums;
            bubble_sort(nums2);
            verify(nums2);
            List<float> nums3 = nums;
            merge_sort(nums3, 0, nums3.Count - 1);
            verify(nums3);*/

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey(true);
        }
    }
}
