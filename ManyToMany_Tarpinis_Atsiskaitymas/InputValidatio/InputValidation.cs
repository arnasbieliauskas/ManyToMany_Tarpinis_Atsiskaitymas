using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ManyToMany_Tarpinis_Atsiskaitymas.DataBase;

namespace ManyToMany_Tarpinis_Atsiskaitymas.InputValidatio
{
    public class InputValidation
    {
        public static bool QuitFromTask()
        {
            Console.WriteLine("Norite nutraukti veiksmus, spauskite 'Q'");
            var quit = Console.ReadLine().ToLower();
            if (quit == "q")
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        public static bool ValidateStringNull(string? name)
        {
            if (string.IsNullOrEmpty(name))
            {
                Console.WriteLine("Iveskite teisingus duomenis");
            }
            return true;
        }
        public static bool ValidateIntNull(string? value)
        {
            if (string.IsNullOrEmpty(value)
                || !int.TryParse(value, out int intValue)
                || intValue == 0)
            {
                Console.WriteLine("Įveskite teisingus duomenis");
                return false;
            }
            return true;
        }
        public static bool ValidateDepartmentID(string input) // Regex tikrina ivestus simbolius kad butu string ir skaiciai
        {
            string pattern = "^[a-zA-Z0-9]{6}$";
            return Regex.IsMatch(input, pattern);
        }
        public static bool ValidateDepartmentsName(string departmentName)// Regex salyga 3-100 simboliu su raidemis ir gali buti skaiciais
        {

            string pattern = "^[a-zA-Z0-9]{3,100}$";
            return Regex.IsMatch(departmentName, pattern);
        }
        public static bool ValidateParse(string input)
        {
            if (int.TryParse(input, out int lesson))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool ValidateDateTimeAndDate(string LessonDate)
        {
            string pattern = @"^\d{2}-\d{2}-\d{4} \d{2}:\d{2}$";
            return Regex.IsMatch(LessonDate, pattern);
        }// Regex tikrina ar ivesta data tinkamas formatas
        public static bool ValidateMail(string studentMail) // Regex tikrina ar validus mail
        {
            string pattern = @"^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            return Regex.IsMatch(studentMail, pattern);
        }
        public static bool CheckIsLessonExist(int lesson)
        {
            var dbContext = new DbContextContext();
            var leson = dbContext.Lessons
                .FirstOrDefault(a => a.LessonId == lesson);
            if (lesson != null)
            {
                
                return true;
            }
            else
            {
                
                return false;
            }
        }
        public static bool CheckIsStudentExistTrue(int id)
        {
            var dbContext = new DbContextContext();
            var student = dbContext.Students
                .FirstOrDefault(a => a.StudentId == id);
            if (student != null)
            {
                return true;
            }
            else
            {

                return false;
            }
        }
        public static bool CheckIsDepartmentExist(string id)
        {
            var dbContext = new DbContextContext();
            var department = dbContext.Departments
                .FirstOrDefault(a => a.DepartmentId == id);
            if (department != null)
            {
                return true;
            }
            else
            {

                return false;
            }
        }
        public static bool CheckIsStudentExistFalse(int id)
        {
            var dbContext = new DbContextContext();
            var student = dbContext.Students
                .FirstOrDefault(a => a.StudentId == id);
            if (student == null)
            {
                return true;
            }
            else
            {

                return false;
            }
        }
    }

}
