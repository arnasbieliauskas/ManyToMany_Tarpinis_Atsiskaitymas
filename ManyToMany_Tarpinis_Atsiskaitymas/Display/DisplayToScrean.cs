using ManyToMany_Tarpinis_Atsiskaitymas.InputToDB;
using ManyToMany_Tarpinis_Atsiskaitymas.InputValidatio;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.Design;
using ManyToMany_Tarpinis_Atsiskaitymas.DataBase;

namespace ManyToMany_Tarpinis_Atsiskaitymas.Display
{
    public class DisplayToScrean
    {
        public static void DisplayLessonByDepartment()
        {
            Console.WriteLine("Kurio departamento paskaitas norite pamatyti");
            var departmentId = Console.ReadLine();

            if (InputValidation.ValidateStringNull(departmentId)
                && InputValidation.ValidateDepartmentID(departmentId))
            {
                var dbContext = new DbContextContext();
                Department department = dbContext.Departments
                    .Include(a => a.Lessons)
                   .FirstOrDefault(b => b.DepartmentId == departmentId);

                if (InputValidation.CheckIsDepartmentExist(departmentId))
                {
                    foreach (var lesson in department.Lessons)
                    {
                        Console.WriteLine(string.Join(Environment.NewLine,
                            $"Department Id: {departmentId}",
                            $"Lesson Id: {lesson.LessonId}",
                            $"Lesson Name: {lesson.LessonName}",
                            $"Lesson Data and time: {lesson.LessonDateAndTime}"));
                        Console.WriteLine("---------------------------------------------------------------------------------------------");
                    }
                    
                }
                else
                {
                    Console.WriteLine($"Departamentas {departmentId} nerastas");
                }
            }
            else
            {
                Console.WriteLine("Iveskite departamento numeri");
            }
        } //atspausdina pagal departamenta susijusias lesson
        public static void DisplayDepartmentInConsole()
        {
            var dbContext = new DbContextContext();
            List<Department> allDepartments = dbContext.Departments.ToList();

            foreach (var department in allDepartments)
            {
                Console.WriteLine($"DepartmentId: {department.DepartmentId}," +
                                $" DepartmentName: {department.DepartmentName}");
                Console.WriteLine("---------------------------------------------------------------------------------------------");
            }
            Console.ReadLine();
        }//atspausdina visa departamenta i Console
        public static void DisplayLessonsByStudent()
        {
            Console.WriteLine("Kurio studento paskaitas norite pamatyti");
            var inputStudentId = Console.ReadLine();

            if (inputStudentId != null
                && inputStudentId.Length == 8)
            {
                if (int.TryParse(inputStudentId, out int id))
                {
                    var dbContext = new DbContextContext();
                    Student student = dbContext.Students
                        .Include(x => x.Lessons)
                        .FirstOrDefault(b => b.StudentId == id);

                    if (InputValidation.CheckIsStudentExistTrue(id))
                    {
                        foreach (var lesson in student.Lessons)
                        {
                            Console.WriteLine(string.Join(Environment.NewLine,
                                $"Studento Id: {id}",
                                $"Lesson Id: {lesson.LessonId}",
                                $"Lesson Name: {lesson.LessonName}",
                                $"Lesson Data and time: {lesson.LessonDateAndTime}"));
                            Console.WriteLine("---------------------------------------------------------------------------------------------");
                        }                       
                    }

                }
                else
                {
                    Console.WriteLine("Iveskite tinkama studento ID");
                }
            }
        } //atspausdina pagal departamenta susijusias lesson
        public static void DisplayStudentByDepartment()
        {
            Console.WriteLine("Kurio departamento studentus norite pamatyti");
            var departmentId = Console.ReadLine();

            if (InputValidation.ValidateStringNull(departmentId)
                && InputValidation.ValidateDepartmentID(departmentId))
            {
                var dbContext = new DbContextContext();
                Department department = dbContext.Departments
                    .Include(a => a.Students)
                    .FirstOrDefault(b => b.DepartmentId == departmentId);

                if (InputValidation.CheckIsDepartmentExist(departmentId))
                {
                    foreach (var student in department.Students)
                    {
                        Console.WriteLine(string.Join(Environment.NewLine,
                                    $"Department Id: {departmentId}",
                                    $"Student Id: {student.StudentId}",
                                    $"Student Name: {student.StudentName}",
                                    $"Student SurName: {student.StudentSurName}"));
                        Console.WriteLine("---------------------------------------------------------------------------------------------");

                    }

                }
                
            }

        }//atspausdina studentus pagal departamenta
    }
}

