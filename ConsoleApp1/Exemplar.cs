using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
    class Exemplar
    {
        public int ExemplarId { get; set; }
        public bool IstAusgeliehen { get; set; }

        public Buch Buch { get; set; }

        public Exemplar(Buch _buch)
        {
            ExemplarId = Controller.exemplare.Count + 1;
            IstAusgeliehen = false;
            Buch = _buch;
        }

        public Exemplar(int _exemplarId, bool _istAusgeliehen, Buch _buch)
        {
            ExemplarId = _exemplarId;
            IstAusgeliehen = _istAusgeliehen;
            Buch = _buch;
        }
    }
}
