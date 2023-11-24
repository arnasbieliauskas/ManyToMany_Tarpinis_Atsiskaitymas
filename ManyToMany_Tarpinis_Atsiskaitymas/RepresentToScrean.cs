using Microsoft.EntityFrameworkCore;
using System.ComponentModel.Design;

namespace ManyToMany_Tarpinis_Atsiskaitymas
{
    public class RepresentToScrean
    {
        public static void RepresentLessonByDepartment()
        {
            Console.WriteLine("Kurio departamento paskaitas norite pamatyti");
            var departmentId = Console.ReadLine();

            if (departmentId != null
                && DepartmentToDB.CheckDepartmentID(departmentId))
            {
                var dbContext = new DbContextContext();
                Department department = dbContext.Departments
                   .FirstOrDefault(b => b.DepartmentId == departmentId);

                if (department != null)
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
        public static void RepresentDepartmentInConsole()
        {

            var dbContext = new DbContextContext();
            List<Department> allDepartments = dbContext.Departments.ToList();

            foreach (var department in allDepartments)
            {
                Console.WriteLine($"DepartmentId: {department.DepartmentId}," +
                                $" DepartmentName: {department.DepartmentName}");
                Console.WriteLine("---------------------------------------------------------------------------------------------");
            }
        }//atspausdina visa departamenta i Console
        public static void RepreentLessonsByStudent()
        {
            Console.WriteLine("Kurio studento paskaitas norite pamatyti");
            var inputStudentId = Console.ReadLine();

            if (inputStudentId != null
                && inputStudentId.Length == 8)
            {
                if (int.TryParse(inputStudentId, out int id))
                {
                    using var dbContext = new DbContextContext();
                    Student student = dbContext.Students
                    .FirstOrDefault(b => b.StudentId == id);

                    if (student != null)
                    {
                        foreach (var lesson in student.Lessons)
                        {
                            Console.WriteLine(string.Join(Environment.NewLine,
                                $"DepartmentId: {id}",
                                $"Lesson Id: {lesson.LessonId}",
                                $"Lesson Name: {lesson.LessonName}",
                                $"Lesson Data and time: {lesson.LessonDateAndTime}"));
                            Console.WriteLine("---------------------------------------------------------------------------------------------");
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Studentas {id} nerastas.");
                    }
                }
                else
                {
                    Console.WriteLine("Iveskite tinkama studento ID");
                }
            }
            else
            {
                Console.WriteLine("Iveskite studento ID");
            }
        } //atspausdina pagal departamenta susijusias lesson
        public static void RepresentStudentByDepartment()
        {
            Console.WriteLine("Kurio departamento studentus norite pamatyti");
            var departmentId = Console.ReadLine();

            if (departmentId != null
                && DepartmentToDB.CheckDepartmentID(departmentId))
            {
                var dbContext = new DbContextContext();
                Department department = dbContext.Departments
                    .FirstOrDefault(b => b.DepartmentId == departmentId);
                if (department != null)
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
                else
                {
                    Console.WriteLine($"Departamentas {departmentId} nerastas");
                }
            }
            else
            {
                Console.WriteLine("Iveskite departamento numeri");
            }

        }//atspausdina studentus pagal departamenta


    }
}

