using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Interceptors;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Aspects.Autofac.Validation
{
    public class ValidationAspect : MethodInterception 
    {
        private Type _validatorType;
        public ValidationAspect(Type validatorType)  //bana validatorType ver diyoruz. Bu bir attribute olduğu için type geçmek zorundayız.
        {
            //depensive coding
            if (!typeof(IValidator).IsAssignableFrom(validatorType)) //gönderilen validatortype bir IValidator değilse ona kız diyoruz.
            {
                throw new System.Exception("Bu bir doğrulama sınıfı değil");
            }

            _validatorType = validatorType;
        }
        protected override void OnBefore(IInvocation invocation) //Base sınıfımız olan methodInterception içindeki OnBefore metotunu burda override ettik.
        {
            var validator = (IValidator)Activator.CreateInstance(_validatorType); //burda reflection var yani çalışma anında birşeyleri çalıştırmamızı sağlıyor. Bana gönderilen validatorType'in bir instance'ını oluştur.
            var entityType = _validatorType.BaseType.GetGenericArguments()[0]; //sonra validatorType'ın basetype'ını bul onun generic argümanlarından ilkini bul.
            var entities = invocation.Arguments.Where(t => t.GetType() == entityType); //ve onun entitytype'ı validator'ın tipine eşit olan parametrelerini bul
            foreach (var entity in entities) //her birini tek tek gez ve validationTool'u kullanarak validate et.
            {
                ValidationTool.Validate(validator, entity);
            }
        }
    }
}
