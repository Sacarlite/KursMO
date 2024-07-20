
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.UserBd
{
    public class User
    {
        public User(string login, string password, Role role)
        {
            Login = login;
            Password = password;
            Role = role;
        }

        public int Id { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }
        public Role Role { get; set; }
    }
}
