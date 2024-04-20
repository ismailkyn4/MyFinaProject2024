using Castle.DynamicProxy;
using System.Reflection;
using IInterceptor = Castle.DynamicProxy.IInterceptor;

namespace Core.Utilities.Interceptors
{
    public class AspectInterceptorSelector : IInterceptorSelector
    {
        public IInterceptor[] SelectInterceptors(Type type, MethodInfo method, IInterceptor[] interceptors)
        {
            var classAttributes = type.GetCustomAttributes<MethodInterceptionBaseAttribute>   //git ilgili class'ın attributelerını oku
                (true).ToList();
            var methodAttributes = type.GetMethod(method.Name)
                .GetCustomAttributes<MethodInterceptionBaseAttribute>(true); //git metodun attrbiutlerını oku (neler olabilir validation, log, cashe vs vs)
            classAttributes.AddRange(methodAttributes);                      //ve bunları bir listeye koy.


            //classAttributes.Add(new ExceptionLogAspect(typeof(FileLogger)));  //Otomatik olarak sistemdeki bütün metotları loga dahil et.
            //                                                                  //Bundan sonra yazılacak metotlarda da acaba programcı loglama yapmayı hatırladı mı unuttu mu düşünmemize gerek yok.

            return classAttributes.OrderBy(x => x.Priority).ToArray();
            //yalnız onlar çalışması sırasını priority'e yani öncelik değerine göre sırala diyoruz.
        }

    }
}
