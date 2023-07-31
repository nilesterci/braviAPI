using Bravi.Domain.Person.Command;
using Bravi.Domain.Person.Model;
using Bravi.Domain.Person.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bravi.Domain.Person.Handler
{
    public class CreateContactCommandHandler : IRequestHandler<CreateContactCommand, Guid?>
    {
        private readonly IPersonRepository _repository;
        public CreateContactCommandHandler(IPersonRepository repository)
        {
            _repository = repository;

        }

        public async Task<Guid?> Handle(CreateContactCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var id = await _repository.AddContactAsync(request.PersonId, new Contact(request.ContactType, request.Value));
                return id;
            }
            catch (Exception)
            {

                return null;
            }
        }
    }
}
