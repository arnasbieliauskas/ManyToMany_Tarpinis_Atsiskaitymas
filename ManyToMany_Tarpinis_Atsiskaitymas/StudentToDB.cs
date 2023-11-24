using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ManyToMany_Tarpinis_Atsiskaitymas
{
    public class StudentToDB
    {



        public static void AddNewStudentsNameAndSurname()
        {
            while (true)
            {
                var dbContext = new DbContextContext();
                Console.WriteLine("Norite nutraukti veiksmus, spauskite 'Q'");
                var quit = Console.ReadLine().ToLower();

                if (quit == "q") { break; }
                Console.Clear();

                Console.WriteLine("Studento vardas:");
                var studentName = Console.ReadLine();
                Console.WriteLine("Studento pavarde:");
                var studentSurname = Console.ReadLine();  
                
                //tikrinama vardas pavarde 3 iki 50 simboliu
                if (!string.IsNullOrEmpty(studentName)
                    && !string.IsNullOrEmpty(studentSurname)
                    && studentName.Count() > 3
                    && studentName.Count() <= 50
                    && studentSurname.Count() > 3
                    && studentSurname.Count() <= 50)
                {

                    Console.WriteLine("Studento unikalus numeris (8 simboliai)");
                    var studentId = Console.ReadLine();

                    //tikrinamas id 8 simboliai
                    if (!string.IsNullOrEmpty(studentId)
                        && studentId.Count() == 8
                        && int.TryParse(studentId, out var id))
                    {

                        Console.WriteLine("Studento elektroninis pastas:");
                        var studentMail = Console.ReadLine();

                        //tikrinamas mail naudojant Regex
                        if (!string.IsNullOrEmpty(studentMail)
                            && IsEmailValid(studentMail) == true) 
                        {

                            Console.WriteLine("Kokio departamento paskaita (nurodykite depertamento ID)");
                            var lessonDepartmentId = Console.ReadLine();

                            //tikrinama ar teisingai suvestas departamento nr naudojant Regex
                            if (!string.IsNullOrEmpty(lessonDepartmentId)
                                && DepartmentToDB.CheckDepartmentID(lessonDepartmentId))
                            {
                                Student newStudent = dbContext.Students
                                        .FirstOrDefault(c => c.Department.DepartmentId == lessonDepartmentId);

                                //tikrinama ar rastas toks departamentas
                                if (newStudent != null)
                                {
                                    newStudent.StudentId = id;
                                    newStudent.StudentName = studentName;
                                    newStudent.StudentSurName = studentSurname;
                                    newStudent.StudentMail = studentMail;

                                    dbContext.Students.Add(newStudent);
                                    dbContext.SaveChanges();
                                    break;
                                }
                                else
                                {
                                    Console.WriteLine($"Departamentas {lessonDepartmentId} nerastas");
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine("Iveskite tinkama Departamenta (Id)");
                        }

                    }
                    else
                    {
                        Console.WriteLine("Iveskite tinkama elektronini pasta");
                    }

                }
                           
                else
            {
                Console.WriteLine("Prasom ivesti Varda ir Pavarde");
            }
        }
    }// vardo ir pavardes ivedimas i DB
    public static bool IsEmailValid(string studentMail) // Regex tikrina ar validus mail
    {
        string pattern = @"^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
        return Regex.IsMatch(studentMail, pattern);
    }
}
}


