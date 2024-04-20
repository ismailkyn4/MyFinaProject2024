using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Results
{
    public class Result : IResult
    {
        public Result(bool success, string message) : this(success) //this yani Resultın constructırına success'si yolla 
        {
            Message = message;
            // Success = success; //overloading yaptığımız durumlarda, kendimizi tekrar etmemek için ve bazen sadece success durumnu istenip mesaj istenmediği durumlarda
            // bu blok çalışmasında diğer constructur  çalışsın yani aşağıda ki ona da buradaki successi yollarız.
            // ikiside çalışacağı durumlarda bu constructur çalışır
        }
        public Result(bool success) //Mesaj verilmesini istemediğimiz durumlarda sadece başarılı olduğu anlamı çıkacak.
        {
            Success = success;
        }
        public bool Success { get; } //Getter readonlydir readonlyler constructor bloğunda set edilebilir.

        public string Message { get; }
    }
}
