using IdentityModel.Client;
using IdentityServer.Client1.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace IdentityServer.Client1.Controllers
{
  public class ProductController : Controller
  {
    private readonly IConfiguration _configuration;

    public ProductController(IConfiguration configuration)
    {
      _configuration = configuration;
    }

    public async Task<IActionResult> Index()
    {
      List<Product> products = new List<Product>();
        
      
      // IdentityModel paketi sayesinde httpcliente extension metodlar ekleniyor.
      // bu metodları kullanarak tokeni kolayca alıyoruz.


      //extension metodları kullanabilmek için httpclient nesnesi oluşturma
      HttpClient httpClient = new HttpClient();

      //GetDiscoveryDocumentAsync extension metodunu kullanarak parametre olarak verilen AuthSErver uygulamasının token endpointi gibi bilgileri kolayca alabiliyoruz.
      var discovery = await httpClient.GetDiscoveryDocumentAsync("https://localhost:7036");

      if (discovery.IsError)
      {
        //loglama
      }

      //kullanıcı olmadan bir identity server kullandığımız için bu nesneyi oluşturuyoruz.
      ClientCredentialsTokenRequest clientCredentialsTokenRequest = new ClientCredentialsTokenRequest();

      //nesneye clientimizle ilgili Id , Secret , ve hangi endpointe gitmesi gerektiğini bildiriyoruz.
      clientCredentialsTokenRequest.ClientId = _configuration["Client:ClientId"];
      clientCredentialsTokenRequest.ClientSecret = _configuration["Client:ClientSecret"];
      //discovery sayesinde endpointi dirrek olarak alabiliyoruz.
      clientCredentialsTokenRequest.Address = discovery.TokenEndpoint;
      
      //token alıyorız.
      var token = await httpClient.RequestClientCredentialsTokenAsync(clientCredentialsTokenRequest);

      if(token.IsError)
      {
        //loglama
      }

    
      //tokeni veriyoruz
      httpClient.SetBearerToken(token.AccessToken);


      var response = await httpClient.GetAsync("https://localhost:7162/api/product/getproducts");

      if(response.IsSuccessStatusCode)
      {
        var content = await response.Content.ReadAsStringAsync();

        products = JsonConvert.DeserializeObject<List<Product>>(content);

      
      }
      else
      {
        //loglama
      }

      return View(products);
    }
  }
}
