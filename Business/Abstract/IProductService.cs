﻿using Core.Utilities.Results;
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
        IResult TransactionalOperation(Product product); //Transaction yönetimi uygulamalarda tutarlılığı korumak için kullandığımız bir yöntem.
                                                       //Örneğin benim hesabımda 100 tl bir para var başkasının hesabına 10 tl aktarıcam.
                                                       //Benim hesabımın 10 tl düşecek şekilde update edilmesi ve gönderdiğim kişinin de 10 tl artacak şekilde update edilmesi.
                                                       //Yani 2 tane veritabanı işlemi var. Benim hesabımı güncellendi fakat giden arkadaşa para gitmeden sistem hata verdi.
                                                       //Benim hesabıma paranın geri iade edilmesi lazım yani işlemi geri alması gerekiyor. bu transaction yönetimi ile yapılıyor.

    }
}
