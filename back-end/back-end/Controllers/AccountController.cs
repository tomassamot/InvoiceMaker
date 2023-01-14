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
        //AccountRepository accountRepository;
        readonly DbContextOptions<MyContext>  options;

        public AccountController()
        {
            //accountRepository = new AccountRepository();
            options = new DbContextOptionsBuilder<MyContext>().UseInMemoryDatabase(databaseName: "Test").Options;
        }


        [HttpPost]
        public async Task<ActionResult<Account>> CreateAccount(AccountCreateDTO /*Account*/ accountCreateDTO)
        {
            /*Account account = accountRepository.CreateAccount(accountCreateDTO);
            return new ActionResult<Account>(account);*/
            Account account;
            using (var context = new MyContext(options))
            {
                int id = context.Accounts.Any() ?
                    context.Accounts.Max(x => x.Id) + 1 : 1;
                account = new Account(id, accountCreateDTO.Username, accountCreateDTO.PasswordHash, accountCreateDTO.PasswordSalt, accountCreateDTO.Location, accountCreateDTO.IsProvider, accountCreateDTO.IsPayingVAT);
                context.Accounts.Add(account);
                await context.SaveChangesAsync();
            }
            return CreatedAtAction(nameof(GetAccountById), new { id = account.Id }, account);
        }
        [HttpPost("Login")]
        public async Task<ActionResult> Login(AccountLoginDTO accountLoginDTO)
        {
            //bool isAuthenticated = accountRepository.Authenticate(accountLoginDTO);
            using (var context = new MyContext(options))
            {
                List<Account> allAccount = await context.Accounts.Select(x => x).ToListAsync();
                foreach(Account account in allAccount)
                {
                    if(account.Username.Equals(accountLoginDTO.Username) && account.PasswordHash.Equals(accountLoginDTO.PasswordHash))
                    {
                        return Ok();
                    }
                }
                /*if (context.Accounts.Any(x => x.PasswordHash.Equals(accountLoginDTO))) // account exists with the same password hash
                {
                    return Ok();
                }*/
            }
            return BadRequest("No account with given username and password found");
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Account>> GetAccountById(int id)
        {
            Account? account;
            using (var context = new MyContext(options))
            {
                //context.A
                account = await context.Accounts.FindAsync(id);
            }
            if(account != null)
            {
                return account;
            }
            else
            {
                return new Account();
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
        
    }
}