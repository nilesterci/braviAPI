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
    internal class DeleteContactCommandHandler : IRequestHandler<DeleteContactCommand, bool>
    {
        private readonly IPersonRepository _repository;

        public DeleteContactCommandHandler(IPersonRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(DeleteContactCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var contact = await _repository.FindContactByIdAsync(request.ContactId);

                if (contact == null)
                {
                    return false;
                }

                await _repository.DeleteContactAsync(request.ContactId);
                return true;
            }
            catch (Exception)
            {

                throw;
            }
            
          
        }  
    }
}
