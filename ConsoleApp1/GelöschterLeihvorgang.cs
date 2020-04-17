using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
    class GelöschterLeihvorgang
    {
        public int GelLeihvorgangId { get; set; }
        public int LeihvorgangId { get; set; }
        public Exemplar Buch { get; set; }
        public string Person { get; set; }
        public string Ausleihdatum { get; set; }
        public string Rückgabedatum { get; set; }

        public GelöschterLeihvorgang(int _leihvorgangid, Exemplar _buch, string _person, string _ausleihdatum, string _rückgabedatum)
        {
            GelLeihvorgangId = Controller.lastDelRentId + 1;
            Controller.lastDelRentId++;
            LeihvorgangId = _leihvorgangid;
            Buch = _buch;
            Person = _person;
            Ausleihdatum = _ausleihdatum;
            Rückgabedatum = _rückgabedatum;
        }

        public GelöschterLeihvorgang(int _gelLeihvorgangId, int _leihvorgangid, Exemplar _buch, string _person, string _ausleihdatum, string _rückgabedatum)
        {
            GelLeihvorgangId = _gelLeihvorgangId;
            LeihvorgangId = _leihvorgangid;
            Buch = _buch;
            Person = _person;
            Ausleihdatum = _ausleihdatum;
            Rückgabedatum = _rückgabedatum;
        }
    }
}
