using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Business.CustomExceptions.AuthExceptions
{
    public class InvalidCredsException:Exception
    {
        public string PropertyName { get; set; }
        public InvalidCredsException()
        {

        }
        public InvalidCredsException(string? message) : base(message)
        {

        }
        public InvalidCredsException(string propertyname, string? message) : base(message)
        {
            PropertyName = propertyname;

        }
    }
}
