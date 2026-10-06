using System;
using System.Collections.Generic;
using System.Text;

namespace Users
{
    internal class User
    {
        string? Id;
        string? Name;
        string? Email;
        string? Password;
        string? Phone;
        string? Date;
        string? Role;

        public User(string _Id, string _Name, string _Email, string _Password, string _Phone, string _Date, string _Role)
        {
            Id = _Id;
            Name = _Name;
            Email = _Email;
            Password = _Password;
            Phone = _Phone;
            Date = _Date;
            Role = _Role;
        }
        public override string ToString()
        {
            return $"{Name} — {Phone} ({Email})";
        }
    }
}
