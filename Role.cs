using System;
using System.Collections.Generic;
using System.Text;

namespace Users
{
    internal class Role
    {
        string? Id;
        string? Name;

        public Role(string _Id, string _Name)
        {
            Id = _Id;
            Name = _Name;
        }
        public override string ToString()
        {
            return Name;
        }
    }
}
