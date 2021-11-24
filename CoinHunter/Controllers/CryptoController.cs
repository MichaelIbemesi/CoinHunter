using CoinHunterMVC.Data;
using CoinHunterMVC.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoinHunterMVC.Controllers
{
    public class CryptoController : Controller
    {
       private readonly static ViewModel viewModel = new ViewModel();

        /// <summary>
        /// Method which provides a list of the top 100 cryptocurrencies.
        /// </summary>
        /// <returns>Returns view containg all 100 cryptocurrencies.</returns>
        public IActionResult GetAllCryptos()
        {
            ICryptoRepository _repository = CryptoRepository.GetInstance();
            _repository.SetCurrency("gbp");
            _repository.Update();

            viewModel.CryptoList = (List<CryptoModel>)_repository.GetAllCryptoObject();
            CryptoModel.CurrencySymbol = _repository.GetCurrencySymbol();

            return View(viewModel);
        }

        /// <summary>
        /// Method which provides information on a specific cryptocurrency.
        /// </summary>
        /// <param name="selectedCrypto"> The specific crypto object to be displayed.</param>
        /// <returns>Returns view containg specific cryptocurrency.</returns>
        public IActionResult GetCrypto(CryptoModel selectedCrypto)
        {
            viewModel.Crypto = selectedCrypto;
            return View(viewModel);
        }
    }
}
