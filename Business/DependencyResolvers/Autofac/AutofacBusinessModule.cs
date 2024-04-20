using Autofac;
using Autofac.Extras.DynamicProxy;
using Business.Abstract;
using Business.CCS;
using Business.Concrete;
using Castle.DynamicProxy;
using Core.Utilities.Interceptors;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DependencyResolvers.Autofac
{
    public class AutofacBusinessModule : Module // sen bir modülsün Autofac modülüsün diyoruz.
                                                // Startupta oluşturacağımız configurasyonları otofacte böyle kuruyoruz.
    {
        protected override void Load(ContainerBuilder builder) //Biz bunu örneğin IIS'de yayınladığımız zaman, uygulama ayağa kalktığında burası çalışacak
                                                               //Ezilebilecek metotları burda eziyoruz. override yazdığımızda ezilebilir yani değiştirilebilir metotlar karşımıza çıkar.
                                                               //Load yükleme demek olduğu için uygulama ayağa kalktığında ezmek istediğimiz metodu Load olarak belirtmiş oluyoruz.
                                                               //Bu yapılandırmayı yaptıktan sonra API'mizin program.cs'ına gelip bunu kullancağımız belirtmemiz lazım.
        {
            //Burası startupta services.singleton'a karşılık geliyor.
            //Kısaca birisi senden IProductService isterse ProductManager'ı register et, PM örneği ver.
            builder.RegisterType<ProductManager>().As<IProductService>().SingleInstance();//singleInstance tek bir instance oluşturuyor ve herkese o instance veriyor. projemizde PM ve IPS Datanın kendisini tutmadığını biliyoruz.
            builder.RegisterType<EfProductDal>().As<IProductDal>().SingleInstance();
            builder.RegisterType<CategoryManager>().As<ICategoryService>().SingleInstance();
            builder.RegisterType<EfCategoryDal>().As<ICategoryDal>().SingleInstance();


            var assembly = System.Reflection.Assembly.GetExecutingAssembly();
            builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces()
                .EnableInterfaceInterceptors(new ProxyGenerationOptions()
                {
                    Selector = new AspectInterceptorSelector()  //yukarıda yazdığımız tüm sınıflarımızın git bi aspecti var mı bak(köşeli parantez ile yazdığımız). Var ise önce bunu çalıştır. 
                }).SingleInstance();
        }
    }
}
