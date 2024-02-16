using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Business.CustomExceptions.ProfessionExceptions
{
    public class ProfessionNotFoundException:Exception
    {
        public string PropertyName { get; set; }
        public ProfessionNotFoundException()
        {
        }

        public ProfessionNotFoundException(string? message) : base(message)
        {
        }
        public ProfessionNotFoundException(string propertyname, string? message) : base(message)
        {
            PropertyName = propertyname;
        }
    }
}
