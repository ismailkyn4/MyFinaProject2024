using Castle.DynamicProxy;

namespace Core.Utilities.Interceptors
{
    public abstract class MethodInterception : MethodInterceptionBaseAttribute
    {
        //Invocation : business method
        protected virtual void OnBefore(IInvocation invocation) { }
        protected virtual void OnAfter(IInvocation invocation) { }
        protected virtual void OnException(IInvocation invocation, System.Exception e) { }
        protected virtual void OnSuccess(IInvocation invocation) { }
        public override void Intercept(IInvocation invocation)  //burdaki ınvocation aslında bizim çalıştırmak istediğimiz metot oluyor. Add gibi mesela
        {
            var isSuccess = true;
            OnBefore(invocation);  //yukarıda verdiğim çalıştırmak istediğim metodu metodun başında çalışır. Gennellikle onBefore ve OnException kullanırız.
            try
            {
                invocation.Proceed();
            }
            catch (Exception e)
            {
                isSuccess = false;
                OnException(invocation, e);   //hata aldığımızda çalışsın istersek buraya
                throw;
            }
            finally
            {
                if (isSuccess)
                {
                    OnSuccess(invocation);   //tüm hepsinin sonunda yanı başarılı olursa çalışsın istersek. Burda çalıştırırız.
                }
            }
            OnAfter(invocation); //metotdan sonra çalışsın istersek bu çalışsın.
        }
    }
}
