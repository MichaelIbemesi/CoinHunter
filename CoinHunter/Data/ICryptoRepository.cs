using CoinHunterMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoinHunterMVC.Data
{
    interface ICryptoRepository
    {

        public void Update();

        public Task CreateCryptoObjectsAsync();

        public CryptoModel GetCryptoObject(string id);

        public ICollection<CryptoModel> GetAllCryptoObject();

        public string GetCurrencySymbol();

        public void SetCurrency(string currency);
    }
}
