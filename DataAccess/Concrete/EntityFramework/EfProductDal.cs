using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfProductDal : EfEntityRepositoryBase<Product, NortwindContext>, IProductDal
    {
        public List<ProductDetailDto> GetProductDetails()
        {
            using (NortwindContext context=new NortwindContext())
            {
                //ürünlere p de categorylere c de ürünlerle categoryleri join et.
                //Neye göre join et p'deki categoryId ile c'deki categoryId eşit ise 
                var result = from p in context.Products
                             join c in context.Categories
                             
                             on p.CategoryId equals c.CategoryId
                             select new ProductDetailDto
                             {
                                 ProductId = p.ProductId, ProductName = p.ProductName,
                                 UnitInStock = p.UnitsInStock, CategoryName = c.CategoryName 
                             };
                return result.ToList(); 
            }
        }
    }
}
