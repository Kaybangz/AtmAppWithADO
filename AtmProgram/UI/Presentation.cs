using AtmApp.Atm.BLL.Implementation;
using AtmApp.Atm.Data.DataStore;
using AtmApp.Atm.Data.Models;
using AtmApp.BLL.HelperFunctions;
using System;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace AtmApp.UI
{
    public class Presentation
    {
        public async Task RunCreateNewAccount()
        {
            using (AtmService atmService = new AtmService())
            {
                var userData = new CustomerModel
                {
                    Pin = 3307,
                    FirstName = "Test",
                    LastName = "Something",
                    PhoneNumber = "093292939293",
                    Gender = "Male",
                    AccountNumber = GenerateRandomNumbers.RandomDigits(10),
                    AccountType = "Savings",
                    AccountBalance = 3000
                };

                var createdUser = await atmService.CreateNewAccount(userData);

                Console.WriteLine(createdUser);
            }
        }


        //Run deposit
        public void RunSomething()
        {
            try
            {
                Console.WriteLine("Enter your pin number: ");
                int pinNumber = Convert.ToInt16(Console.Read());

                Console.WriteLine("Confirm pin number: ");
                int pinNumberConfirm = Convert.ToInt16(Console.Read());


                
            }

            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        } 

        //Run withdrawal

        //Run transfer
    }
}
