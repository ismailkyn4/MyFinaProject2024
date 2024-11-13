using Castle.DynamicProxy;
using Core.Utilities.Interceptors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Core.Aspects.Autofac.Transaction
{
    public class TransactionScopeAspect : MethodInterception
    {
        public override void Intercept(IInvocation invocation)
        {
            //disposible olduğu için using içerisinde kulalnıyoruz. Bellekte yer kaplamaya devam etmemesi için kullanılır.
            using (TransactionScope transactionScope = new TransactionScope())
            {
                try
                {
                    invocation.Proceed(); // metodumu çalıştır. Yani hangi metodun üstüne yazarsak onu önce bi çalıştır.
                    transactionScope.Complete(); //işlemin başarılı bir şekilde tamamlandığını belirtmek için çağrılır.
                }
                catch (System.Exception e)
                {
                    transactionScope.Dispose();  //exception olduğunda transactionScope nokta dispose et. Bir işlem nesnesinin ömrünü sonlandırır ve kaynakları serbest bırakır.
                    throw;
                }
            }
        }
    }
}
