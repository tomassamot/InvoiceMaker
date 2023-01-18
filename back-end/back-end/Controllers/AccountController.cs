using Microsoft.AspNetCore.Mvc;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Diagnostics.Metrics;
using System.Collections.Generic;
using back_end.Models;
using Microsoft.EntityFrameworkCore;
using back_end.DTOs.AccountDTOs;
using System.Security.Principal;

namespace back_end.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        readonly DbContextOptions<MyContext>  options;

        public AccountController()
        {
            options = new DbContextOptionsBuilder<MyContext>().UseInMemoryDatabase(databaseName: "Database").Options;
        }


        [HttpPost]
        public async Task<ActionResult<Account>> CreateAccount(AccountCreateDTO accountCreateDTO)
        {
            if(accountCreateDTO.Username == "")
            {
                return BadRequest("Username can't be empty");
            }
            if(accountCreateDTO.Password == "")
            {
                return BadRequest("Password can't be empty");
            }
            if (accountCreateDTO.ConfirmPassword == "")
            {
                return BadRequest("Confirm Password can't be empty");
            }
            if (accountCreateDTO.Password != accountCreateDTO.ConfirmPassword)
            {
                return BadRequest("Password and Confirm Password doesn't match");
            }
           
            Account account;
            using (var context = new MyContext(options))
            {
                int id = context.Accounts.Any() ?
                    context.Accounts.Max(x => x.Id) + 1 : 1;
                account = new Account(id, accountCreateDTO.Username, Account.GetHashString(accountCreateDTO.Password), accountCreateDTO.LocationName, accountCreateDTO.LocationRegion, accountCreateDTO.LocationVAT, accountCreateDTO.IsProvider, accountCreateDTO.IsPayingVAT);
                context.Accounts.Add(account);
                await context.SaveChangesAsync();
            }
            return CreatedAtAction(nameof(GetAccountById), new { id = account.Id }, account);
        }
        [HttpPost("Login")]
        public async Task<ActionResult> Login(AccountLoginDTO accountLoginDTO)
        {
			if(accountLoginDTO.Username == "")
            {
                return BadRequest("Username can't be empty");
            }
            if(accountLoginDTO.Password == "")
            {
                return BadRequest("Password can't be empty");
            }
			
            var username = accountLoginDTO.Username;
            var passwordHash = Account.GetHashString(accountLoginDTO.Password);
            using (var context = new MyContext(options))
            {
                List<Account> allAccount = await context.Accounts.Select(x => x).ToListAsync();
                foreach(Account account in allAccount)
                {
                    if(account.Username.Equals(username) && account.Password.Equals(passwordHash))
                    {
                        return Ok(account.Id);
                    }
                }
            }
            return BadRequest("No account with given username and password found");
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Account>> GetAccountById(int id)
        {
            Account? account;
            using (var context = new MyContext(options))
            {
                account = await context.Accounts.FindAsync(id);
            }
            if(account != null)
            {
                return account;
            }
            else
            {
                return BadRequest();
            }
        }
        [HttpGet]
        public async Task<ActionResult<List<Account>>> GetAllAccounts()
        {
            List<Account> allAccounts = new();
            using (var context = new MyContext(options))
            {
                //context.A
                allAccounts = await context.Accounts.Select(x => x).ToListAsync();
            }
            return Ok(allAccounts);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteById(int id)
        {

            using (var context = new MyContext(options))
            {
                Account? accountToDelete = context.Accounts.Find(id);
                if(accountToDelete != null)
                {
                    context.Accounts.Remove(accountToDelete);
                    await context.SaveChangesAsync();
                    return Ok();
                }
            }
            return BadRequest();
        }
    }
}