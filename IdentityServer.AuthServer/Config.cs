using Duende.IdentityServer.Models;
using Duende.IdentityServer.Test;
using System.Security.Claims;

namespace IdentityServer.AuthServer
{
  public static class Config
  {

    //apiler için resource tanımlanma
    public static IEnumerable<ApiResource> GetApiResources()
    {
      return new List<ApiResource>()
      {
        new ApiResource("resource_api1")
        {
          Scopes = { "api1.read", "api1.write", "api1.Update" },
          ApiSecrets = new[] {new Secret("secretapi1".Sha256())}
        },
        new ApiResource("resource_api2")
        {
          Scopes = { "api2.read", "api2.write", "api2.Update" },
          ApiSecrets = new[] {new Secret("secretapi2".Sha256())}
        }
      };
    }

    //apilerin izilerinin tanımlandığı alan
    public static IEnumerable<ApiScope> GetApiScopes()
    {
      return new List<ApiScope>()
      {
        new ApiScope ("api1.read","API 1 için okuma izni"),
        new ApiScope ("api1.write","API 1 için yazma izni"),
        new ApiScope ("api1.update","API 1 için güncelleme izni"),
        new ApiScope ("api2.read","API 2 için okuma izni"),
        new ApiScope ("api2.write","API 2 için yazma izni"),
        new ApiScope ("api2.update","API 2 için güncelleme izni")
      };
    }

    //client tanımlama ve hangi clientlerin hangi apilerden hangi izinlere erişme izni olacağını tanımlama
    public static IEnumerable<Client> GetClients()
    {
      return new List<Client>()
      {
       new Client()
        {
          ClientId="Client1",
          ClientName = "Client 1 app uygulaması",
          ClientSecrets=new[] {new Secret("secret".Sha256()) },
          AllowedGrantTypes= GrantTypes.ClientCredentials,
          AllowedScopes={"api1.read"}
        },
        new Client()
        {
          ClientId="Client2",
          ClientName = "Client 2 app uygulaması",
          ClientSecrets=new[] {new Secret("secret".Sha256()) },
          AllowedGrantTypes= GrantTypes.ClientCredentials,
          AllowedScopes={"api1.read","api1.update", "api2.write","api2.update"}
        }
      };
    }

    public static IEnumerable<IdentityResource> GetIdentityResources()
    {
      return new List<IdentityResource>()
     {
       //token içerisinde mutlaka bir OpenId identity resource bulunmak zorunda bu openıd token'ı alan kullanıcının id sini tutar.
       new IdentityResources.OpenId(),
       //kullanıcı ile ilgili claimleri tutar
       new IdentityResources.Profile()
     };
    }

    public static IEnumerable<TestUser> GetUsers()
    {
      return new List<TestUser>()
     {
       new TestUser() {SubjectId="1", Username="testuser11", Password="password", Claims=new List<Claim>(){new Claim("given_name","ibrahim"), new Claim("family_name","aksoy"),}},

       new TestUser() {SubjectId="2", Username="testuser22", Password="password", Claims=new List<Claim>(){new Claim("given_name","tarik"), new Claim("family_name","aksoy"),}}
     };
    }
  }
}
