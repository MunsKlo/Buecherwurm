using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
    class Leihvorgang
    {
        public int LeihvorgangId { get; set; }
        public Exemplar Buch { get; set; }
        public Person Person { get; set; }
        public string Ausleihdatum { get; set; }
        public string Rückgabedatum { get; set; }

        public Leihvorgang(int _exemplarId, Person _person, string _ausleihdatum)
        {
            Buch = Controller.BekommeBuchDurchExemplarId(_exemplarId);
            Person = _person;
            Ausleihdatum = _ausleihdatum;
            Rückgabedatum = ErrechneDatum(_ausleihdatum);

        }


        private void LeihvorgangBearbeiten(string eigenschaft, string neuerWert)
        {
            try
            {
                if (eigenschaft == "Buch")
                    Buch = Controller.BekommeBuchDurchExemplarId(Convert.ToInt32(neuerWert));
                else if (eigenschaft == "Person")
                    Person = Controller.BekommePersonDurchPersonId(Convert.ToInt32(neuerWert));
                else if (eigenschaft == "Ausleihdatum")
                    Console.WriteLine("Test");
                else if (eigenschaft == "Rückgabedatum")
                    Console.WriteLine("Test");
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
