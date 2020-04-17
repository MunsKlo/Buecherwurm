using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
    class OutputOfThings
    {
        public static void OutputLists(Controller.Area area)
        {
            if(area == Controller.Area.Books)
            {
                foreach (var item in Controller.books)
                {
                    Console.WriteLine($"Buch: {item.BuchId} {item.Titel} von {item.Autor}");
                    Console.WriteLine("========================================================");
                }
            }
            else if(area == Controller.Area.Copy)
            {
                foreach (var item in Controller.copies)
                {
                    Console.WriteLine($"Exemplar: {item.ExemplarId} {item.Buch.Titel} von {item.Buch.Autor}. Ist es ausgeliehen: {Controller.IsBookBorowString(item)}");
                }
            }
            else if (area == Controller.Area.Rent)
            {
                foreach (var item in Controller.rents)
                {
                    Console.WriteLine($"ID: {item.LeihvorgangId} | Person: {item.Person} | Buch: {item.Buch.Buch.Titel} von {item.Buch.Buch.Autor} | Rückgabe: {item.Rückgabedatum}");
                }
            }
            else if (area == Controller.Area.DelRent)
            {
                foreach (var item in Controller.delRents)
                {
                    Console.WriteLine($"ID: {item.GelLeihvorgangId} | Person: {item.Person} | Buch: {item.Buch.Buch.Titel} von {item.Buch.Buch.Autor} | Rückgabe: {item.Rückgabedatum}");
                }
            }

        }

        public static void OutputCopies(List<Exemplar> list, Controller.Area area)
        {
            if(area == Controller.Area.Rent)
            {
                foreach (var item in list)
                {
                    Console.WriteLine($"Exemplar: {item.ExemplarId}");
                }
            }
        }

        public static void OutputObject(object obj, Controller.Area area)
        {
            Console.WriteLine("Infromationen zum ausgewählten Gegenstand:");
            if (area == Controller.Area.Books)
            {
                Buch newObj = (Buch)obj;
                Console.WriteLine($"ID: {newObj.BuchId}");
                Console.WriteLine($"Titel: {newObj.Titel}");
                Console.WriteLine($"Autor: {newObj.Autor}");
                Console.WriteLine($"Seiten: {newObj.Seiten}");
                Console.WriteLine($"Jahr: {newObj.Jahr}");
                Console.WriteLine($"Sprache: {newObj.Sprache}");
                Console.WriteLine($"Bildlink: {newObj.BildLink}");
                Console.WriteLine($"Exemplare: {newObj.Exemplare}");
                Console.WriteLine($"Link: {newObj.Link}");
            }
            else if (area == Controller.Area.Copy)
            {
                Exemplar newObj = (Exemplar)obj;
                Console.WriteLine($"ID: {newObj.ExemplarId}");
                Console.WriteLine($"Ausgeliehen: {Controller.IsBookBorowString(newObj)}");
                Console.WriteLine($"Buch: {newObj.Buch.Titel} von {newObj.Buch.Autor}");
            }
            else if (area == Controller.Area.Rent)
            {
                Leihvorgang newObj = (Leihvorgang)obj;
                Console.WriteLine($"ID: {newObj.LeihvorgangId}");
                Console.WriteLine($"Person: {newObj.Person}");
                Console.WriteLine($"Buch: {newObj.Buch.Buch.Titel} von {newObj.Buch.Buch.Autor}");
                Console.WriteLine($"Ausleihadatum: {newObj.Ausleihdatum}");
                Console.WriteLine($"Rückgabedatum: {newObj.Rückgabedatum}");
            }
            else if(area == Controller.Area.DelRent)
            {
                GelöschterLeihvorgang newObj = (GelöschterLeihvorgang)obj;
                Console.WriteLine($"ID: {newObj.GelLeihvorgangId}");
                Console.WriteLine($"Buch: {newObj.Buch.Buch.Titel} von {newObj.Buch.Buch.Autor}");
                Console.WriteLine($"Ausgeliehene Person: {newObj.Person}");
                Console.WriteLine($"Ausleihdatum: {newObj.Ausleihdatum}");
                Console.WriteLine($"Rückgabedatum: {newObj.Rückgabedatum}");
            }
        }

        public static void ReadKeyMethod()
        {
            Console.WriteLine("Drücken Sie Enter um weiter zu kommen.");
            Console.ReadKey();
        }
    }
}
