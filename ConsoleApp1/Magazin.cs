using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
    class Magazin : IPh_Produkt
    {
        public int Id { get; set; }
        public string Autor { get; set; }
        public string Titel { get; set; }
        public string Gruppe { get; set; }
        public string Sachgruppe { get; set; }
        public int Exemplare { get; set; }
        public Magazin()
        {

        }
        public Magazin(string _author, string _titel, string _gruppe, string _sachgrupppe)
        {
            Id = Controller.lastProductId + 1;
            Controller.lastProductId++;
            Autor = _author;
            Titel = _titel;
            Gruppe = _gruppe;
            Sachgruppe = _sachgrupppe;
            Exemplare = 2;
            ErstelleExemplare();
        }

        public Magazin(int _bookId, string _author, string _titel, string _exemplare, string _gruppe, string _sachgrupppe)
        {
            Id = _bookId;
            Autor = _author;
            Titel = _titel;
            Gruppe = _gruppe;
            Sachgruppe = _sachgrupppe;
            Exemplare = Convert.ToInt32(_exemplare);
        }

        public Magazin(string _author, string _titel, string _exemplare, string _gruppe, string _sachgrupppe)
        {
            Id = Controller.lastProductId + 1;
            Controller.lastProductId++;
            Autor = _author;
            Titel = _titel;
            Gruppe = _gruppe;
            Sachgruppe = _sachgrupppe;
            Exemplare = Convert.ToInt32(_exemplare);
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
                if (Controller.copies[i].Produkt == this)
                    counter++;
            }
            if (counter == Exemplare)
                return new List<string> { "true" };
            else if (counter < Exemplare && counter > 0)
                return new List<string> { "false", Convert.ToString(Exemplare - counter) };
            else
                return new List<string> { "false", Exemplare.ToString() };
        }


        public void LöscheExemplare(string neuerWert)
        {
            var newObject = (object)this;
            var listeVonExemplaren = Controller.GetPresentCopies(newObject);
            if (listeVonExemplaren.Count < Exemplare - Convert.ToInt32(neuerWert))
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
            else if (eigenschaft == "Gruppe")
                Gruppe = neuerWert;
            else if (eigenschaft == "Titel")
                Titel = neuerWert;
            else if (eigenschaft == "Sachgruppe")
                Sachgruppe = neuerWert;
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
