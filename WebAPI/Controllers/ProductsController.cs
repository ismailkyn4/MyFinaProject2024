using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

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
        public IActionResult GetAll()
        {
            var result = _productService.GetAll();
            if (result.Success)
            {
                //return Ok(result.Data); //Boşta gönderebiliriz. Overload'ı içine veri girilmesi durumudur. Eğer işlem başarılıysa data'yıda döndürürüz.
                return Ok(result);        //Sadece result döndürüp diğer işlem parametrelerinide görebiliriz
            }
            return BadRequest(result);    //bu şekilde hem datayı hem mesajı hemde succes durumunu verir.
            //return BadRequest(result.Message); // sadece mesajı bermiş oluruz.
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
    }
}
