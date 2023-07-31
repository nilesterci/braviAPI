using Bravi.Application.Base.Models;
using Bravi.Application.Person.Enums;
using Bravi.Application.Person.Inputs;
using Bravi.Application.Person.Interfaces;
using Bravi.Application.Person.Outputs;
using Bravi.Domain.Person.Command;
using Bravi.Domain.Person.Enum;
using Bravi.Domain.Person.Model;
using Bravi.Domain.Person.Repository;
using MediatR;

namespace Bravi.Application.Person
{
    public class PersonService : IPersonService
    {
        private IPersonRepository _repository;
        private IMediator _mediator;

        public PersonService(IPersonRepository repository, IMediator mediator)
        {
            _repository = repository;
            _mediator = mediator;
        }

        public async Task<Guid?> Add(RegisterNewPersonInput person)
        {
            var contacts = new List<CreateContactCommand>();
            foreach (var contact in person.Contacts)
            {
                contacts.Add(new CreateContactCommand(contact.Value, (ContactType)contact.ContactType));
            }

            var command = new CreatePersonCommand(person.Name, person.Birthday, contacts);
            var personId = await _mediator.Send(command);
            return personId;
        }

        public async Task<bool> Delete(Guid id)
        {
            var command = new DeletePersonCommand(id);
            var result = await _mediator.Send(command);
            return result;
        }

        public async Task<BaseList<GetPersonList>> FindAll(int pageIndex, int pageSize, string query)
        {
            var persons = await _repository.FindAllAsync(pageIndex, pageSize, query);
            var result = new List<GetPersonList>();
            foreach (var row in persons.Rows)
            {
                result.Add(new GetPersonList
                {
                    Aniversario = row.Birthday,
                    CriadoEm = row.CreatedAt,
                    Nome = row.Name,
                    Id = row.Id
                });
            }

            var mappedResult = new BaseList<GetPersonList>(persons.TotalCount, pageIndex, pageSize, result);
            return mappedResult;
        }

        public async Task<GetPersonDetail?> FindById(Guid id)
        {
            PersonModel person = await _repository.FindByIdAsync(id);
            var contacts = new List<GetContactDetail>();
            foreach (var item in person.Contacts)
            {
                contacts.Add(new GetContactDetail(item.Value, (ContactTypeOutput)item.ContactType, item.CreatedAt, item.Id));
            }

            var result = new GetPersonDetail
            {
                Aniversario = person.Birthday,
                Nome = person.Name,
                Contatos = contacts,
                CriadoEm = person.CreatedAt
            };

            return result;

        }

        public Task<bool> Update(UpdateNewPerson person)
        {
            var command = new UpdatePersonCommand(person.Nome, person.Aniversario, person.Id);
            var result = _mediator.Send(command);
            return result;
        }

        public async Task<Guid?> AddContract(RegisterNewContactInput contact, Guid personId)
        {
            var command = new CreateContactCommand(contact.Value, (ContactType)contact.ContactType, personId);
            var result = await _mediator.Send(command);
            return result;
        }

        public async Task<bool> DeleteContact(Guid contractId)
        {
            var command = new DeleteContactCommand(contractId);
            var result = await _mediator.Send(command);
            return result;

        }
    }
}
