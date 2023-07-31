using Bravi.Application.Base.Models;
using Bravi.Application.Person.Inputs;
using Bravi.Application.Person.Outputs;

namespace Bravi.Application.Person.Interfaces
{
    public interface IPersonService
    {
        public Task<BaseList<GetPersonList>> FindAll(int pageIndex, int pageSize, string query);
        public Task<GetPersonDetail?> FindById(Guid id);
        public Task<Guid?> Add(RegisterNewPersonInput person);
        public Task<bool> Update(UpdateNewPerson person);
        public Task<bool> Delete(Guid id);
        public Task<Guid?> AddContract(RegisterNewContactInput contact, Guid personId);
        public Task<bool> DeleteContact(Guid contractId);
    }
}
