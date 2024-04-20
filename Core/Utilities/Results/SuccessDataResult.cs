using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Results
{
    public class SuccessDataResult<T> : DataResult<T>
    {
        //Base bu işlemin sonucu data'dır işlem sonucu true'dır ve mesaj şuduru ver.
        public SuccessDataResult(T data, string message): base(data,true,message)
        {
            
        }
        //data ve mesajı döndürmek içn bunu kullandık.
        public SuccessDataResult(T data): base(data,true)
        {
            
        }
        //Data'yı default haliyle kullanmak isteyebilir. Yani çalıştığı T'nin default'ı döner. Yani dataya denk geliyor.
        public SuccessDataResult(string message) : base(default,true,message)
        {
            
        }
        public SuccessDataResult() : base(default,true)
        {
            
        }
    }
}
