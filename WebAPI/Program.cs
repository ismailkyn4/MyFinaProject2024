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
                .UseServiceProviderFactory(new AutofacServiceProviderFactory()) //kullanacaðýmýz metodu belirteceðimiz yer. Öncelikle managenugetpackages kýsmýnýndan autofac.extensions.depencyýnjection'ý indiriyoruz. Yoksa görmez burayý
                .ConfigureContainer<ContainerBuilder>(builder =>
                {
                    builder.RegisterModule(new AutofacBusinessModule());        //configurasyonu yaptýktan son kullanacaðýmýz kendi oluþturduðumuz business içindeki Module ismini yazýyoruz.
                                                                                //Ben baþka bir IOC contaigner kullanmak istediðimde bu hareketi yapýcaz.
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
