using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
    class EBook : IA_Produkt
    {
        public string Titel { get; set; }
        public string Autor { get; set; }
        public int Id { get; set; }
        public object Produkt { get; set; }
        public string DownloadLink { get; set; }
        
        public EBook(object _product, string _downloadLink)
        {
            Id = Controller.lastEId + 1;
            Controller.lastEId++;
            Produkt = _product;
            DownloadLink = _downloadLink;
            var obj = (IPh_Produkt)Produkt;
            Titel = obj.Titel;
            Autor = obj.Autor;
        }

        public EBook(int _eBookId, object _product, string _downloadLink)
        {
            Id = _eBookId;
            Produkt = _product;
            DownloadLink = _downloadLink;
            var obj = (IPh_Produkt)Produkt;
            Titel = obj.Titel;
            Autor = obj.Autor;
        }

        public void ÄndereEigenschaft(string neuerWert, string eigenschaft)
        {
            if (eigenschaft == "Download" && IstEsEinDowloadLink(neuerWert))
                DownloadLink = neuerWert;
        }

        public bool IstEsEinDowloadLink(string neuerLink)
        {
            var link = "http://www.buecherwurm/download/";
            var IstRichtig = true;
            for (int i = 0; i < link.Length; i++)
            {
                if(link[i] != neuerLink[i])
                {
                    IstRichtig = false;
                    break;
                }
            }
            return IstRichtig;
        }
    }
}