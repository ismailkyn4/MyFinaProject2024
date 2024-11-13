using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.IoC
{

    //core gördüğünüz zaman bu bizim framework katmanımız.
    //Bütün projelerimizde kullanabileceğimiz hatta yarın öbür gün şirket değiştirsek orda da kullabaileceğimiz kodları içeren yapıdır.
    public interface ICoreModule 
    {
        void Load(IServiceCollection serviceCollection); //yani biz buna startuptaki ServiceCollectionı vericez yükleme işlemini burası yapıcak.
    }
}
