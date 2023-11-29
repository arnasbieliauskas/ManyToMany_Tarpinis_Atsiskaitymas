using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ManyToMany_Tarpinis_Atsiskaitymas.DataBase;

namespace ManyToMany_Tarpinis_Atsiskaitymas.DataBase
{
    public class Student
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)] //leidzia patiems sugeneruoti PK
        public int StudentId { get; set; } //skaiciai tiksliai 8 simboliai
        public string? StudentName { get; set; } //turi buti nuo 2 iki 50 simboliu
        public string? StudentSurName { get; set; }

        public string? StudentMail { get; set; } //mail turi buti teisingo formato Regex panaudoti

        //public Department Department { get; set; }
        //public Lesson Lesson { get; set; }
      public List<Department> Departments { get; set; } 
        public List<Lesson> Lessons { get; set; }   //daug paskaitu
       // public List<DepartmentStudent> DepartmentStudent { get; set; }
    }
}
