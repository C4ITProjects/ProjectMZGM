using System;
using Soneta.Business;
using Soneta.Business.UI;
using ProjectMZGM.Workers;
using ProjectMZGM;
using Soneta.Types;
using ProjectMZGM.Czynsze;

[assembly: Worker(typeof(ExRozliczeniaWorker), typeof(Rozliczenie))]
namespace ProjectMZGM.Workers
{
    class ExRozliczeniaWorker
    {
        [Context]
        public Rozliczenie Rozliczenie { get; set; }

        public Currency Woda
        {
            get
            {
                return Rozliczenie.WodaKorekta + Rozliczenie.WodaStawka;
            }
        }
        public Currency Ścieki
        {
            get
            {
                return Rozliczenie.SciekiKorekta + Rozliczenie.SciekiStawka;
            }
        }
        public Currency CO
        {
            get
            {
                return Rozliczenie.CoKorekta + Rozliczenie.CoStawka;
            }
        }
        public Currency CWU
        {
            get
            {
                return Rozliczenie.CWUKorekta + Rozliczenie.CWUStawka;
            }
        }
        public Currency Śmieci
        {
            get
            {
                return Rozliczenie.SmieciKorekta + Rozliczenie.SmieciStawka;
            }
        }
        public Currency ŚmieciNieSelekcjonowane
        {
            get
            {
                return Rozliczenie.SmieciNselKorekta + Rozliczenie.SmieciNselStawka;
            }
        }
        public Currency ŚmieciSelekcjonowane
        {
            get
            {
                return Rozliczenie.SmieciSelKorekta + Rozliczenie.SmieciSelStawka;
            }
        }
        public Currency KosztyEkslpotacji
        {
            get
            {
                return Rozliczenie.KEksplKorekta + Rozliczenie.KEksplStawka;
            }
        }
        public Currency WynagrodzenieZarzadu
        {
            get
            {
                return Rozliczenie.WynZarzKorekta + Rozliczenie.WynZarzStawka;
            }
        }
        public Currency Domofon
        {
            get
            {
                return Rozliczenie.DomofonKorekta + Rozliczenie.DomofonStawka;
            }
        }
        public Currency Winda
        {
            get
            {
                return Rozliczenie.WindaKorekta + Rozliczenie.WindaStawka;
            }
        }
        public Currency Antena
        {
            get
            {
                return Rozliczenie.AntenaKorekta + Rozliczenie.AntenaStawka;
            }
        }
    }
}
