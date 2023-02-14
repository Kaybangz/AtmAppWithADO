using AtmApp.Atm.BLL.Implementation;
using AtmApp.Atm.BLL.Interface;
using AtmApp.Atm.Data.DataStore;
using AtmApp.BLL.Implementation;
using AtmApp.UI;

namespace AtmClient
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            IAtmService atmService = new AtmService();

            atmService.Deposit(2345);

            //Console.WriteLine(result);
        }
    }
}