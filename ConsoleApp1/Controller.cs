using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ConsoleApp1
{
    class Controller
    {
        public static List<Exemplar> copies = new List<Exemplar>();
        public static List<Leihvorgang> leihvorgänge = new List<Leihvorgang>();
        public static List<IPh_Produkt> products = new List<IPh_Produkt>();
        public static List<Leihvorgang> rents = new List<Leihvorgang>();
        public static List<GelöschterLeihvorgang> delRents = new List<GelöschterLeihvorgang>();
        public static List<IA_Produkt> eProducts = new List<IA_Produkt>();
        public static ControllerClass cc;

        public static int lastProductId;
        public static int lastCopyId;
        public static int lastRentId;
        public static int lastDelRentId;
        public static int lastEId;

        public enum Area
        {
            Menu, Book, Magazin, Copy, Rent, DelRent, EProduct
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
                foreach (var item in products)
                {
                    if (item.Id == id)
                        return item;
                }
            }
            if (area == Area.Magazin)
            {
                foreach (var item in products)
                {
                    if (item.Id == id)
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
            else if(area == Area.EProduct)
            {
                foreach (var item in eProducts)
                {
                    var newProduct = (IPh_Produkt)item.Produkt;
                    if (newProduct.Id == id)
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

        public static void FillLists(object newObject)
        {
            var isInList = false;
            IPh_Produkt newItem = (IPh_Produkt)newObject;
            foreach (var item in products)
            {
                if (item.Id == newItem.Id)
                {
                    isInList = true;
                    break;
                }
            }
            if (!isInList)
                products.Add(newItem);
            if(newObject.GetType() == typeof(Buch) && !File.Exists("ebooks.json"))
            {
                CreateEBook(newObject);
            }
        }

        public static void FillLists(object newObject, object eBook)
        {
            var isInList = false;
            IPh_Produkt newItem = (IPh_Produkt)newObject;
            foreach (var item in products)
            {
                if (item.Id == newItem.Id)
                {
                    isInList = true;
                    break;
                }
            }
            if (!isInList)
                products.Add(newItem);
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
            for (int i = 0; i < products.Count; i++)
            {
                if (products[i] == book)
                {
                    products.Remove(products[i]);
                    i--;
                }
            }
        }

        public static void DeleteMagazin(Magazin magazine)
        {
            for (int i = 0; i < products.Count; i++)
            {
                if (products[i] == magazine)
                {
                    products.Remove(products[i]);
                    i--;
                }
            }
        }

        public static void DeleteCopies(object _object)
        {
            for (int i = 0; i < copies.Count; i++)
            {
                var newObject = (Buch)_object;
                var newItem = (Buch)copies[i].Produkt;
                if (newItem.Autor == newObject.Autor && newItem.Titel == newObject.Titel)
                {
                    copies.Remove(copies[i]);
                    i--;
                } 
            }
        }

        public static void DeleteCopies(object _object, int count)
        {
            for (int j = 0; j < count; j++)
            {
                for (int i = 0; i < copies.Count; i++)
                {
                    var newObject = (IPh_Produkt)_object;
                    var newItem = (IPh_Produkt)copies[i].Produkt;
                    if (newItem.Id == newObject.Id)
                    {
                        copies.Remove(copies[i]);
                        break;
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
            var isCopieInRent = false;
            foreach (var item in copies)
            {
                var newObject = (IPh_Produkt)_object;
                var newItem = (IPh_Produkt)item.Produkt;
                if (newItem.Id == newObject.Id && item.IstAusgeliehen)
                {
                    isCopieInRent = true;
                    break;
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
                if(rent.Buch.GetType() == typeof(Exemplar))
                {
                    var copy = (Exemplar)rent.Buch;
                    copy.IstAusgeliehen = true;
                }
                Console.WriteLine("Die Wiederherstellung war erfolgreich.");
            }
            else
                Console.WriteLine("Das Buch in diesem Leihvorgang ist zurzeit schon ausgeliehen. Der Wiederherstellung war nicht erfolgreich.");
            OutputOfThings.ReadKeyMethod();
            
        }

        public static bool IsCopyInRebuildingRentPresent(GelöschterLeihvorgang delRent)
        {
            if(delRent.Buch.GetType() == typeof(Exemplar))
            {
                var copy = (Exemplar)delRent.Buch;
                return !copy.IstAusgeliehen;
            }
            return true;
            
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
            var presentCopies = new List<Exemplar>();
            foreach (var item in copies)
            {
                var newObject = (IPh_Produkt)_object;
                var newItem = (IPh_Produkt)item.Produkt;
                if (newItem.Id == newObject.Id && !item.IstAusgeliehen)
                    presentCopies.Add(item);
            }
            return presentCopies;
        }


        public static int GetLowestNumberInList(Area area)
        {
            var id = 0;
            if(area == Area.Book)
            {
                foreach (var item in products)
                {
                    if (id == 0)
                        id = item.Id;

                    else if (id >= item.Id)
                        id = item.Id;
                }
            }
            else if (area == Area.Magazin)
            {
                foreach (var item in products)
                {
                    if (id == 0)
                        id = item.Id;

                    else if (id >= item.Id)
                        id = item.Id;
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
                foreach (var item in products)
                {
                    if (id == 0)
                        id = item.Id;

                    else if (id <= item.Id)
                        id = item.Id;
                }
            }
            else if (area == Area.Magazin)
            {
                foreach (var item in products)
                {
                    if (id == 0)
                        id = item.Id;

                    else if (id <= item.Id)
                        id = item.Id;
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
            var product = (IPh_Produkt)obj;
            EBook eBook = new EBook(obj, CreateDownloadLink(product.Titel));
            eProducts.Add(eBook);
        }

        public static void CreateEPapper(object obj)
        {
            var product = (IPh_Produkt)obj;
            EPapper ePapper = new EPapper(obj, CreateDownloadLink(product.Titel));
            eProducts.Add(ePapper);
        }

        public static bool YesAndNoInput(string description)
        {
            var input = "";
            while (true)
            {
                Console.WriteLine(description);
                input = Console.ReadLine();
                if (input == "j" || input == "n")
                    break;
            }
            return input == "j";
        }

        public static string CreateDownloadLink(string title)
        {
            if (title.Contains(" "))
                return "http://www.buecherwurm/download/" + title.Replace(' ', '_');
            return "http://www.buecherwurm/download/" + title;
        }
    }

    class ControllerClass
    {
        public int LastBookId { get; set; }
        public int LastCopyId { get; set; }
        public int LastRentId { get; set; }
        public int LastDelRentId { get; set; }
        public int LastEBookId { get; set; }

        public ControllerClass()
        {
            LastBookId = Controller.lastProductId;
            LastCopyId = Controller.lastCopyId;
            LastRentId = Controller.lastRentId;
            LastDelRentId = Controller.lastDelRentId;
            LastEBookId = Controller.lastEId;

        }
    }
}
