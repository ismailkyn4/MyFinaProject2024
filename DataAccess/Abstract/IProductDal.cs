using Core.DataAccess;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    //Dal=Data Access Layer
    public interface IProductDal : IEntityRepository<Product> //Bu sayede GetAll, Add ve diğer metotları tek tek her nesne(Product,Category) için yazmamıza gerek kalmayacak.
    {
        List<ProductDetailDto> GetProductDetails();
    }
}

