using System.Reflection.Metadata.Ecma335;

namespace Array
{
    internal class Program
    {
        // https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/arrays
        static void Main(string[] args)
        {
            // 1D Array (Mảng 1 chiều)
            // ------------------------
            // Chỉ có một chiều duy nhất (axis 0)
            // Shape: (4,)
            // [ 7, 2, 9, 10 ]

            // 2D Array (Mảng 2 chiều)
            // ------------------------
            // Có hai chiều: axis 0 (hàng), axis 1 (cột)
            // Shape: (2, 3)
            // [
            //   [ 5.2, 3.0, 4.5 ],
            //   [ 9.1, 0.1, 0.3 ]
            // ]

            // 3D Array (Mảng 3 chiều)
            // ------------------------
            // Có ba chiều: axis 0 (tầng), axis 1 (hàng), axis 2 (cột)
            // Shape: (4, 3, 2)
            // [
            //   [ [1, 2], [3, 4], [1, 4] ],
            //   [ [2, 9], [7, 7], [7, 5] ],
            //   [ [1, 3], [0, 0], [0, 2] ],
            //   [ [9, 6], [9, 9], [9, 8] ]
            // ]

            // Truy xuất phần tử:
            // 1D: array1D[index]
            // 2D: array2D[row, column]
            // 3D: array3D[layer, row, column]

            singleDimensionalArray();
            multiDemensionalArray();
            jaggedArrays();


        }
        static void singleDimensionalArray()
        {
            int[] array1 = new int[5]; //default ele value 0
            int[]array2 = [1,2,3,4];

            for (int i = 0; i < array1.Length; i++)
            {
                Console.WriteLine(array1[i]);
            }

            foreach (var item in array2)
            {
                Console.WriteLine(item);
            }
        }

        static void multiDemensionalArray()
        {

            int[,] twoDemensionalArray = new int[2, 4];
            int[,] twoDemensionalArray2 = { { 1, 2, 7}, { 3, 4, 5 } };

            Console.WriteLine($"total length: {twoDemensionalArray.Length}");
            Console.WriteLine($"rank: {twoDemensionalArray2.Rank}");

            for (int i = 0; i < twoDemensionalArray2.GetLength(0); i++)
            {
                for (global::System.Int32 j = 0; j < twoDemensionalArray2.GetLength(1); j++)
                {
                    Console.Write(twoDemensionalArray2[i, j]);
                }
                Console.WriteLine();
            }

            int[,,] threeDemensionalArray = new int[3, 3, 2];
        }

        static void jaggedArrays()
        {
            // A jagged array is an array whose elements are arrays, possibly of different sizes.
            // A jagged array is sometimes called an "array of arrays."
            int[][] jaggedArrays = new int[3][];

            jaggedArrays[0] = new int[3];
            jaggedArrays[1] = [1, 2, 3];
            jaggedArrays[2] = [8,4,5,6,7,8];

            jaggedArrays[0][0] = 6;

            for (int i = 0; i < jaggedArrays.Length; i++)
            {
                for (global::System.Int32 j = 0; j < jaggedArrays[i].Length; j++)
                {
                    Console.Write(jaggedArrays[i][j]);
                }
                Console.WriteLine();
            }

        }
    }
}
