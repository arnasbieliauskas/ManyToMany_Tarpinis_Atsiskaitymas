using Microsoft.EntityFrameworkCore;
using ManyToMany_Tarpinis_Atsiskaitymas.DataBase;

namespace ManyToMany_Tarpinis_Atsiskaitymas.DataBase
{
    public class DbContextContext : DbContext
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
