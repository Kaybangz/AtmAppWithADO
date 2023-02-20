using AtmApp.Atm.BLL.Interface;
using AtmApp.Atm.Data.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Data;


namespace AtmApp.Atm.BLL.Implementation
{
    public class AtmService : IAtmService
    {
        private readonly string _connectionString = @"Data Source=KAYBANGZ;Initial Catalog=AtmAppDatabase;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";



        public long CreateNewAccount(CustomerModel customer)
        {
            using (SqlConnection connection = new SqlConnection())
            {
                connection.ConnectionString = _connectionString;
                connection.Open();

                string createCustomerQuery = @"INSERT INTO Customers (Pin, Firstname, Lastname, Phonenumber, Gender, Bank, AccountNumber, AccountType, AccountBalance) VALUES (@pin, @Firstname, @Lastname, @Phonenumber, @Gender, @Bank, @AccountNumber, @AccountType, @AccountBalance)";

                SqlCommand myCommand = new SqlCommand(createCustomerQuery, connection);

                myCommand.Parameters.AddRange(new SqlParameter[]
                {
                    new SqlParameter
                    {
                        ParameterName = "@Pin",
                        Value = customer.Pin,
                        SqlDbType = SqlDbType.SmallInt,
                        Direction = ParameterDirection.Input
                    },

                    new SqlParameter
                    {
                        ParameterName = "@Firstname",
                        Value = customer.FirstName,
                        SqlDbType = SqlDbType.NVarChar,
                        Direction = ParameterDirection.Input,
                        Size = 20
                    },

                    new SqlParameter
                    {
                        ParameterName = "@Lastname",
                        Value = customer.LastName,
                        SqlDbType = SqlDbType.NVarChar,
                        Direction = ParameterDirection.Input,
                        Size = 20
                    },

                    new SqlParameter
                    {
                        ParameterName = "@Phonenumber",
                        Value = customer.PhoneNumber,
                        SqlDbType = SqlDbType.NVarChar,
                        Direction = ParameterDirection.Input,
                        Size = 20
                    },

                    new SqlParameter
                    {
                        ParameterName = "@Gender",
                        Value = customer.Gender,
                        SqlDbType = SqlDbType.NVarChar,
                        Direction = ParameterDirection.Input,
                        Size = 10
                    },

                    new SqlParameter
                    {
                        ParameterName = "@Bank",
                        Value = customer.Bank,
                        SqlDbType = SqlDbType.NVarChar,
                        Direction = ParameterDirection.Input,
                        Size = 30
                    },

                    new SqlParameter
                    {
                        ParameterName = "@AccountNumber",
                        Value = customer.AccountNumber,
                        SqlDbType = SqlDbType.NVarChar,
                        Direction = ParameterDirection.Input,
                        Size = 20
                    },

                    new SqlParameter
                    {
                        ParameterName = "@AccountType",
                        Value = customer.AccountType,
                        SqlDbType = SqlDbType.NVarChar,
                        Direction = ParameterDirection.Input,
                        Size = 20
                    },

                    new SqlParameter
                    {
                        ParameterName = "@AccountBalance",
                        Value = customer.AccountBalance,
                        SqlDbType = SqlDbType.BigInt,
                        Direction = ParameterDirection.Input,
                    },
                });

                long customerId = (long)myCommand.ExecuteScalar();

                return customerId;
            }
        }



        public void Withdraw(int pin, decimal amountToWithdraw)
        {
            using (SqlConnection connection = new SqlConnection())
            {
                connection.ConnectionString = _connectionString;
                connection.Open();

                string getAccountBalance = @"SELECT AccountBalance FROM Customers WHERE Pin = @Pin";

                SqlCommand myCommand = new SqlCommand(getAccountBalance, connection);

                myCommand.Parameters.Add("@Pin", SqlDbType.SmallInt).Value = pin;

                long accountBalance = (long)myCommand.ExecuteScalar();



                
                if (amountToWithdraw > accountBalance)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Insufficient balance...\n");
                    Console.ForegroundColor = ConsoleColor.White;

                    return;
                }

                decimal getUpdatedBalance = accountBalance - amountToWithdraw;

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

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Withdrawal successful\n");
                Console.ForegroundColor = ConsoleColor.White;



                //string updateWithdrawTable = @"INSERT INTO WithdrawTransactions (Firstname, Lastname, AccountNumber, AccountType) SELECT (Firstname, Lastname, AccountNumber, AccountType) FROM Customers WHERE Pin = @Pin; ";

                //myCommand = new SqlCommand(updateWithdrawTable, connection);

                //myCommand.Parameters.AddRange(new SqlParameter[]
                //{
                //    new SqlParameter
                //    {
                //        ParameterName = "@Pin",
                //        Value = pin,
                //        SqlDbType = SqlDbType.SmallInt,
                //        Direction = ParameterDirection.Input
                //    }
                //});
            }
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
