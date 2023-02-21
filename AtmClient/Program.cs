using AtmApp.UI;

namespace AtmClient
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
            ProgramStart:
                Console.WriteLine("*********************************************\n\tWelcome to Genesys Bank\n********************************************* \n\n");


                Console.WriteLine("-> Press 0 to exit\n-> Press 1 to create new account\n-> Press 2 to carry out transaction");
                int optionSelect = Convert.ToInt16(Console.ReadLine());

                switch (optionSelect)
                {
                    case 0:
                        Environment.Exit(0);
                        break;
                    case 1:
                        Presentation.RunCreateNewAccount();
                        break;
                    case 2:
                        try
                        {
                        TransactionStart:
                            Console.Write("Enter your four-digit pin: ");
                            int pin = Convert.ToInt16(Console.ReadLine());

                            Console.WriteLine("-> Press 1 to Withdraw\n-> Press 2 to transfer\n-> Press 3 to view account details\n-> Press 4 to exit");
                            int transactionOption = Convert.ToInt16(Console.ReadLine());

                            switch (transactionOption)
                            {
                                case 1:
                                    Presentation.RunWithdraw(pin);
                                    break;
                                case 2:
                                    break;
                                case 3:
                                    Presentation.RunViewAccountDetails(pin);
                                    break;
                                case 4:
                                    Environment.Exit(0);
                                    break;
                                default:
                                    Console.ForegroundColor = ConsoleColor.DarkRed;
                                    Console.WriteLine("Please select an option from the prompt\n");
                                    Console.ForegroundColor = ConsoleColor.White;
                                    goto TransactionStart;
                            }
                        }
                        catch (Exception ex)
                        {
                            if (ex is FormatException) Console.WriteLine("Incorrect input...\n");
                            else if (ex is NullReferenceException) Console.WriteLine("Input cannot be null...\n");
                            else Console.WriteLine("Something went wrong...\n");
                        }
                        break;
                    default:
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("Please select an option from the prompt\n");
                        Console.ForegroundColor = ConsoleColor.White;
                        goto ProgramStart;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nSomething went wrong...\n{ex.Message}");
            }
        }
    }
}
