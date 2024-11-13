using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Caching;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Aspects.Autofac.Caching
{
    public class CacheAspect : MethodInterception
    {
        private int _duration;
        private ICacheManager _cacheManager;

        public CacheAspect(int duration = 60) //default olarak 60 belirlendi yani biz bir değer vermezsek bu veri 60 dakka boyunca cache de duracak.
        {
            _duration = duration;
            _cacheManager = ServiceTool.ServiceProvider.GetService<ICacheManager>(); 
        }

        public override void Intercept(IInvocation invocation) //MethodIntercephor da ki Intercepti invoke et.
        {
            var methodName = string.Format($"{invocation.Method.ReflectedType.FullName}.{invocation.Method.Name}"); // öncelikle methodumun ismini bul reflectedType yani namespace isminin tamamını al ve interface olanın ismini al.
                                                                                                                    // Kısacası bu kod namespace + class ismini verir. managerlar ınterface üzerinden çalıştığı için interface yani.
                                                                                                                    //.method ismini al.
            var arguments = invocation.Arguments.ToList();  //metodun parametresi varsa parametrelerini listeye çevir. 
            var key = $"{methodName}({string.Join(",", arguments.Select(x => x?.ToString() ?? "<Null>"))})";  //eğer parametre değerim varsa o parametre değerini getallun(hangisi ile çalışacaksak) içine ekliyorum.
                                                                                                              //String.join bir araya getirmek neyi bir araya getirsin, aralarına virgül koyarak parametrelerin her biri için.
            if (_cacheManager.IsAdd(key)) //Git bak bakalım bellekte böyle birşey var mı ? 
            {
                invocation.ReturnValue = _cacheManager.Get(key);  //eğer cache de varsa metodu hiç çalıştırmadan geri dön yani returnValue, peki geriye ne dönsün, buda _cacheManagerda var olan keyi yani verileri dönsün demek.
                return;
            }
            invocation.Proceed(); //eğer yoksa invocationı devam ettir.
            _cacheManager.Add(key, invocation.ReturnValue, _duration); //veri tabanından gelen değerleri daha önce eklenmemiş cache ekle.
        }
    }
}
