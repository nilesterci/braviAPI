using Bravi.Domain.Person.Command;
using Bravi.Domain.Person.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bravi.Domain.Person.Handler
{
    public class UpdatePersonCommandHandler : IRequestHandler<UpdatePersonCommand, bool>
    {
        private readonly IPersonRepository _repository;
        public UpdatePersonCommandHandler(IPersonRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(UpdatePersonCommand request, CancellationToken cancellationToken)
        {
            var person = await _repository.FindByIdAsync(request.Id);

            if (person == null)
            {
                return false;
            }

            person.UpdateName(request.Name);
            person.UpdateBirthday(request.DataAniversario);

            await _repository.UpdateAsync(person);
            return true;

        }
    }
}
