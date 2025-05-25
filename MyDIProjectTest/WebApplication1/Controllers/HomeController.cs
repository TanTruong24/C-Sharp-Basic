using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly IServiceProvider _serviceProvider;

        public HomeController(ILogger<HomeController> logger, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
        }

        public IActionResult Index()
        {
            object? obj;

            Console.WriteLine("Get singleton service");
            obj = _serviceProvider.GetService<IMySingletonService>();
            obj = _serviceProvider.GetService<IMySingletonService>();
            obj = _serviceProvider.GetService<IMySingletonService>();

            Console.WriteLine("Get scoped service");
            obj = _serviceProvider .GetService<IMyScopedService>();
            obj = _serviceProvider .GetService<IMyScopedService>();
            obj = _serviceProvider .GetService<IMyScopedService>();

            Console.WriteLine("Get transient service");
            obj = _serviceProvider.GetService<IMyTransientService>();
            obj = _serviceProvider.GetService<IMyTransientService>();
            obj = _serviceProvider.GetService<IMyTransientService>();

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
