using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Business.CustomExceptions.RegisterExceptions
{
    public class InvalidRegisterOperation:Exception
    {
        public string PropertyName { get; set; }
        public InvalidRegisterOperation()
        {
        }

        public InvalidRegisterOperation(string? message) : base(message)
        {
        }
        public InvalidRegisterOperation(string propertyname, string? message) : base(message)
        {
            PropertyName = propertyname;
        }
    }
}
