using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.CrossCuttingConcerns.Validation
{
    //Bu tarz toollar static oluşturulur. Tek bir instance oluşturulur uygulama belleği sadece onu kullanır. Bidaha ben onu newlemeyeyim diye.
    public static class ValidationTool  
    {
        public static void Validate(IValidator validator,object entity) //ProductValidator'ü burda kullancağımız için ProductValidatorün base sınıfı içinde bir interface ararız.
                                                                        //IValidatoru bulduktan sonra anlıyoruz ki buraya ProductValidator,CustomerValidator vs vs gelebilir.
                                                                        //daha sonra yanına entity,dto belirtmek istediğimizden object enttiy yapyırouz.çünkü object bütün hepsinin base'idir 
                                                                        //tekrar tekrar yazmamak için Bir yerden validator kontrolü sağlıyoruz.
        {
            var context = new ValidationContext<object>(entity);
            var result = validator.Validate(context);
            if (!result.IsValid)
            {
                throw new ValidationException(result.Errors);
            }
        }
    }
}
