using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace case_study1.Exceptions
{
    internal class UserNotFoundException: Exception
    {
        public UserNotFoundException(string message) : base(message)
        {

        }
    }
}
