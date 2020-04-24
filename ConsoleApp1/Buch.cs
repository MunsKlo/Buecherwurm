using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
    class Buch : IPh_Produkt
    {
        public int ProduktId { get; set; }
        public string Autor { get; set; }
        public string Land { get; set; }
        public string BildLink { get; set; }
        public string Sprache { get; set; }
        public string Link { get; set; }
        public int Seiten { get; set; }
        public string Titel { get; set; }
        public int Jahr { get; set; }
        public int Exemplare { get; set; }

        public Buch()
        {

        }

        public Buch(int _bookId, string _author, string _land, string _bildLink, string _sprache, string _link, string _seiten, string _titel, string _jahr, string _exemplare)
        {
            ProduktId = _bookId;
            Autor = _author;
            Land = _land;
            BildLink = _bildLink;
            Sprache = _sprache;
            Link = _link;
            Seiten = Convert.ToInt32(_seiten);
            Titel = _titel;
            Jahr = Convert.ToInt32(_jahr);
            Exemplare = Convert.ToInt32(_exemplare);
        }

        public Buch(string _author, string _land, string _bildLink, string _sprache, string _link, string _seiten, string _titel, string _jahr, string _exemplare)
        {
            ProduktId = Controller.lastProductId + 1;
            Controller.lastProductId++;
            Autor = _author;
            Land = _land;
            BildLink = _bildLink;
            Sprache = _sprache;
            Link = _link;
            Seiten = Convert.ToInt32(_seiten);
            Titel = _titel;
            Jahr = Convert.ToInt32(_jahr);
            Exemplare = Convert.ToInt32(_exemplare);
            ErstelleExemplare();
        }
        public Buch(string _author, string _land, string _bildLink, string _sprache, string _link, string _seiten, string _titel, string _jahr)
        {
            ProduktId = Controller.lastProductId + 1;
            Controller.lastProductId++;
            Autor = _author;
            Land = _land;
            BildLink = _bildLink;
            Sprache = _sprache;
            Link = _link;
            Seiten = Convert.ToInt32(_seiten);
            Titel = _titel;
            Jahr = Convert.ToInt32(_jahr);
            Exemplare = 2;
            ErstelleExemplare();
        }

        public void ErstelleExemplare()
        {
            var informations = ExemplarSchonVorhanden();
            if (!Convert.ToBoolean(informations[0]))
            {
                for (int i = 0; i < Convert.ToInt32(informations[1]); i++)
                {
                    var neuExemplar = new Exemplar(this);
                    Controller.copies.Add(neuExemplar);
                }
            }
            
        }

        public List<string> ExemplarSchonVorhanden()
        {
            var informations = new List<string>();
            var counter = 0;
            for (int i = 0; i < Controller.copies.Count; i++)
            {
                if (Controller.copies[i].Buch == this)
                    counter++;
            }
            if (counter == Exemplare)
                return new List<string> { "true" };
            else if (counter < Exemplare && counter > 0)
                return new List<string> { "false", Convert.ToString(Exemplare - counter) };
            else
                return new List<string> { "false", Exemplare.ToString()};
        }


        public void LöscheExemplare(string neuerWert)
        {
            var listeVonExemplaren = Controller.GetPresentCopies(this);
            if(listeVonExemplaren.Count < Exemplare - Convert.ToInt32(neuerWert))
                Console.WriteLine("Du kannst keine Exemplare von Büchern verringern, welche zurzeit nicht verfügbar sind!");
            else
                Controller.DeleteCopies(this, Exemplare - Convert.ToInt32(neuerWert));
        }
        public void ÄndereExemplarZahl(int neueExemplarZahl)
        {
            Exemplare = neueExemplarZahl;
        }

        public void ÄndereEigenschaft(string neuerWert, string eigenschaft)
        {
            if (eigenschaft == "Autor")
                Autor = neuerWert;
            else if (eigenschaft == "Land")
                Land = neuerWert;
            else if (eigenschaft == "Titel")
                Titel = neuerWert;
            else if (eigenschaft == "Bildlink")
                BildLink = neuerWert;
            else if (eigenschaft == "Link")
                Link = neuerWert;
            else if (eigenschaft == "Jahr")
                Jahr = Convert.ToInt32(neuerWert);
            else if (eigenschaft == "Seiten")
                Seiten = Convert.ToInt32(neuerWert);
            else if (eigenschaft == "Sprache")
                Sprache = neuerWert;
            else if (eigenschaft == "Exemplare" && Exemplare < Convert.ToInt32(neuerWert))
            {
                Exemplare = Convert.ToInt32(neuerWert);
                ErstelleExemplare();
            }
            else if (eigenschaft == "Exemplare" && Exemplare > Convert.ToInt32(neuerWert))
            {
                LöscheExemplare(neuerWert);
                Exemplare = Convert.ToInt32(neuerWert);
            }
            else
                Console.WriteLine($"Es wurde keine Eigenschaft mit dem Namen {eigenschaft} gefunden.");

        }
    }
}
