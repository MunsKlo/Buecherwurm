using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
    class Leihvorgang
    {
        public int LeihvorgangId { get; set; }
        public object Buch { get; set; }
        public string Person { get; set; }
        public string Ausleihdatum { get; set; }
        public string Rückgabedatum { get; set; }

        public Leihvorgang(object _exemplar, string _person, string _ausleihdatum)
        {
            LeihvorgangId = Controller.lastRentId + 1;
            Controller.lastRentId++;
            Buch = _exemplar;
            Person = _person;
            Ausleihdatum = _ausleihdatum;
            Rückgabedatum = ErrechneDatum(_ausleihdatum);

        }

        public Leihvorgang(int _id, object _exemplar, string _person, string _ausleihdatum, string _rückgabedatum)
        {
            LeihvorgangId = _id;
            Buch = _exemplar;
            Person = _person;
            Ausleihdatum = _ausleihdatum;
            Rückgabedatum = _rückgabedatum;
        }


        public void LeihvorgangBearbeiten(string eigenschaft, string neuerWert)
        {
            try
            {
                if (eigenschaft == "Buch")
                    Buch = Controller.BekommeExemplarDurchId(Convert.ToInt32(neuerWert));

                else if (eigenschaft == "Ausleihdatum")
                {
                    Ausleihdatum = neuerWert;
                    ErrechneDatum(neuerWert);
                }
                else if (eigenschaft == "Rückgabedatum")
                    Rückgabedatum = neuerWert;
                else if (eigenschaft == "Person")
                    Person = neuerWert;
            }
            catch (Exception e)
            {
                Console.WriteLine("Irgendwas lief schief!");
            }
            
        }

        private string ErrechneDatum(string datum)
        {
            DateTime neuDatum = DateTime.Parse(datum);
            return neuDatum.AddDays(30).ToString();
        }

    }
}
