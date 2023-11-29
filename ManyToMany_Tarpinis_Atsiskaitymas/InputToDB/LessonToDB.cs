using ManyToMany_Tarpinis_Atsiskaitymas.InputValidatio;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static Program;
using ManyToMany_Tarpinis_Atsiskaitymas.DataBase;

namespace ManyToMany_Tarpinis_Atsiskaitymas.InputToDB
{
    public class LessonToDB
    {
        public static void AddLessonToDB() //prideda paskaitos pavadinima 
        {
            while (true)
            {
                if (InputValidation.QuitFromTask()) { break; }
                Console.Clear();

                Console.WriteLine("Paskaitos unikaus numeris");
                var lessonId = Console.ReadLine();
                if (InputValidation.ValidateStringNull(lessonId))
                {
                    if (int.TryParse(lessonId, out int leson))  //tikrinama ar ivestas skaicius
                    {
                        Console.WriteLine("Paskaitos pavadinimas (ne maziau 5 simboliu)");
                        var lessonName = Console.ReadLine();

                        if (InputValidation.ValidateStringNull(lessonName)   //tikrinama ar ivestas paskaitos pavadinimas atitinka formata
                            && lessonName.Length >= 5)

                        {
                            Console.WriteLine("Paskaitos data ir laiksa (dd-mm-yyyy HH-mm)");
                            var LessonDate = Console.ReadLine();

                            if (InputValidation.ValidateDateTimeAndDate(LessonDate))      //tikrinama ar atitinka datos formata
                            {
                                if (InputValidation.CheckIsLessonExist(leson))
                                {
                                    var dbContext = new DbContextContext();
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
                                    Console.WriteLine($"Pamoka su ID {leson} jau sukurta");
                                }
                            }

                        }

                    }

                }
                else
                {
                    Console.WriteLine("Neteisingai ivestas Paskaitos unikalus numeris");
                }
            }
        }


        public static void AddLessonToStudent()
        {
            while (true)
            {
                if (InputValidation.QuitFromTask()) { break; }
                Console.Clear();

                Console.WriteLine("Kuriam studentui norite prideti paskaita (ID)");
                var studentToAdd = Console.ReadLine();

                if (InputValidation.ValidateStringNull(studentToAdd)
                    && int.TryParse(studentToAdd, out int studentId))
                {
                    Console.WriteLine("Kuria paskaita norite prideti (ID)");
                    var lesson = Console.ReadLine();

                    if (InputValidation.ValidateStringNull(studentToAdd)
                   && int.TryParse(lesson, out int lessonId))
                    {
                        var dbContext = new DbContextContext();

                        var student = dbContext.Students
                            .Include(b => b.Lessons)
                            .FirstOrDefault(a => a.StudentId == studentId);

                        var lessonAdd = dbContext.Lessons
                            .Include(b => b.Students)
                            .FirstOrDefault(a => a.LessonId == lessonId);

                        if (InputValidation.CheckIsLessonExist(lessonId)
                            && InputValidation.CheckIsStudentExistTrue(studentId))
                        {
                            student.Lessons.Add(lessonAdd);                                
                            dbContext.SaveChanges();
                            break;
                        }
                        else
                        {
                            Console.WriteLine($"Iveskite teisingus duomenis");
                        }
                    }                  
                }             
            }
        }
    }
}




