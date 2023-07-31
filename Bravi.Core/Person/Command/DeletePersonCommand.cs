using MediatR;

namespace Bravi.Domain.Person.Command
{
    public class DeletePersonCommand : IRequest<bool>
    {
        public DeletePersonCommand(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; private set; }
    }
}
