using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace Collection
{
    internal class Program
    {
        // https://learn.microsoft.com/en-us/dotnet/standard/collections/
        static void Main(string[] args)
        {
            var list = new List<int>();

            list = ReadString(list);

            Console.WriteLine("Original string:");
            Print(list);

            var bubbleSort = BubbleSort(list);
            Console.WriteLine("After Bubble sort: ");
            Print(bubbleSort);

            var selectionSort = SelectionSort(list);
            Console.WriteLine("After Selection sort: ");
            Print(selectionSort);

            var mySortAlgo = BoundaryBasedInsertion(list);
            Console.WriteLine("After mySortAlgo sort: ");
            Print(mySortAlgo);

            var insertSort = InsertSort(list);
            Console.WriteLine("After Insert sort: ");
            Print(insertSort);
        }

        /**
         * O(n^2)
         */
        private static List<int> BubbleSort(List<int> list)
        {
            for (int i = 0; i < list.Count-1; i++)
            {
                for (int j = i+1; j < list.Count; j++)
                {
                    if (list[i] >= list[j])
                    {
                        var temp = list[i];
                        list[i] = list[j];
                        list[j] = temp;
                    }
                }
            }
            return list;
        }

        /**
         * O(n^2)
         */
        private static List<int> SelectionSort(List<int> nums)
        {
            for (int i = 0; i < nums.Count -1; i++)
            {
                int idxSelectValue = i;
                for (int j = i + 1; j < nums.Count; j++) 
                { 
                    if (nums[idxSelectValue] >= nums[j])
                    {
                        idxSelectValue = j;
                    }
                }
                if (idxSelectValue != i)
                {
                    int temp = nums[i];
                    nums[i] = nums[idxSelectValue];
                    nums[idxSelectValue] = temp;
                }
            }
            return nums;
        }

        private static LinkedList<int> BoundaryBasedInsertion(List<int> nums)
        {
            var sortedLinkedNums = new LinkedList<int>();

            sortedLinkedNums.AddFirst(nums[0]);

            for (int i = 1; i < nums.Count; i++)
            {
                if (nums[i] <= sortedLinkedNums.First())
                {
                    sortedLinkedNums.AddFirst(nums[i]);
                }
                else if (nums[i] >= sortedLinkedNums.Last())
                {
                    sortedLinkedNums.AddLast(nums[i]);
                }
            }
            return sortedLinkedNums;
        }

        private static List<int> InsertSort(List<int> nums)
        {
            for (int i = 1; i < nums.Count; i ++)
            {
                int pos = i;
                int value = nums[i];
                
                while (pos > 0 && nums[pos-1] >= value)
                {   
                    nums[pos] = nums[pos-1];
                    pos--;
                    nums[pos] = value;
                }
            }
            return nums;
        }

        private static List<int> MergeSort(List<int> nums)
        {
            return nums;
        }


        private static void Print(IEnumerable<int> list)
        {
            foreach (var item in list)
            {
                Console.WriteLine(item);
            }
        }

        private static List<int> ReadString(List<int> list)
        {
            Console.Write("input: ");
            string? s = Console.ReadLine();

            do
            {
                if (!string.IsNullOrEmpty(s))
                {
                    var items = s.Trim(' ').Split(' ');
                    foreach (var item in items)
                    {
                        if (int.TryParse(item, out int num))
                        {
                            list.Add(num);
                        }
                    }
                }
            }
            while (string.IsNullOrEmpty(s));

            return list;
        }
    }
}
