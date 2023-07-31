using Bravi.Domain.Core.Repository;
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
    public class DeletePersonCommandHandler : IRequestHandler<DeletePersonCommand, bool>
    {
        private readonly IPersonRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public DeletePersonCommandHandler(IPersonRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(DeletePersonCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _unitOfWork.BeginTransaction();
                var result = await _repository.FindByIdAsync(request.Id);
                if (result == null)
                {
                    return false;
                }

                await _repository.RemoveAllPersonContactsAsync(request.Id);
                await _repository.RemovePersonAsync(request.Id);
                
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
