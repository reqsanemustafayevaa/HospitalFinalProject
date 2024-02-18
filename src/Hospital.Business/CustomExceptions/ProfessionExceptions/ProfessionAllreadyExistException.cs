using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Business.CustomExceptions.ProfessionExceptions
{
    public class ProfessionAllreadyExistException:Exception
    {
        public string PropertyName { get; set; }
        public ProfessionAllreadyExistException()
        {
        }

        public ProfessionAllreadyExistException(string? message) : base(message)
        {
        }
        public ProfessionAllreadyExistException(string propertyname, string? message) : base(message)
        {
            PropertyName = propertyname;
        }
    }
}
