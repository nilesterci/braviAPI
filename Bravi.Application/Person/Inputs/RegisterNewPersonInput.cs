using Bravi.Domain.Person.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bravi.Application.Person.Inputs
{
    public class RegisterNewPersonInput
    {
        public RegisterNewPersonInput(string name, DateTime birthday, IEnumerable<RegisterNewContactInput> contacts)
        {
            Name = name;
            Birthday = birthday;
            Contacts = contacts;
        }

        public string Name { get; set; }
        public DateTime Birthday { get; set; }
        public IEnumerable<RegisterNewContactInput> Contacts { get; set; }
    }
}
