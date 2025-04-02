using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interface
{
    public interface IMatrix
    {
        // Định nghĩa indexer để truy cập phần tử trong ma trận
        int this[int row, int col] { get; set; }
    }

    public class Matrix: IMatrix
    {
        // Đặt readonly cho mảng 2D. có thể chỉ được gán giá trị một lần, thường là trong constructor hoặc khi khai báo.
        // Sau khi trường đã được gán giá trị, nó không thể thay đổi được nữa trong suốt vòng đời của đối tượng
        private readonly int[,] _matrix;

        // constructor để khởi tạo ma trận với số hàng và cột
        public Matrix(int row, int col)
        {
            _matrix = new int[row, col];  // Gán giá trị trong constructor
        }

        // triển khai indexer để truy xuất và thiết lập phần tử tại vị trí [row, col]
        public int this[int row, int col]
        {
            get => _matrix[row, col];
            set => _matrix[row, col] = value;
        }

        // phương thức để hiện thị ma trận
        public void DisplayMatrix()
        {
            int rows = _matrix.GetLength(0);
            int cols = _matrix.GetLength(1);
            
            for (int i = 0; i < cols; i++)
            {
                for (int j = 0; j < rows; j++)
                {
                    Console.Write(_matrix[i, j] + "\t");
                }
                Console.WriteLine();
            }
        }
    }
}
