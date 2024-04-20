using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.InMemory //Şuanda Hafızada çalışıyoruz. Daha sonra Database üzerinde çalışacağız
{
    public class InMemoryProductDal : IProductDal
    {
        List<Product> _products; //Product tipinde bir _products oluştur.
        public InMemoryProductDal() 
        {
            //Oracle, Sql Server, Postgress veya MongoDb veri tabanından geliyormuş gibi simüle ediyoruz.
            _products = new List<Product> 
            {
               new Product{ProductId=1,CategoryId=1,ProductName="Bardak",UnitPrice=15,UnitsInStock=15},
               new Product{ProductId=2,CategoryId=1,ProductName="Kamera",UnitPrice=500,UnitsInStock=3},
               new Product{ProductId=3,CategoryId=2,ProductName="Telefon",UnitPrice=1500,UnitsInStock=2},
               new Product{ProductId=4,CategoryId=2,ProductName="Klavye",UnitPrice=150,UnitsInStock=65},
               new Product{ProductId=5,CategoryId=2,ProductName="Fare",UnitPrice=85,UnitsInStock=1}
            };
        }
        public void Add(Product product)
        {
            _products.Add(product); //Gönderdiğimiz product eklenir
        }

        public void Delete(Product product)
        {
            //LINQ - Language Integrated Query
            //_products.Remove(product); //Bu istenilen değeri tablodan silmez nedenide referans değer ile çalışıyoruz newlediğimiz anda referansı değişir.
            Product productToDelete = _products.SingleOrDefault(p => p.ProductId == product.ProductId); //SingleOrDefault tek bir eleman bulmaya yarar.
                                                                                                        //p'nin product idsi benim gönderdiğimiz product'ın product idsine eşittir.
            _products.Remove(productToDelete);
        }

        public List<Product> GetAll()
        {
            return _products; //_product ürünlerinin tümünü döndürdük.
        }

        public void Update(Product product)
        {
            Product productToUpdate = _products.SingleOrDefault(p => p.ProductId == product.ProductId); //Gönderdiğim ürün idsine sahip olan listedeki ürünü bul.
            productToUpdate.ProductName = product.ProductName;
            productToUpdate.UnitPrice = product.UnitPrice;
            productToUpdate.UnitsInStock = product.UnitsInStock;
            productToUpdate.CategoryId = product.CategoryId;
        }
        public List<Product> GetAllByCategory(int categoryId)
        {
            return _products.Where(p => p.CategoryId == categoryId).ToList(); //içindeki şarta uyan bütün elemanları liste haline getirdik ve onu döndürdük
        }

        public List<Product> GetAll(Expression<Func<Product, bool>> filter = null)
        {
            throw new NotImplementedException();
        }

        public Product Get(Expression<Func<Product, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public List<ProductDetailDto> GetProductDetails()
        {
            throw new NotImplementedException();
        }
    }
}
