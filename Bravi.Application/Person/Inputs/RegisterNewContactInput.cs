using Bravi.Application.Person.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bravi.Application.Person.Inputs
{
   
    public class RegisterNewContactInput
    {
        public RegisterNewContactInput(string value, ContactTypeOutput contactType)
        {
            Value = value;
            ContactType = contactType;
        }

        public string Value { get; set; }
        public ContactTypeOutput ContactType { get; set; }
    }
}
