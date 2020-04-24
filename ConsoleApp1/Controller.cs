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
        public static List<GelöschterLeihvorgang> delRents = new List<GelöschterLeihvorgang>();
        public static List<Magazin> magazins = new List<Magazin>();
        public static List<eBook> eBooks = new List<eBook>();
        public static ControllerClass cc;

        public static int lastBookId;
        public static int lastCopyId;
        public static int lastRentId;
        public static int lastDelRentId;
        public static int lastMagazinId;
        public static int lastEBookId;

        public enum Area
        {
            Menu, Book, Magazin, Copy, Rent, DelRent
        }

        public static Exemplar BekommeExemplarDurchId(int id)
        {
            var buch = new Buch();
            foreach (var item in copies)
            {
                if (item.ExemplarId == id)
                    return item;
            }
            return null;
        }

        public static object GetObjectThroughNumber(int id, Area area)
        {
            if(area == Area.Book)
            {
                foreach (var item in books)
                {
                    if (item.BuchId == id)
                        return item;
                }
            }
            if (area == Area.Magazin)
            {
                foreach (var item in magazins)
                {
                    if (item.MagazinId == id)
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
            else if (area == Area.Rent)
            {
                foreach (var item in rents)
                {
                    if (item.LeihvorgangId == id)
                        return item;
                }
            }
            else if(area == Area.DelRent)
            {
                foreach(var item in delRents)
                {
                    if (item.GelLeihvorgangId == id)
                        return item;
                }
            }
            return null;
        }

        public static bool IsItADate(string input)
        {
            input = ChangeToEnglishDate(input);
            return DateTime.TryParse(input, out DateTime date);
        }

        public static string ChangeToEnglishDate(string input)
        {
            var newInput = "";
            for (int i = 0; i < input.Length; i++)
            {
                if (i == 2 || i == 5)
                    newInput += "/";
                else
                    newInput += input[i];
            }
            return newInput;
        }

        public static void FillList(object newObject)
        {
            if(newObject.GetType() == typeof(Buch))
            {
                bool isInList = false;
                var newItem = (Buch)newObject;
                foreach (var item in books)
                {
                    if (item.BuchId == newItem.BuchId)
                    {
                        isInList = true;
                        break;
                    }
                }
                if (!isInList)
                    books.Add(newItem);
            }
            else
            {
                bool isInList = false;
                var newItem = (Magazin)newObject;
                foreach (var item in magazins)
                {
                    if (item.MagazinId == newItem.MagazinId)
                    {
                        isInList = true;
                        break;
                    }
                }
                if (!isInList)
                    magazins.Add(newItem);
            }
            
        }

        public static bool IsNumbProperty(string property)
        {
            return property == "Jahr" || property == "Seiten" || property == "Exemplare";
        }

        public static void CreateDelRent(Leihvorgang rent)
        {
            var delRent = new GelöschterLeihvorgang(rent.LeihvorgangId, rent.Buch, rent.Person, rent.Ausleihdatum, rent.Rückgabedatum);
            delRents.Add(delRent);
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

        public static void DeleteMagazin(Magazin magazine)
        {
            for (int i = 0; i < magazins.Count; i++)
            {
                if (magazins[i] == magazine)
                {
                    magazins.Remove(magazins[i]);
                    i--;
                }
            }
        }

        public static void DeleteCopies(object _object)
        {
            var type = _object.GetType();
            var isBook = false;
            if (type == typeof(Buch))
                isBook = true;
            for (int i = 0; i < copies.Count; i++)
            {
                if (isBook)
                {
                    var newObject = (Buch)_object;
                    var newItem = (Buch)copies[i].Buch;
                    if (newItem.Autor == newObject.Autor && newItem.Titel == newObject.Titel)
                    {
                        copies.Remove(copies[i]);
                        i--;
                    }
                }
                else
                {
                    var newObject = (Magazin)_object;
                    var newItem = (Magazin)copies[i].Buch;
                    if (newItem.Autor == newObject.Autor && newItem.Titel == newObject.Titel)
                    {
                        copies.Remove(copies[i]);
                        i--;
                    }
                }
                
            }
        }

        public static void DeleteCopies(object _object, int count)
        {
            var type = _object.GetType();
            var isBook = false;
            if (type == typeof(Buch))
                isBook = true;
            for (int j = 0; j < count; j++)
            {
                for (int i = 0; i < copies.Count; i++)
                {
                    if (isBook)
                    {
                        var newObject = (Buch)_object;
                        var newItem = (Buch)copies[i].Buch;
                        if (newItem.Autor == newObject.Autor && newItem.Titel == newObject.Titel)
                        {
                            copies.Remove(copies[i]);
                            break;
                        }
                    }
                    else
                    {
                        var newObject = (Magazin)_object;
                        var newItem = (Magazin)copies[i].Buch;
                        if (newItem.Autor == newObject.Autor && newItem.Titel == newObject.Titel)
                        {
                            copies.Remove(copies[i]);
                            break;
                        }
                    }
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

        public static void DeleteTheRent(Leihvorgang rent)
        {
            for (int i = 0; i < rents.Count; i++)
            {
                if(rents[i] == rent)
                {
                    rents.Remove(rent);
                    break;
                }
                    
            }
        }

        public static bool HaveDeletingBookCopieInRents(object _object)
        {
            var type = _object.GetType();
            var isBook = false;
            if (type == typeof(Buch))
                isBook = true;
            var isCopieInRent = false;
            foreach (var item in copies)
            {
                if (isBook)
                {
                    var newObject = (Buch)_object;
                    var newItem = (Buch)item.Buch;
                    if (newItem.Autor == newObject.Autor && newItem.Titel == newObject.Titel && item.IstAusgeliehen)
                    {
                        isCopieInRent = true;
                        break;
                    }
                }
                else
                {
                    var newObject = (Magazin)_object;
                    var newItem = (Magazin)item.Buch;
                    if (newItem.Autor == newObject.Autor && newItem.Titel == newObject.Titel && item.IstAusgeliehen)
                    {
                        isCopieInRent = true;
                        break;
                    }
                }
                
            }
            if (isCopieInRent)
            {
                Console.WriteLine("Das Buch konnte nicht gelöscht werden! Da sich Exemplare von diesem Buch noch im Ausleih befinden.");
                OutputOfThings.ReadKeyMethod();
            }
            return isCopieInRent;
                
        }

        public static void RebuildRent(GelöschterLeihvorgang delRent)
        {
            if (IsCopyInRebuildingRentPresent(delRent))
            {
                Leihvorgang rent = new Leihvorgang(delRent.LeihvorgangId, delRent.Buch, delRent.Person, delRent.Ausleihdatum, delRent.Rückgabedatum);
                delRents.Remove(delRent);
                rents.Add(rent);
                rent.Buch.IstAusgeliehen = true;
                Console.WriteLine("Die Wiederherstellung war erfolgreich.");
            }
            else
                Console.WriteLine("Das Buch in diesem Leihvorgang ist zurzeit schon ausgeliehen. Der Wiederherstellung war nicht erfolgreich.");
            OutputOfThings.ReadKeyMethod();
            
        }

        public static bool IsCopyInRebuildingRentPresent(GelöschterLeihvorgang delRent)
        {
            return !delRent.Buch.IstAusgeliehen;
        }

        public static string IsBookBorowString(Exemplar copy)
        {
            if (copy.IstAusgeliehen)
                return "Ja";
            else
                return "Nein";
        }

        public static bool IsCopyInRent(Exemplar copy)
        {
            foreach (var item in rents)
            {
                if (item.Buch == copy)
                    return true;
            }
            return false;
        }

        public static List<Exemplar> GetPresentCopies(object _object)
        {
            var type = _object.GetType();
            var isBook = false;
            if (type == typeof(Buch))
                isBook = true;

            var presentCopies = new List<Exemplar>();
            foreach (var item in copies)
            {
                if (isBook)
                {
                    var newObject = (Buch)_object;
                    var newItem = (Buch)item.Buch;
                    if (newItem.Autor == newObject.Autor && newItem.Titel == newObject.Titel && !item.IstAusgeliehen)
                        presentCopies.Add(item);
                }
                else
                {
                    var newObject = (Magazin)_object;
                    var newItem = (Magazin)item.Buch;
                    if (newItem.Autor == newObject.Autor && newItem.Titel == newObject.Titel && !item.IstAusgeliehen)
                        presentCopies.Add(item);
                }
                
            }
            return presentCopies;
        }


        public static int GetLowestNumberInList(Area area)
        {
            var id = 0;
            if(area == Area.Book)
            {
                foreach (var item in books)
                {
                    if (id == 0)
                        id = item.BuchId;

                    else if (id >= item.BuchId)
                        id = item.BuchId;
                }
            }
            else if (area == Area.Magazin)
            {
                foreach (var item in magazins)
                {
                    if (id == 0)
                        id = item.MagazinId;

                    else if (id >= item.MagazinId)
                        id = item.MagazinId;
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
            else if (area == Area.DelRent)
            {
                foreach (var item in delRents)
                {
                    if (id == 0)
                        id = item.GelLeihvorgangId;

                    else if (id >= item.GelLeihvorgangId)
                        id = item.GelLeihvorgangId;
                }
            }
            return id;
        }

        public static int GetHighestNumberInList(Area area)
        {
            var id = 0;
            if (area == Area.Book)
            {
                foreach (var item in books)
                {
                    if (id == 0)
                        id = item.BuchId;

                    else if (id <= item.BuchId)
                        id = item.BuchId;
                }
            }
            else if (area == Area.Magazin)
            {
                foreach (var item in magazins)
                {
                    if (id == 0)
                        id = item.MagazinId;

                    else if (id <= item.MagazinId)
                        id = item.MagazinId;
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
            else if (area == Area.DelRent)
            {
                foreach (var item in delRents)
                {
                    if (id == 0)
                        id = item.GelLeihvorgangId;

                    else if (id <= item.GelLeihvorgangId)
                        id = item.GelLeihvorgangId;
                }
            }
            return id;
        }

        public static bool IsTypeABook(object _object)
        {
            return _object.GetType() == typeof(Buch);
        }

        public static void CreateEBook(object obj)
        {
            var buch = (Buch)obj;
            eBook eBook = new eBook(buch, CreateDownloadLink(buch.Titel));
        }

        public static string CreateDownloadLink(string title)
        {
            if (title.Contains(" "))
                title.Replace(' ', '_');
            return title;
        }
    }

    class ControllerClass
    {
        public int LastBookId { get; set; }
        public int LastCopyId { get; set; }
        public int LastRentId { get; set; }
        public int LastDelRentId { get; set; }
        public int LastMagazinId { get; set; }
        public int LastEBookId { get; set; }

        public ControllerClass()
        {
            LastBookId = Controller.lastBookId;
            LastCopyId = Controller.lastCopyId;
            LastRentId = Controller.lastRentId;
            LastDelRentId = Controller.lastDelRentId;
            LastMagazinId = Controller.lastMagazinId;
            LastEBookId = Controller.lastEBookId;

        }
    }
}
