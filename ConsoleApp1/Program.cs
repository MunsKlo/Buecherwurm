using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;


namespace ConsoleApp1
{
    class Program
    {
        private static Controller.Area area = new Controller.Area();
        static void Main(string[] args)
        {
            var jsonString = File.ReadAllText("books.json");
            if (!File.Exists("test.json"))
            {
                JSONIO.ReadDataBooks();
                JSONIO.SaveData("test.json", Controller.copies);
            }
            if(File.Exists("controller.json"))
                JSONIO.ReadDataController();
            JSONIO.ReadDataCopies();
            StartProgram();
        }

        private static void StartProgram()
        {
            var input = "";
            while(true)
            {
                area = Controller.Area.Menu;
                Console.WriteLine("===========================================================================");
                Console.WriteLine("Willkommen beim Verwaltungsprogramm 'Bücherwurm'!");
                Console.WriteLine("===========================================================================");
                Console.WriteLine();
                Console.WriteLine("Schreiben Sie die Nummer der Kategorie oder 'b' um das Programm zu beenden!");
                Output(new List<string> {"1. Bücher", "2. Exemplare", "3. Leihvorgang" });
                input = Input(0, 4, "b");
                if (input == "b")
                    break;
                Console.Clear();
                ChooseCategory(input);
            }
            //Speichere die Daten
            JSONIO.SaveDataController();
        }

        private static void Output(List<string> informations)
        {
            foreach (var item in informations)
            {
                Console.WriteLine(item);
            }
        }

        private static string Input(int minNumb, int maxNumb, string specialLetter)
        {
            Console.WriteLine($"Schreiben Sie die Nummer oder '{specialLetter}' um zurück zu kommen!");
            var input = "";
            while (true)
            {
                input = Console.ReadLine();
                if (int.TryParse(input, out int numb) && numb > minNumb - 1 && numb < maxNumb + 1)
                {
                    break;
                }
                else if (input == specialLetter)
                    break;
            }
            return input;
        }

        private static void ChooseCategory(string input)
        {
            if (input == "1")
            {
                area = Controller.Area.Books;
                CategoryBook();
            }
            else if(input == "2")
            {
                area = Controller.Area.Copy;
                CategoryCopy();
            }
            else if (input == "3")
            {
                area = Controller.Area.Copy;
                CategoryRent();
            }

        }
        /// <summary>
        /// Bereich Bücher
        /// </summary>
        private static void CategoryBook()
        {
            var input = "";
            while (true)
            {
                Output(new List<string> { "1. Erstellen", "2. Bearbeiten", "3. Löschen" });
                input = Input(0, 4, "z");
                if (input == "z")
                    break;
                if (input == "1")
                    CreateBook();
                else if (input == "2")
                    EditBook();
                else if (input == "3")
                    DeleteBook();
            }
            
        }

        private static void CreateBook()
        {
            Console.Clear();
            Console.WriteLine("Füllen Sie die Daten auf");
            Controller.lastBookId++;
            var author = GetUserInputData("Autor", false);
            var country = GetUserInputData("Land", false);
            var imageLink = GetUserInputData("BIldlink", false);
            var language = GetUserInputData("Sprache", false);
            var link = GetUserInputData("Link", false);
            var pages = GetUserInputData("Seiten", true);
            var title = GetUserInputData("Titel", false);
            var year = GetUserInputData("Jahr", true);
            var exemplare = GetUserInputData("Exemplare", true);

            var book = new Buch(author, country, imageLink, language, link, pages, title, year, exemplare);
            Controller.books.Add(book);
        }

        private static void EditBook()
        {
            
            if (Controller.books.Count != 0)
            {
                var property = "";
                var newValue = "";
                var input = "";
                while (true)
                {
                    OutputOfThings.OutputLists(area);
                    input = Input(Controller.GetLowestNumberInList(area), Controller.GetHighestNumberInList(area) + 1, "z");
                    if (input == "z")
                        break;
                    var buch = (Buch) Controller.BekommeObjektDurchNummer(Convert.ToInt32(input), area);
                    if (buch == null)
                        Console.WriteLine("Es wurde kein Buch mit der eingegebenen Id gefunden.");
                    else
                    {
                        Console.Clear();
                        OutputOfThings.OutputObject(buch, area);
                        Console.WriteLine("Was davon wollen Sie ändern?");
                        property = GetUserInputData("Eigenschaft", false);
                        newValue = GetUserInputData("Neuer Wert", Controller.IsNumbProperty(property));
                        buch.ÄndereEigenschaft(newValue, property);
                        OutputOfThings.OutputObject(buch, area);
                        Console.ReadKey();
                    }
                }
                
            }
        }

        private static void DeleteBook()
        {
            var input = "";
            while (true) 
            {
                OutputOfThings.OutputLists(area);
                input = Input(Controller.GetLowestNumberInList(area), Controller.GetHighestNumberInList(area) + 1, "z");
                if (input == "z")
                    break;
                var book = (Buch)Controller.BekommeObjektDurchNummer(Convert.ToInt32(input), area);
                if (book == null)
                    Console.WriteLine("Es wurde kein Buch mit der eingegebenen Id gefunden.");
                else
                {
                    Console.Clear();
                    OutputOfThings.OutputObject(book, area);
                    while (true)
                    {
                        Console.WriteLine("Wollen Sie wirklich das Buch entfernen! [j/n]");
                        input = Console.ReadLine();
                        if (input == "j")
                        {
                            Controller.DeleteCopies(book);
                            Controller.DeleteBook(book);
                            break;
                        }
                        else if (input == "n")
                            break;
                    }
                }
            }
        }

        /// <summary>
        /// Bereich Exemplare
        /// </summary>

        private static void CategoryCopy()
        {
            var input = "";
            while (true)
            {
                Output(new List<string> {"1. Bearbeiten", "2. Löschen" });
                input = Input(0, 3, "z");
                if (input == "z")
                    break;
                if (input == "1")
                    EditCopy();
                else if (input == "2")
                    DeleteCopy();
            }
        }

        private static void EditCopy()
        {
            var input = "";
            var property = false;
            while (true)
            {
                OutputOfThings.OutputLists(area);
                input = Input(Controller.GetLowestNumberInList(area), Controller.GetHighestNumberInList(area), "z");
                if (input == "z")
                    break;
                var copy = (Exemplar)Controller.BekommeObjektDurchNummer(Convert.ToInt32(input), area);
                if (copy == null)
                    Console.WriteLine("Es wurde kein Buch mit der eingegebenen Id gefunden.");
                else
                {
                    Console.Clear();
                    OutputOfThings.OutputObject(copy, area);
                    Console.WriteLine("Bei Exemplaren können Sie nur den Zustand 'Geliehen' ändern.");
                    property = GetUserInputData("Ist das Exemplar vorhanden? [j/n]", false) != "j";
                    copy.ÄndereEigenschaftVonExemplar(property);
                }
            }

        }

        private static void DeleteCopy()
        {
            var input = "";
            while (true)
            {
                OutputOfThings.OutputLists(area);
                input = Input(Controller.GetLowestNumberInList(area), Controller.GetHighestNumberInList(area), "z");
                if (input == "z")
                    break;
                var copy = (Exemplar)Controller.BekommeObjektDurchNummer(Convert.ToInt32(input), area);
                if (copy == null)
                    Console.WriteLine("Es wurde kein Buch mit der eingegebenen Id gefunden.");
                else
                {
                    Console.Clear();
                    OutputOfThings.OutputObject(copy, area);
                    while (true)
                    {
                        Console.WriteLine("Wollen Sie wirklich das Exemplar entfernen! [j/n]");
                        input = Console.ReadLine();
                        if (input == "j")
                        {
                            Controller.DeleteTheCopy(copy);
                            break;
                        }
                        else if (input == "n")
                            break;
                    }
                    
                }
            }
        }

        /// <summary>
        /// Kategorie: Leihvorgang
        /// </summary>

        public static void CategoryRent()
        {
            var input = "";
            while (true)
            {
                Output(new List<string> { "1. Erstellen", "2. Bearbeiten", "3. Löschen" });
                input = Input(0, 3, "z");
                if (input == "z")
                    break;
                if (input == "1")
                    CreateRent();
                else if (input == "2")
                    EditRent();
                else if (input == "3")
                    DeleteRent();
            }
        }

        public static void CreateRent()
        {
            Console.Clear();
            Console.WriteLine("Füllen Sie die Daten auf");
            var 
        }

        public static void EditRent()
        {

        }

        public static void DeleteRent()
        {

        }


        private static string GetUserInputData(string description, bool isInt)
        {
            var input = "";
            while (true)
            {
                Console.WriteLine(description);
                input = Console.ReadLine();
                if (isInt && int.TryParse(input, out int numb))
                {
                    break;
                }
                else if (!isInt)
                {
                    break;
                }
            }
            return input;
        }
    }
}
