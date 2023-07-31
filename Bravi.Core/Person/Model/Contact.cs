using Bravi.Domain.Core.Utils;
using Bravi.Domain.Person.Enum;


namespace Bravi.Domain.Person.Model
{
    public class Contact
    {
        public Contact(Guid id, string contactType, string value, DateTime createdAt)
        {
            Id = id;
            CreatedAt = createdAt;
            Value  = value;
            ContactType = ContactType.GetValueFromDescription(contactType);
        }

        public Contact(ContactType contactType, string value)
        {
            ContactType = contactType;
            Value = value;
        }

        public Guid Id { get; private set; }
        public ContactType ContactType { get; private set; }
        public string Value { get; private set; }
        public DateTime CreatedAt { get; private set; }
    }
}
