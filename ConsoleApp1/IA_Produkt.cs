using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
    interface IA_Produkt : IProdukt
    {
        int Id { get; set; }
        object Produkt { get; set; }
        string DownloadLink { get; set; }
    }
}
