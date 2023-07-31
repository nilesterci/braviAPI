using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bravi.Domain.Person.Command
{

    public class CreatePersonCommand : IRequest<Guid?>
    {
        public CreatePersonCommand(string name, DateTime birthday, List<CreateContactCommand> contacts)
        {
            Name = name;
            Birthday = birthday;
            Contacts = contacts;
        }

        public string Name { get; private set; }
        public DateTime Birthday { get; private set; }
        public IReadOnlyList<CreateContactCommand> Contacts { get; private set; }
    }
}
