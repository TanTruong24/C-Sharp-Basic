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

            int pivotValue = inputs[lastIndex];

            int pos = Partition(inputs, firstIndex, lastIndex);

            recQuickSort(inputs, firstIndex, pos - 1);
            recQuickSort(inputs, pos, lastIndex);
        }

        static int Partition(List<int> inputs, int firstIndex, int lastIndex)
        {
            int pivotValue = inputs[lastIndex];

            int storeIndex = firstIndex;

            for (int i = firstIndex; i < lastIndex; i++)
            {
                if (inputs[i] <= pivotValue)
                {
                    swap(inputs, i, storeIndex);
                    storeIndex++;
                }
            }
            swap(inputs, storeIndex, lastIndex);
            return storeIndex;

        }

        static void swap(List<int> arr, int leftIdx, int rightIdx)
        {
            (arr[rightIdx], arr[leftIdx]) = (arr[leftIdx], arr[rightIdx]);
        }
    }
}
