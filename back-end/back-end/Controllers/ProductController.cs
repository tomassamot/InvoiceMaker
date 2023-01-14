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
            options = new DbContextOptionsBuilder<MyContext>().UseInMemoryDatabase(databaseName: "Test").Options;
        }
        
        [HttpPost]
        public async Task<ActionResult> CreateProduct(ProductCreateDTO productCreateDTO)
        {
            Product product;
            using (var context = new MyContext(options))
            {
                int id = context.Products.Any() ?
                    context.Products.Max(x => x.Id) + 1 : 1;
                product = new Product(id, productCreateDTO.Name, productCreateDTO.Description, productCreateDTO.SumWithVAT, productCreateDTO.SumWithoutVAT, productCreateDTO.AccountId);
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
                return new Product();
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