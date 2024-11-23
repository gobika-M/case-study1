using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace case_study1.Exceptions
{
    internal class ExpenseNotFoundException: Exception
    {
        public ExpenseNotFoundException(string message) : base(message)
        {
        }
    }
}
