using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public class NationalCodeAlreadyExistException : Exception
    {
        public NationalCodeAlreadyExistException(string errorMessage) : base(errorMessage)
        {

        }
    }
}
