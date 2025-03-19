using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Numerics;
using System.Security.Cryptography;
using System.Xml.Linq;
using System;

namespace HeapSortOptimal
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

            HeapSort(numbers);

            Console.WriteLine("Output: " + string.Join(", ", numbers));
        }
        /*
         * Thuật toán Heap Sort
         * 
         * Đầu tiên, chuyển mảng thành một max-heap bằng cách sử dụng Heapify.
         * Lưu ý rằng quá trình này thực hiện tại chỗ (in-place),
         * nghĩa là các phần tử trong mảng sẽ được sắp xếp lại để tuân theo tính chất của heap.
         * 
         * Sau đó, lần lượt xóa phần tử gốc (root) của max-heap, thay thế nó bằng phần tử cuối cùng trong heap,
         * và tiếp tục Heapify để duy trì tính chất max-heap.
         * Quá trình này được lặp lại cho đến khi kích thước của heap chỉ còn một phần tử.
         * 
         * Các bước thực hiện:
         * 1. Sắp xếp lại các phần tử của mảng để tạo thành một Max Heap.
         * 2. Lặp lại các bước sau cho đến khi heap chỉ còn một phần tử:
         *    - Hoán đổi phần tử gốc của heap (là phần tử lớn nhất) với phần tử cuối cùng của heap.
         *    - Loại bỏ phần tử cuối cùng khỏi heap (vị trí của nó đã được sắp xếp đúng). 
         *      Thực chất chỉ là giảm kích thước heap chứ không xóa phần tử khỏi mảng.
         *    - Gọi Heapify để duy trì tính chất max-heap với phần còn lại của heap.
         * 3. Cuối cùng, ta thu được mảng đã sắp xếp theo thứ tự tăng dần.
         */
        static void swap(List<int> nums, int i, int j)
        {
            (nums[i], nums[j]) = (nums[j], nums[i]);
        }

        static void Heapify(List<int> nums, int length, int rootIndex)
        {
            int largestIdx = rootIndex;
            int leftIdx = 2 * rootIndex + 1;
            int rightIdx = 2 * rootIndex + 2;

            if (leftIdx < length && nums[leftIdx] > nums[largestIdx])
            {
                largestIdx = leftIdx;
            }
            if (rightIdx < length && nums[rightIdx] > nums[largestIdx])
            {
                largestIdx = rightIdx;
            }

            if (largestIdx != rootIndex)
            {
                swap(nums, rootIndex, largestIdx);
                Heapify(nums, length, largestIdx); // Đệ quy xuống nhánh mới bị thay đổi, Chỉ đi sâu xuống một nhánh duy nhất
            }
        }

        static void HeapSort(List<int> nums)
        {
            int lenght = nums.Count;

            // Các node lá (leaf nodes) không cần Heapify
            // Trong cây nhị phân dạng mảng, các node lá nằm ở nửa cuối của mảng. Những node này không có con, vì vậy chúng đã là một heap hợp lệ.
            // Vị trí của các node lá: Chỉ mục từ n/2 đến n-1 trong mảng.
            for (int i = lenght / 2 - 1; i >= 0; i--)
            {
                // Node đầu tiên cần Heapify là n/2 - 1. Lý do: Các node từ n/2 trở đi là lá, nên node cha cuối cùng là n/2 - 1.
                // Các node từ n/2 - 1 về 0 có con, nên cần kiểm tra và sửa đổi để đảm bảo max-heap.
                Heapify(nums, lenght, i);
            }

            for (int i = lenght-1; i > 0; i--)
            {
                swap(nums, i, 0);
                Heapify(nums, i, 0);
            }
        }
    }
}
