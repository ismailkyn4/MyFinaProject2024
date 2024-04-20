using Business.Abstract;
using Business.CCS;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class ProductManager : IProductService
    {
        IProductDal _productDal;
        ICategoryService _categoryService; //ben productan bağımsız bir kuralı yani başka bir servisi enjekte ederken bu yöntemi kullanmalıyız.
        //Neden ICategoryDal kullanmadık çünkü Bir entityManager kendisi hariç başka dalı enjdekte edemez.
        public ProductManager(IProductDal productDal, ICategoryService categoryService) //ProductManager newlendiğinde contructor bana bir tane IProductDal referansı ver diyor.
        {
            _productDal = productDal;
            _categoryService = categoryService;
        }


        [ValidationAspect(typeof(ProductValidator))]  //Add metotunu doğrula ProductValidator kullanarak. 
        public IResult Add(Product product)
        {
            //ValidationTool.Validate(new ProductValidator(),product); //ValidationAspect eklemeden önce kullanım bu şekilde yapılıyor.

            IResult result = BusinessRules.Run(CheckIfProductNameExists(product.ProductName),
                CheckIfProductCountOfCategoryCorrect(product.CategoryId),
                CheckIfCategoryLimitExceded()); //Bizim oluşturduğumuz iş kuralı

            if (result != null) //eğer kurala uymayan bir durum oluşmuşsa.
            {
                return result;
            }
            _productDal.Add(product);
            return new SuccessResult(Messages.ProductAdded);


            //Sonuç olarak başarılı mı başarısız mı ve ona göre mesaj göndereceğiz.
            //Bunu da Resultın contractır bloğu içinde yapacağız.
            //Bunu mesaj göndermeden de çalıştırabiliriz Bunu overloading işlemi ile SuccessResultın base'in de yaptık.
        }


        public IDataResult<List<Product>> GetAll()
        {
            
            //if (DateTime.Now.Hour==23) //sistem saatini verir.
            //{
            //    return new ErrorDataResult<List<Product>>(Messages.MaintenanceTime);
            //}
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(), Messages.ProductListed);
        }

        public IDataResult<List<Product>> GetAllByCategoryId(int id)
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(p => p.CategoryId == id), Messages.GetAllByCategoryId); //Her p için p'nin CategoryId si benim gönderdiğim id'ye eşitse onları filtrele
        }

        public IDataResult<Product> GetById(int productId)
        {
            return new SuccessDataResult<Product>(_productDal.Get(p => p.ProductId == productId)); //Mesaj vermek istersen yazabiliriz.
        }

        public IDataResult<List<Product>> GetByUnitPrice(decimal min, decimal max)
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(p => p.UnitPrice >= min && p.UnitPrice <= max));
        }

        public IDataResult<List<ProductDetailDto>> GetProductDetails() //Bu join etttiğimiz nesneleri getirecek
        {
            //if (DateTime.Now.Hour == 23) //sistem saatini verir.
            //{
            //    return new ErrorDataResult<List<ProductDetailDto>>(Messages.MaintenanceTime);
            //}
            return new SuccessDataResult<List<ProductDetailDto>>(_productDal.GetProductDetails());
        }

        [ValidationAspect(typeof(ProductValidator))]
        public IResult Update(Product product)
        {
            var productIdSayisi = _productDal.GetAll(p => p.ProductId == product.ProductId).Count;
            if (productIdSayisi >= 10)
            {
                return new ErrorResult(Messages.ProductCountOfCategoryError);
            }
            throw new NotImplementedException();
        }
        private IResult CheckIfProductCountOfCategoryCorrect(int categoryId) //bu categorye 15den fazla ürün eklenemez.
        {
            //Select count(*) from products where categoryId=1 çalıştırır.
            var result = _productDal.GetAll(p => p.CategoryId == categoryId).Count;
            if (result >= 15)
            {
                return new ErrorResult(Messages.ProductCountOfCategoryError);
            }
            return new SuccessResult();
        }
        private IResult CheckIfProductNameExists(string productName)
        {
            var result = _productDal.GetAll(p => p.ProductName == productName).Any(); //şuna uyan kayıt var mı ? 
            if (result)
            {
                return new ErrorResult(Messages.ProductNameAlreadExists);
            }
            return new SuccessResult();
        }
        private IResult CheckIfCategoryLimitExceded()
        {
                var result = _categoryService.GetAll();
                if (result.Data.Count > 15)
                {
                    return new ErrorResult(Messages.CategoryLimitExceded);
                }

            return new SuccessResult();
        }
    }
}
