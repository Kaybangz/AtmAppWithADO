using System;
using System.Collections.Generic;
using System.Text;

namespace AtmApp.DAL.Models
{
    public class RecipientModel
    {
        public string? AccountNumber { get; set; }
        public string? Bank { get; set; }
        public decimal TransferAmount { get; set; }
    }
}
