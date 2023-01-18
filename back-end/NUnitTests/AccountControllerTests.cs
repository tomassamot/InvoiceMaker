using back_end.Controllers;
using back_end.DTOs.AccountDTOs;
using back_end.Models;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using System.Net;

namespace NUnitTests
{
    public class AccountControllerTests
    {
        AccountController controller=new AccountController();
        [SetUp]
        public async Task Setup()
        {
            var allAccounts = await GetAllAccounts();
            if(allAccounts != null)
            {
                for (int i = 0; i < allAccounts.Count; i++)
                {
                    await controller.DeleteById(allAccounts[i].Id);
                }
            }
        }
        private async Task<List<Account>> GetAllAccounts()
        {
            var response = await controller.GetAllAccounts();
            var result = (OkObjectResult)response.Result;
            return (List<Account>)result.Value;
        }
        [Test]
        public async Task CreateAccount_AccountCreation_AccountCreated()
        {
            AccountCreateDTO accountToCreate = new AccountCreateDTO("bob", "pass1", "pass1", "location1", "region1", 20.5f, false, false);

            await controller.CreateAccount(accountToCreate);
            List<Account> allAccounts = await GetAllAccounts();

            Assert.That(allAccounts, Has.Count.EqualTo(1), "Full account list doesn't have new account");
        }
    }
}