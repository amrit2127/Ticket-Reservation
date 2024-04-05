using Microsoft.AspNetCore.Mvc;
using NationalPark_WebApp_116.Models;
using NationalPark_WebApp_116.Models.ViewModels;
using NationalPark_WebApp_116.Repository.IRepository;
using System.Diagnostics;

namespace NationalPark_WebApp_116.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ITrailRepository _trailRepository;
        private readonly INationalParkRepository _nationalParkRepository;

        public HomeController(ILogger<HomeController> logger, INationalParkRepository nationalParkRepository,ITrailRepository trailRepository)
        {
            _logger = logger;
            _nationalParkRepository = nationalParkRepository;
            _trailRepository = trailRepository;
        }

        public async Task<IActionResult> Index()
        {
            IndexVM indexVM = new IndexVM()
            {
                NationalParkList = await _nationalParkRepository.GetAllAsync(SD.NationalParkAPIPath),
                TrailList = await _trailRepository.GetAllAsync(SD.TrailAPIPath)
            };
            return View(indexVM);
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