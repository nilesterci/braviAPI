using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bravi.Domain.Person.Command
{
    public class UpdatePersonCommand: IRequest<bool>
    {
        public UpdatePersonCommand(string name, DateTime dataAniversario, Guid id)
        {
            Name = name;
            DataAniversario = dataAniversario;
            Id = id;
        }

        public string Name { get; private set; }
        public DateTime DataAniversario { get; private set; }
        public Guid Id { get; private set; }
    }
}
