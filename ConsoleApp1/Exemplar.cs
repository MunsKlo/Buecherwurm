using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
    class Exemplar : IProdukt
    {
        public int Id { get; set; }
        public string Titel { get; set; }
        public string Autor { get; set; }
        public int ExemplarId { get; set; }
        public bool IstAusgeliehen { get; set; }
        public object Produkt { get; set; }

        public Exemplar(object _object)
        {
            ExemplarId = Controller.lastCopyId + 1;
            Controller.lastCopyId++;
            IstAusgeliehen = false;
            Produkt = _object;
        }

        public Exemplar(int _exemplarId, bool _istAusgeliehen, object _object)
        {
            ExemplarId = _exemplarId;
            IstAusgeliehen = _istAusgeliehen;
            Produkt = _object;
        }

        public Exemplar()
        {

        }

        public void ÄndereEigenschaft(string _eigenschaft, string neuerWert)
        {
            IstAusgeliehen = bool.Parse(neuerWert);
        }
    }
}
