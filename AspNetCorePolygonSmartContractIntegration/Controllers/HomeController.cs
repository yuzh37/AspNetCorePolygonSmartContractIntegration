using AspNetCorePolygonSmartContractIntegration.Models;
using AspNetCorePolygonSmartContractIntegration.Service;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AspNetCorePolygonSmartContractIntegration.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IPolygonNetworkService _polygonNetworkService;

        public HomeController(ILogger<HomeController> logger, IPolygonNetworkService polygonNetworkService)
        {
            _logger = logger;
            _polygonNetworkService = polygonNetworkService;
        }

        public async Task<IActionResult> Index()
        {
            var connected = _polygonNetworkService.ConnectWeb3();
            
            //await _polygonNetworkService.ConnectSmartContractUsingAlchemy();
            //await _polygonNetworkService.ConnectSmartContractUsingNethereumTestChain();
            await _polygonNetworkService.ConnectSmartContractUsingInfura();
            
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