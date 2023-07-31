using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bravi.Application.Person.Enums
{
    public enum ContactTypeOutput: short
    {
        [Description("WhatsApp")]
        WhatApp = 0,
        [Description("Telefone")]
        Telephone = 1,
        [Description("Celular")]
        CellPhone = 2,
        [Description("Email")]
        Email = 3
    }
}
