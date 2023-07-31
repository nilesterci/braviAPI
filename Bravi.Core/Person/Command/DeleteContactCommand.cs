using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bravi.Domain.Person.Command
{
    public class DeleteContactCommand: IRequest<bool>
    {
        public DeleteContactCommand(Guid contactId)
        {
            ContactId = contactId;
        }

        public Guid ContactId { get; private set; }
    }
}
