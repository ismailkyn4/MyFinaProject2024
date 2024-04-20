using Castle.DynamicProxy;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IInterceptor = Castle.DynamicProxy.IInterceptor;

namespace Core.Utilities.Interceptors
{
    //Bu atribute metotlara ve classlara uygulanabilsin, birden fazla yerde kullanılsın. inherid edilen birde bu attribute kullanılsın
    //Niye birden fazla kullanayım ki dyebiliriz. Mesela hem veri tabanına loglama yapıyoruz hemde bir dosyaya loglama yapıyo olabiliriz. Farklı parametrelerle aynı attribute çağırabiliriz.
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public abstract class MethodInterceptionBaseAttribute : Attribute, IInterceptor 
    {
        public int Priority { get; set; } //priority öncelik demek hangi atrribute önce çalışsınız belirleriz.

        public virtual void Intercept(IInvocation invocation)
        {

        }
    }
}
