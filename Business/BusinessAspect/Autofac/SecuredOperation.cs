using Business.Constants;
using Castle.DynamicProxy;
using Core.Extensions;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Business.BusinessAspect.Autofac
{
    //JWT için
    public class SecuredOperation : MethodInterception
    {
        private string[] _roles;
        private IHttpContextAccessor _httpContextAccessor; 
        //Biz http isteği attığımızda o anda binlerce istek olabilir. Her bir istek için bir httpcontext oluşur.
        public SecuredOperation(string roles)
        {
            //Manager classında kullanacağımız atributleri virgül ile ayırırız bunun için uyanıklık yapıp roles.
            //split diyorum. Split bir metni senin belirttiğin karakterlere göre ayırıp arraye atıyor.
            _roles = roles.Split(','); 
            _httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();
            //ServiceTool bizim injection altyapımız okuyan bir araç olacak.
            //Kendimiz autofacle oluşturduğumuz servis mimarimize ulaş ve injection yap

        }

        protected override void OnBefore(IInvocation invocation) //hangi metotun üstüne yazarsak önce burası çalışır.
        {
            var roleClaims = _httpContextAccessor.HttpContext.User.ClaimRoles(); //o anki kullanıcının claim rollerini bul 
            foreach (var role in _roles) //bu kullanıcının rollerini gez 
            {
                if (roleClaims.Contains(role)) //eğer claimlerinin içinde ilgili rol varsa 
                {
                    return; //return et yani metotu çalıştırmaya devam et
                }
            }
            throw new Exception(Messages.AuthorizationDenied); //Eğer ki yoksa hata verdir.
        }
    }
}
