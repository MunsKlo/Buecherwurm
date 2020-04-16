using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
    class Controller
    {
        public static List<Exemplar> copies = new List<Exemplar>();
        public static List<Leihvorgang> leihvorgänge = new List<Leihvorgang>();
        public static List<Buch> books = new List<Buch>();
        public static List<Leihvorgang> rents = new List<Leihvorgang>();
        public static ControllerClass cc;

        public static int lastBookId;
        public static int lastCopyId;

        public enum Area
        {
            Menu, Books, Copy, Rent
        }

        public static Exemplar BekommeBuchDurchExemplarId(int id)
        {
            var buch = new Buch();
            foreach (var item in copies)
            {
                if (item.ExemplarId == id)
                    return item;
            }
            return null;
        }

        public static object BekommeObjektDurchNummer(int id, Area area)
        {
            if(area == Area.Books)
            {
                foreach (var item in books)
                {
                    if (item.BuchId == id)
                        return item;
                }
            }
            else if(area == Area.Copy)
            {
                foreach (var item in copies)
                {
                    if (item.ExemplarId == id)
                        return item;
                }
            }
            return null;
        }

        public static Person BekommePersonDurchPersonId(int id)
        {
            return new Person();
        }

        private void LeihvorgangErstellen(string name, Exemplar Buch, string ausleihdatum)
        {

        }

        public static void FillBookList(Buch newBook)
        {
            bool isBookInList = false;
            foreach (var item in books)
            {
                if (item.BuchId == newBook.BuchId)
                {
                    isBookInList = true;
                    break;
                }
            }
            if (!isBookInList)
                books.Add(newBook);
        }

        public static bool IsNumbProperty(string property)
        {
            return property == "Jahr" || property == "Seiten" || property == "Exemplare";
        }

        public static void DeleteBook(Buch book)
        {
            for (int i = 0; i < books.Count; i++)
            {
                if (books[i] == book)
                {
                    books.Remove(books[i]);
                    i--;
                }
            }
        }

        public static void DeleteCopies(Buch book)
        {
            for (int i = 0; i < copies.Count; i++)
            {
                if(copies[i].Buch == book)
                {
                    copies.Remove(copies[i]);
                    i--;
                }
            }
        }

        public static void DeleteTheCopy(Exemplar copy)
        {
            for (int i = 0; i < copies.Count; i++)
            {
                if(copies[i] == copy)
                {
                    copies.Remove(copy);
                    break;
                }

            }
        }

        public static string IsBookBorow(Exemplar copy)
        {
            if (copy.IstAusgeliehen)
                return "Ja";
            else
                return "Nein";
        }

        public static int GetLowestNumberInList(Area area)
        {
            var id = 0;
            if(area == Area.Books)
            {
                foreach (var item in books)
                {
                    if (id == 0)
                        id = item.BuchId;

                    else if (id >= item.BuchId)
                        id = item.BuchId;
                }
            }
            else if(area == Area.Copy)
            {
                foreach (var item in copies)
                {
                    if (id == 0)
                        id = item.ExemplarId;

                    else if (id >= item.ExemplarId)
                        id = item.ExemplarId;
                }
            }
            else if(area == Area.Rent)
            {
                foreach (var item in rents)
                {
                    if (id == 0)
                        id = item.LeihvorgangId;

                    else if (id >= item.LeihvorgangId)
                        id = item.LeihvorgangId;
                }
            }
            return id;
        }

        public static int GetHighestNumberInList(Area area)
        {
            var id = 0;
            if (area == Area.Books)
            {
                foreach (var item in books)
                {
                    if (id == 0)
                        id = item.BuchId;

                    else if (id <= item.BuchId)
                        id = item.BuchId;
                }
            }
            else if (area == Area.Copy)
            {
                foreach (var item in copies)
                {
                    if (id == 0)
                        id = item.ExemplarId;

                    else if (id <= item.ExemplarId)
                        id = item.ExemplarId;
                }
            }
            else if (area == Area.Rent)
            {
                foreach (var item in rents)
                {
                    if (id == 0)
                        id = item.LeihvorgangId;

                    else if (id <= item.LeihvorgangId)
                        id = item.LeihvorgangId;
                }
            }
            return id;
        }
    }

    class ControllerClass
    {
        public int LastBookId { get; set; }
        public int LastCopyId { get; set; }

        public ControllerClass()
        {
            LastBookId = Controller.lastBookId;
            LastCopyId = Controller.lastCopyId;
        }
    }
}
