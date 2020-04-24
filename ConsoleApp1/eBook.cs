using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
    class eBook : IProdukt
    {
        public int eBookId { get; set; }
        public Buch buch { get; set; }

        public string DownloadLink { get; set; }
        
        public eBook(Buch _buch, string _downloadLink)
        {
            eBookId = Controller.lastEBookId + 1;
            Controller.lastEBookId++;
            buch = _buch;
            DownloadLink = _downloadLink;
        }

        public eBook(int _eBookId, Buch _buch, string _downloadLink)
        {
            eBookId = _eBookId;
            buch = _buch;
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
