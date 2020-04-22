using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
    class Exemplar
    {
        public int ExemplarId { get; set; }
        public bool IstAusgeliehen { get; set; }
        public object Buch { get; set; }

        public Exemplar(object _object)
        {
            ExemplarId = Controller.lastCopyId + 1;
            Controller.lastCopyId++;
            IstAusgeliehen = false;
            Type type = _object.GetType();
            if (type == typeof(Buch))
                Buch = (Buch)_object;
            else
                Buch = (Magazin)_object;
        }

        public Exemplar(int _exemplarId, bool _istAusgeliehen, object _object)
        {
            ExemplarId = _exemplarId;
            IstAusgeliehen = _istAusgeliehen;
            Type type = _object.GetType();
            if (type == typeof(Buch))
                Buch = (Buch)_object;
            else
                Buch = (Magazin)_object;
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
