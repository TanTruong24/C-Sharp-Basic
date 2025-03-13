using System.Reflection.Metadata.Ecma335;

namespace QuickSort
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("input: ");

            string inputString = Console.ReadLine();

            List<int> inputs = new List<int>();

            foreach (var item in inputString.Trim(' ').Split(" "))
            {
                inputs.Add(int.Parse(item));
            }


             QuickSort(inputs, 0, inputs.Count - 1);

            Console.Write($"sorted input: {string.Join(" ", inputs)}");
        }

        public static void QuickSort(List<int> inputs, int leftMostIndex, int rightMostIndex)
        {
            Partition(inputs, leftMostIndex, rightMostIndex);
        }

        static void Partition(List<int> inputs, int leftMostIndex, int rightMostIndex)
        {
            if (leftMostIndex >= rightMostIndex) return;

            int pivotValue = inputs[rightMostIndex];

            int i = leftMostIndex;
            int j = rightMostIndex;

            while (i <= j)
            {
                while (inputs[i] < pivotValue) i++;
                while (inputs[j] > pivotValue) j--;
                if (i <= j)
                {
                    swap(ref inputs, i, j);
                    i++;
                    j--;
                }
            }
            Partition(inputs, leftMostIndex, j);
            Partition(inputs, i, rightMostIndex);
        }

        static void swap(ref List<int> arr, int leftIdx, int rightIdx)
        {
            (arr[rightIdx], arr[leftIdx]) = (arr[leftIdx], arr[rightIdx]);
        }
    }
}
