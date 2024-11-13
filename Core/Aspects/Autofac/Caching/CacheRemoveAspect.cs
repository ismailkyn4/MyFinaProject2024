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
    public class CacheRemoveAspect : MethodInterception
    {
        //Veriyi manipüle eden metotlara cacheRemoveAspect uygularız. Cache remove aspect datamız bozulduğu zaman çalışır. Datamız yeni data eklendiğinde güncellendiğinde data silinirse 
        private string _pattern;
        private ICacheManager _cacheManager;

        public CacheRemoveAspect(string pattern)
        {
            _pattern = pattern;
            _cacheManager = ServiceTool.ServiceProvider.GetService<ICacheManager>();
        }

        protected override void OnSuccess(IInvocation invocation)  //niye onsucceste uyguluyoruz. Çünkü metot başarılı olduğunda eklemesi gerek. nedeni ise belki add işlemi hata vericek veya güncelleme başarısız olacak. Bu işlem tamamlandığında çalışsın diye.
        {
            _cacheManager.RemoveByPattern(_pattern);
        }
    }
}
