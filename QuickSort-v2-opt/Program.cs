using System.Reflection.Metadata.Ecma335;

namespace QuickSort
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("input: ");

            List<int> inputs = Console.ReadLine()
                            .Trim()
                            .Split(' ')
                            .Select(int.Parse)
                            .ToList();


            QuickSort(inputs);

            Console.Write($"sorted input: {string.Join(" ", inputs)}");
        }

        /*
         * Thuật toán Quick Sort (Sắp xếp nhanh)
         * Quick Sort là thuật toán sắp xếp chia để trị, hoạt động bằng cách:
         * 1. Chọn một phần tử làm pivot (điểm chốt).
         * 2. Phân hoạch mảng thành hai phần:
         *    - Các phần tử nhỏ hơn hoặc bằng pivot ở bên trái.
         *    - Các phần tử lớn hơn pivot ở bên phải.
         * 3. Đệ quy sắp xếp hai phần còn lại.
         * 4. Khi không thể chia nhỏ hơn, các phần tử đã sắp xếp xong.
         * 
         * Độ phức tạp trung bình: O(n log n), trường hợp xấu nhất O(n²).
         */

        public static void QuickSort(List<int> inputs)
        {
            recQuickSort(inputs, 0, inputs.Count - 1);
        }

        static void recQuickSort(List<int> inputs, int firstIndex, int lastIndex)
        {
            // Điều kiện dừng: nếu phạm vi mảng chỉ còn 1 phần tử hoặc không còn phần tử nào
            if (firstIndex >= lastIndex) return;

            // Chọn pivot ngẫu nhiên và thực hiện phân hoạch
            int pivotValue = inputs[lastIndex];

            int pos = Partition(inputs, firstIndex, lastIndex);

            // Đệ quy sắp xếp mảng con bên trái pivot
            recQuickSort(inputs, firstIndex, pos - 1);

            // Đệ quy sắp xếp mảng con bên phải pivot
            recQuickSort(inputs, pos + 1, lastIndex);
        }

        /*
         * Hàm Partition (Phân hoạch mảng)
         * 1. Chọn pivot ngẫu nhiên để tránh trường hợp xấu nhất O(n²).
         * 2. Swap pivot vào vị trí cuối cùng để dễ thực hiện Lomuto Partitioning.
         * 3. Sắp xếp lại mảng sao cho:
         *    - Các phần tử nhỏ hơn hoặc bằng pivot nằm bên trái.
         *    - Các phần tử lớn hơn pivot nằm bên phải.
         * 4. Đưa pivot về đúng vị trí và trả về chỉ mục của nó.
         */
        static int Partition(List<int> inputs, int firstIndex, int lastIndex)
        {
            // Chọn pivot ngẫu nhiên để tránh worst-case O(n²)
            Random rand = new Random();
            int pivotIndex = rand.Next(firstIndex, lastIndex + 1);

            // Đưa pivot về cuối mảng để dễ dàng sử dụng thuật toán Lomuto Partition
            swap(inputs, pivotIndex, lastIndex);

            int pivotValue = inputs[lastIndex];

            int storeIndex = firstIndex; // Vị trí để hoán đổi các phần tử nhỏ hơn pivot

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
