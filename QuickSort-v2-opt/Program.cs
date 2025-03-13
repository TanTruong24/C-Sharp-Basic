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


            QuickSort(inputs);

            Console.Write($"sorted input: {string.Join(" ", inputs)}");
        }

        public static void QuickSort(List<int> inputs)
        {
            recQuickSort(inputs, 0, inputs.Count - 1);
        }

        static void recQuickSort(List<int> inputs, int firstIndex, int lastIndex)
        {
            if (firstIndex >= lastIndex) return;
            else
            {
                int pivotValue = inputs[lastIndex];

                int pos = Partition(inputs, firstIndex, lastIndex);

                recQuickSort(inputs, firstIndex, pos - 1);
                recQuickSort(inputs, firstIndex + 1, pos);
            }
        }

        static int Partition(List<int> inputs, int firstIndex, int lastIndex)
        {
            int pivotValue = inputs[lastIndex];

            int left = firstIndex + 1;
            int right = lastIndex;

            while (left <= right)
            {
                while (left < right && inputs[left] < pivotValue) left++;
                while (right >= left && inputs[right] >= pivotValue) right--;
                if (left < right)
                {
                    swap(inputs, left, right);
                }
            }
            if (right != firstIndex)
            {
                inputs[firstIndex] = inputs[right];
                inputs[right] = pivotValue;
            }

            return right;
        }

        static void swap(List<int> arr, int leftIdx, int rightIdx)
        {
            (arr[rightIdx], arr[leftIdx]) = (arr[leftIdx], arr[rightIdx]);
        }
    }
}
