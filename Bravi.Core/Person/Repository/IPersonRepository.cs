using Bravi.Domain.Person.Model;

namespace Bravi.Domain.Person.Repository
{
    public interface IPersonRepository
    {
        public Task<(IEnumerable<PersonModel> Rows, long TotalCount)> FindAllAsync(int pageIndex, int pageSize, string query);
        public Task<Model.PersonModel> FindByIdAsync(Guid id);
        public Task<Guid> InsertAsync(PersonModel model);
        public Task<Guid> AddContactAsync(Guid id, Contact contact);
        public Task<bool> UpdateAsync(Model.PersonModel model);
        public Task<bool> RemovePersonAsync(Guid id);
        public Task<bool> RemoveAllPersonContactsAsync(Guid personId);
        public Task<Contact?> FindContactByIdAsync(Guid Id);
        public Task<bool> DeleteContactAsync(Guid id);


    }
}
