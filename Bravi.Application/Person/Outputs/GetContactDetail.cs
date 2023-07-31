using Bravi.Application.Person.Enums;
using Bravi.Domain.Core.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bravi.Application.Person.Outputs
{
    public class GetContactDetail
    {
        public GetContactDetail(string valor, ContactTypeOutput type, DateTime criadoEm, Guid id)
        {
            Valor = valor;
            _type = type;
            CriadoEm = criadoEm;
            Id = id;
        }

        private ContactTypeOutput _type;
        public string Valor { get; set; }

        public DateTime CriadoEm { get; set; }
        public string Tipo => _type.GetEnumDescription();
        public Guid Id { get; set; }
    }
}
