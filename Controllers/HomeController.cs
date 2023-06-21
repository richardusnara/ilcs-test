using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Threading.Tasks;

namespace ShippingCalculator.Controllers
{
    public class HomeController : Controller
    {
        private readonly HttpClient _httpClient;

        public HomeController()
        {
            _httpClient = new HttpClient();
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<JsonResult> GetCountries(string term)
        {
            var url = $"https://insw-dev.ilcs.co.id/n/negara?ur_negara={term}";
            var response = await _httpClient.GetAsync(url);
            var countries = await response.Content.ReadAsStringAsync();
            return Json(countries);
        }

        [HttpGet]
        public async Task<JsonResult> GetPorts(string country, string term)
        {
            var url = $"https://insw-dev.ilcs.co.id/n/pelabuhan?kd_negara={country}&ur_pelabuhan={term}";
            var response = await _httpClient.GetAsync(url);
            var ports = await response.Content.ReadAsStringAsync();
            return Json(ports);
        }

        [HttpGet]
        public async Task<JsonResult> GetProductDetails(string hsCode)
        {
            var url = $"https://insw-dev.ilcs.co.id/n/barang?hs_code={hsCode}";
            var response = await _httpClient.GetAsync(url);
            var productDetails = await response.Content.ReadAsStringAsync();
            return Json(productDetails);
        }

        [HttpGet]
        public async Task<JsonResult> GetCustomsTariff(string hsCode)
        {
            var url = $"https://insw-dev.ilcs.co.id/n/tarif?hs_code={hsCode}";
            var response = await _httpClient.GetAsync(url);
            var customsTariff = await response.Content.ReadAsStringAsync();
            return Json(customsTariff);
        }

        [HttpPost]
        public IActionResult CalculateShipping(decimal price, decimal tariffPercentage)
        {
            var tariffAmount = price * (tariffPercentage / 100);
            var totalPrice = price + tariffAmount;

            return Json(new { tariffAmount, totalPrice });
        }
    }
}
