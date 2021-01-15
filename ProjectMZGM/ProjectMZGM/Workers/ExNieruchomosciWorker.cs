using System;
using Soneta.Business;
using Soneta.Business.UI;
using ProjectMZGM.Workers;
using ProjectMZGM;
using Soneta.Types;
using ProjectMZGM.Czynsze;

[assembly: Worker(typeof(ExNieruchomosciWorker), typeof(Nieruchomosc))]
namespace ProjectMZGM.Workers
{
    class ExNieruchomosciWorker
    {
        [Context]
        public Nieruchomosc Nieruchomosc { get; set; }

        public Currency ZaplataZaliczka
        {
            get
            {
                CzynszeModule cm = CzynszeModule.GetInstance(Nieruchomosc);
                View rozliczenia = cm.Rozliczenia.CreateView();
                var cond = RowCondition.Empty;
                cond &= new FieldCondition.Equal("AdresPelnyNieruchomosci", Nieruchomosc.AdresPelnyNieruchomosci);
                cond &= new FieldCondition.Equal("MiesiacRok", Date.Today.ToYearMonth().ToString());
                rozliczenia.Condition = cond;
                Currency wynik = 0;
                foreach (Rozliczenie roz in rozliczenia)
                 wynik = roz.ZaplataZaliczka;
                return wynik;
            }
        }
        public Currency SumaStawek
        {
            get
            {
                return Nieruchomosc.StawkaAntena + Nieruchomosc.StawkaCo + Nieruchomosc.StawkaCWU + Nieruchomosc.StawkaDomofon
                        + Nieruchomosc.StawkaEnergia + Nieruchomosc.StawkaFunduszRemontowy + Nieruchomosc.StawkaKEkspl + Nieruchomosc.StawkaScieki + Nieruchomosc.StawkaSmieci
                        + Nieruchomosc.StawkaSmieciNsel + Nieruchomosc.StawkaSmieciSel + Nieruchomosc.StawkaWinda + Nieruchomosc.StawkaWoda + Nieruchomosc.StawkaWynZarz;
            }
        }
        public Currency ZaplataFundusz
        {
            get
            {
                CzynszeModule cm = CzynszeModule.GetInstance(Nieruchomosc);
                View rozliczenia = cm.Rozliczenia.CreateView();
                var cond = RowCondition.Empty;
                cond &= new FieldCondition.Equal("AdresPelnyNieruchomosci", Nieruchomosc.AdresPelnyNieruchomosci);
                cond &= new FieldCondition.Equal("MiesiacRok", Date.Today.ToYearMonth().ToString());
                rozliczenia.Condition = cond;
                Currency wynik = 0;
                foreach (Rozliczenie roz in rozliczenia)
                 wynik = roz.ZaplataFundusz;
                return wynik;
            }
        }
    }
}
