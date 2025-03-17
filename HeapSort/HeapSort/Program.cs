﻿namespace HeapSort
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            Console.WriteLine("Input: ");
            List<int> numbers = Console.ReadLine()
                           .Split(' ')
                           .Select(int.Parse)
                           .ToList();

            Heapify(numbers);

            Console.WriteLine("Output: " + string.Join(", ", numbers));
        }

        static void Heapify (List<int> nums)
        {
            int lenghtIndexNums = nums.Count - 1;
            int i = lenghtIndexNums;
            while (i >= 0)
            {
                int parentIndex = (i - 1) / 2;
                if (parentIndex >= 0 && nums[parentIndex] < nums[i])
                {
                    swap(nums, i, parentIndex);
                }
                i--;
            }
            i = 0;
            while (i <= lenghtIndexNums)
            {
                int childLeftIndex = 2 * i + 1;
                int childRightIndex = 2 * i + 2;
                if (childLeftIndex <= lenghtIndexNums && nums[childLeftIndex] > nums[i])
                {
                    swap(nums, i, childLeftIndex);
                }
                if (childRightIndex <= lenghtIndexNums && nums[childRightIndex] > nums[i])
                {
                    swap(nums, i, childRightIndex);
                }
                i++;
            }
        }

        static void swap(List<int> nums, int i, int j) 
        {
            (nums[i], nums[j]) = (nums[j], nums[i]);
        }
    }
}
