using back_end.Controllers;
using back_end.DTOs.AccountDTOs;
using back_end.DTOs.ProductDTOs;
using back_end.Models;
using Newtonsoft.Json;

AccountController accountController = new AccountController();
ProductController productController = new ProductController();

await accountController.CreateAccount(new AccountCreateDTO("provider1", "pass1", "pass1", "we", "we", 20.15f, true, false));
await accountController.CreateAccount(new AccountCreateDTO("provider2", "pass1", "pass1", "we", "we", 20.15f, true, true));
await accountController.CreateAccount(new AccountCreateDTO("provider3", "pass1", "pass1", "one", "we", 20.15f, true, true));
await accountController.CreateAccount(new AccountCreateDTO("provider4", "pass1", "pass1", "one", "we", 20.15f, true, true));
await accountController.CreateAccount(new AccountCreateDTO("provider5", "pass1", "pass1", "same", "same", 20.15f, true, true));

await accountController.CreateAccount(new AccountCreateDTO("buyer1", "pass1", "pass1", "we", "we", 18.62f, false, true));
await accountController.CreateAccount(new AccountCreateDTO("buyer2", "pass1", "pass1", "angola", "africa", 18.62f, false, true));
await accountController.CreateAccount(new AccountCreateDTO("buyer3", "pass1", "pass1", "other", "europe", 18.62f, false, false));
await accountController.CreateAccount(new AccountCreateDTO("buyer4", "pass1", "pass1", "other", "europe", 18.62f, false, true));
await accountController.CreateAccount(new AccountCreateDTO("buyer5", "pass1", "pass1", "same", "same", 18.62f, false, false));


await productController.CreateProduct(new ProductCreateDTO("product1", "should apply 0 percent VAT", 5.00M, 1));
await productController.CreateProduct(new ProductCreateDTO("product2", "should apply 0 percent VAT", 5.00M, 2));
await productController.CreateProduct(new ProductCreateDTO("product3", "should apply 18.62 percent VAT (based on buyer)", 5.00M, 3));
await productController.CreateProduct(new ProductCreateDTO("product4", "should apply 0 percent VAT", 5.00M, 4));
await productController.CreateProduct(new ProductCreateDTO("product5", "should apply 18.62 percent VAT (based on buyer)", 5.00M, 5));


var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins, policy =>
    {
        policy.WithOrigins("http://localhost:3000").AllowAnyHeader().AllowAnyMethod();
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

app.UseCors(MyAllowSpecificOrigins);

app.UseAuthorization();

app.MapControllers();

app.Run();
