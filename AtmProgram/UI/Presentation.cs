using AtmApp.Atm.BLL.Implementation;
using AtmApp.Atm.Data.DataStore;
using AtmApp.Atm.Data.Models;
using AtmApp.BLL.HelperFunctions;
using System;


namespace AtmApp.UI
{
    public class Presentation
    {
        public static void RunViewAccountDetails(int pin)
        {
            AtmService atmService = new AtmService();

            var customer = atmService.ViewAccountDetails(pin);


            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"*****************\nFirstname: {customer.FirstName}\nLastname: {customer.LastName}\nAccountNumber: {customer.AccountNumber}\nAccountType: {customer.AccountType}\nAccountBalance: {customer.AccountBalance}\n\n*****************\n");
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
