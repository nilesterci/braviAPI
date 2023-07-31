using Bravi.Domain.Person.Enum;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bravi.Domain.Person.Command
{
    public class CreateContactCommand: IRequest<Guid?>
    {
        public CreateContactCommand(string value, ContactType contactType, Guid personId)
        {
            Value = value;
            ContactType = contactType;
            PersonId = personId;
        }

        public CreateContactCommand(string value, ContactType contactType)
        {
            Value = value;
            ContactType = contactType;
        }

     

        public string Value { get; set; }
        public ContactType  ContactType { get; set; }
        public Guid PersonId { get; private set; }

        public void UpdatePerson(Guid personId)
        {
            PersonId = personId;
        }

    }
}
