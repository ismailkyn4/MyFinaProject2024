using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Security.JWT
{
    public class AccessToken
    {
        public string Token { get; set; } //json web token değerimiz. kullanıcı postmandan kullanıcı adı ve paralosını verecek bizde ona bir token vericez.
        public DateTime Expiration { get; set; } //tokenin bitiş tarihi 

    }
}
