using AtmApp.Atm.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AtmApp.Atm.BLL.Interface
{
    public interface IAtmService : IDisposable
    {
        //Create new account
        //Deposit 
        //Withdraw
        //Transfer
        //Account details
        Task<long> CreateNewAccount(CustomerModel customer);
        void ViewAccountDetails(int pin);
        void Deposit(int pin, decimal depositAmount);
        void Withdraw(int pin, decimal amount);
        void Transfer(int pin);
    }
}
