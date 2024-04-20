using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Results
{
    //DataResulta diyoruz ki sen bir Result'sın yani Resulttaki mesaj ve succesi kullanabilirsin.
    public class DataResult<T> : Result, IDataResult<T>
    {
        //Resulttan tek farkı bunda birde T türünde bir Data var 
        //base diyerek Result'a success ve message parametrelerini gönderiyoruz. burada this demye gerek yok
        public DataResult(T data,bool success,string message) : base(success,message)
        {
            Data = data;
        }
        public DataResult(T data,bool success) : base(success)
        {
            Data = data;
        }
        public T Data { get; }
    }
}
