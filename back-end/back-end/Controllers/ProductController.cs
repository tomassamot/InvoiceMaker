using Microsoft.AspNetCore.Mvc;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Diagnostics.Metrics;
using System.Collections.Generic;
using back_end.Models;
using Microsoft.EntityFrameworkCore;
using back_end.DTOs.ProductDTOs;

namespace back_end.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        readonly DbContextOptions<MyContext> options;

        public ProductController()
        {
            options = new DbContextOptionsBuilder<MyContext>().UseInMemoryDatabase(databaseName: "Database").Options;
        }
        
        [HttpPost]
        public async Task<ActionResult> CreateProduct(ProductCreateDTO productCreateDTO)
        {
            Product product;
            using (var context = new MyContext(options))
            {
                int id = context.Products.Any() ?
                    context.Products.Max(x => x.Id) + 1 : 1;
                product = new Product(id, productCreateDTO.Name, productCreateDTO.Description, productCreateDTO.Price, productCreateDTO.AccountId);
                context.Products.Add(product);
                await context.SaveChangesAsync();
            }
            return CreatedAtAction(nameof(GetProductById), new { id = product.Id }, product);
        }
        [HttpPut]
        public ActionResult ModifyProductById()
        {
            throw new NotImplementedException();
        }
        [HttpDelete]
        public ActionResult DeleteProductById(int id)
        {
            throw new NotImplementedException();
        }
        private async Task<decimal> CalculatePriceWithVAT(MyContext context, Product product, Account buyerAccount)
        {
            decimal priceWithVAT = product.Price;
            Account? providerAccount = await context.Accounts.FindAsync(product.AccountId);
            if (providerAccount == null)
            {
                Console.WriteLine("Provider not found");
                return -1M;
            }

            // provider is paying VAT and buyer is in the same country as provider
            if (providerAccount.IsPayingVAT && providerAccount.LocationName == buyerAccount.LocationName)
            {
                priceWithVAT = product.Price + Math.Round((product.Price * (decimal)buyerAccount.LocationVAT / 100), 2);
            }
            // provider is paying VAT, buyer isn't paying VAT, buyer isn't in the same country as provider and both are in the EU
            else if (providerAccount.IsPayingVAT && !buyerAccount.IsPayingVAT && buyerAccount.LocationName != providerAccount.LocationName && buyerAccount.LocationRegion.ToLower() == "europe")
            {
                priceWithVAT = product.Price + Math.Round((product.Price * (decimal)buyerAccount.LocationVAT / 100), 2);
            }

            return priceWithVAT;
        }
        [HttpGet("{buyerId},{productId}")]
        public async Task<ActionResult<Product>> GetProductWithVAT(int buyerId, int productId)
        {
            Account? buyerAccount;
            Product? product;
            using (var context = new MyContext(options))
            {
                buyerAccount = await context.Accounts.FindAsync(buyerId);
                product = await context.Products.FindAsync(productId);
                if (buyerAccount == null || product == null)
                {
                    return BadRequest();
                }
                decimal priceWithVAT = await CalculatePriceWithVAT(context, product, buyerAccount);
                product.PriceWithVAT = priceWithVAT;
            }
            return Ok(product);


        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProductById(int id)
        {
            Product? product;
            using (var context = new MyContext(options))
            {
                product = await context.Products.FindAsync(id);
            }
            if (product != null)
            {
                return product;
            }
            else
            {
                return BadRequest();
            }
        }
        [HttpGet]
        public async Task<ActionResult> GetAllProducts()
        {
            List<Product> allProducts = new();
            using (var context = new MyContext(options))
            {
                allProducts = await context.Products.Select(x => x).ToListAsync();
            }
            return Ok(allProducts);
        }
    }
}