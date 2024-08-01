using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.UserBd
{
 
    public class Role
    {
        public Role(string? name)
        {
            Name = name;
        }

        public int Id { get; set; }
        [Required, Display(Name = "Роль"), Column(TypeName = "varchar(25)")]
        public string? Name { get; set; }
        public override string ToString()
        {
            return Name==null?"":Name;
        }
    }
}
