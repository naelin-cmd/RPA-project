
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPAWindowsApp.Models
{
    public class TokenModel
    {
        
        public int id { get; set; }
        public string token { get; set; }
        public string refreshToken { get; set; }
    }
    public class Root
    {
        public bool isSuccess { get; set; }
        public string message { get; set; }
        public TokenModel data { get; set; }
    }
}

