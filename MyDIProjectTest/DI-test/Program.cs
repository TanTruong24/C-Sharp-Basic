using Microsoft.Extensions.DependencyInjection;

/**
 * 
 * IServiceCollection: Danh sách để đăng ký các dịch vụ, dùng trong cấu hình khởi tạo app
 * 
 * IServiceProvider: Container thực sự giữ các instance đã được đăng ký
 * 
 * Lifecycle (Singleton, Scoped, Transient): xác định vòng đời của service
 * 
 *  + AddSingleton<TServiceInterface, IServiceImplementation>()
 *      -> Dịch vụ được tạo một lần duy nhất cho toàn bộ ứng dụng
 *      -> Phù hợp cho: Config, Logger, Caching, các service không thay đổi theo thời gian
 *      
 *  + AddScoped<TService, TImplementation>()
 *      -> Tạo một instance duy nhất cho mỗi HTTP request
 *      -> Cần giữ trạng thái tạm trong request hiện tại
 *      -> Phù hợp cho: Repository, UnitOfWork, xử lý logic liên quan đến request
 *  
 *  + AddTransient<TService, TImplementation>()
 *      -> Tạo một instance mới mỗi lần được inject
 *      -> Dùng cho dịch vụ nhẹ, không giữ trạng thái, không chia sẻ giữa các phần của ứng dụng
 * 
 */

namespace DI_test
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var serviceCollection = new ServiceCollection();

            /**
             * Một instance duy nhất của MySingletonService sẽ được tạo ngay lần đầu tiên nó được yêu cầu từ root provider (service).
             * Tất cả các yêu cầu sau này — dù là từ root provider hay scope mới — đều nhận lại cùng một instance.
             */
            serviceCollection.AddSingleton<IMySingletonService>(services => new MySingletonService());
            serviceCollection.AddScoped<IMyScopedService, MyScopedService>();
            serviceCollection.AddTransient<IMyTransientService, MyTransientService>();

            var service = serviceCollection.BuildServiceProvider();

            object? obj;

            // Được gọi 1 lần
            // → Console.WriteLine("Singleton!: 1") sẽ được in chỉ một lần khi constructor được gọi lần đầu tiên.
            Console.WriteLine("Get singleton service");
            obj = service.GetService<IMySingletonService>();
            obj = service.GetService<IMySingletonService>();
            obj = service.GetService<IMySingletonService>();

            // Được gọi 1 lần
            Console.WriteLine("Get scoped service");
            obj = service.GetService<IMyScopedService>();
            obj = service.GetService<IMyScopedService>();
            obj = service.GetService<IMyScopedService>();

            // Được gọi 3 lần
            Console.WriteLine("Get transient service");
            obj = service.GetService<IMyTransientService>();
            obj = service.GetService<IMyTransientService>();
            obj = service.GetService<IMyTransientService>();

            Console.WriteLine();
            Console.WriteLine("--- Create new scope ---");
            Console.WriteLine();

            var scope = service.CreateScope();

            // singleton là 1 instance trong toàn bộ vòng đời
            // → Vì IMySingletonService là singleton, scope.ServiceProvider không tạo lại instance mới mà dùng lại instance đã được tạo từ service,
            // nên constructor không được gọi lại, và do đó không có log thêm được in ra.
            Console.WriteLine("Get singleton service");
            obj = scope.ServiceProvider.GetService<IMySingletonService>();
            obj = scope.ServiceProvider.GetService<IMySingletonService>();
            obj = scope.ServiceProvider.GetService<IMySingletonService>();

            // scope có 1 instance trong 1 scope
            Console.WriteLine("Get scoped service");
            obj = scope.ServiceProvider.GetService<IMyScopedService>();
            obj = scope.ServiceProvider.GetService<IMyScopedService>();
            obj = scope.ServiceProvider.GetService<IMyScopedService>();

            Console.WriteLine("Get transient service");
            obj = scope.ServiceProvider.GetService<IMyTransientService>();
            obj = scope.ServiceProvider.GetService<IMyTransientService>();
            obj = scope.ServiceProvider.GetService<IMyTransientService>();

            /**
            Get singleton service
            Singleton!: 1
            Get scoped service
            Scope!: 1
            Get transient service
            Transient!: 1
            Transient!: 2
            Transient!: 3

            -- - Create new scope -- -

            Get singleton service
            Get scoped service
            Scope!: 2
            Get transient service
            Transient!: 4
            Transient!: 5
            Transient!: 6
            */
        }
    }
}
