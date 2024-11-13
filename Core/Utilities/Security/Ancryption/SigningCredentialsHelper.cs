using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Security.Ancryption
{
    public class SigningCredentialsHelper
    {
        //SigningCredentials json web token servislerinin örneğin web apide web apinin kullanabileceği json web tokenlarının oluşturulabilmesi için kullanılır.
        public static SigningCredentials CreateSigningCredentials(SecurityKey securityKey)
        {
            //Biz burada aspnete diyoruz ki sen hashing işlemi yaparken anahtar securityKey kullan şifreleme olarakta güvenlik algoritmalarından şunu kullan.
            return new SigningCredentials(securityKey,SecurityAlgorithms.HmacSha256Signature);
        }
    }
}
