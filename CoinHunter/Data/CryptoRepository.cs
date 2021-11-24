using CoinHunterMVC.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;


/// <summary>
/// Repository claas utilised for all fucntions relating the the cryptocurrencies.
/// </summary>
namespace CoinHunterMVC.Data
{
    public class CryptoRepository : ICryptoRepository
    {
        private static readonly CryptoRepository _Instance = new CryptoRepository();
        private static string Currency;
        private static string CurrencySymbol;
        List<CryptoModel> CryptoList;

        private CryptoRepository()
        {
            CryptoList = new List<CryptoModel>();
        }

        /// <summary>
        /// Method which retuns an instance of the class.
        /// </summary>
        /// <returns></returns>
        public static CryptoRepository GetInstance()
        {
            return _Instance;
        }
        
        /// <summary>
        /// Method which creates all the crypto objects using the CoinGecko API.
        /// </summary>
        /// <param name="currency"></param>
        /// <returns></returns>
        public async Task CreateCryptoObjectsAsync()
        {
            string url;

            switch (Currency)
            {
                case "usd":
                    url = $"https://api.coingecko.com/api/v3/coins/markets?vs_currency=usd&order=market_cap_desc&per_page=100&page=1&sparkline=false";
                    CurrencySymbol = "$";
                    break;
                case "gbp":
                    url = $"https://api.coingecko.com/api/v3/coins/markets?vs_currency=gbp&order=market_cap_desc&per_page=100&page=1&sparkline=false";
                    CurrencySymbol = "£";
                    break;
                case "eur":
                    url = $"https://api.coingecko.com/api/v3/coins/markets?vs_currency=eur&order=market_cap_desc&per_page=100&page=1&sparkline=false";
                    CurrencySymbol = "€";
                    break;
                default:
                    url = $"https://api.coingecko.com/api/v3/coins/markets?vs_currency=gbp&order=market_cap_desc&per_page=100&page=1&sparkline=false";
                    CurrencySymbol = "£";
                    break;
            }

            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    JArray cryptoArray = Newtonsoft.Json.JsonConvert.DeserializeObject<JArray>(await response.Content.ReadAsStringAsync());

                    foreach (JObject crypto in cryptoArray)
                    {
                        CryptoModel cryptocurrency = Newtonsoft.Json.JsonConvert.DeserializeObject<CryptoModel>(crypto.ToString());

                        CryptoList.Add(cryptocurrency);
                    }
                }
            }
        }

        /// <summary>
        /// method which returns a list of all 100 crypto objects sorted by market cap rank.
        /// </summary>
        /// <returns>List of crypto objects</returns>
        public ICollection<CryptoModel> GetAllCryptoObject()
        {

            CryptoList = CryptoList.OrderBy(x => x.Market_cap_rank).ToList();
            
            return CryptoList;
        }

        /// <summary>
        /// Method which returns a specific crypto object. 
        /// </summary>
        /// <param name="id">identifier for the crypto object being returned. </param>
        /// <returns>A crypto object.</returns>
        public CryptoModel GetCryptoObject(string id)
        {
            CryptoModel result = null;

            foreach (var crypto in CryptoList)
            {
                if (crypto.Id == id)
                {
                    result = crypto;
                }
            }

            return result;
        }

        /// <summary>
        /// Method which creates a new list of crypto objects.
        /// </summary>
        public void Update()
        {
            ApiHelper.InitialiseClient();
            CryptoList.Clear();
            CreateCryptoObjectsAsync().Wait();
        }

        /// <summary>
        /// Method to set the currency used to get the correct values from the API.
        /// </summary>
        /// <param name="currency">Currency to be used.</param>
        public void SetCurrency(string currency)
        {
            Currency = currency;
        }

        public string GetCurrencySymbol()
        {
            return CurrencySymbol;
        }
    }
}
