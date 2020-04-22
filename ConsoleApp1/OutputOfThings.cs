using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
    class OutputOfThings
    {
        public static void OutputLists(Controller.Area area)
        {
            if(area == Controller.Area.Book)
            {
                foreach (var item in Controller.books)
                {
                    Console.WriteLine($"Buch: {item.BuchId} {item.Titel} von {item.Autor}");
                    Console.WriteLine("========================================================");
                }
            }
            else if(area == Controller.Area.Magazin)
            {
                foreach (var item in Controller.magazins)
                {
                    Console.WriteLine($"Buch: {item.MagazinId} {item.Titel} von {item.Autor}");
                    Console.WriteLine("========================================================");
                }
            }
            else if(area == Controller.Area.Copy)
            {
                foreach (var item in Controller.copies)
                {
                    var newItem = (Buch)item.Buch;
                    Console.WriteLine($"Exemplar: {item.ExemplarId} {newItem.Titel} von {newItem.Autor}. Ist es ausgeliehen: {Controller.IsBookBorowString(item)}");
                }
            }
            else if (area == Controller.Area.Rent)
            {
                foreach (var item in Controller.rents)
                {
                    var isBook = false;
                    if (item.Buch.Buch.GetType() == typeof(Buch))
                        isBook = true;
                    if (isBook)
                    {
                        var newItem = (Buch)item.Buch.Buch;
                        Console.WriteLine($"ID: {item.LeihvorgangId} | Person: {item.Person} | Buch: {newItem.Titel} von {newItem.Autor} | Rückgabe: {item.Rückgabedatum}");
                    }
                    else
                    {
                        var newItem = (Magazin)item.Buch.Buch;
                        Console.WriteLine($"ID: {item.LeihvorgangId} | Person: {item.Person} | Buch: {newItem.Titel} von {newItem.Autor} | Rückgabe: {item.Rückgabedatum}");
                    }
                    
                }
            }
            else if (area == Controller.Area.DelRent)
            {
                foreach (var item in Controller.delRents)
                {
                    var isBook = false;
                    if (item.Buch.Buch.GetType() == typeof(Buch))
                        isBook = true;
                    if (isBook)
                    {
                        var newItem = (Buch)item.Buch.Buch;
                        Console.WriteLine($"ID: {item.GelLeihvorgangId} | Person: {item.Person} | Buch: {newItem.Titel} von {newItem.Autor} | Rückgabe: {item.Rückgabedatum}");
                    }
                    else
                    {
                        var newItem = (Magazin)item.Buch.Buch;
                        Console.WriteLine($"ID: {item.GelLeihvorgangId} | Person: {item.Person} | Buch: {newItem.Titel} von {newItem.Autor} | Rückgabe: {item.Rückgabedatum}");
                    }
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
            if (area == Controller.Area.Book)
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
                var isBook = Controller.IsTypeABook(newObj.Buch);
                if (isBook)
                {
                    var newItem = (Buch)newObj.Buch;
                    Console.WriteLine($"Buch: {newItem.Titel} von {newItem.Autor}");
                }
                else
                {
                    var newItem = (Magazin)newObj.Buch;
                    Console.WriteLine($"Buch: {newItem.Titel} von {newItem.Autor}");
                }
                
            }
            else if (area == Controller.Area.Rent)
            {
                Leihvorgang newObj = (Leihvorgang)obj;
                var isBook = Controller.IsTypeABook(newObj.Buch.Buch);
                
                Console.WriteLine($"ID: {newObj.LeihvorgangId}");
                Console.WriteLine($"Person: {newObj.Person}");
                if (isBook)
                {
                    var newItem = (Buch)newObj.Buch.Buch;
                    Console.WriteLine($"Buch: {newItem.Titel} von {newItem.Autor}");
                }
                else
                {
                    var newItem = (Magazin)newObj.Buch.Buch;
                    Console.WriteLine($"Buch: {newItem.Titel} von {newItem.Autor}");
                }
                    
                Console.WriteLine($"Ausleihadatum: {newObj.Ausleihdatum}");
                Console.WriteLine($"Rückgabedatum: {newObj.Rückgabedatum}");
            }
            else if(area == Controller.Area.DelRent)
            {
                GelöschterLeihvorgang newObj = (GelöschterLeihvorgang)obj;
                var isBook = Controller.IsTypeABook(newObj.Buch.Buch);
                Console.WriteLine($"ID: {newObj.GelLeihvorgangId}");
                if (isBook)
                {
                    var newItem = (Buch)newObj.Buch.Buch;
                    Console.WriteLine($"Buch: {newItem.Titel} von {newItem.Autor}");
                }
                Console.WriteLine($"Ausgeliehene Person: {newObj.Person}");
                Console.WriteLine($"Ausleihdatum: {newObj.Ausleihdatum}");
                Console.WriteLine($"Rückgabedatum: {newObj.Rückgabedatum}");
            }
        }

        public static void ReadKeyMethod()
        {
            Console.WriteLine("Drücken Sie Enter um weiter zu kommen.");
            Console.ReadKey();
            Console.Clear();
        }
    }
}
