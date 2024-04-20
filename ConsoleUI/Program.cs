// See https://aka.ms/new-console-template for more information
using Business.Concrete;
using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using DataAccess.Concrete.InMemory;
using Entities.Concrete;

//SOLID
//Open Closed Principle
//Yaptığımız yazılıma yeni bir özellik ekliyorsak mevcuttaki hiç bir koduma dokunamayız.
//ProductManager productManager = new ProductManager(new InMemoryProductDal()); //Beni newleyebilmen için bana hangi veri yöntemiyle çalıştığını söylemen lazım der.
//Bu inmemory, Oracle, sql server postgres veya mongodb de olabilir.
//Böyle çalışmamızın sebebi EntityFrameworke veya başka bir altyapıya geçeceğiğimiz DataAccess katmanında
//oluşturacağımız EntityFramework klasörü içindeki classı IProductDaldan implemente edip
//burada ProductManager içinde kullanacağımızı belirterek kolayca geçiş sağlıyoruz.

//ProductTest();
ProductGetAllTest();
//CategoryTest();
//ProductAdded();

static void ProductTest()
{
    ProductManager productManager = new ProductManager(new EfProductDal(), new CategoryManager(new EfCategoryDal()));//Görüldüğüzü üzere Business katmanında ProductManagerda birşey değiştirmedik.
                                                                           //Sadece InMemory yerine EntiyFramework kulanacağımız için EfProductDal oluşturduk
                                                                           //Artık memoryde değil gerçek veritabanında çalışıyoruz.
    var result = productManager.GetProductDetails();
    if (result.Success == true)
    {
        foreach (var product in result.Data)
        {
            Console.WriteLine(product.ProductName + " / " + product.CategoryName);

        }
    }
    else
    {
        Console.WriteLine(result.Message);
    }

    //foreach (var product in productManager.GetAll())
    //{
    //    Console.WriteLine(product.ProductName);
    //}
    //foreach (var product in productManager.GetAllByCategoryId(5))
    //{
    //    Console.WriteLine(product.ProductName);
    //}
    //Console.WriteLine("\nGetByUnit ile getirme\n");
    //foreach (var product in productManager.GetByUnitPrice(50, 100))
    //{
    //    Console.WriteLine(product.ProductName);

    //}
}

static void CategoryTest()
{
    CategoryManager categoryManager = new CategoryManager(new EfCategoryDal());
    foreach (var category in categoryManager.GetAll().Data)
    {
        Console.WriteLine(category.CategoryName);
    }
}

static void ProductAdded()
{
    ProductManager productManager = new ProductManager(new EfProductDal(),new CategoryManager(new EfCategoryDal()));
    Product product = new Product
    {
        CategoryId = 1,
        ProductName = "Test",
        UnitPrice = 10,
        UnitsInStock = 10,
    };
    Console.WriteLine(productManager.Add(product).Message);
}

static void ProductGetAllTest()
{
    ProductManager productManager = new ProductManager(new EfProductDal(), new CategoryManager(new EfCategoryDal()));
    var result = productManager.GetAll();
    foreach (var results in result.Data)
    {
        Console.WriteLine(results.ProductName);
    }
}