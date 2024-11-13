using Castle.DynamicProxy;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Aspects.Autofac.Performance
{
    public class PerformanceAspect : MethodInterception
    {
        //bu perfomanceAspecti metotların üstüne koyarsak sadece koyduğumuz metotlarda çalışır. Ama eğer Core da ki interceptorlarımıza koyarsak sistemdeki herşeyi takip eder.
        private int _interval;
        private Stopwatch _stopwatch; //bu metot nekadar sürecek diye bir timer koyuyorum.

        public PerformanceAspect(int interval)  //burda bir interval değeri oluşturuyoruz. Bunun sebebi ise bunu kullanacağımız yerde ki metotun çalışması örneğin 5 sn geçerse beni uyar. Bu aspecti kullanırken parantez içine yazıyoruz süreyi.
        {
            _interval = interval;
            _stopwatch = ServiceTool.ServiceProvider.GetService<Stopwatch>(); //Stopwatch bir kronometre
        }


        protected override void OnBefore(IInvocation invocation)
        {
            _stopwatch.Start(); //metotun önünde kronometreyi başlatıyorum.
        }

        protected override void OnAfter(IInvocation invocation) //metot bittiğinde ise 
        {
            if (_stopwatch.Elapsed.TotalSeconds > _interval) //o ana kadar geçen süreyi hesaplıyorum. 
            {
                Debug.WriteLine($"Performance : {invocation.Method.DeclaringType.FullName}.{invocation.Method.Name}-->{_stopwatch.Elapsed.TotalSeconds}"); //burda console log olarak yazmışız. Bunun yerine mail gönderilebilir veya log alınabilir.
            }
            _stopwatch.Reset(); 
        }
    }
}
