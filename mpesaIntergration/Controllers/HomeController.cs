using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using mpesaIntergration.Models;

namespace mpesaIntergration.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHttpClientFactory _httpClientFactory;

        public HomeController(ILogger<HomeController> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
        }

        public IActionResult Index()
        {
            return View();
        }
        public async Task <string> AccessToken()
        {
            var client = _httpClientFactory.CreateClient("Mpesa");
            var authstring = "GEV8iKNEhzvspHGlveGG2b1GSQJCsknfR047AhTB9PgATKRh:0uIDzDy5kc1D4QV3lNEBlBq5TGyvvznUFn01z9AF0337pWaD2Cmg5gAAwa8lhtHD";
            var encoded=Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(authstring));
            var url = "/mpesa/qrcode/v1/generate";
            var request =new HttpRequestMessage(HttpMethod.Get, url);
            request.Headers.Add("Authorization",$"Basic {encoded}");
            var response = await client.SendAsync(request);
            var mpesaresponse=await response.Content.ReadAsStringAsync();
            return mpesaresponse;

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
