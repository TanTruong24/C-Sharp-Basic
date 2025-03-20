namespace MergeSortOptimal
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            Console.WriteLine("Input:");

            List<int> nums = Console.ReadLine().Split(' ').Select(int.Parse).ToList();

            int[] temp = new int[nums.Count];
            MergeSort(nums, temp, 0, nums.Count-1);

            Console.WriteLine($"Sorted output: {string.Join(" ", nums)}");
        }

        static void MergeSort(List<int> nums, int[] temp, int first, int last)
        {
            if (first >= last) return;

            int mid = (first + last) / 2;

            MergeSort(nums, temp, first, mid);
            MergeSort(nums, temp, mid + 1, last);
            Merge(nums, temp, first, mid, last);
        }

        static void Merge(List<int> nums, int[] temp, int first, int mid, int last)
        {
            int leftIdx = first;
            int rightIdx = mid + 1;
            int tempIdx = first;

            while (leftIdx <= mid && rightIdx <= last)
            {
                if (nums[leftIdx] <= nums[rightIdx])
                {
                    temp[tempIdx] = nums[leftIdx];
                    leftIdx++;
                }
                else
                {
                    temp[tempIdx] = nums[rightIdx];
                    rightIdx++;
                }
                tempIdx++;
            }

            while (leftIdx <= mid)
            {
                temp[tempIdx] = nums[leftIdx];
                leftIdx++;
                tempIdx++;
            }

            while (rightIdx <= last)
            {
                temp[tempIdx] = nums[rightIdx];
                rightIdx++;
                tempIdx++;
            }

            for (int i = first; i <= last; i++)
                nums[i] = temp[i];

        }
    }
}
