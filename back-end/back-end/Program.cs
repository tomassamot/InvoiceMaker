using back_end.Controllers;
using back_end.DTOs.AccountDTOs;
using back_end.DTOs.ProductDTOs;

AccountController accountController = new AccountController();
ProductController productController = new ProductController();

await accountController.CreateAccount(new AccountCreateDTO("username1", "passhash1", "salt1", "location1", true, true));
await accountController.CreateAccount(new AccountCreateDTO("username2", "passhash2", "salt2", "location2", false, true));
await productController.CreateProduct(new ProductCreateDTO("product1", "desc1", 5.00M, 5.00M, 1));

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

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

app.UseAuthorization();

app.MapControllers();

app.Run();
