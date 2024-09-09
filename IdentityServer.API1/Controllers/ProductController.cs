using IdentityServer.API1.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IdentityServer.API1.Controllers
{
  [Route("api/[controller]/[action]")]
  [ApiController]
  public class ProductController : ControllerBase
  {

    //Bu methodun endpointi : /api/product/getproducts
    [Authorize(Policy = "ReadProduct")]
    [HttpGet]
    public IActionResult GetProducts()
    {
      var productList = new List<Product>()
      {
        new Product { Id = 1, Name = "Kalem", Price  = 100 , Stock = 10 },
        new Product { Id = 2, Name = "Silgi", Price  = 200 , Stock = 20 },
        new Product { Id = 3, Name = "Defter", Price  = 300 , Stock = 30 },
        new Product { Id = 4, Name = "Kırtasiye", Price  = 400 , Stock = 40 },
        new Product { Id = 5, Name = "Bant", Price  = 500 , Stock = 50 }
      };
      return Ok(productList);
    }


    //Bu methodun endpointi : /api/product/updateproduct
    [HttpPost]
    [Authorize(Policy = "UpdateOrCreateProduct")]
    public IActionResult UpdateProduct(int id)
    {
      return Ok($"id'si {id} olan pruduct güncellenmiştir.");
    }


    //Bu methodun endpointi : /api/product/createproduct
    [HttpPost]
    [Authorize(Policy = "UpdateOrCreateProduct")]
    public IActionResult CreateProduct(Product product)
    {
      return Ok(product);
    }

  }
}
