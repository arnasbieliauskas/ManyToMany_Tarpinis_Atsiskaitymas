using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static Program;

namespace ManyToMany_Tarpinis_Atsiskaitymas
{
    public class LessonToDB
    {
        public static void AddLessonToDB() //prideda paskaitos pavadinima 
        {
            while (true)
            {
                Console.WriteLine("Norite nutraukti veiksmus, spauskite 'Q'");
                var quit = Console.ReadLine().ToLower();

                if (quit == "q") { break; }

                Console.WriteLine("Paskaitos unikaus numeris");
                var lessonId = Console.ReadLine();

                if (int.TryParse(lessonId, out int leson))  //tikrinama ar ivestas skaicius
                {
                    Console.WriteLine("Paskaitos pavadinimas (ne maziau 5 simboliu)");
                    var lessonName = Console.ReadLine();

                    if (!string.IsNullOrEmpty(lessonName)   //tikrinama ar ivestas paskaitos pavadinimas atitinka formata
                        && lessonName.Length >= 5)

                    {
                        Console.WriteLine("Paskaitos data ir laiksa (dd-mm-yyyy HH-mm)");
                        var LessonDate = Console.ReadLine();


                        if (LessonValidDateTimeAndDate(LessonDate))      //tikrinama ar atitinka datos formata
                        {
                            Console.WriteLine("Kokio departamento paskaita");
                            var lessonDepartmentId = Console.ReadLine();

                            if ((lessonDepartmentId != null)                    //tikrinama ar departamento ivestis atitinka formata
                                && DepartmentToDB.CheckDepartmentID(lessonDepartmentId))
                            {
                                var dbContext = new DbContextContext();

                                Lesson lesson = dbContext.Lessons
                                    .FirstOrDefault(a => a.Department.DepartmentId == lessonDepartmentId);

                                if (lesson != null)                                 //tikrinama ar yra toks departamentas
                                {
                                    Lesson lessonAdd = new Lesson
                                    {
                                        LessonId = leson,
                                        LessonName = lessonName,
                                        LessonDateAndTime = LessonDate
                                    };
                                    dbContext.Lessons.Add(lessonAdd);
                                    dbContext.SaveChanges();
                                    break;
                                }
                                else
                                {
                                    Console.WriteLine($"Departamentas {lessonDepartmentId} nerastas");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Neteisingai ivestas Departamento ID");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Elektroninis pastas netinkamo formato");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Ivedant 'Paskaitos unikalus numeris' naudokite tinkamus sumbolius");
                    }
                }
                else
                {
                    Console.WriteLine("Neteisingai ivestas Paskaitos unikaus numeris");
                }
            }
        }

        public static void AddLessonToStudent()
        {
            while (true)
            {
                Console.WriteLine("Norite nutraukti veiksmus, spauskite 'Q'");
                var quit = Console.ReadLine().ToLower();

                if (quit == "q") { break; }
                Console.Clear();

                Console.WriteLine("Kuriam studentui norite prideti paskaita (ID)");
                var student = Console.ReadLine();

                if (!string.IsNullOrEmpty(student)
                    && (int.TryParse(student, out int studentId)))
                {
                    Console.WriteLine("Kuria paskaita norite prideti (ID)");
                    var lesson = Console.ReadLine();

                    if (!string.IsNullOrEmpty(student)
                   && (int.TryParse(lesson, out int lessonId)))
                    {
                        var dbContext = new DbContextContext();

                        Lesson lessonAdd = dbContext.Lessons
                       .FirstOrDefault(a => a.Student.StudentId == studentId);

                        if (lessonAdd != null)
                        {
                            lessonAdd.Student.StudentId = studentId;
                            dbContext.SaveChanges();
                            break;
                        }
                        else
                        {
                            Console.WriteLine($"Studento {studentId} nerastas");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Iveskite tinkama paskaitos ID");
                    }
                }
                else
                {
                    Console.WriteLine("Iveskite tinkama studento ID");
                }
            }
        }

        static bool LessonValidDateTimeAndDate(string LessonDate)
        {
            string pattern = @"^\d{2}-\d{2}-\d{4} \d{2}:\d{2}$";
            return Regex.IsMatch(LessonDate, pattern);
        }// Regex tikrina ar ivesta data tinkamas formatas
    }
}




