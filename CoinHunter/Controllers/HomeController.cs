using CoinHunterMVC.Data;
using CoinHunterMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace CoinHunterMVC.Controllers
{
    public class HomeController : Controller
    {

        private readonly static ViewModel viewModel = new ViewModel();
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Method which returns a view of the home-page.
        /// </summary>
        /// <returns> Home-page view.</returns>
        public IActionResult Index()
        {
            CryptoRepository _repository = CryptoRepository.GetInstance();
            _repository.SetCurrency("gbp");
            _repository.Update();

            viewModel.CryptoList = (List<CryptoModel>)_repository.GetAllCryptoObject();
            CryptoModel.CurrencySymbol = _repository.GetCurrencySymbol();


            return View(viewModel);
        }

        /// <summary>
        /// Method which returns a view of the privacy policy.
        /// </summary>
        /// <returns>Privacy policy view.</returns>
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
