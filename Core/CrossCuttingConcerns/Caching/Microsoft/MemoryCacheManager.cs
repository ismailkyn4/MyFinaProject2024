using Core.Utilities.IoC;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Core.CrossCuttingConcerns.Caching.Microsoft
{
    //bunun dependency ayarını core içindeki dependencyResolverstaki CoreModule da yaparız.
    public class MemoryCacheManager : ICacheManager
    {
        //görüldüğü üzere biz normalde microsoftun daha önceden yazmış olduğu(_memoryCache altyapısı) şeyleri kullanacağımız yerlerde çağırmak yerine aynı şekilde bir MemoryCache oluşturup bunun üzerinden çağırıyoruz.
        //Bunun sebebi biz bu kodları hardcore şekilde yazarsak yarın öbür gün farklı bir cache sistemi ile çalışırsam patlarım. Ben bunun yerine .net coredan gelen kodu kendime uyarlıyorum.
        //Biz kullanılan patternlardan AdapterPattern yani var olan birşeyi kendi sistemimize uyarlıyoruz. 
        IMemoryCache _memoryCache;
        public MemoryCacheManager()
        {
            _memoryCache = ServiceTool.ServiceProvider.GetService<IMemoryCache>(); //bunu yaptığımız zaman IOC de gidiyo IMemoryCache karlışığı var mı diye bakıyor. (CoreModule da ekledik AddMemoryCache oalrak)
        }
        public void Add(string key, object value, int duration)
        {
            _memoryCache.Set(key, value, TimeSpan.FromMinutes(10)); //set edebilmek için istediği şeylerden birisi de TimeSpan cinsinden zaman. bizde 10 dakka olarak beliredik.
        }

        public T Get<T>(string key)
        {
            return _memoryCache.Get<T>(key);
        }

        public object Get(string key)
        {
            return _memoryCache.Get(key);
        }

        public bool IsAdd(string key)
        {
            return _memoryCache.TryGetValue(key, out _); //TryGetValue hem bizden bir key hemde out olarak bir değer döndürmemizi bekliyor.
                                                         //Biz eğer birşey döndürmek istemiyorsak c# da kullanılan teknik olan out _ kullanıyoruz.
        }

        public void Remove(string key)
        {
            _memoryCache.Remove(key);
        }

        //removeByPattern çalışma anında bellekten silmeye yarıyor.
        public void RemoveByPattern(string pattern)
        {
            //Reflection ile biz kodu çalışma anında oluşturma, çalışma anında müdahele etme gibi şeyleri yaparız.
            var cacheEntriesCollectionDefinition = typeof(MemoryCache).GetProperty("EntriesCollection", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            //Yukarıdaki kodda öncelikle bellekte cache ile ilgili olan yapıyı çekiyoruz. Microsoft diyo ki ben bunu cachelediğimde cache datalarını EntriesCollection diye birşeyin içine atarım. Bu memoryCahce in .net dökümantasyonunda yazar.

            var cacheEntriesCollection = cacheEntriesCollectionDefinition.GetValue(_memoryCache) as dynamic; //definitionı memoryCache olanları bul 
            List<ICacheEntry> cacheCollectionValues = new List<ICacheEntry>();

            //sonra her bir cache elemanını gez 
            foreach (var cacheItem in cacheEntriesCollection)
            {
                ICacheEntry cacheItemValue = cacheItem.GetType().GetProperty("Value").GetValue(cacheItem, null);
                cacheCollectionValues.Add(cacheItemValue); // 
            }


            //bu noktada o cache datası içerisinde anahtarlardan benim gönderdiğim değere uygun olanlar varsa içerisinde onlar geçiyorsa onları keysToRemove içersine atacak 
            var regex = new Regex(pattern, RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.IgnoreCase);
            var keysToRemove = cacheCollectionValues.Where(d => regex.IsMatch(d.Key.ToString())).Select(d => d.Key).ToList();

            foreach (var key in keysToRemove)
            {
                _memoryCache.Remove(key); //ve o gezilen key değerlerini bul ve onları bellekten sil.
            }
        }
    }
}
