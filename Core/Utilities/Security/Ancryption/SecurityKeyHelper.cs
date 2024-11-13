using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Security.Ancryption
{
    //SecuirtyKeyHelper:bizim elimizde bulunan string bir securityKeyleri parametre olarak geçemediğimiz için onu bir byte array haline getirmemize yarayan
    //ve onu bir simetrik bir simetrik bir anahtar haline getirmeye yarayan class  
    public class SecurityKeyHelper
    {
        //bana bir tane string securityKey ver yani startuptaki appsetingteki değeri ver. Bende sana onun SecurityKey karşılığını verim.
        //SecurityKey system.IdentityModel.Tokens kütüphanesinden gelir ve çözeriz.
        public static SecurityKey CreateSecurityKey(string securityKey)
        {
            return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));
        }
    }
}
