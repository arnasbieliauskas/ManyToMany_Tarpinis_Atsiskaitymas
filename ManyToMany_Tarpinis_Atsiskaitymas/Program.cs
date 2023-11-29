using ManyToMany_Tarpinis_Atsiskaitymas.Display;
using ManyToMany_Tarpinis_Atsiskaitymas.InputToDB;
using Microsoft.EntityFrameworkCore;
using ManyToMany_Tarpinis_Atsiskaitymas.DataBase;

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
                        Thread.Sleep(500);
                        Environment.Exit(0);
                        break;

                    case ChooseAction.CreatDepartment:      //sukuriamas naujas departamentas
                        DepartmentToDB.AddDepartment();
                        Console.Clear();
                        break;


                    case ChooseAction.CreateLesson:         //sukuriama nauaja paskaita
                        LessonToDB.AddLessonToDB();
                        Console.Clear();
                        break;


                    case ChooseAction.CreateStudent:                //sukuriamas naujas studentas
                        StudentToDB.AddNewStudentsNameAndSurname(); //ir pridedamas prie departamento
                        Console.Clear();
                        break;


                    case ChooseAction.AddLessontoStudent:           //pridedama paskaita prie departamento
                        LessonToDB.AddLessonToStudent();
                        Console.Clear();
                        break;

                    //ok
                    case ChooseAction.MoveExistingLessonToDepartment:   //esamos paskaitos perkelimas i kita departamenta
                        DepartmentToDB.AddLessonToDepartment();
                        Console.Clear();
                        break;

                    //ok
                    case ChooseAction.MoveExistingStudentToDepartment:  //esamo studento perkelimas i kita departamenta                         
                        DepartmentToDB.ChangeStudentsDepartment();
                        Console.Clear();
                        break;

                    //ok
                    case ChooseAction.DisplayStudentsByDepartment:    //studentu atvaizdavimas pagal departaenta
                        DisplayToScrean.DisplayStudentByDepartment();                        
                        Console.ReadLine();
                        Console.Clear();
                        break;

                    //ok
                    case ChooseAction.DisplayLessonsByStudent:        //paskaitu atvaizdavimas pagal studenta
                        
                        DisplayToScrean.DisplayLessonsByStudent();
                        Console.ReadLine();
                        Console.Clear();
                        break;

                    //ok
                    case ChooseAction.DisplayLessonByDepartment:      //paskaitu atvaizdavimas pagal departamenta
                        DisplayToScrean.DisplayLessonByDepartment();
                        Console.ReadLine();
                        Console.Clear();
                        break;

                    //ok
                    case ChooseAction.DisplayDepartment:              //viso departamento atvaizdavimas   
                        DisplayToScrean.DisplayDepartmentInConsole();
                        Console.ReadLine();
                        Console.Clear();
                        break;

                    default:
                        Console.WriteLine("Netinkamas pasirinkitmas");
                        Thread.Sleep(1000);
                        Console.Clear();
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
        DisplayStudentsByDepartment,      // 7
        DisplayLessonsByStudent,          // 8
        DisplayLessonByDepartment,        // 9
        DisplayDepartment                 // 10
    }
    public static void Meniu()
    {
        Console.WriteLine(string.Join(Environment.NewLine,
    "1) Prideti Departamenta",
    "2) Prideti Paskaita",
    "3) Prideti studenta",
    "4) Prideti studentui paskaita",
    "5) Prideti departamentui paskaita",
    "6) Pakeisti studentui departamenta",
    "7) Atvaizduoti studentus pagal departamenta",
    "8) Atvaizduoti studento paskaitas",
    "9) Atvaizduoti paskaitas pagal departamenta",
    "10) Atvaizduoti departamentus",
    "0) Exit"));
    }
}

