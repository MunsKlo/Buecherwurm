﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace ConsoleApp1
{
    class JSONIO
    {
        public static void ReadData(string filename)
        {
            var jsonString = File.ReadAllText(filename);
            var product = new object();
            if(filename == "books.json")
            {
                using (JsonDocument js = JsonDocument.Parse(jsonString))
                {
                    foreach (var item in js.RootElement.EnumerateArray())
                    {
                        var author = item.GetProperty("author").ToString();
                        var country = item.GetProperty("country").ToString();
                        var imageLink = item.GetProperty("imageLink").ToString();
                        var language = item.GetProperty("language").ToString();
                        var link = item.GetProperty("link").ToString();
                        var pages = item.GetProperty("pages").ToString();
                        var title = item.GetProperty("title").ToString();
                        var year = item.GetProperty("year").ToString();

                        product = new Buch(author, country, imageLink, language, link, pages, title, year);
                        Controller.FillLists(product);
                    }
                }
            }
            else
            {
                using (JsonDocument js = JsonDocument.Parse(jsonString))
                {
                    foreach (var item in js.RootElement.EnumerateArray())
                    {
                        var author = item.GetProperty("Verlag / Herausgeber").ToString();
                        var title = item.GetProperty("Titel").ToString();
                        var group = item.GetProperty("Gruppe").ToString();
                        var topicGroup = item.GetProperty("Sachgruppe").ToString();

                        product = new Magazin(author, title, group, topicGroup);
                        Controller.FillLists(product);
                    }
                }
            }
            

        }

        public static void SaveData<T>(string filename, List<T> listObj)
        {
            var jsonString = "[\n";
            for (int i = 0; i < listObj.Count; i++)
            {
                jsonString += JsonSerializer.Serialize(listObj[i]);
                if(i != listObj.Count - 1)
                    jsonString += ",\n";
            }
            jsonString += "]";
            File.WriteAllText(filename, jsonString);
        }

        public static void ReadDataCopies()
        {
            var jsonString = File.ReadAllText("copies.json");
            Controller.copies = new List<Exemplar>();
            Controller.products = new List<IPh_Produkt>();
            Controller.eBooks = new List<eBook>();
            var obj = new Object();
            using (JsonDocument js = JsonDocument.Parse(jsonString))
            {
                foreach (var item in js.RootElement.EnumerateArray())
                {
                    if (item.GetProperty("Buch").TryGetProperty("BildLink", out JsonElement newThing))
                    {
                        var id = Convert.ToInt32(item.GetProperty("Buch").GetProperty("ProduktId").ToString());
                        var author = item.GetProperty("Buch").GetProperty("Autor").ToString();
                        var country = item.GetProperty("Buch").GetProperty("Land").ToString();
                        var imageLink = item.GetProperty("Buch").GetProperty("BildLink").ToString();
                        var language = item.GetProperty("Buch").GetProperty("Sprache").ToString();
                        var link = item.GetProperty("Buch").GetProperty("Link").ToString();
                        var pages = Convert.ToInt32(item.GetProperty("Buch").GetProperty("Seiten").ToString());
                        var title = item.GetProperty("Buch").GetProperty("Titel").ToString();
                        var year = Convert.ToInt32(item.GetProperty("Buch").GetProperty("Jahr").ToString());
                        var copies = Convert.ToInt32(item.GetProperty("Buch").GetProperty("Exemplare").ToString());
                        obj = new Buch(id, author, country, imageLink, language, link, pages.ToString(), title, year.ToString(), copies.ToString());
                    }
                    else
                    {
                        var id = item.GetProperty("Buch").GetProperty("ProduktId").ToString();
                        var author = item.GetProperty("Buch").GetProperty("Autor").ToString();
                        var title = item.GetProperty("Buch").GetProperty("Titel").ToString();
                        var group = item.GetProperty("Buch").GetProperty("Gruppe").ToString();
                        var topicGroup = item.GetProperty("Buch").GetProperty("Sachgruppe").ToString();
                        var copies = Convert.ToInt32(item.GetProperty("Buch").GetProperty("Exemplare").ToString());
                        obj = new Magazin(Convert.ToInt32(id), author, title, copies.ToString(), group, topicGroup);
                    }
                    
                    var exemplarId = item.GetProperty("ExemplarId").ToString();
                    var istAusgeliehen = item.GetProperty("IstAusgeliehen").ToString();

                    

                    
                    var copy = new Exemplar(Convert.ToInt32(exemplarId), Convert.ToBoolean(istAusgeliehen), obj);
                    Controller.copies.Add(copy);
                    Controller.FillLists(obj);
                }
            }
        }

        public static void ReadDataRents()
        {
            var jsonString = File.ReadAllText("rents.json");
            using (JsonDocument js = JsonDocument.Parse(jsonString))
            {
                foreach (var item in js.RootElement.EnumerateArray())
                {
                    var rentId = Convert.ToInt32(item.GetProperty("LeihvorgangId").ToString());
                    var person = item.GetProperty("Person").ToString();
                    var firstDate = item.GetProperty("Ausleihdatum").ToString();
                    var secondDate = item.GetProperty("Rückgabedatum").ToString();

                    var copyId = Convert.ToInt32(item.GetProperty("Buch").GetProperty("ExemplarId").ToString());
                    var copy =(Exemplar) Controller.GetObjectThroughNumber(Convert.ToInt32(copyId), Controller.Area.Copy);

                    Leihvorgang rent = new Leihvorgang(rentId, copy, person, firstDate, secondDate);
                    Controller.rents.Add(rent);
                }
            }
        }

        public static void ReadDataDelRents()
        {
            var jsonString = File.ReadAllText("delrents.json");
            using (JsonDocument js = JsonDocument.Parse(jsonString))
            {
                foreach (var item in js.RootElement.EnumerateArray())
                {
                    var rentDelId = Convert.ToInt32(item.GetProperty("GelLeihvorgangId").ToString());
                    var rentId = Convert.ToInt32(item.GetProperty("LeihvorgangId").ToString());
                    var person = item.GetProperty("Person").ToString();
                    var firstDate = item.GetProperty("Ausleihdatum").ToString();
                    var secondDate = item.GetProperty("Rückgabedatum").ToString();

                    var copyId = Convert.ToInt32(item.GetProperty("Buch").GetProperty("ExemplarId").ToString());
                    var copy = (Exemplar)Controller.GetObjectThroughNumber(Convert.ToInt32(copyId), Controller.Area.Copy);

                    GelöschterLeihvorgang delRent = new GelöschterLeihvorgang(rentDelId, rentId, copy, person, firstDate, secondDate);
                    Controller.delRents.Add(delRent);
                }
            }
        }

        public static void SaveDataController()
        {
            var controllerClass = new ControllerClass();
            var jsonString = JsonSerializer.Serialize(controllerClass);
            File.WriteAllText("controller.json", jsonString);
        }

        public static void ReadDataController()
        {
            var jsonString = File.ReadAllText("controller.json");
            Controller.cc = JsonSerializer.Deserialize<ControllerClass>(jsonString);
            Controller.lastProductId = Controller.cc.LastBookId;
            Controller.lastCopyId = Controller.cc.LastCopyId;
            Controller.lastRentId = Controller.cc.LastRentId;
            Controller.lastDelRentId = Controller.cc.LastDelRentId;
            Controller.lastProductId = Controller.cc.LastMagazinId;
            Controller.lastEBookId = Controller.cc.LastEBookId;
        }

        public static void ReadDataEBooks()
        {
            var jsonString = File.ReadAllText("ebooks.json");
            using (JsonDocument js = JsonDocument.Parse(jsonString))
            {
                foreach (var item in js.RootElement.EnumerateArray())
                {
                    var eBookId = Convert.ToInt32(item.GetProperty("EBookId").ToString());
                    var downloadLink = item.GetProperty("DownloadLink").ToString();
                    var productId = Convert.ToInt32(item.GetProperty("Produkt").GetProperty("ProduktId").ToString());

                    var product = (IPh_Produkt)Controller.GetObjectThroughNumber(productId, Controller.Area.Book);
                    var eBook = new eBook(eBookId, product, downloadLink);
                    Controller.eBooks.Add(eBook);
                }
            }
        }
    }
}
