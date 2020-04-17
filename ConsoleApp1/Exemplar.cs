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
            ExemplarId = Controller.lastCopyId + 1;
            Controller.lastCopyId++;
            IstAusgeliehen = false;
            Buch = _buch;
        }

        public Exemplar(int _exemplarId, bool _istAusgeliehen, Buch _buch)
        {
            ExemplarId = _exemplarId;
            IstAusgeliehen = _istAusgeliehen;
            Buch = _buch;
        }

        public Exemplar()
        {

        }

        public void ÄndereEigenschaftVonExemplar(bool neuerWert)
        {
            IstAusgeliehen = neuerWert;
        }
    }
}
