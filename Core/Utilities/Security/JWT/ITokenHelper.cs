using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Security.JWT
{
    public interface ITokenHelper
    {
        //Bu sistem; kullanıcı postmanden kullanıcı adı şifre girecek web apiye gönderecek. Eğer doğruysa burada bizim alttaki(CreateToken) operasyonumuz çalışacak.
        //İlgili User için veri tabanına gidicek veri tabanında bu kullanıcının Claimlerini bulucak orada bir tane json web token üretecek ve bunları postmane geri göndrecek.
        AccessToken CreateToken(User user,List<OperationClaim> operationClaims);
    }
}
