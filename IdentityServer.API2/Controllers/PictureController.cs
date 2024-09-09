using IdentityServer.API2.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IdentityServer.API2.Controllers
{
  [Route("api/[controller]/[action]")]
  [ApiController]
  public class PictureController : ControllerBase
  {

    [Authorize]
    [HttpGet]
    public IActionResult GetPictures()
    {
      var pictureList = new List<Picture>()
      {
        new Picture { Id = 1,Name= "Picture1", Url = "picture1.jpg" },
        new Picture { Id = 2,Name= "Picture2", Url = "picture2.jpg" },
        new Picture { Id = 3,Name= "Picture3", Url = "picture3.jpg" },
        new Picture { Id = 4,Name= "Picture4", Url = "picture4.jpg" },
        new Picture { Id = 5,Name= "Picture5", Url = "picture5.jpg" }
      };
      return Ok(pictureList);
    }
  }
}
