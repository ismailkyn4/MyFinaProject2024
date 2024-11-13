using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.IoC
{
    public static class ServiceTool
    {
        //Bu kod bizim web apide veya autofacte injectionları oluşturabilmemize yarıyor.
        public static IServiceProvider ServiceProvider { get; private set; }

        //.netin servislerini kullanarak Servislerini(IServiceCollection services) al.ve
        public static IServiceCollection Create(IServiceCollection services)
        {
            ServiceProvider = services.BuildServiceProvider(); //ve onları kendin build et.
            return services;
        }
    }
}
