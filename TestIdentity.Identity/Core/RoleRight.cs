using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestIdentity.Identity.Core
{
    public enum RoleRight : byte
    {
        Administrator = 255,
        Manager = 127,
        User = 63,
        Customer = 31,
        Unassigned = 0,
    }
}
