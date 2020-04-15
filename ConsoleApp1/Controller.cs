using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
    class Controller
    {
        public static List<Exemplar> exemplare = new List<Exemplar>();
        public static List<Leihvorgang> leihvorgänge = new List<Leihvorgang>();
        public static List<Buch> books = new List<Buch>();

        public static int letzteId;

        public static void SetzeLetzteId()
        {
            var neueId = 0;
            for (int i = 0; i < exemplare.Count; i++)
            {
                if (exemplare[i].ExemplarId > neueId)
                    neueId = exemplare[i].ExemplarId;
            }
            letzteId = neueId;
        }

        public static Exemplar BekommeBuchDurchExemplarId(int id)
        {
            var buch = new Buch();
            foreach (var item in exemplare)
            {
                if (item.ExemplarId == id)
                    return item;
            }
            return null;
        }

        public static Buch BekommeBuchDurchNummer(int id)
        {

        }

        public static Person BekommePersonDurchPersonId(int id)
        {

        }

        private void LeihvorgangErstellen(string name, Exemplar Buch, string ausleihdatum)
        {

        }
    }

    class ControllerClass
    {
        public int letzteId { get; set; }

        public ControllerClass()
        {
            letzteId = Controller.letzteId;
        }
    }
}
