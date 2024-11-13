using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.CrossCuttingConcerns.Caching
{
    public interface ICacheManager
    {
        T Get<T>(string key);
        object Get(string key);
        void Add(string key, object value, int duration);
        bool IsAdd(string key); //cache de var mı diye kontrol yapacağımız metot.
        void Remove(string key);
        void RemoveByPattern(string pattern); //mesela isminde category olanları uçur diye bir pattern vericez, bunları uçur
    }
}
