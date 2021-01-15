using System;
using Soneta.Business;
using Soneta.Business.UI;
using ProjectMZGM.Workers;
using Soneta.Types;

[assembly: Worker(typeof(PrzeliczZaplaty), typeof(ProjectMZGM.Nieruchomosc))]
namespace ProjectMZGM.Workers
{
    public class PrzeliczZaplaty
    {
        [Action("Wygeneruj Zapłaty", Priority = 1, Icon = ActionIcon.Wizard, Target = ActionTarget.ToolbarWithText,
             Mode = ActionMode.OnlyTable | ActionMode.SingleSession | ActionMode.NonAccept)]
        public MessageBoxInformation ToDo()
        {
            return new MessageBoxInformation("Przelicz Zapłaty")
            {
                Text = "Czy rozpocząć przeliczanie?",
                YesHandler = () =>
                {
                    
                    GenerujZaplaty();
                    return "Zakończono przeliczanie";
                },
                NoHandler = () => "Przerwano przeliczanie"
            };
        }

        [Context]
        public static Session Session { get; set; }

        public static void GenerujZaplaty()
        {
            ProjectMZGM.Czynsze.CzynszeModule cm = Czynsze.CzynszeModule.GetInstance(Session);
            View w1 = cm.Nieruchomosci.WgAdresuNieruchomosci.CreateView();
            using (ITransaction trans = Session.Logout(true))
            {
                foreach (Nieruchomosc nieruchomosc in w1)
                {
                    if (nieruchomosc.Nieaktywny == true)
                        continue;
                    if (cm.Rozliczenia.WgAdresuNieruchomosci[nieruchomosc.AdresPelnyNieruchomosci].GetNext() != null)
                    {
                        if (cm.Rozliczenia.WgAdresuNieruchomosci[nieruchomosc.AdresPelnyNieruchomosci].GetNext().Data.ToYearMonth() == Date.Today.ToYearMonth())
                            continue;
                    }
                    nieruchomosc.StawkaCalosc = nieruchomosc.StawkaAntena + nieruchomosc.StawkaCo + nieruchomosc.StawkaCWU +
                    nieruchomosc.StawkaDomofon + nieruchomosc.StawkaEnergia + nieruchomosc.StawkaKEkspl +
                    nieruchomosc.StawkaScieki + nieruchomosc.StawkaSmieci + nieruchomosc.StawkaSmieciNsel + nieruchomosc.StawkaSmieciSel +
                    nieruchomosc.StawkaWinda + nieruchomosc.StawkaWoda + nieruchomosc.StawkaWynZarz + nieruchomosc.StawkaFunduszRemontowy;
                    
                    nieruchomosc.SumaStawekBezFR = nieruchomosc.StawkaAntena + nieruchomosc.StawkaCo + nieruchomosc.StawkaCWU +
                    nieruchomosc.StawkaDomofon + nieruchomosc.StawkaEnergia + nieruchomosc.StawkaKEkspl +
                    nieruchomosc.StawkaScieki + nieruchomosc.StawkaSmieci + nieruchomosc.StawkaSmieciNsel + nieruchomosc.StawkaSmieciSel +
                    nieruchomosc.StawkaWinda + nieruchomosc.StawkaWoda + nieruchomosc.StawkaWynZarz;


                    Rozliczenie rozliczenie = new Rozliczenie();
                    cm.Rozliczenia.AddRow(rozliczenie);
                    rozliczenie.Zarzadca = nieruchomosc.Zarzadca;

                    rozliczenie.Nieruchomosc_wsk = nieruchomosc;
                    rozliczenie.AdresPelnyNieruchomosci = nieruchomosc.AdresPelnyNieruchomosci;
                    rozliczenie.Data = Date.Today;
                    rozliczenie.MiesiacRok = Date.Today.ToYearMonth().ToString();
                    rozliczenie.SumaStawek = nieruchomosc.StawkaCalosc;
                    rozliczenie.SumaStawekBezFR = nieruchomosc.SumaStawekBezFR;
                    rozliczenie.FunduszRemontowy = nieruchomosc.StawkaFunduszRemontowy;
                    rozliczenie.ZaplataFundusz = rozliczenie.FunduszRemontowy;
                    rozliczenie.ZaplataZaliczka = rozliczenie.SumaStawekBezFR;

                    rozliczenie.WynZarzStawka = nieruchomosc.StawkaWynZarz;
                    rozliczenie.WodaStawka = nieruchomosc.StawkaWoda;
                    rozliczenie.WindaStawka = nieruchomosc.StawkaWinda;
                    rozliczenie.SmieciStawka = nieruchomosc.StawkaSmieci;
                    rozliczenie.SmieciSelStawka = nieruchomosc.StawkaSmieciSel;
                    rozliczenie.SmieciNselStawka = nieruchomosc.StawkaSmieciNsel;
                    rozliczenie.SciekiStawka = nieruchomosc.StawkaScieki;
                    rozliczenie.KEksplStawka = nieruchomosc.StawkaKEkspl;
                    rozliczenie.FunduszRemontowyStawka = nieruchomosc.StawkaFunduszRemontowy;
                    rozliczenie.EnergiaStawka = nieruchomosc.StawkaEnergia;
                    rozliczenie.DomofonStawka = nieruchomosc.StawkaDomofon;
                    rozliczenie.CWUStawka = nieruchomosc.StawkaCWU;
                    rozliczenie.CoStawka = nieruchomosc.StawkaCo;
                    rozliczenie.AntenaStawka = nieruchomosc.StawkaAntena;
                    //rozliczenie.SumaStawek = nieruchomosc.StawkaAntena + nieruchomosc.StawkaCo + nieruchomosc.StawkaCWU +
                    //nieruchomosc.StawkaDomofon + nieruchomosc.StawkaEnergia + nieruchomosc.StawkaKEkspl +
                    //nieruchomosc.StawkaScieki + nieruchomosc.StawkaSmieci + nieruchomosc.StawkaSmieciNsel + nieruchomosc.StawkaSmieciSel +
                    //nieruchomosc.StawkaWinda + nieruchomosc.StawkaWoda + nieruchomosc.StawkaWynZarz + nieruchomosc.StawkaFunduszRemontowy;

                    rozliczenie.Paragraf4260 = nieruchomosc.StawkaWoda + nieruchomosc.StawkaCo + nieruchomosc.StawkaCWU;
                    rozliczenie.Paragraf4300 = nieruchomosc.StawkaScieki + nieruchomosc.StawkaKEkspl + nieruchomosc.StawkaWynZarz + nieruchomosc.StawkaSmieci + nieruchomosc.StawkaSmieciSel + nieruchomosc.StawkaSmieciNsel + nieruchomosc.StawkaEnergia;
                    rozliczenie.Paragraf4270 = nieruchomosc.StawkaDomofon + nieruchomosc.StawkaWinda + nieruchomosc.StawkaAntena;
                    rozliczenie.FunduszRemontowyParagraf4270 = nieruchomosc.StawkaFunduszRemontowy;

                    rozliczenie.SumaParagraf4260 = rozliczenie.Paragraf4260;
                    rozliczenie.SumaParagraf4270 = rozliczenie.Paragraf4270;
                    rozliczenie.SumaParagraf4300 = rozliczenie.Paragraf4300;
                    rozliczenie.SumaParagraf4270FR = rozliczenie.FunduszRemontowyParagraf4270;


                    trans.Commit();
                }
            }
        }
    }
}



