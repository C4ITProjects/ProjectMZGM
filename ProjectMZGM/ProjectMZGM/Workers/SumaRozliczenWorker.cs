using System;
using Soneta.Business;
using Soneta.Business.UI;
using ProjectMZGM.Workers;
using ProjectMZGM;
using Soneta.Types;

[assembly: Worker(typeof(SumaRozliczenWorker), typeof(Rozliczenie))]
namespace ProjectMZGM.Workers
{
    public class SumaRozliczenWorker
    {

    [Context]

    public Rozliczenie Rozliczenie
        { get; set; }


        public Currency SumaKorekt
        {
            get
            {
                Currency wynik = Rozliczenie.AntenaKorekta + Rozliczenie.CoKorekta + Rozliczenie.CWUKorekta + Rozliczenie.DomofonKorekta + Rozliczenie.EnergiaKorekta + Rozliczenie.FunduszRemontowyKorekta + Rozliczenie.KEksplKorekta + Rozliczenie.SciekiKorekta + Rozliczenie.SmieciKorekta + Rozliczenie.SmieciNselKorekta + Rozliczenie.SmieciSelKorekta + Rozliczenie.WindaKorekta + Rozliczenie.WodaKorekta + Rozliczenie.WynZarzKorekta;
                return wynik;
            }
        }

    }


}
