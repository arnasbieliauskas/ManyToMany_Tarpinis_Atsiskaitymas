// See https://aka.ms/new-console-template for more information
using ManyToMany_Tarpinis_Atsiskaitymas;
using Microsoft.EntityFrameworkCore;

internal class Program
{
    public static void Main(string[] args)
    {
        /*
         Sąryšis su SQL
        Suvedami studento duomenys. Studentui priskiriamas departamentas, priskiriamos paskaitos.
        Suvedami Departamentai 
        Suvedamos Paskaitos
        -Studento vardas ir pavardė: tik raidės, 2-50 simbolių.
        -Studento numeris: unikalus, tik skaičiai. Tiksliai 8 simboliai.
        -Studento el.paštas: turi būti teisingo formato (tikrinama naudojant Regex)
        -Departamento pavadinimas: 3-100 simbolių, gali būti raidės ir skaičiai (tikrinama naudojant Regex).
        -Departamento kodas: unikalus, tik raidės ir skaičiai. Tiksliai 6 simboliai (tikrinama naudojant Regex).
        -Paskaitos pavadinimas: unikalus, ne mažiau nei 5 simboliai.
        -Paskaitos laikas: turi atitikti realius laiko intervalus (tikrinama naudojant Regex).
        +Validacija atliekama prieš įrašant duomenis į duomenų bazę.
        +Visuose veiksmuose tikrinama ar nėra Null.
         */
        var dbContext = new DbContextContext(new DbContextOptionsBuilder<DbContextContext>()
                .UseSqlServer($"Server=(localdb)\\mssqllocaldb;Database=University_Students_Schedual;Trusted_Connection=True;")
                .Options);
        while (true)
        {
            Meniu();
            Console.WriteLine();
            Console.WriteLine("Jusu pasirinkimas");
            var input = Console.ReadLine();
            if (input != null
                && int.TryParse(input, out int inputChoice))        //tikrinama vartotojo ivestis
            {
                ChooseAction selectedAction = (ChooseAction)inputChoice;
                switch (selectedAction)
                {
                    case ChooseAction.Exit:
                        Console.WriteLine("Aciu");
                        Thread.Sleep(5000);
                        Environment.Exit(0);
                        break;
                        //ok
                    case ChooseAction.CreatDepartment:      //sukuriamas naujas departamentas
                        DepartmentToDB.AddDepartment();
                        break;

                        //mail validacija patikrinti                       
                    case ChooseAction.CreateLesson:         //sukuriama nauaja paskaita
                        LessonToDB.AddLessonToDB();
                        break;

                        //ok
                    case ChooseAction.CreateStudent:                //sukuriamas naujas studentas
                        StudentToDB.AddNewStudentsNameAndSurname(); //ir pridedamas prie departamento
                        break;

                        //nesutvarkyta
                        //patikrinti mail verifikacija
                       
                    case ChooseAction.AddLessontoStudent:           //pridedama paskaita prie departamento
                        LessonToDB.AddLessonToStudent();
                        break;

                        //ok
                    case ChooseAction.MoveExistingLessonToDepartment:   //esamos paskaitos perkelimas i kita departamenta
                        DepartmentToDB.ChangeLessonsDepartment();
                        break;

                        //ok
                    case ChooseAction.MoveExistingStudentToDepartment:  //esamo studento perkelimas i kita departamenta
                        DepartmentToDB.ChangeStudentsDepartment();
                        break;

                        //ok
                    case ChooseAction.RepresentStudentsByDepartment:    //studentu atvaizdavimas pagal departaenta
                        RepresentToScrean.RepresentStudentByDepartment();
                        break;

                        //ok
                    case ChooseAction.RepresentLessonsByStudent:        //paskaitu atvaizdavimas pagal studenta
                        RepresentToScrean.RepreentLessonsByStudent();
                        break;

                        //ok
                    case ChooseAction.RepresentLessonByDepartment:      //paskaitu atvaizdavimas pagal departamenta
                        RepresentToScrean.RepresentLessonByDepartment();
                        break;

                        //ok
                    case ChooseAction.RepresentDepartment:              //viso departamento atvaizdavimas   
                        RepresentToScrean.RepresentDepartmentInConsole();
                        break;

                    default:
                        Console.WriteLine("Netinkamas pasirinkitmas");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Meniu pasirinkimui naudokite skaicius");
            }
        }
    }
    enum ChooseAction
    {
        Exit,                               // 0
        CreatDepartment,                    // 1
        CreateLesson,                       // 2
        CreateStudent,                      // 3
        AddLessontoStudent,                 // 4 
        MoveExistingLessonToDepartment,     // 5
        MoveExistingStudentToDepartment,    // 6   
        RepresentStudentsByDepartment,      // 7
        RepresentLessonsByStudent,          // 8
        RepresentLessonByDepartment,        // 9
        RepresentDepartment                 // 10
    }
    public static void Meniu()
    {
        Console.WriteLine(string.Join(Environment.NewLine,
    "1) Prideti Departamenta",
    "2) Prideti Paskaita",
    "3) Prideti studenta",
    "4) Prideti studentui paskaita",
    "5) Perkelti paskaita i kita departamenta",
    "6) Pakeisti studentui departamenta",
    "7) Atvaizduoti studentus pagal departamenta",
    "8) Atvaizduoti studento paskaitas",
    "9) Atvaizduoti paskaitas pagal departamenta",
    "10) Atvaizduoti departamentus",
    "0) Exit"));
    }
}

