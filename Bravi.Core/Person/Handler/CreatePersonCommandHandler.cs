using Bravi.Domain.Core.Repository;
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
    public class CreatePersonCommandHandler : IRequestHandler<CreatePersonCommand, Guid?>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPersonRepository _repository;
        private readonly IMediator _mediator;

        public CreatePersonCommandHandler(IUnitOfWork unitOfWork, IPersonRepository repository, IMediator mediator)
        {
            _unitOfWork = unitOfWork;
            _repository = repository;
            _mediator = mediator;
        }

        public async Task<Guid?> Handle(CreatePersonCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _unitOfWork.BeginTransaction();
                var personId = await _repository.InsertAsync(new PersonModel(request.Name, request.Birthday));

                foreach (var item in request.Contacts)
                {
                    item.UpdatePerson(personId);
                    await _mediator.Send(item);
                }

                _unitOfWork.Commit();
                return personId;
            }
            catch (Exception)
            {
                _unitOfWork.Rollback();
                return null;
                
            }
        }
    }
}
