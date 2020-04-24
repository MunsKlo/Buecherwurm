using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
    class eBook : IProdukt
    {
        public int EBookId { get; set; }
        public IPh_Produkt Produkt { get; set; }

        public string DownloadLink { get; set; }
        
        public eBook(IPh_Produkt _product, string _downloadLink)
        {
            EBookId = Controller.lastEBookId + 1;
            Controller.lastEBookId++;
            Produkt = _product;
            DownloadLink = _downloadLink;
        }

        public eBook(int _eBookId, IPh_Produkt _product, string _downloadLink)
        {
            EBookId = _eBookId;
            Produkt = _product;
            DownloadLink = _downloadLink;
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
