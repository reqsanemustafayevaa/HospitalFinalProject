﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Business.CustomExceptions.CommonExceptions
{
    public class EntityNotFoundException:Exception
    {
        public string PropertyName { get; set; }
        public EntityNotFoundException()
        {
        }

        public EntityNotFoundException(string? message) : base(message)
        {
        }
        public EntityNotFoundException(string propertyname, string? message) : base(message)
        {
            PropertyName = propertyname;
        }
    }
}
