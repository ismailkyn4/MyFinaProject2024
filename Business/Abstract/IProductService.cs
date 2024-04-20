using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IProductService
    {
        IDataResult<List<Product>> GetAll(); //Hem liste hem data döndürmek istediğimizde IDataResult olarak tanımlama yaparız.
                                             //IDataResult IResulttan impelemente olduğu için
                                             //IResultın bkullandığı succes ve mesaj parametrelerinide kullanırız.
        IDataResult<List<Product>> GetAllByCategoryId(int id);

        IDataResult<List<Product>> GetByUnitPrice(decimal min, decimal max);
        IDataResult<List<ProductDetailDto>> GetProductDetails();
        IDataResult<Product> GetById(int productId); 
        IResult Add(Product product); //Void olan yerlerde IResult diyerek kullanımı profesyönelleştiriyoruz.
        IResult Update(Product product);

    }
}
