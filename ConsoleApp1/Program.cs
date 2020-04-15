using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;


namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            var jsonString = File.ReadAllText("books.json");
            if (!File.Exists("test.json"))
            {
                JSONIO.ReadDataBooks();
                JSONIO.SaveData("test.json", Controller.exemplare);
            }
            JSONIO.ReadDataCopies();
            StartProgram();
        }

        private static void StartProgram()
        {
            var input = "";
            while(input != "b")
            {
                Console.WriteLine("===========================================================================");
                Console.WriteLine("Willkommen beim Verwaltungsprogramm 'Bücherwurm'!");
                Console.WriteLine("===========================================================================");
                Console.WriteLine();
                Console.WriteLine("Schreiben Sie die Nummer der Kategorie oder 'b' um das Programm zu beenden!");
                Output(new List<string> {"1. Bücher", "2. Exemplare", "3. Leihvorgang" });
                input = Input(0, 4, "b");
                Console.Clear();
                ChooseCategory(input);
            }
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
            var input = "";
            while (true)
            {
                input = Console.ReadLine();
                if (int.TryParse(input, out int numb) && numb > minNumb && numb < maxNumb)
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
                CategoryBook();
        }

        private static void CategoryBook()
        {
            var input = "";
            while (input != "z")
            {
                Console.WriteLine("Schreiben Sie die Nummer oder 'z' um zurück zu kommen!");
                Output(new List<string> { "1. Erstellen", "2. Bearbeiten", "3. Löschen" });
                input = Input(0, 4, "z");
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
            Console.WriteLine("Füllen Sie die Daten auf");
            var author = GetUserInputData("Autor", false) ;
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
            if(Controller.books.Count == 0)
            {
                var input = "";
                while (input != "z")
                {
                    Input(Controller.books.Count - (Controller.books.Count - 1), Controller.books.Count, "z");
                    var buch = Controller.BekommeBuchDurchNummer();
                    //jo du wart hier

                }
                
            }
        }

        private static void DeleteBook()
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
