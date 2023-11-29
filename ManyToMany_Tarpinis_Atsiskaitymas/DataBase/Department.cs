using Microsoft.EntityFrameworkCore.Migrations.Operations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ManyToMany_Tarpinis_Atsiskaitymas.DataBase;

namespace ManyToMany_Tarpinis_Atsiskaitymas.DataBase
{
    public class Department
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string DepartmentId { get; set; }       //tiksliai 6 simboliai ir raides ir skaiciai
        public string DepartmentName { get; set; } //pavadinimas 3-100 simboliu galibuti ir skaiciai

        public List<Lesson> Lessons { get; set; }   //turi daug paskaitu       
       public List<Student> Students { get; set; } //turi daug studentu
        //public List<DepartmentStudent> DepartmentStudent { get; set; }


        //public List<DepartmentStudent> DepartmentStudent { get; set; }

    }
}
