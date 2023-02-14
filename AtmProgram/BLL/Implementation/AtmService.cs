using AtmApp.Atm.BLL.Interface;
using AtmApp.Atm.Data.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Data;
using System.Data.SqlTypes;
using System.Threading.Tasks;


namespace AtmApp.Atm.BLL.Implementation
{
    public class AtmService : IAtmService
    {
        private readonly string _connectionString = @"Data Source=KAYBANGZ;Initial Catalog=AtmAppDatabase;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public Task<long> CreateNewAccount(CustomerModel customer)
        {
            throw new NotImplementedException();
        }

        public void Deposit(int pin, decimal depositAmount)
        {
            using (SqlConnection connection = new SqlConnection())
            {
                connection.ConnectionString = _connectionString;
                connection.Open();
                
                string getAccountBalance = @"SELECT AccountBalance FROM Customers WHERE Pin = @Pin";

                SqlCommand myCommand = new SqlCommand(getAccountBalance, connection);

                myCommand.Parameters.Add("@Pin", SqlDbType.SmallInt).Value = pin;

                long accountBalance = (long)myCommand.ExecuteScalar();

                if(depositAmount <= 0 || depositAmount.GetType() is String)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Amount must be a number and must be greater than zero\n");
                    Console.ForegroundColor = ConsoleColor.White;

                    return;
                }

                decimal getUpdatedBalance = accountBalance + depositAmount;

                string updateCustomer = @"UPDATE Customers SET AccountBalance = @AccountBalance WHERE Pin = @Pin";

                myCommand = new SqlCommand(updateCustomer, connection);

                myCommand.Parameters.AddRange(new SqlParameter[]
                {
                    new SqlParameter
                    {
                        ParameterName = "@AccountBalance",
                        Value = getUpdatedBalance,
                        SqlDbType = SqlDbType.BigInt,
                        Direction = ParameterDirection.Input
                    },

                    new SqlParameter
                    {
                        ParameterName = "@Pin",
                        Value = pin,
                        SqlDbType = SqlDbType.SmallInt,
                        Direction = ParameterDirection.Input
                    }
                });

                myCommand.ExecuteNonQuery();

                Console.WriteLine("Deposit successful");
            }
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public void Transfer(int pin)
        {
            throw new NotImplementedException();
        }

        public void ViewAccountDetails(int pin)
        {
            throw new NotImplementedException();
        }

        public void Withdraw(int pin, decimal amount)
        {
            throw new NotImplementedException();
        }
    }
}
