
using Bravi.Domain.Core.Utils;
using Bravi.Domain.Person.Model;
using Bravi.Domain.Person.Repository;
using Bravi.Infrastructure.Session;
using Dapper;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System.ComponentModel;
using System.Text;

namespace Bravi.Infrastructure.Repository
{
    public class PersonRepository : BaseRepository, IPersonRepository
    {
        public PersonRepository(DbSession session) : base(session)
        {
        }

        private async Task<long> GetCountAsync(string query)
        {
            var parameters = new DynamicParameters();

            StringBuilder sqlQuery = new StringBuilder(@$"
                SELECT COUNT(1)
	                FROM person ");

            if (!string.IsNullOrWhiteSpace(query))
            {
                sqlQuery.AppendLine("WHERE name like '%@query%'");
                parameters.Add("@query", query);
            }

            var totalCount = await _session.Connection.QueryFirstAsync<long>(sqlQuery.ToString(), parameters);
            return totalCount;
        }

        private async Task<IEnumerable<PersonModel>> GetPersonsAsync(string query, int? pageIndex = null, int? pageSize = null)
        {
            var parameters = new DynamicParameters();
            var sqlQuery = new StringBuilder(@$"
                SELECT person_id {nameof(PersonModel.Id)}, 
                        name {nameof(PersonModel.Name)}, 
                        birthday {nameof(PersonModel.Birthday)},
                        created_at {nameof(PersonModel.CreatedAt)}
	                FROM person ");


            if (!string.IsNullOrWhiteSpace(query))
            {
                sqlQuery.AppendLine("WHERE name like '%@query%'");
                parameters.Add("@quey", query);
            }

            sqlQuery.AppendLine("ORDER BY name");

            if (pageIndex != null && pageSize != null)
            {
                sqlQuery.AppendLine(@"LIMIT @pageSize 
                                  OFFSET @offset ");

                parameters.Add("@pageSize", pageSize);
                parameters.Add("@offset", GetOffset((int)pageIndex, (int)pageSize));
            }

            var data = await _session.Connection.QueryAsync<PersonModel>(sqlQuery.ToString(), parameters);
            return data;
        }

        private async Task<PersonModel> getPersonByIdAsync(Guid id)
        {
            var sqlQuery = @$"
                SELECT person_id {nameof(PersonModel.Id)}, 
                        name {nameof(PersonModel.Name)}, 
                        birthday {nameof(PersonModel.Birthday)},
                        created_at {nameof(PersonModel.CreatedAt)}
	                FROM person 
                      WHERE person_id = @id";

            var person = await _session.Connection.QueryFirstAsync<PersonModel>(sqlQuery, new { id });
            return person;
        }

        private async Task<IEnumerable<Contact>> getContactByUserAsync(Guid id)
        {
            var sqlQuery = @$"SELECT contact_id {nameof(Contact.Id)},
                                     contact_type {nameof(Contact.ContactType)}, 
                                     value {nameof(Contact.Value)},
                                     created_at {nameof(Contact.CreatedAt)}
	                           FROM contact
	                          WHERE person_id = @id";

            var contacts = await _session.Connection.QueryAsync<Contact>(sqlQuery, new { id });
            return contacts;
        }

        private async Task<Contact?> getContactByIdAsync(Guid id)
        {
            var sqlQuery = @$"SELECT contact_id {nameof(Contact.Id)},
                                     contact_type {nameof(Contact.ContactType)}, 
                                     value {nameof(Contact.Value)},
                                     created_at {nameof(Contact.CreatedAt)}
	                           FROM contact
	                          WHERE contact_id = @id";

            var contacts = await _session.Connection.QueryFirstOrDefaultAsync<Contact>(sqlQuery, new { id });
            return contacts;
        }

        private async Task<Guid> InsertPersonAsync(PersonModel person)
        {
            var sqlQuery = $@" INSERT INTO person
                                 (name, birthday)
	                           VALUES (@name, @birthday)
	                           RETURNING person_id;";

            var result = await _session.Connection.ExecuteScalarAsync<Guid>(sqlQuery, new { name = person.Name, birthday = person.Birthday });
            return result;
        }

        private async Task<Guid> InsertContactAsync(Guid personId, Contact contact)
        {
            // Aparentemente a utilização de parametros utilizando tipos criados, que foi o caso do contact_type
            // não funciona utilizando enum no c# e enum postgres a solução encontrada foi fazer utilizando concatenação de strings
            // até encontrar uma interoperalidade entre os enums e mesmo utilizando dessa forma não é possível fazer um SQL Injection
            var typeDescription = contact.ContactType.GetEnumDescription();
            var sqlQuery = $@"INSERT INTO public.contact
                              (person_id, value, contact_type)
	                         VALUES (@personId, @value, '{typeDescription}')
	                         RETURNING contact_id;";

            var contactId = await _session.Connection.ExecuteScalarAsync<Guid>(sqlQuery,
                new
                {
                    personId,
                    value = contact.Value,
                });

            return contactId;
        }

        private async Task UpdatePersonAsync(PersonModel person)
        {
            var sqlQuery = $@"UPDATE public.person
	                            SET  name=@name, created_at=NOW(), birthday=@birthday
	                          WHERE person_id = @personId";

            await _session.Connection.ExecuteAsync(sqlQuery, new
            {
                name = person.Name,
                birthday = person.Birthday,
                personId = person.Id
            });
        }

        private async Task DeletePersonAsync(Guid id)
        {
            var sqlQuery = $@"DELETE FROM person
	                          WHERE person_id = @personId";

            await _session.Connection.ExecuteAsync(sqlQuery, new { personId = id });
        }

        private async Task DeleteAllPersonContactsAsync(Guid personId)
        {
            var sqlQuery = $@"DELETE FROM contact
	                          WHERE person_id = @personId";

            await _session.Connection.ExecuteAsync(sqlQuery, new { personId });
        }

        private async Task DeleteContactByIdAsync(Guid id)
        {
            var sqlQuery = $@"DELETE FROM contact
	                          WHERE contact_id = @id";

            await _session.Connection.ExecuteAsync(sqlQuery, new { id });
        }

        public async Task<(IEnumerable<PersonModel> Rows, long TotalCount)> FindAllAsync(int pageIndex, int pageSize, string query)
        {

            var persons = await GetPersonsAsync(query, pageIndex, pageSize);
            var totalCount = await GetCountAsync(query);
            return (persons, totalCount);

        }

        public async Task<PersonModel> FindByIdAsync(Guid id)
        {
            var person = await getPersonByIdAsync(id);
            var contacts = await getContactByUserAsync(id);
            person.AddContact(contacts);
            return person;
        }

        public async Task<Guid> InsertAsync(PersonModel model)
        {
            var id = await InsertPersonAsync(model);
            return id;
        }

        public async Task<bool> UpdateAsync(PersonModel model)
        {
            await UpdatePersonAsync(model);
            return true;
        }

        public async Task<bool> RemovePersonAsync(Guid id)
        {
            await DeletePersonAsync(id);
            return true;
        }

        public async Task<bool> RemoveAllPersonContactsAsync(Guid personId)
        {
            await DeleteAllPersonContactsAsync(personId);
            return true;
        }

        public async Task<Guid> AddContactAsync(Guid id, Contact contact)
        {
            var contactId = await InsertContactAsync(id, contact);
            return contactId;
        }

        public async Task<Contact?> FindContactByIdAsync(Guid Id)
        {
            var contact = await getContactByIdAsync(Id);
            return contact;
        } 
        
        public async Task<bool> DeleteContactAsync(Guid id)
        {
            await DeleteContactByIdAsync(id);
            return true;
        }
    }
}
