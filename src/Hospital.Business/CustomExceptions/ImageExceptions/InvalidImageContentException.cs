using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Business.CustomExceptions.ImageExceptions
{
    public class InvalidImageContentException:Exception
    {
        public string PropertyName { get; set; }
        public InvalidImageContentException()
        {
        }

        public InvalidImageContentException(string? message) : base(message)
        {
        }
        public InvalidImageContentException(string propertyname, string? message) : base(message)
        {
            PropertyName = propertyname;
        }
    }
}
