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
                    Console.WriteLine($"Exemplar: {item.ExemplarId} {item.Buch.Titel} von {item.Buch.Autor}. Ist es ausgeliehen: {Controller.IsBookBorow(item)}");
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
                Console.WriteLine($"Link: {newObj.Link}");
            }
            else if (area == Controller.Area.Copy)
            {
                Exemplar newObj = (Exemplar)obj;
                Console.WriteLine($"ID: {newObj.ExemplarId}");
                Console.WriteLine($"Ausgeliehen: {Controller.IsBookBorow(newObj)}");
                Console.WriteLine($"Buch: {newObj.Buch.Titel} von {newObj.Buch.Autor}");
            }
        }
    }
}
