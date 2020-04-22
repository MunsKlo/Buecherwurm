using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
    public interface IProdukt
    {
        string Autor { get; set; }
        string Land { get; set; }
        string BildLink { get; set; }
        string Sprache { get; set; }
        string Link { get; set; }
        int Seiten { get; set; }
        string Titel { get; set; }
        int Jahr { get; set; }
        int Exemplare { get; set; }

        void ErstelleExemplare();
        List<string> ExemplarSchonVorhanden();
        void LöscheExemplare(string neuerWert);
        void ÄndereExemplarZahl(int neueExemplarZahl);
        void ÄndereEigenschaft(string neuerWert, string eigenschaft);
    }
}
