using AtmApp.Atm.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AtmApp.Atm.BLL.Interface
{
    public interface IAtmService : IDisposable
    {
        long CreateNewAccount(CustomerModel customer);
        CustomerModel ViewAccountDetails(int pin);
        void Withdraw(int pin, decimal amount);
        void Transfer(int pin);
    }
}
