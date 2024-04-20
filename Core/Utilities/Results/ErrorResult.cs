using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Results
{
    public class ErrorResult : Result
    {
        public ErrorResult(string message) : base(false, message) //ErrorResult parametresinin çalıştırıldığı yerde mesaj bilgisi girilirse
                                                                 //buradan mesaj bilgisi ve success true olarak
                                                                 //base yaniResult'ı gönderilecek ve o constructur çalışacak.

        {

        }
        public ErrorResult() : base(false) //Eğer ErrorResult parametresinin çalıştığı yerde herhangi bir parametre girilmez ise doğrudan başarısız olduğu
                                           //yani işlem false dönecek mesaj dönmeyecek.
        {

        }
    }
}
