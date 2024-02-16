using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Business.CustomExceptions.CommonExceptions
{
    public class AllreadyExistException:Exception
    {
        public string PropertyName { get; set; }
        public AllreadyExistException()
        {
        }

        public AllreadyExistException(string? message) : base(message)
        {
        }
        public AllreadyExistException(string propertyname, string? message) : base(message)
        {
            PropertyName = propertyname;
        }
    }
}
