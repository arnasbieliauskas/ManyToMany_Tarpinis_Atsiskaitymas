using Microsoft.EntityFrameworkCore;

namespace ManyToMany_Tarpinis_Atsiskaitymas
{
    public class   DbContextContext : DbContext
    {
 public DbContextContext()
        {
            
        }

        public DbContextContext(DbContextOptions options) : base(options)
        {
        }     


        public DbSet<Student> Students { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<DepartmentStudent> DepartmentStudent { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // cia konfiguruojame sarysy many to many


            modelBuilder.Entity<DepartmentStudent>().HasKey(bc => new { bc.DepartmentId, bc.StudentId });// ka naudojame i sujungima many to many

            modelBuilder.Entity<DepartmentStudent>() //join kokiu budu per koki rakta susijungia
                .HasOne(bc => bc.Students)
                .WithMany(c => c.DepartmentStudent)
                .HasForeignKey(bc => bc.StudentId);

            modelBuilder.Entity<DepartmentStudent>()
               .HasOne(bc => bc.Departments)
               .WithMany(c => c.DepartmentStudent)
               .HasForeignKey(bc => bc.DepartmentId);

            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) 
        {
            if (!optionsBuilder.IsConfigured)//patikrina ar is Program.cs yra panaudotas kelias
            {
                optionsBuilder.UseSqlServer($"Server=(localdb)\\mssqllocaldb;Database=University_Students_Schedual;Trusted_Connection=True;MultipleActiveResultSets=true")
                    .LogTo(s => System.Diagnostics.Debug.WriteLine(s));
            }
        }
    }
}
