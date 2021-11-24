using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoinHunterMVC.Models
{
    public class ViewModel
    {
        public List<CryptoModel> CryptoList { get; set;}
        public CryptoModel Crypto { get; set; }
        public string Currency { get; set; }
    }
}
