using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Results
{
    public interface IResult //normalde void döndüreceğimiz yerlerde IResult döndürerek yapılan işlem başarılı mı ve message ne gibi durumları kontrol altına alacağız.
    {
        bool Success { get; } //Get sadece okunabilir demek. Biz burada sadece get kullanacağız. Bunu constractırda set edeceğiz.
        string Message { get; }
    }
}
