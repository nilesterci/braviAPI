namespace Bravi.Domain.Person.Model
{
    public class PersonModel
    {
        public PersonModel(string name, DateTime birthday)
        {
            Name = name;
            Birthday = birthday;
            _contacts = new List<Contact>();
        }

        public PersonModel(Guid id, string name, DateTime birthday, DateTime createdAt): this(name, birthday)
        {
            Id = id;
            Birthday = birthday;
            CreatedAt = createdAt;
        }

        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public DateTime Birthday { get; private set; } 
        public DateTime CreatedAt { get; private set; }
        private List<Contact> _contacts;
        public IReadOnlyList<Contact> Contacts { get => _contacts; }

        public void AddContact(Contact contact)
        {
            _contacts.Add(contact);
        }

        public void AddContact(IEnumerable<Contact> contacts)
        {
            _contacts.AddRange(contacts);
        }

        public void UpdateName(string name)
        {
            Name = name;
        }

        public void UpdateBirthday(DateTime birthday)
        {
            Birthday = birthday;
        }
    }
}