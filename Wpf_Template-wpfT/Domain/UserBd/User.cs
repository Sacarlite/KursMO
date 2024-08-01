
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.UserBd
{
    public class User
    {
        public User(string login, string password)
        {
            Login = login;
            Password = password;
        }

        public int Id { get; set; }
        [Required, Display(Name = "Имя"), Column(TypeName = "varchar(50)")]
        public string Login { get; set; }
        [Required, Display(Name = "Пароль"), Column(TypeName = "varchar(50)")]
        public string Password { get; set; }
        public Role Role { get; set; }
    }
}
