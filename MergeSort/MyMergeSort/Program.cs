namespace MyMergeSort
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            Console.WriteLine("Input:");

            List<int> nums = Console.ReadLine().Split(' ').Select(int.Parse).ToList();

            List<int> sorted = new List<int>();

            MergeSort(sorted, nums, 0, nums.Count-1);

            Console.WriteLine($"Sorted output: {string.Join(" ", nums)}");
        }

        static void MergeSort(List<int> sorted, List<int> nums, int startIdx, int endIdx)
        {
            if (startIdx == endIdx) return;
            int pivot = (startIdx + endIdx) / 2;
            MergeSort(sorted, nums, startIdx, pivot);
            MergeSort(sorted, nums, pivot + 1, endIdx);
            Merge(sorted, nums, startIdx, pivot, endIdx);

        }

        static void Merge(List<int> sorted, List<int> nums, int startIdx, int pivot, int endIdx)
        {

            List<int> subLeft = nums.Slice(startIdx, pivot-startIdx + 1);
            List<int> subRight = nums.Slice(pivot+1, endIdx-(pivot + 1) + 1);

            int storeIdxSubLeft = 0;
            int storeIdxSubRight = 0;
            
            if (subLeft.Count == 1 && subRight.Count == 1)
            {
                if (subLeft[0] > subRight[0])
                {
                    sorted.Add(subRight[0]);
                    sorted.Add(subLeft[0]);
                    return;
                }
                sorted.Add(subLeft[0]);
                sorted.Add(subRight[0]);
                return;
            }
            while (storeIdxSubLeft < subLeft.Count || storeIdxSubRight < subRight.Count)
            {
                if (storeIdxSubLeft < subLeft.Count && storeIdxSubRight < subRight.Count)
                {
                    if (subLeft[storeIdxSubLeft] < subRight[storeIdxSubRight])
                    {
                        sorted.Add((subLeft[storeIdxSubLeft]));
                        storeIdxSubLeft++;
                    }
                    else
                    {
                        sorted.Add((subRight[storeIdxSubRight]));
                        storeIdxSubRight++;
                    }
                }
                else if (storeIdxSubLeft < subLeft.Count)
                {
                    sorted.AddRange(subLeft.Slice(storeIdxSubLeft, subLeft.Count - storeIdxSubLeft + 1));
                    return;
                }
                else
                {
                    sorted.AddRange(subRight.Slice(storeIdxSubRight, subRight.Count - storeIdxSubRight + 1));
                    return;
                }
            }

        }
    }
}
