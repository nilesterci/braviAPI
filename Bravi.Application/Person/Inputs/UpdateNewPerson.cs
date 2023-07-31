using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bravi.Application.Person.Inputs
{
    public class UpdateNewPerson
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public DateTime Aniversario { get; set; }
    }
}
