using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORM_MINI_PROJECT.Excaption
{
    public class UserAuthenticationException : Exception
    {
        public UserAuthenticationException(string message) : base(message)
        {
        }
    }
}
