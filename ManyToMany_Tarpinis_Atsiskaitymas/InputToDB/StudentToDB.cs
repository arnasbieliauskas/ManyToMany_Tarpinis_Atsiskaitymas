using ManyToMany_Tarpinis_Atsiskaitymas.InputValidatio;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ManyToMany_Tarpinis_Atsiskaitymas.DataBase;

namespace ManyToMany_Tarpinis_Atsiskaitymas.InputToDB
{
    public class StudentToDB
    {

        public static void AddNewStudentsNameAndSurname()
        {
            while (true)
            {

                if (InputValidation.QuitFromTask()) { break; }
                Console.Clear();

                Console.WriteLine("Studento vardas:");
                var studentName = Console.ReadLine();
                Console.WriteLine("Studento pavarde:");
                var studentSurname = Console.ReadLine();

                //tikrinama vardas pavarde 3 iki 50 simboliu
                if (InputValidation.ValidateStringNull(studentName)
                    && InputValidation.ValidateStringNull(studentSurname)
                    && studentName.Count() > 3
                    && studentName.Count() <= 50
                    && studentSurname.Count() > 3
                    && studentSurname.Count() <= 50)
                {
                    Console.WriteLine("Studento unikalus numeris (8 simboliai)");
                    var studentId = Console.ReadLine();

                    //tikrinamas id 8 simboliai
                    if (InputValidation.ValidateStringNull(studentId)
                        && studentId.Count() == 8
                        && int.TryParse(studentId, out var id))
                    {
                        Console.WriteLine("Studento elektroninis pastas:");
                        var studentMail = Console.ReadLine();

                        //tikrinamas mail naudojant Regex
                        if (InputValidation.ValidateStringNull(studentMail)
                            && InputValidation.ValidateMail(studentMail))
                        {

                            Console.WriteLine("Kokio departamento paskaita (nurodykite depertamento ID)");
                            var lessonDepartmentId = Console.ReadLine();

                            //tikrinama ar teisingai suvestas departamento nr naudojant Regex
                            if (InputValidation.ValidateStringNull(lessonDepartmentId)
                                && InputValidation.ValidateDepartmentID(lessonDepartmentId))
                            {
                                var dbContext = new DbContextContext();
                                var department = dbContext.Departments
                                 .FirstOrDefault(a => a.DepartmentId == lessonDepartmentId);
                                                                
                                if (InputValidation.CheckIsStudentExistFalse(id)
                                    && InputValidation.CheckIsDepartmentExist(lessonDepartmentId))
                                {
                                    var student = new Student
                                    {
                                        StudentId = id,
                                        StudentName = studentName,
                                        StudentSurName = studentSurname,
                                        StudentMail = studentMail
                                    };
                                    dbContext.Students.Add(student);
                                    student.Departments.Add(department);                                    
                                    dbContext.SaveChanges();
                                    break;
                                }
                                else
                                {
                                    Console.WriteLine($"Studentas su ID {id} jau egzistuoja");
                                }
                            }
                        }

                    }
                }
            }
        }// vardo ir pavardes ivedimas i DB

    }
}


