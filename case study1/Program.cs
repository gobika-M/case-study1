using case_study1.Service;
using case_study1.utility;

namespace case_study1
{
    internal class Program
    {
        static void Main(string[] args)
        {

            FinanceService financeService = new FinanceService();


            financeService.DisplayMenu();

        }
    }
}
