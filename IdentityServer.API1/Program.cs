using Microsoft.AspNetCore.Authentication.JwtBearer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
{
  options.Authority = "https://localhost:7036";
  options.Audience = "resource_api1";
});
builder.Services.AddAuthorization(options =>
{
  options.AddPolicy("ReadProduct", policy =>
  {
    policy.RequireClaim("scope", "api1.read");
  });

  options.AddPolicy("UpdateOrCreateProduct", policy =>
  {
    policy.RequireClaim("scope", new[] {"api1.update","api1.write"});
  });


});
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
