using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventWaitHandleDemo
{
    class BlockingQueue<T>
    {
        private readonly List<T> _queue = [];

        private bool b = false;

        public void EnQueueError(T item)
        {
            _queue.Add(item);
            b = true;
        }

        //public bool IsEmpty() { return _queue.Count == 0; }

        /**
         * race condition
         * Thread 1 đang chạy DeQueueError():
         *      - Nó kiểm tra while (!b) và thấy b == true, đi vào xử lý.
         *      - Nhưng chưa kịp thực hiện b = false (vì bị context switch).
         *      
         * Ngay lúc đó, Thread 2 cũng đi vào vòng lặp: Nó cũng thấy b == true, và cũng tiếp tục xử lý.
         * 
         * => Cả hai thread đều đi vào cùng lúc!
         *      - Cùng lấy phần tử đầu tiên từ _queue → lỗi logic.
         *      - Cùng RemoveAt(0) → exception hoặc mất dữ liệu.
         * 
         */
        public T DeQueueError()
        {
            while (!b) ;

            Console.WriteLine($"{Environment.CurrentManagedThreadId} - b == {b}");

            b = false;

            Console.WriteLine($"{Environment.CurrentManagedThreadId} - b == {b}");

            var item = _queue.First();

            _queue.RemoveAt(0);

            return item;
        }

        // Using EventWaitHandle.

        // true -> lỗi???
        /**
         * Khi bạn gọi WaitOne():
         *      - Nếu trạng thái đang là false → thread sẽ bị block.
         *      - Nếu trạng thái đang là true → thread được cho qua ngay lập tức, và trạng thái bị tự động reset về false (do AutoReset).
         * 
         */
        //private readonly EventWaitHandle ewh = new EventWaitHandle(true, EventResetMode.AutoReset);

        private readonly EventWaitHandle ewh = new EventWaitHandle(false, EventResetMode.AutoReset);


        public void EnQueue(T item)
        {
            _queue.Add(item);
            ewh.Set();
        }

        public T DeQueue()
        {
            ewh.WaitOne();

            /**
             * Thread-Safety Issue
             * Nguy cơ: Khi có nhiều thread gọi DeQueue() đồng thời, có thể dẫn đến:
             *      - Race condition: Cả hai thread có thể truy cập _queue.First() trước khi thread nào kịp RemoveAt(0).
             *      - InvalidOperationException: nếu _queue bị trống trong lúc thread khác gọi First() hoặc RemoveAt(0).
             *      - Out-of-range lỗi: nếu _queue.RemoveAt(0) được gọi khi danh sách đã rỗng.
             * 
             */

            Console.WriteLine($"DeQueue: {Environment.CurrentManagedThreadId}");
            var item = _queue.First();
            _queue.RemoveAt(0);

            //ewh.Reset();

            return item;
        }
    }
}
