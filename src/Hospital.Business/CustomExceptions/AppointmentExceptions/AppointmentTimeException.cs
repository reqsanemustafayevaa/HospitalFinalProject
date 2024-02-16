using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Business.CustomExceptions.AppointmentExceptions
{
    public class AppointmentTimeException:Exception
    {
        public string PropertyName { get; set; }
        public AppointmentTimeException()
        {
        }

        public AppointmentTimeException(string? message) : base(message)
        {
        }
        public AppointmentTimeException(string propertyname, string? message) : base(message)
        {
            PropertyName = propertyname;
        }
    }
}
