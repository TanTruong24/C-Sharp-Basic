using System.Reflection.Metadata.Ecma335;

namespace QuickSort
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("input: ");

            string inputString = Console.ReadLine();

            List<int> inputs = inputString
                .Trim()
                .Split(" ")
                .Select(int.Parse)
                .ToList();

            QuickSort(inputs);

            Console.Write($"sorted input: {string.Join(" ", inputs)}");
        }

        public static void QuickSort(List<int> inputs)
        {
            Partition(inputs, 0, inputs.Count - 1);
        }

        static void Partition(List<int> inputs, int leftMostIndex, int rightMostIndex)
        {
            if (leftMostIndex >= rightMostIndex) return;

            int pivotValue = inputs[rightMostIndex];

            int left = leftMostIndex;
            int right = rightMostIndex;

            while (left <= right)
            {
                while (inputs[left] < pivotValue) left++;
                while (inputs[right] > pivotValue) right--;
                if (left <= right)
                {
                    swap(inputs, left, right);
                    left++;
                    right--;
                }
            }
            Partition(inputs, 0, right);
            Partition(inputs, left, rightMostIndex);
        }
        

        static void swap(List<int> arr, int leftIdx, int rightIdx)
        {
            (arr[rightIdx], arr[leftIdx]) = (arr[leftIdx], arr[rightIdx]);
        }

        static Action<List<int>, int, int> swapLambda = (arr, leftIdx, rightIdx) =>
        (arr[rightIdx], arr[leftIdx]) = (arr[leftIdx], arr[rightIdx]);
    }
}
