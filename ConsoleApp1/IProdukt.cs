using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
    public interface IProdukt
    {
        int Id { get; set; }
        string Titel { get; set; }
        string Autor { get; set; }
        void ÄndereEigenschaft(string neuerWert, string eigenschaft);
    }
}
