namespace ManyToMany_Tarpinis_Atsiskaitymas
{
    public class DepartmentStudent //jungiamoji lentele
    {
        public string DepartmentId { get; set; }
        public int StudentId { get; set; }
      

        public Department Departments { get; set; }
        public Student Students { get; set; }
       
    }
}
