using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.UserBd;

namespace Domain.MethodsBD
{
   
    public class Method
    {
        public Method(string name,string path, string description )
        {
            Name = name;
            Path = path;
            Description = description;
        }
        public int Id { get; set; }
        [Required, Display(Name = "Название метода"), Column(TypeName = "varchar(25)")]
        public string Name { get; set; }
        [Required, Display(Name = "Путь"), Column(TypeName = "varchar(25)")]
        public string Path { get; set; }
        public string Description { get; set; }
        public Сlassification Classification { get; set; }
    }
}
