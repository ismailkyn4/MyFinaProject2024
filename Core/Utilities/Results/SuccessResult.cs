using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Results
{
    public class SuccessResult : Result
    {
        public SuccessResult(string message) : base(true, message) //SuccessResult parametresinin çalıştırıldığı yerde mesaj bilgisi girilirse
                                                                   //buradan mesaj bilgisi ve success true olarak
                                                                   //base yani Result'ı gönderilecek ve o constructur çalışacak.
                                                                   
        {

        }
        public SuccessResult() : base(true) //Eğer SuccessResult parametresinin çalıştığı yerde herhangi bir parametre girilmez ise doğrudan başarılı olduğu
                                            //yani işlem true dönecek mesaj dönmeyecek.
        {

        }
    }
}
