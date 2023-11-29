using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ManyToMany_Tarpinis_Atsiskaitymas.DataBase;

namespace ManyToMany_Tarpinis_Atsiskaitymas.DataBase
{
    public class Lesson
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int LessonId { get; set; }
        public string? LessonName { get; set; }
        public string? LessonDateAndTime { get; set; }

        

        public List<Student> Students { get; set; }       //daug paskaitu
        public List<Department> Departments { get; set; } //daug departamentu

    }
}
