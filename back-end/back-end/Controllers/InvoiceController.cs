using back_end.DTOs.AccountDTOs;
using back_end.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Principal;

namespace back_end.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InvoiceController : ControllerBase
    {
        readonly DbContextOptions<MyContext> options;

        public InvoiceController()
        {
            options = new DbContextOptionsBuilder<MyContext>().UseInMemoryDatabase(databaseName: "Database").Options;
        }

        [HttpPost]
        public async Task<ActionResult> CreateInvoice(int accountId, List<Product> products)
        {
            Invoice invoice;
            List<Product> allProducts = new List<Product>();

            using (var context = new MyContext(options))
            {
                Account? account = await context.Accounts.FindAsync(accountId);
                if (account == null || products.Count == 0)
                {
                    return BadRequest("Account or product not found");
                }
                for(int i = 0; i < products.Count; i++)
                {
                    var product = await context.Products.FindAsync(products[i].Id);
                    if(product != null)
                    {
                        allProducts.Add(product);
                    }
                }

                int invoiceId = context.Invoices.Any() ?
                    context.Invoices.Max(x => x.Id) + 1 : 1;
                DateTime dateTime = DateTime.UtcNow;

                invoice = new Invoice(invoiceId, dateTime, allProducts, accountId);
                context.Invoices.Add(invoice);
                await context.SaveChangesAsync();
            }
            return CreatedAtAction(nameof(GetInvoiceById), new { id = invoice.Id }, invoice);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Invoice>> GetInvoiceById(int id)
        {
            Invoice? invoice;
            using (var context = new MyContext(options))
            {
                invoice = await context.Invoices.FindAsync(id);
            }
            if (invoice != null)
            {
                return invoice;
            }
            else
            {
                return BadRequest();
            }
        }
        [HttpGet("GetAll")]
        public async Task<ActionResult> GetAllInvoices()
        {
            List<Invoice> allInvoices = new();
            using (var context = new MyContext(options))
            {
                allInvoices = await context.Invoices.Select(x => x).ToListAsync();
            }
            return Ok(allInvoices);
        }
        [HttpGet("GetAll/{accountId}")]
        public async Task<ActionResult> GetAllInvoicesByAccountId(int accountId)
        {
            List<Invoice> accountsInvoices = new List<Invoice>();
            using (var context = new MyContext(options))
            {
                List<Invoice> allInvoices = await context.Invoices.Select(x => x).ToListAsync();
                for(int i = 0; i < allInvoices.Count; i++)
                {
                    Invoice invoice = allInvoices[i];
                    if(invoice.AccountId == accountId)
                    {
                        accountsInvoices.Add(invoice);
                    }
                }
            }
            return Ok(accountsInvoices);
        }
    }
}
