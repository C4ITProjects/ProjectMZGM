using System;
using Soneta.Business;
using Soneta.Business.UI;
using ProjectMZGM.Workers;
using Soneta.Types;
using Soneta.Ksiega;
using Soneta.Core;
using Soneta.CRM;
using Soneta.Business.App;

[assembly: Worker(typeof(GenerujPK), typeof(ProjectMZGM.Nieruchomosc))]
namespace ProjectMZGM.Workers
{
    public class GenerujPK
    {
        [Action("Generuj PKWS", Priority = 10, Icon = ActionIcon.Book, Target = ActionTarget.ToolbarWithText,
             Mode = ActionMode.OnlyTable | ActionMode.SingleSession | ActionMode.NonAccept)]
        public MessageBoxInformation ToDo()
        {
            return new MessageBoxInformation("Generowanie PKWS dla Księgowości")
            {
                Text = "Czy wygenerować PKWS?",
                YesHandler = () =>
                {
                    Zapis_PK();
                    return "Zakończono generowanie";
                },
                NoHandler = () => "Przerwano generowanie"
            };
        }


        [Context]
        public static Session Session { get; set; }
        public static void Zapis_PK()
        {
            CoreModule cm = CoreModule.GetInstance(Session);
            KsiegaModule ksm = KsiegaModule.GetInstance(Session);
            ProjectMZGM.Czynsze.CzynszeModule czy = Czynsze.CzynszeModule.GetInstance(Session);
            View zarzadca_view = czy.Zarzadcy.CreateView();
            var zarzadca_cond = RowCondition.Empty;
            zarzadca_cond &= new FieldCondition.In("Nieaktywny", false);
            zarzadca_view.Condition = zarzadca_cond;

            


            foreach (Zarzadca zarzadca in zarzadca_view)
            {
                Currency kwota_suma = 0;
                Currency kwotaParagraf4260 = 0;
                Currency kwotaParagraf4300 = 0;
                Currency kwotaParagraf4270 = 0;
                Currency kwotaParagraf4270FR = 0;
                Currency kwotaWoda = 0;
                Currency kwotaScieki = 0;
                Currency kwotaCO = 0;
                Currency kwotaCWU = 0;
                Currency kwotaSmieci = 0;
                Currency kwotaKekspl = 0;
                Currency kwotaKosztyZarz = 0;
                Currency kwotaDomofon = 0;
                Currency kwotaAntena = 0;
                Currency kwotaWinda = 0;
                Currency kwotaFunduszRemontowy = 0;

                View nieruchomosci = czy.Nieruchomosci.WgZarzadca[zarzadca].CreateView();
                foreach (Nieruchomosc nieruchomosc in nieruchomosci)
                {
                    kwotaWoda += nieruchomosc.StawkaWoda;
                    kwotaScieki += nieruchomosc.StawkaScieki;
                    kwotaCO += nieruchomosc.StawkaCo;
                    kwotaCWU += nieruchomosc.StawkaCWU;
                    kwotaSmieci += (nieruchomosc.StawkaSmieci + nieruchomosc.StawkaSmieciNsel + nieruchomosc.StawkaSmieciSel);
                    kwotaKekspl += nieruchomosc.StawkaKEkspl;
                    kwotaKosztyZarz += nieruchomosc.StawkaWynZarz;
                    kwotaDomofon += nieruchomosc.StawkaDomofon;
                    kwotaAntena += nieruchomosc.StawkaAntena;
                    kwotaWinda += nieruchomosc.StawkaWinda;
                    kwotaFunduszRemontowy += nieruchomosc.StawkaFunduszRemontowy;

                    kwotaParagraf4260 += (nieruchomosc.StawkaWoda + nieruchomosc.StawkaCo + nieruchomosc.StawkaCWU);
                    kwotaParagraf4270 += (nieruchomosc.StawkaDomofon + nieruchomosc.StawkaWinda + nieruchomosc.StawkaAntena);
                    kwotaParagraf4300 += (nieruchomosc.StawkaScieki + nieruchomosc.StawkaKEkspl + nieruchomosc.StawkaWynZarz + nieruchomosc.StawkaSmieci + nieruchomosc.StawkaSmieciNsel + nieruchomosc.StawkaSmieciSel);
                    kwotaParagraf4270FR += nieruchomosc.StawkaFunduszRemontowy;

                    kwota_suma += (nieruchomosc.StawkaWoda + nieruchomosc.StawkaCo + nieruchomosc.StawkaCWU) + (nieruchomosc.StawkaDomofon + nieruchomosc.StawkaWinda + nieruchomosc.StawkaAntena) + (nieruchomosc.StawkaScieki + nieruchomosc.StawkaKEkspl + nieruchomosc.StawkaWynZarz + nieruchomosc.StawkaSmieci + nieruchomosc.StawkaSmieciNsel + nieruchomosc.StawkaSmieciSel) + nieruchomosc.StawkaFunduszRemontowy;


                    View rozliczenia = czy.Rozliczenia.CreateView();
                    var cond = RowCondition.Empty;
                    cond &= new FieldCondition.Equal("AdresPelnyNieruchomosci", nieruchomosc.AdresPelnyNieruchomosci);
                    cond &= new FieldCondition.GreaterEqual("Data", Date.Today.FirstDayMonth());
                    cond &= new FieldCondition.LessEqual("Data", Date.Today.LastDayMonth());
                    rozliczenia.Condition = cond;
                    Currency kwota_suma2 = 0;
                    Currency kwotaParagraf42602 = 0;
                    Currency kwotaParagraf43002 = 0;
                    Currency kwotaParagraf42702 = 0;
                    Currency kwotaParagraf4270FR2 = 0;
                    Currency kwotaWoda2 = 0;
                    Currency kwotaScieki2 = 0;
                    Currency kwotaCO2 = 0;
                    Currency kwotaCWU2 = 0;
                    Currency kwotaSmieci2 = 0;
                    Currency kwotaKekspl2 = 0;
                    Currency kwotaKosztyZarz2 = 0;
                    Currency kwotaDomofon2 = 0;
                    Currency kwotaAntena2 = 0;
                    Currency kwotaWinda2 = 0;
                    Currency kwotaFunduszRemontowy2 = 0;

                    foreach (Rozliczenie roz in rozliczenia)
                    {
                        kwotaWoda2 += roz.WodaKorekta;
                        kwotaScieki2 += roz.SciekiKorekta;
                        kwotaCO2 += roz.CoKorekta;
                        kwotaCWU2 += roz.CWUKorekta;
                        kwotaSmieci2 += (roz.SmieciKorekta + roz.SmieciSelKorekta + roz.SmieciNselKorekta);
                        kwotaKekspl2 += roz.KEksplKorekta;
                        kwotaKosztyZarz2 += roz.WynZarzKorekta;
                        kwotaDomofon2 += roz.DomofonKorekta;
                        kwotaAntena2 += roz.AntenaKorekta;
                        kwotaWinda2 += roz.WindaKorekta;
                        kwotaFunduszRemontowy2 += roz.FunduszRemontowyKorekta;

                        kwotaParagraf42602 += (roz.WodaKorekta + roz.CoKorekta + roz.CWUKorekta);
                        kwotaParagraf42702 += (roz.DomofonKorekta + roz.WindaKorekta + roz.AntenaKorekta);
                        kwotaParagraf43002 += (roz.SciekiKorekta + roz.KEksplKorekta + roz.WynZarzKorekta + roz.SmieciKorekta + roz.SmieciSelKorekta + roz.SmieciNselKorekta);
                        kwotaParagraf4270FR2 += roz.FunduszRemontowyKorekta;

                        kwota_suma2 += (roz.WodaKorekta + roz.CoKorekta + roz.CWUKorekta) + (roz.DomofonKorekta + roz.WindaKorekta + roz.AntenaKorekta) + (roz.SciekiKorekta + roz.KEksplKorekta + roz.WynZarzKorekta + roz.SmieciKorekta + roz.SmieciSelKorekta + roz.SmieciNselKorekta) + roz.FunduszRemontowyKorekta;
                    }

                    kwota_suma += kwota_suma2;
                    kwotaParagraf4260 += kwotaParagraf42602;
                    kwotaParagraf4300 += kwotaParagraf43002;
                    kwotaParagraf4270 += kwotaParagraf42702;
                    kwotaParagraf4270FR += kwotaParagraf4270FR2;
                    kwotaWoda += kwotaWoda2;
                    kwotaScieki += kwotaScieki2;
                    kwotaCO += kwotaCO2;
                    kwotaCWU += kwotaCWU2;
                    kwotaSmieci += kwotaSmieci2;
                    kwotaKekspl += kwotaKekspl2;
                    kwotaKosztyZarz += kwotaKosztyZarz2;
                    kwotaDomofon += kwotaDomofon2;
                    kwotaAntena += kwotaAntena2;
                    kwotaWinda += kwotaWinda2;
                    kwotaFunduszRemontowy += kwotaFunduszRemontowy2;
                }
                
                if (kwota_suma < 1)
                    continue;

                using (ITransaction trans = Session.Logout(true))
                {
                    PKEwidencja pk = new PKEwidencja();
                    cm.DokEwidencja.AddRow(pk);
                    DefinicjaDokumentu definicja = cm.DefDokumentow.WgSymbolu["PKWS"];
                    pk.Definicja = definicja;
                    pk.Opis = "PK należności podstawowe " + Date.Today.ToString() + ".";
                    pk.DataDokumentu = Date.Today;
                    string nazwa = zarzadca.NazwaZarzadcy.ToString();
                    pk.NumerDokumentu = "PKWS " + nazwa;
                    OkresObrachunkowy okres = ksm.OkresyObrach.WgSymbolu[Date.Today.Year.ToString()];
                    if (zarzadca.KontoZarzadca3 != string.Empty)
                    {
                        Dekret dekret = new Dekret(okres, pk);
                        ksm.Dziennik.AddRow(dekret);
                        dekret.Data = Date.Today;
                        NowyZapis(ksm, dekret, okres, zarzadca.KontoZarzadca3, "WN", kwota_suma, nazwa, nazwa);
                        NowyZapis(ksm, dekret, okres, zarzadca.KontoParagraf4260, "MA", kwotaParagraf4260, "Paragraf 4260 " + nazwa, nazwa);
                        NowyZapis(ksm, dekret, okres, zarzadca.KontoParagraf4300, "MA", kwotaParagraf4300, "Paragraf 4300 " + nazwa, nazwa);
                        NowyZapis(ksm, dekret, okres, zarzadca.KontoParagraf4270, "MA", kwotaParagraf4270, "Paragraf 4270 " + nazwa, nazwa);
                        NowyZapis(ksm, dekret, okres, zarzadca.KontoParagraf4270FR, "MA", kwotaParagraf4270FR, "Paragraf 4270FR " + nazwa, nazwa);
                        NowyZapis(ksm, dekret, okres, zarzadca.KontoWoda4, "WN", kwotaWoda, "WODA " + nazwa, nazwa);
                        NowyZapis(ksm, dekret, okres, zarzadca.KontoWoda5, "WN", kwotaWoda, "WODA " + nazwa, nazwa);
                        NowyZapis(ksm, dekret, okres, zarzadca.KontoScieki4, "WN", kwotaScieki, "ŚCIEKI " + nazwa, nazwa);
                        NowyZapis(ksm, dekret, okres, zarzadca.KontoScieki5, "WN", kwotaScieki, "ŚCIEKI " + nazwa, nazwa);
                        NowyZapis(ksm, dekret, okres, zarzadca.KontoCO4, "WN", kwotaCO, "CO " + nazwa, nazwa);
                        NowyZapis(ksm, dekret, okres, zarzadca.KontoCO5, "WN", kwotaCO, "CO " + nazwa, nazwa);
                        NowyZapis(ksm, dekret, okres, zarzadca.KontoCWU4, "WN", kwotaCWU, "CWU " + nazwa, nazwa);
                        NowyZapis(ksm, dekret, okres, zarzadca.KontoCWU5, "WN", kwotaCWU, "CWU " + nazwa, nazwa);
                        NowyZapis(ksm, dekret, okres, zarzadca.KontoSmieci4, "WN", kwotaSmieci, "ŚMIECI " + nazwa, nazwa);
                        NowyZapis(ksm, dekret, okres, zarzadca.KontoSmieci5, "WN", kwotaSmieci, "ŚMIECI " + nazwa, nazwa);
                        NowyZapis(ksm, dekret, okres, zarzadca.KontoKekspl4, "WN", kwotaKekspl, "KOSZTY EKSPLOATACJI " + nazwa, nazwa);
                        NowyZapis(ksm, dekret, okres, zarzadca.KontoKekspl5, "WN", kwotaKekspl, "KOSZTY EKSPLOATACJI " + nazwa, nazwa);
                        NowyZapis(ksm, dekret, okres, zarzadca.KontoKosztyZarz4, "WN", kwotaKosztyZarz, "KOSZTY ZARZĄDU " + nazwa, nazwa);
                        NowyZapis(ksm, dekret, okres, zarzadca.KontoKosztyZarz5, "WN", kwotaKosztyZarz, "KOSZTY ZARZĄDU " + nazwa, nazwa);
                        NowyZapis(ksm, dekret, okres, zarzadca.KontoDomofon4, "WN", kwotaDomofon, "DOMOFON " + nazwa, nazwa);
                        NowyZapis(ksm, dekret, okres, zarzadca.KontoDomofon5, "WN", kwotaDomofon, "DOMOFON " + nazwa, nazwa);
                        NowyZapis(ksm, dekret, okres, zarzadca.KontoAntena4, "WN", kwotaAntena, "ANTENA " + nazwa, nazwa);
                        NowyZapis(ksm, dekret, okres, zarzadca.KontoAntena5, "WN", kwotaAntena, "ANTENA " + nazwa, nazwa);
                        NowyZapis(ksm, dekret, okres, zarzadca.KontoWinda4, "WN", kwotaWinda, "WINDA " + nazwa, nazwa);
                        NowyZapis(ksm, dekret, okres, zarzadca.KontoWinda5, "WN", kwotaWinda, "WINDA " + nazwa, nazwa);
                        NowyZapis(ksm, dekret, okres, zarzadca.KontoFunduszRem4, "WN", kwotaFunduszRemontowy, "FUNDUSZ REMONTOWY " + nazwa, nazwa);
                        NowyZapis(ksm, dekret, okres, zarzadca.KontoFunduszRem5, "WN", kwotaFunduszRemontowy, "FUNDUSZ REMONTOWY " + nazwa, nazwa);
                        NowyZapis(ksm, dekret, okres, zarzadca.KontoZarzadca3, "MA", kwota_suma, nazwa, nazwa);
                        NowyZapis(ksm, dekret, okres, zarzadca.KontoZarzadca4, "MA", kwota_suma, nazwa, nazwa);
                    }
                    trans.Commit();
                }
            }

        }

        private static void NowyZapis(KsiegaModule ksm, Dekret dekret, OkresObrachunkowy okres, string Konto, string WNMA, Currency wartosc, string tresc, string opis)
        {
            if (wartosc != Currency.Empty & Konto != string.Empty)
                using (ITransaction trans = Session.Logout(true))
                {
                    KontoBase[] konta = new KontoBase[1];
                    konta[0] = ksm.Konta.WgOkres[okres, Konto];
                    foreach (KontoBase konto in konta)
                    {
                        Zapis zapis = new Zapis(dekret);
                        ksm.ZapisyKsiegowe.AddRow(zapis);
                        zapis.DataPodatkowa = dekret.Data;
                        zapis.Konto = konto;
                        zapis.Features["Treść"] = tresc;
                        zapis.Opis = opis;
                        if (WNMA == "WN")
                        {
                            zapis.WinienOperacji = wartosc;
                        }
                        else
                        {
                            zapis.MaOperacji = wartosc;
                        }
                    }
                    trans.Commit();
                }

        }
    }
}




