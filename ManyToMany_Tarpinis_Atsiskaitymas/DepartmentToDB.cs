using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ManyToMany_Tarpinis_Atsiskaitymas
{
    public class DepartmentToDB
    {

        public static void AddDepartment() //Pridedami derpatamentai
        {
            while (true)
            {


                var dbContext = new DbContextContext();
                Console.WriteLine("Departamento pavadinimas");
                string departmentName = Console.ReadLine();
                Console.WriteLine("Departamento unikalus numeris (raides ir skaiciai 6 simboliai");
                var departmentId = Console.ReadLine();

                if (!string.IsNullOrEmpty(departmentName)
                    && !string.IsNullOrEmpty(departmentId)
                    && CheckDepartmentsName(departmentName)
                    && CheckDepartmentID(departmentId))
                {
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
        public static void ChangeLessonsDepartment()
        {
            while (true)
            {
                Console.WriteLine("Norite nutraukti veiksmus, spauskite 'Q'");
                var quit = Console.ReadLine().ToLower();

                if (quit == "q") { break; }
                Console.Clear();


                Console.WriteLine("Kuria paskaita norite prideti (ID)");
                var lessonToMove = Console.ReadLine();

                if (!string.IsNullOrEmpty(lessonToMove)             //tikrinama ar ne null, tinkamai ivesti skaiciai
                    && (int.TryParse(lessonToMove, out int lesson)))
                {

                    Console.WriteLine("I kuri departamenta (ID) norite prideti paskaitas");
                    var toDepartmentAdd = Console.ReadLine();

                    if ((toDepartmentAdd != null                    //tikrinama ar departamento ivestis atitinka formata
                       && DepartmentToDB.CheckDepartmentsName(toDepartmentAdd)))
                    {
                        var dbContext = new DbContextContext();
                        Lesson lessonMove = dbContext.Lessons
                       .FirstOrDefault(a => a.LessonId == lesson);

                        if (lessonMove != null)
                        {
                            lessonMove.Department.DepartmentId = toDepartmentAdd;
                            dbContext.SaveChanges();
                        }
                        else
                        {
                            Console.WriteLine($"Paskaita {lesson} nerasta");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Iveskite tinkama departamento ID");
                    }

                }
                else
                {
                    Console.WriteLine("Iveskite tinkama paskaitos Id");
                }

            }//pakeisti esamu pamoku departamenta
        }//pakeisti paskaitu departamenta
        public static void ChangeStudentsDepartment()
        {
            while (true)
            {
                Console.WriteLine("Norite nutraukti veiksmus, spauskite 'Q'");
                var quit = Console.ReadLine().ToLower();

                if (quit == "q") { break; }
                Console.Clear();



                Console.WriteLine("Kuri studenta norite perkelti (ID)");
                var studentToMove = Console.ReadLine();

                if (!string.IsNullOrEmpty(studentToMove)
                    && (int.TryParse(studentToMove, out int student)))
                {
                    Console.WriteLine("I kuri departamenta (ID) norite prideti paskaitas");
                    var toDepartmentAdd = Console.ReadLine();

                    if (!string.IsNullOrEmpty(toDepartmentAdd)
                    && CheckDepartmentID(toDepartmentAdd))
                    {
                        var dbContext = new DbContextContext();
                        Student studentMove = dbContext.Students
                       .FirstOrDefault(a => a.StudentId == student);
                        if (studentMove != null)
                        {
                            studentMove.Department.DepartmentId = toDepartmentAdd;
                            dbContext.SaveChanges();
                            break;
                        }
                        else
                        {
                            Console.WriteLine($"Studentas {student} nerastas");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Iveskite tinkama depatamento ID");
                    }
                }
                }
            }//pakeicia esamu studentu departamenta

            public static bool CheckDepartmentID(string input) // Regex tikrina ivestus simbolius kad butu string ir skaiciai
            {
                string pattern = "^[a-zA-Z0-9]{6}$";
                return Regex.IsMatch(input, pattern);
            }
            static bool CheckDepartmentsName(string departmentName)// Regex salyga 3-100 simboliu su raidemis ir gali buti skaiciais
            {

                string pattern = "^[a-zA-Z0-9]{3,100}$";
                return Regex.IsMatch(departmentName, pattern);
            }

        }
    }


