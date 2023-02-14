using System;
using System.Collections.Generic;
using System.Text;

namespace AtmApp.Atm.Data.Models
{
    public class CustomerModel
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public String? PhoneNumber { get; set; }
        public string? Gender { get; set; }
        public string? AccountNumber { get; set; }
        public string? AccountType { get; set; }
        public string? Bank { get; set; } = "Genesys bank";
        public decimal AccountBalance { get; set; } = 0;
        public int Pin { get; set; }
    }
}
