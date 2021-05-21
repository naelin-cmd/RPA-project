using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPAWindowsApp.Models
{
    public class LoginModel
    {

        public string FirstName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        [JsonProperty("userId")]
        public int Id { get; set; }
        [JsonProperty("token")]
        public string Token { get; set; }

        [JsonProperty("refreshToken")]
        public string RefreshToken { get; set; }
    }
}
