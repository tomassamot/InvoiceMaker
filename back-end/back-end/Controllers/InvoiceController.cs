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
        //AccountRepository accountRepository;
        readonly DbContextOptions<MyContext> options;

        public InvoiceController()
        {
            //accountRepository = new AccountRepository();
            options = new DbContextOptionsBuilder<MyContext>().UseInMemoryDatabase(databaseName: "Test").Options;
        }

        [HttpPost]
        public ActionResult CreateInvoice(int accountId, int productId)
        {
            Invoice invoice = new();
            using (var context = new MyContext(options))
            {
                Account? account = context.Accounts.Find(accountId);
                Product? product = context.Products.Find(productId);

                if(account == null || product == null)
                {
                    return BadRequest("Account or product not found");
                }

                int invoiceId = context.Invoices.Any() ?
                    context.Invoices.Max(x => x.Id) + 1 : 1;
                DateTime dateTime = DateTime.UtcNow;

                invoice = new Invoice(invoiceId, dateTime, productId, accountId);
                context.Invoices.Add(invoice);
                context.SaveChanges();
            }
            return CreatedAtAction(nameof(GetInvoiceById), new { id = invoice.Id }, invoice);
        }
        [HttpGet("{id}")]
        public ActionResult<Invoice> GetInvoiceById(int id)
        {
            Invoice? invoice;
            using (var context = new MyContext(options))
            {
                invoice = context.Invoices.Find(id);
            }
            if (invoice != null)
            {
                return invoice;
            }
            else
            {
                return new Invoice();
            }
        }
        [HttpGet]
        public ActionResult GetAllInvoices()
        {
            List<Invoice> allInvoices = new();
            using (var context = new MyContext(options))
            {
                //context.A
                allInvoices = context.Invoices.Select(x => x).ToList();
            }
            return Ok(allInvoices);
        }
        
    }
}
