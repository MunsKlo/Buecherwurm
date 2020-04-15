using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
    class Buch
    {
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
        public Buch(string _author, string _land, string _bildLink, string _sprache, string _link, int _seiten, string _titel, int _jahr)
        {
            Autor = _author;
            Land = _land;
            BildLink = _bildLink;
            Sprache = _sprache;
            Link = _link;
            Seiten = _seiten;
            Titel = _titel;
            Jahr = _jahr;
            Exemplare = 2;
            ErstelleExemplare();
        }

        public Buch(string _author, string _land, string _bildLink, string _sprache, string _link, string _seiten, string _titel, string _jahr, string _exemplare)
        {
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

        private void ErstelleExemplare()
        {
            var informations = ExemplarSchonVorhanden();
            if (!Convert.ToBoolean(informations[0]))
            {
                for (int i = 0; i < Convert.ToInt32(informations[1]); i++)
                {
                    var neuExemplar = new Exemplar(this);
                    Controller.exemplare.Add(neuExemplar);
                }
            }
            
        }

        private List<string> ExemplarSchonVorhanden()
        {
            var informations = new List<string>();
            var counter = 0;
            for (int i = 0; i < Controller.exemplare.Count; i++)
            {
                if (Controller.exemplare[i].Buch == this)
                    counter++;
            }
            if (counter == Exemplare)
                return new List<string> { "true" };
            else if (counter < Exemplare && counter > 0)
                return new List<string> { "false", Convert.ToString(Exemplare - counter) };
            else
                return new List<string> { "false", Exemplare.ToString()};
        }

        private void ÄndereExemplarZahl(int neueExemplarZahl)
        {
            Exemplare = neueExemplarZahl;
        }
    }
}
