using AtmApp.Atm.BLL.Implementation;
using AtmApp.Atm.Data.Models;
using AtmApp.BLL.HelperFunctions;
using System;


namespace AtmApp.UI
{
    public class Presentation
    {
        public static void RunCreateNewAccount()
        {
            try
            {
                Console.WriteLine("Enter your new pin: ");
                int userPin = Convert.ToInt16(Console.ReadLine());

                Console.WriteLine("Confirm your pin: ");
                int userPinConfirm = Convert.ToInt16(Console.ReadLine());

                if (userPinConfirm != userPin)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Both pins do not match...");
                    Console.ForegroundColor = ConsoleColor.White;
                    return;
                }

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("New pin saved!\n");
                Console.ForegroundColor = ConsoleColor.White;

                Console.WriteLine("Enter your first name: ");
                string firstname = Console.ReadLine();

                Console.WriteLine("Enter your last name: ");
                string lastname = Console.ReadLine();

                Console.WriteLine("Enter your phone number: ");
                long phoneNumber = Convert.ToInt64(Console.ReadLine());

                Console.WriteLine("Select your gender(press 1 for male || press 2 for female): ");
                int genderSelect = Convert.ToInt16(Console.ReadLine());

                string gender;

                if (genderSelect == 1)
                {
                    gender = "Male";
                }
                else if (genderSelect == 2)
                {
                    gender = "Female";
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid input...\n");
                    Console.ForegroundColor = ConsoleColor.White;
                    return;
                }


                Console.WriteLine("Select type of account(press 1 for savings || press 2 for current): ");
                int accountTypeSelect = Convert.ToInt32(Console.ReadLine());

                string accountType;

                if(accountTypeSelect == 1)
                {
                    accountType = "Savings";
                }
                else if(accountTypeSelect == 2)
                {
                    accountType = "Current";
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid input...\n");
                    Console.ForegroundColor = ConsoleColor.White;
                    return;
                }


                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Account created successfully\n");
                Console.ForegroundColor = ConsoleColor.White;


                var customerData = new CustomerModel
                {
                    Pin = userPin,
                    FirstName = firstname,
                    LastName = lastname,
                    PhoneNumber = phoneNumber.ToString(),
                    Bank = "Genesys Bank",
                    Gender = gender,
                    AccountNumber = GenerateRandomNumbers.RandomDigits(10).ToString(),
                    AccountType = accountType,
                    AccountBalance = 0
                };

                AtmService atmService = new AtmService();

                atmService.CreateNewAccount(customerData);

            }
            catch (Exception ex)
            {
                if (ex is FormatException)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid input...\n");
                    Console.ForegroundColor = ConsoleColor.White;
                }


                if (ex is Exception)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Something went wrong...\n");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                
            }
        }




        public static void RunWithdraw(int pin)
        {
            try
            {
                Console.WriteLine("Enter amount to withdraw: ");
                decimal amount = Convert.ToDecimal(Console.Read());


                AtmService atmService = new AtmService();

                atmService.Withdraw(pin, amount);


                Console.WriteLine("Withdrawal successful");
            }
            catch (Exception ex)
            {

                if (ex is FormatException) Console.WriteLine("Invalid input...");
                if (ex is NullReferenceException) Console.WriteLine("Must enter an input...");
                if (ex is Exception) Console.WriteLine("Something went wrong...");
            }

        }




        public static void RunViewAccountDetails(int pin)
        {
            AtmService atmService = new AtmService();

            var customer = atmService.ViewAccountDetails(pin);


            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\n*****************\nFirstname: {customer.FirstName}\nLastname: {customer.LastName}\nAccountNumber: {customer.AccountNumber}\nAccountType: {customer.AccountType}\nAccountBalance: {customer.AccountBalance}\n\n*****************\n");
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
