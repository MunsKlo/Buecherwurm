using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
    interface IPh_Produkt : IProdukt
    {
        int ProduktId { get; set; }
        int Exemplare { get; set; }
        string Autor { get; set; }
        string Titel { get; set; }

        void ErstelleExemplare();
        List<string> ExemplarSchonVorhanden();
        void LöscheExemplare(string neuerWert);
        void ÄndereExemplarZahl(int neueExemplarZahl);
    }
}
