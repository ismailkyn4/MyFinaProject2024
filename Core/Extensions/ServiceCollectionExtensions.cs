using Core.Utilities.IoC;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Extensions
{
    public static class ServiceCollectionExtensions
    {
        //IServiceCollection bizim aspnet uygulamamızın yani kısacası apimizin servis bağımlıklarını eklediğimiz
        //yada araya girmesini istediğim servisleri eklediğimiz collecktionun ta kendisidir.
        //biz burda this ile extension oluşturarak genişletiyoruz.
        //Amacı ise CoreModule gibi bana başka modullar verebilsin diye array haline getirdik.
        public static IServiceCollection AddDependencyResolvers(this IServiceCollection serviceCollection, ICoreModule[] modules)
        {
            foreach (var module in modules)
            {
                module.Load(serviceCollection); //her bir modulü ekleyebileceğiz.
            }
            return ServiceTool.Create(serviceCollection); 
            //Bu yaptığımız hareket kısacası core katmanı da dahil olmak üzere ekleyeceğimiz bütün injectionları bir arada toplayabileceğimiz bir yapı dönüştü.

        }
    }
}
