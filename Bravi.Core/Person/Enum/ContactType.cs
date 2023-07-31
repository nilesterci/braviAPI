using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bravi.Domain.Person.Enum
{
    public enum ContactType: short
    {
        [Description("WhatsApp")]
        [NpgsqlTypes.PgName("WhatsApp")]
        WhatApp = 0,
        [Description("Telephone")]
        Telephone = 1,
        [Description("CellPhone")]
        CellPhone = 2,
        [Description("Email")]
        Email = 3
    }
}
