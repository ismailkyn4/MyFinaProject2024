using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.FluentValidation
{
    public class ProductValidator : AbstractValidator<Product> //Bu product için bir validator olduğu için Product olarak belirledik. Dto veya diğer nesnelerimiz içinde aynı işlemi yapabiliriz.
    {
        public ProductValidator() //Bu kurallar bir constructor içine yazılır.
        {
            RuleFor(p => p.ProductName).NotEmpty(); //productName boş olamaz.
            RuleFor(p => p.ProductName).MinimumLength(2); //kuralları belirlemeye başlıyoruz. ProductName 2 karakter olmalıdır.
            RuleFor(p => p.UnitPrice).NotEmpty();
            RuleFor(p => p.UnitPrice).GreaterThan(0);  //unitPrice 0'dan büyük olmalı
            RuleFor(p => p.UnitPrice).GreaterThanOrEqualTo(10).When(p => p.CategoryId == 1); //categoryId'si 1 olan ürünlerin fiyatı minimum 10 lira olmalı diyoruz. üstekini categoryId'si = 1 olan için ezdik.
            //RuleFor(p => p.ProductName).Must(StartWithA).WithMessage("Ürünler A harfi ile başlamalı");
            //RuleFor kuralları içinde olmayan kuralları vermek için StartWithA diye bir kural oluşturuyoruz. Benim yazdığım hata mesajını ver demek istersek .withMessage ile yapıyoruz.

        } //bu kuralları yazdık ama nasıl çalıştırıcaz. Bunu gidiyoruz ProductManager içinde iş kurallarında yazıyoruz.
        private bool StartWithA(string arg) //arg burda bizim gönderdiğimiz product, eğer burda true döndürürsek yukarıdaki kural çalışır false ise çalışmaz.
        {
            return arg.StartsWith("A");   //Eğer A ile başlarsa true döner.
        }
    }
}
