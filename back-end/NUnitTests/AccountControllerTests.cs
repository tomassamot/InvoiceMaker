using back_end.Controllers;
using back_end.DTOs.AccountDTOs;
using back_end.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace NUnitTests
{
    public class AccountControllerTests
    {
        AccountController controller=new();
        [SetUp]
        public void Setup()
        {
            var allAccounts = GetAllAccounts();
            for(int i = 0; i < allAccounts.Count; i++)
            {
                controller.DeleteById(allAccounts[i].Id);
            }
        }
        private List<Account> GetAllAccounts()
        {
            var response = controller.GetAllAccounts();
            var result = (OkObjectResult)response.Result;
            return (List<Account>)result.Value;
        }
        [Test]
        public void CreateAccount_AccountCreation_AccountCreated()
        {
            Console.WriteLine("b");
            AccountCreateDTO accountToCreate = new("bob25", "hash", "salt", "Latvia", false, true);

            
            controller.CreateAccount(accountToCreate);
            List<Account> allAccounts = GetAllAccounts();

            Assert.That(allAccounts, Has.Count.EqualTo(1), "Full account list doesn't have new account");
        }
        [Test]
        public void CreateAccount_IdenticalAccountCreation_SecondAccountNotCreated()
        {
            AccountCreateDTO firstToCreate = new("bob25", "hash", "salt", "Latvia", false, true);
            AccountCreateDTO secondToCreate = new("bob25", "newhash", "newsalt", "Romania", true, true);

            controller.CreateAccount(firstToCreate);
            controller.CreateAccount(secondToCreate);
            List<Account> allAccounts = GetAllAccounts();

            Assert.That(allAccounts, Has.Count.EqualTo(1));
        }
    }
}