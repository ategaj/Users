using System;
using System.Collections.Generic;
using System.Text;

namespace Users
{
    internal class User
    {
        public int Id;
        public string? Name;
        public string? Email;
        public string? Password;
        public string? Phone;
        public string? Date;
        public Role? Role;

        public User(int _Id, string _Name, string _Email, string _Password, string _Phone, string _Date, Role _Role)
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
            return $"Id:{Id} role Id:{Role?.Id} | {Name} | Email {Email}";
        }
    }
}
