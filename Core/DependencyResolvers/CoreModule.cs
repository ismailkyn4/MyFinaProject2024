using Autofac.Core;
using Core.CrossCuttingConcerns.Caching;
using Core.CrossCuttingConcerns.Caching.Microsoft;
using Core.Utilities.IoC;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DependencyResolvers
{
    public class CoreModule : ICoreModule
    {
        public void Load(IServiceCollection serviceCollection)
        {
            //HTTP context aslında her bir yapılan istekle ilgili oluşan context diyebilir.
            //Yani bizm clientımız bir istek yaptığımda o istediğin başlangıçtan bitişine kadar o isteğin takip edilme işini bu arkadaş yapıyor.
            //Bunu startuptan buraya çektik.
            serviceCollection.AddMemoryCache(); //Microsoftun kendi otomatik yaptığı injection bunu yazdığımız IMemoryCache karşılığı otamatik tanımlanmış oluyor.
            serviceCollection.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            serviceCollection.AddSingleton<ICacheManager, MemoryCacheManager>(); //yarın öbür gün Redis'e geçersek burayı RedisCacheManager yapıp addMemoryCache koymamıza da gerek kalmaz.
            serviceCollection.AddSingleton<Stopwatch>();


        }
    }
}
