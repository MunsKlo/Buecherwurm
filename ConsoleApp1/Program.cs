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
            if (!File.Exists("copies.json"))
            {
                Controller.lastBookId = 0;
                Controller.lastCopyId = 0;
                Controller.lastDelRentId = 0;
                Controller.lastRentId = 0;
                JSONIO.ReadDataBooks();
                JSONIO.SaveData("copies.json", Controller.copies);
            }
            else
            {
                JSONIO.ReadDataController();
                JSONIO.ReadDataCopies();
                JSONIO.ReadDataRents();
                JSONIO.ReadDataDelRents();
            }
            
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
                Output(new List<string> {"1. Bücher", "2. Exemplare", "3. Leihvorgang", "4. Gelöschte Leihvorgänge" });
                input = Input(0, 4, "b");
                if (input == "b")
                    break;
                Console.Clear();
                ChooseCategory(input);
            }
            //Speichere die Daten
            JSONIO.SaveDataController();
            JSONIO.SaveData("newbooks.json", Controller.books);
            JSONIO.SaveData("rents.json", Controller.rents);
            JSONIO.SaveData("delrents.json", Controller.delRents);
            JSONIO.SaveData("copies.json", Controller.copies);
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
                area = Controller.Area.Rent;
                CategoryRent();
            }
            else if (input == "4")
            {
                area = Controller.Area.DelRent;
                CategoryDelRent();
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
                    input = Input(Controller.GetLowestNumberInList(area), Controller.GetHighestNumberInList(area), "z");
                    if (input == "z")
                        break;
                    var book = (Buch) Controller.GetObjectThroughNumber(Convert.ToInt32(input), area);
                    if (book == null)
                        Console.WriteLine("Es wurde kein Buch mit der eingegebenen Id gefunden.");
                    else
                    {
                        Console.Clear();
                        OutputOfThings.OutputObject(book, area);
                        Console.WriteLine("Was davon wollen Sie ändern?");
                        property = GetUserInputData("Eigenschaft", false);
                        newValue = GetUserInputData("Neuer Wert", Controller.IsNumbProperty(property));
                        book.ÄndereEigenschaft(newValue, property);
                        OutputOfThings.OutputObject(book, area);
                        OutputOfThings.ReadKeyMethod();
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
                input = Input(Controller.GetLowestNumberInList(area), Controller.GetHighestNumberInList(area), "z");
                if (input == "z")
                    break;
                var book = (Buch)Controller.GetObjectThroughNumber(Convert.ToInt32(input), area);
                if (book == null)
                    Console.WriteLine("Es wurde kein Buch mit der eingegebenen Id gefunden.");
                else
                {
                    Console.Clear();
                    OutputOfThings.OutputObject(book, area);
                    while (true)
                    {
                        if (Controller.HaveDeletingBookCopieIinRents(book))
                            break;
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
                Output(new List<string> {"1. Bearbeiten" });
                input = Input(0, 3, "z");
                if (input == "z")
                    break;
                if (input == "1")
                    EditCopy();
                /*else if (input == "2")
                    DeleteCopy();*/
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
                var copy = (Exemplar)Controller.GetObjectThroughNumber(Convert.ToInt32(input), area);
                if (copy == null)
                    Console.WriteLine("Es wurde kein Buch mit der eingegebenen Id gefunden.");
                else
                {
                    Console.Clear();
                    OutputOfThings.OutputObject(copy, area);
                    Console.WriteLine("Bei Exemplaren können Sie nur den Zustand 'Geliehen' ändern.");
                    property = GetUserInputData("Ist das Exemplar vorhanden? [j/n]", false) != "j";
                    if (Controller.IsCopyInRent(copy))
                    {
                        Console.WriteLine("Exemplar ist zurzeit noch ausgeliehen, deshalb kann sich der zustand nicht ändern!");
                        OutputOfThings.ReadKeyMethod();
                    }
                        
                    else
                    {
                        copy.ÄndereEigenschaftVonExemplar(property);
                    }
                }
            }

        }
        /*
        private static void DeleteCopy()
        {
            var input = "";
            while (true)
            {
                OutputOfThings.OutputLists(area);
                input = Input(Controller.GetLowestNumberInList(area), Controller.GetHighestNumberInList(area), "z");
                if (input == "z")
                    break;
                var copy = (Exemplar)Controller.GetObjectThroughNumber(Convert.ToInt32(input), area);
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
        */

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
                Console.Clear();
                if (input == "z")
                    break;
                if (input == "1")
                    CreateRent();
                else if (input == "2" && Controller.rents.Count > 0)
                    EditRent();
                else if (input == "3")
                    DeleteRent();
            }
        }

        public static void CreateRent()
        {
            var copy = GetExemplar();
            if(copy != null)
            {
                var person = GetPerson();
                var date = GetRentBeginDate();
                var newRent = new Leihvorgang(copy, person, date);
                Controller.rents.Add(newRent);
                copy.IstAusgeliehen = true;
                Console.Clear();
                Console.WriteLine("Leihvorgang wurde erfolgreich erstellt!");
            }
        }

        public static Exemplar GetExemplar()
        {
            var input = "";
            var copy = new Exemplar();
            while (true)
            {
                Console.Clear();
                OutputOfThings.OutputLists(Controller.Area.Books);
                Console.WriteLine("Welches Buch soll ausgeliehen werden?");
                input = Input(Controller.GetLowestNumberInList(Controller.Area.Books), Controller.GetHighestNumberInList(Controller.Area.Books), "z");
                if (input == "z")
                    break;
                var book = (Buch)Controller.GetObjectThroughNumber(Convert.ToInt32(input), Controller.Area.Books);
                if (book == null)
                    Console.WriteLine("Es wurde kein Buch mit der eingegebenen Id gefunden.");
                else
                {
                    while (true)
                    {
                        var listOfCopies = Controller.GetPresentCopies(book);
                        if (listOfCopies.Count == 0)
                        {
                            Console.WriteLine("Dieses Buch ist zurzeit nicht vorhanden!");
                            copy = null;
                            break;
                        }
                        Console.WriteLine($"Diese {listOfCopies.Count} Exemplare sind vorhanden");
                        OutputOfThings.OutputCopies(listOfCopies, area);
                        input = GetUserInputData("Nummer vom Exemplar", true);
                        copy = (Exemplar)Controller.GetObjectThroughNumber(Convert.ToInt32(input), Controller.Area.Copy);
                        if (copy == null)
                            Console.WriteLine("Es wurde kein Exemplar mit der eingegebenen Id gefunden");
                        else
                        {
                            break;
                        }
                    }
                    break;
                }

            }
            if (input == "z")
                return null;
            return copy;
        }

        public static string GetPerson()
        {
            return GetUserInputData("Name der ausleihenden Person", false);
        }

        public static string GetRentBeginDate()
        {
            var input = "";
            while (true)
            {
                input = GetUserInputData("Datum der Übergabe zum verleihen (Bitte so eingeben: 'TT.MM.JJJJ')", false);
                if (input == "h")
                {
                    input = DateTime.Now.ToString();
                    input = input.Substring(0, input.Length - 9);
                }
                    
                if (Controller.IsItADate(input))
                    break;
            }
            return input;
        }

        public static void EditRent()
        {
            var input = "";
            var property = "";
            var newValue = "";
            while (true)
            {
                OutputOfThings.OutputLists(area);
                input = Input(Controller.GetLowestNumberInList(area), Controller.GetHighestNumberInList(area), "z");
                if (input == "z")
                    break;
                var rent = (Leihvorgang)Controller.GetObjectThroughNumber(Convert.ToInt32(input), area);
                if (rent == null)
                    Console.WriteLine("Es wurde kein Leihvorgang mit der eingegebenen Id gefunden.");
                else
                {
                    Console.Clear();
                    OutputOfThings.OutputObject(rent, area);
                    Console.WriteLine("Was davon wollen Sie ändern?");
                    property = GetUserInputData("Eigenschaft", false);
                    if (property == "Ausleihdatum" || property == "Rückgabedatum")
                    {
                        newValue = GetRentBeginDate();
                    }
                    else if (property == "Buch")
                    {
                        OutputOfThings.OutputLists(Controller.Area.Books);
                        newValue = Input(Controller.GetLowestNumberInList(Controller.Area.Books), Controller.GetHighestNumberInList(Controller.Area.Books), "z");
                        var book = (Buch)Controller.GetObjectThroughNumber(Convert.ToInt32(newValue), Controller.Area.Books);
                        var list = Controller.GetPresentCopies(book);
                        if (list.Count == 0)
                            Console.WriteLine("Dieses Buch ist zurzeit nicht auf Lager.");
                        else
                        {
                            Console.WriteLine($"Diese {list.Count} Exemplare sind vorhanden");
                            OutputOfThings.OutputCopies(list, area);
                            input = GetUserInputData("Nummer vom Exemplar", true);
                            var copy = (Exemplar)Controller.GetObjectThroughNumber(Convert.ToInt32(input), Controller.Area.Copy);
                            if (copy == null)
                                Console.WriteLine("Es wurde kein Exemplar mit der eingegebenen Id gefunden.");
                            else
                            {
                                copy.IstAusgeliehen = true;
                                rent.Buch.IstAusgeliehen = false;
                                rent.LeihvorgangBearbeiten(property, input);
                            }
                                
                        }
                            
                    }
                    else
                    {
                        newValue = GetUserInputData("Neuer Wert", Controller.IsNumbProperty(property));
                    }
                    OutputOfThings.OutputObject(rent, area);
                    rent.LeihvorgangBearbeiten(property, newValue);
                    OutputOfThings.ReadKeyMethod();
                }
            }
        }

        public static void DeleteRent()
        {
            var input = "";
            while (true)
            {
                OutputOfThings.OutputLists(area);
                input = Input(Controller.GetLowestNumberInList(area), Controller.GetHighestNumberInList(area), "z");
                if (input == "z")
                    break;
                var rent = (Leihvorgang)Controller.GetObjectThroughNumber(Convert.ToInt32(input), area);
                if (rent == null)
                    Console.WriteLine("Es wurde kein Leihvorgang mit der eingegebenen Id gefunden.");
                else
                {
                    rent.Buch.IstAusgeliehen = false;
                    Controller.DeleteTheRent(rent);
                    Controller.CreateDelRent(rent);
                }
            }
        }

        /// <summary>
        /// Kategorie Gelöschte Leihvorgänge
        /// </summary>

        public static void CategoryDelRent()
        {
            var input = "";
            while (true)
            {
                Output(new List<string> { "1. Wiederherstellen" });
                input = Input(0, 3, "z");
                Console.Clear();
                if (input == "z")
                    break;
                if (input == "1")
                    RebuildDelRent();
            }
        }

        public static void RebuildDelRent()
        {
            var input = "";
            if(Controller.delRents.Count > 0)
            {
                while (true)
                {
                    OutputOfThings.OutputLists(area);
                    input = Input(Controller.GetLowestNumberInList(area), Controller.GetHighestNumberInList(area), "z");
                    if (input == "z")
                        break;
                    var delRent =(GelöschterLeihvorgang)Controller.GetObjectThroughNumber(Convert.ToInt32(input), area);
                    if (delRent == null)
                        Console.WriteLine("Es wurde kein Leihvorgang mit der eingegebenen Id gefunden.");
                    else
                    {
                        OutputOfThings.OutputObject(delRent, area);
                        Console.WriteLine("Wollen Sie den Leihvorgang wirklich wiederherstellen? [j/n]");
                        input = Console.ReadLine();
                        if (input == "j")
                        {
                            Controller.RebuildRent(delRent);
                            break;
                        }
                        else if (input == "n")
                            break;
                    }
                }
            }
            else
            {
                Console.WriteLine("Keine gelöschten Leihvorgänge vorhanden.");
            }
            
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
