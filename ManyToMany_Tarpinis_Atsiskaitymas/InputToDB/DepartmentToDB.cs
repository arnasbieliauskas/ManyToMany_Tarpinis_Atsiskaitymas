using Azure;
using ManyToMany_Tarpinis_Atsiskaitymas.InputValidatio;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ManyToMany_Tarpinis_Atsiskaitymas.DataBase;

namespace ManyToMany_Tarpinis_Atsiskaitymas.InputToDB
{
    public class DepartmentToDB
    {

        public static void AddDepartment() //Pridedami derpatamentai
        {
            while (true)
            {
                if (InputValidation.QuitFromTask()) { break; }
                Console.Clear();
                Console.WriteLine("Departamento pavadinimas");
                string departmentName = Console.ReadLine();

                Console.WriteLine("Departamento unikalus numeris (raides ir skaiciai 6 simboliai)");
                var departmentId = Console.ReadLine();

                if (InputValidation.ValidateStringNull(departmentName)
                    && InputValidation.ValidateStringNull(departmentId))
                {
                    if (InputValidation.ValidateDepartmentsName(departmentName)
                    && InputValidation.ValidateDepartmentID(departmentId))
                    {
                        var dbContext = new DbContextContext();
                        dbContext.Departments.Add(new Department
                        {
                            DepartmentName = departmentName,
                            DepartmentId = departmentId,
                        });
                        dbContext.SaveChanges();
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Iveskite teisingus duomenis");
                    }
                }
            }

        }
        public static void AddLessonToDepartment()
        {
            while (true)
            {
                if (InputValidation.QuitFromTask()) { break; }
                Console.Clear();

                Console.WriteLine("Kuria paskaita norite prideti (ID)");
                var lessonToMove = Console.ReadLine();

                if (InputValidation.ValidateStringNull(lessonToMove))  //tikrinama ar ne null, tinkamai ivesti skaiciai
                {
                    if (int.TryParse(lessonToMove, out int lesson))
                    {
                        Console.WriteLine("I kuri departamenta (ID) norite prideti paskaitas");
                        var toDepartmentAdd = Console.ReadLine();

                        if (InputValidation.ValidateStringNull(toDepartmentAdd)                    //tikrinama ar departamento ivestis atitinka formata
                           && InputValidation.ValidateDepartmentsName(toDepartmentAdd))
                        {
                            var dbContext = new DbContextContext();
                            var lessonMove = dbContext.Lessons
                                .Include(b => b.Departments)
                                .FirstOrDefault(a => a.LessonId == lesson);

                            var department = dbContext.Departments
                                .Include(a => a.Lessons)
                                 .FirstOrDefault(b => b.DepartmentId == toDepartmentAdd);

                            if (InputValidation.CheckIsLessonExist(lesson))                 //tikrinama ar ne null
                            {
                                lessonMove.Departments.Add(department);
                                dbContext.SaveChanges();
                            }
                            else
                            {
                                Console.WriteLine($"Paskaita {lesson} nerasta");
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("Neteisingai ivestas Paskaitos unikaus numeris");
                    }
                }
            }
        }

        public static void ChangeStudentsDepartment()
        {
            while (true)
            {

                if (InputValidation.QuitFromTask()) { break; }
                Console.Clear();

                Console.WriteLine("Kuri studenta norite perkelti (ID)");
                var studentToMove = Console.ReadLine();

                if (InputValidation.ValidateIntNull(studentToMove)
                    && int.TryParse(studentToMove, out int student))
                {
                    Console.WriteLine("I kuri departamenta (ID) norite prideti paskaitas");
                    var toDepartmentAdd = Console.ReadLine();

                    if (InputValidation.ValidateStringNull(toDepartmentAdd)
                    && InputValidation.ValidateDepartmentID(toDepartmentAdd))
                    {
                        var dbContext = new DbContextContext();

                        var studentMove = dbContext.Students
                            .Include(b => b.Departments)
                            .FirstOrDefault(a => a.StudentId == student);

                        var department = dbContext.Departments
                             .Include(a => a.Students)
                             .FirstOrDefault(b => b.DepartmentId == toDepartmentAdd);

                        var departmentRemove = dbContext.Departments
                            .Include(a => a.Students)
                            .FirstOrDefault(b => b.Students.Any(c => c.StudentId ==  student));

                        if (InputValidation.CheckIsStudentExistTrue(student)
                            && InputValidation.CheckIsDepartmentExist(toDepartmentAdd))                 //tikrinama ar ne null
                        {
                            studentMove.Departments.Remove(departmentRemove);
                            studentMove.Departments.Add(department);                            
                            dbContext.SaveChanges();
                            break;
                        }
                        else
                        {
                            Console.WriteLine($"Neteisingai suvesti duomenys");
                        }
                    }
                }
            }//pakeicia esamu studentu departamenta
        } //pakeisti studento departamenta
    }
}


