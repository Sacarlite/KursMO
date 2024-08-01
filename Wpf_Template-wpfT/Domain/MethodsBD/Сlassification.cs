using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.MethodsBD
{
    public class Сlassification
    {
        public Сlassification(string? name)
        {
            Name = name;
        }

        public int Id { get; set; }
        [Required, Display(Name = "Класс"), Column(TypeName = "varchar(25)")]
        public string? Name { get; set; }
        public override string ToString()
        {
            return Name == null ? "" : Name;
        }
    }
}
