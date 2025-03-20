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

        /*
         Using the Divide and Conquer technique, we divide a problem into subproblems. 
        When the solution to each subproblem is ready, we 'combine' the results from the subproblems to solve the main problem.
        Suppose we had to sort an array A. A subproblem would be to sort a sub-section of this array starting at index p and ending at index r, denoted as A[p..r].

        Divide
            If q is the half-way point between p and r, then we can split the subarray A[p..r] into two arrays A[p..q] and A[q+1, r].
        Conquer
            In the conquer step, we try to sort both the subarrays A[p..q] and A[q+1, r]. 
            If we haven't yet reached the base case, we again divide both these subarrays and try to sort them.
        Combine
            When the conquer step reaches the base step and we get two sorted subarrays A[p..q] and A[q+1, r] for array A[p..r], 
            we combine the results by creating a sorted array A[p..r] from two sorted subarrays A[p..q] and A[q+1, r].
         */

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
