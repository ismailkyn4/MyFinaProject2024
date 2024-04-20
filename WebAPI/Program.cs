using Autofac;
using Autofac.Extensions.DependencyInjection;
using Business.DependencyResolvers.Autofac;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseServiceProviderFactory(new AutofacServiceProviderFactory()) //kullanaca��m�z metodu belirtece�imiz yer. �ncelikle managenugetpackages k�sm�n�ndan autofac.extensions.depency�njection'� indiriyoruz. Yoksa g�rmez buray�
                .ConfigureContainer<ContainerBuilder>(builder =>
                {
                    builder.RegisterModule(new AutofacBusinessModule());        //configurasyonu yapt�ktan son kullanaca��m�z kendi olu�turdu�umuz business i�indeki Module ismini yaz�yoruz.
                                                                                //Ben ba�ka bir IOC contaigner kullanmak istedi�imde bu hareketi yap�caz.
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
