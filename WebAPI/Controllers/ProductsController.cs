using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;
using System.Threading;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        //Loosely coupled
        //naming convention
        //IoC Container -- Inversion of Control

        IProductService _productService;

        public  ProductsController(IProductService productService)
        {
            _productService = productService;
        }


        [HttpGet("getall")]
        [ProducesResponseType(200,Type=typeof(Product))] //Bu şekilde durum kodlarınında swegarda gözükmesini sağlarız. Fakat 200 kodunun olduğu yerde tip belirtmezsek sadece succes durumu gözükür.
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
       // [ProducesResponseType(StatusCodes.Status201Created)]

        public IActionResult GetAll()
        {
            Thread.Sleep(1000);
            var result = _productService.GetAll();
            if (result.Success)
            {
                //return Ok(result.Data); //Boşta gönderebiliriz. Overload'ı içine veri girilmesi durumudur. Eğer işlem başarılıysa data'yıda döndürürüz.
                return Ok(result);        //Sadece result döndürüp diğer işlem parametrelerinide görebiliriz
            }
            return BadRequest(result);    //bu şekilde hem datayı hem mesajı hemde succes durumunu verir.
            //return BadRequest(result.Message); // sadece mesajı bermiş oluruz.
        }
        [HttpGet("GetProductDetails")]
        public IActionResult GetProductDetails()
        {
            var result = _productService.GetProductDetails();
            if (result.Success)
                return Ok(result);
            return BadRequest(result);
        }
        [HttpGet("getbyid")]
        public IActionResult GetById(int id)
        {
            var result = _productService.GetById(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpGet("getbycategoryid")]
        public IActionResult GetByCategoryId(int categoryId)
        {
            var result = _productService.GetAllByCategoryId(categoryId);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpPost("Add")]
        public IActionResult Add(Product product) //postmandan, clienttan, angullardan gönderdiğin ürünü buraya koy diyoruz. 
        {
            var result = _productService.Add(product);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpPost("Update")]
        public IActionResult Update(Product product) 
        {
            var result = _productService.Update(product);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpGet("getbycategory")]
        public IActionResult GetByCategory(int categoryId)
        {
            var result = _productService.GetAllByCategoryId(categoryId);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpPost("TransactionTest")]
        public IActionResult TransactionTest(Product product) 
        {
            var result = _productService.TransactionalOperation(product);
            if (result.Success)
                return Ok(result);
            return BadRequest(result);
        }
    }
}
