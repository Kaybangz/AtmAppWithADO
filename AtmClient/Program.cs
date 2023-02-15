using AtmApp.UI;

namespace AtmClient
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Presentation.RunViewAccountDetails(2345);
        }
    }
}