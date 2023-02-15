using AtmApp.Atm.BLL.Interface;
using AtmApp.Atm.Data.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Data;
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


                //Move to presentation
                if (depositAmount <= 0 || depositAmount.GetType() is String)
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

                string transactionQuery = @"INSERT INTO CustomersTransactions (TransactionType, TransactionTime) VALUES ()";

                //string insertIntoDepositTransaction = @"INSERT INTO DepositTransaction (customer_Id, Firstname, Lastname, AccountNumber, AccountType, TransactionType, TransactionTime) ";

                //                string transactionQuery = @"SELECT 
                //                Customers.customer_Id, 
                //                Customers.Firstname, 
                //                Customers.Lastname, 
                //                Customers.AccountNumber, 
                //                Customers.AccountType,

                //INTO
                //CustomersTransactions
                //FROM
                //Customers
                //LEFT JOIN Customers ON CustomersTransactions.customer_Id = Customers.customer_Id";
            }
        }


        public void Withdraw(int pin, decimal amount)
        {
            throw new NotImplementedException();
        }


        public void Transfer(int pin)
        {
            throw new NotImplementedException();
        }

        public CustomerModel ViewAccountDetails(int pin)
        {
            using (SqlConnection connection = new SqlConnection())
            {
                connection.ConnectionString = _connectionString;
                connection.Open();

                string getAccountDetails = @"SELECT Firstname,Lastname,Phonenumber,Gender,Bank,AccountNumber,AccountType,AccountBalance FROM Customers WHERE Pin = @Pin";

                SqlCommand myCommand = new SqlCommand(getAccountDetails, connection);

                myCommand.Parameters.Add("@Pin", SqlDbType.SmallInt).Value = pin;

                CustomerModel customer = new CustomerModel();

                using (SqlDataReader dataReader = myCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        customer.FirstName = dataReader["Firstname"].ToString();
                        customer.LastName = dataReader["Lastname"].ToString();
                        customer.PhoneNumber = dataReader["Phonenumber"].ToString();
                        customer.Gender = dataReader["Gender"].ToString();
                        customer.Bank = dataReader["Bank"].ToString();
                        customer.AccountNumber = dataReader["AccountNumber"].ToString();
                        customer.AccountType = dataReader["AccountType"].ToString();
                        customer.AccountBalance = (long)dataReader["AccountBalance"];
                    }
                }

                return customer;
            }
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
