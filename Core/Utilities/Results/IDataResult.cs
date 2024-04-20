using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Results
{
    //IDataResult hem işlem sonucunu hemde mesajı iletsin istiyoruz. İşlem sonucunu hangi türde döndüreceğini çalıştırdığımız yerde belirleyeceğiz
    //Bunun için tekrardan mesaj komutlarını yazmıyoruz zaten bu işlemi IResult yapıyor bunu inherit ediyoruz.
    public interface IDataResult<T> : IResult
    {
        T Data { get; }
    }
}
