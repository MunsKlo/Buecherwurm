using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ConsoleApp1
{
    class JSONIO
    {
        public static void ReadDataBooks()
        {
            var jsonString = File.ReadAllText("books.json");
            using (JsonDocument js = JsonDocument.Parse(jsonString))
            {
                foreach (var item in js.RootElement.EnumerateArray())
                {
                    var author = item.GetProperty("author").ToString();
                    var country = item.GetProperty("country").ToString();
                    var imageLink = item.GetProperty("imageLink").ToString();
                    var language = item.GetProperty("language").ToString();
                    var link = item.GetProperty("link").ToString();
                    var pages = Convert.ToInt32(item.GetProperty("pages").ToString());
                    var title = item.GetProperty("title").ToString();
                    var year = Convert.ToInt32(item.GetProperty("year").ToString());

                    var Buch = new Buch(author, country, imageLink, language, link, pages, title, year);
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
            var jsonString = File.ReadAllText("test.json");
            Controller.exemplare = new List<Exemplar>();
            Console.WriteLine(Controller.exemplare.Count);
            using (JsonDocument js = JsonDocument.Parse(jsonString))
            {
                foreach (var item in js.RootElement.EnumerateArray())
                {
                    var exemplarId = item.GetProperty("ExemplarId").ToString();
                    var istAusgeliehen = item.GetProperty("IstAusgeliehen").ToString();

                    var author = item.GetProperty("Buch").GetProperty("Autor").ToString();
                    var country = item.GetProperty("Buch").GetProperty("Land").ToString();
                    var imageLink = item.GetProperty("Buch").GetProperty("BildLink").ToString();
                    var language = item.GetProperty("Buch").GetProperty("Sprache").ToString();
                    var link = item.GetProperty("Buch").GetProperty("Link").ToString();
                    var pages = Convert.ToInt32(item.GetProperty("Buch").GetProperty("Seiten").ToString());
                    var title = item.GetProperty("Buch").GetProperty("Titel").ToString();
                    var year = Convert.ToInt32(item.GetProperty("Buch").GetProperty("Jahr").ToString());
                    var copies = Convert.ToInt32(item.GetProperty("Buch").GetProperty("Exemplare").ToString());

                    var buch = new Buch(author, country, imageLink, language, link, pages, title, year, copies);
                    Controller.books.Add(buch);

                    var exemplar = new Exemplar(Convert.ToInt32(exemplarId), Convert.ToBoolean(istAusgeliehen), buch);
                    Controller.exemplare.Add(exemplar);
                }
            }
            for (int i = 0; i < Controller.exemplare.Count; i++)
            {
                Console.WriteLine(Controller.exemplare[i].ExemplarId);
            }
        }

        public static void SaveDataController()
        {
            var controllerClass = new ControllerClass();
            var jsonString = JsonSerializer.Serialize(controllerClass);
            File.WriteAllText("controller.json", jsonString);
        }
    }
}
