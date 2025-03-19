namespace MyMergeSort
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            Console.WriteLine("Input:");

            List<int> nums = Console.ReadLine().Split(' ').Select(int.Parse).ToList();

            List<int> sorted = MergeSort(nums);

            Console.WriteLine($"Sorted output: {string.Join(" ", sorted)}");
        }

        static List<int> MergeSort(List<int> nums)
        {
            if (nums.Count <= 1)
            {
                return nums;
            }

            int mid = nums.Count / 2;

            List<int> leftHalf = MergeSort(nums.GetRange(0, mid));
            List<int> rightHalf = MergeSort(nums.GetRange(mid, nums.Count-mid));

            List <int> sorted = Merge(leftHalf, rightHalf);
            return sorted;

        }

        static List<int> Merge(List<int> leftHalf, List<int> rightHalf)
        {
            List<int> sorted = new List<int>();
            int storeIdxLeft = 0;
            int storeIdxRight = 0;

            while (storeIdxLeft < leftHalf.Count && storeIdxRight < rightHalf.Count) 
            {
                if (leftHalf[storeIdxLeft] >  rightHalf[storeIdxRight])
                {
                    sorted.Add(rightHalf[storeIdxRight]);
                    storeIdxRight++;
                }
                else
                {
                    sorted.Add(leftHalf[storeIdxLeft]);
                    storeIdxLeft++;
                }
            }
            while (storeIdxLeft < leftHalf.Count)
            {
                sorted.Add(leftHalf[storeIdxLeft]);
                storeIdxLeft++;
            }
            while (storeIdxRight < rightHalf.Count)
            {
                sorted.Add(rightHalf[storeIdxRight]);
                storeIdxRight++;
            }
            return sorted;
        }
    }
}
