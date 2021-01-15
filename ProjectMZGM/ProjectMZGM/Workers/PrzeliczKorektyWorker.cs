using System;
using Soneta.Business;
using Soneta.Business.UI;
using ProjectMZGM.Workers;
using Soneta.Types;

[assembly: Worker(typeof(PrzeliczKorektyWorker), typeof(ProjectMZGM.Rozliczenie))]
namespace ProjectMZGM.Workers
{
    public class PrzeliczKorektyWorker
    {


        [Action("Przelicz Korekty", Priority = 1, Icon = ActionIcon.Wizard, Target = ActionTarget.ToolbarWithText,
             Mode = ActionMode.OnlyWinForms | ActionMode.OnlyForm | ActionMode.SingleSession | ActionMode.NonAccept)]
        public MessageBoxInformation ToDo()
        {

            return new MessageBoxInformation("Przelicz")
            {
                Text = "Czy rozpocząć przeliczanie?",
                YesHandler = () =>
                {
                    Przelicz();
                    return "Zakończono przeliczanie";
                },

                NoHandler = () => "Przerwano przeliczanie"
            };


        }

        [Context]
        public static Session Session { get; set; }
        [Context]
        public static Rozliczenie Rozliczenie { get; set; }

        public static void Przelicz()
        {


            using (ITransaction trans = Session.Logout(true))
            {
                Rozliczenie.Paragraf4260Korekta = Rozliczenie.WodaKorekta + Rozliczenie.CoKorekta + Rozliczenie.CWUKorekta;
                Rozliczenie.Paragraf4300Korekta = Rozliczenie.SciekiKorekta + Rozliczenie.KEksplKorekta + Rozliczenie.WynZarzKorekta + Rozliczenie.SmieciSelKorekta + Rozliczenie.SmieciNselKorekta + Rozliczenie.EnergiaKorekta + Rozliczenie.SmieciKorekta;
                Rozliczenie.Paragraf4270Korekta = Rozliczenie.DomofonKorekta + Rozliczenie.WindaKorekta + Rozliczenie.AntenaKorekta;
                Rozliczenie.FunduszRemontowyParagraf4270Korekta = Rozliczenie.FunduszRemontowyKorekta;
                Rozliczenie.SumaKorekt = Rozliczenie.Paragraf4260Korekta + Rozliczenie.Paragraf4270Korekta + Rozliczenie.Paragraf4300Korekta + Rozliczenie.FunduszRemontowyParagraf4270Korekta;

                Rozliczenie.ZaplataZaliczka = Rozliczenie.SumaStawekBezFR + (Rozliczenie.SumaKorekt - Rozliczenie.FunduszRemontowyParagraf4270Korekta) + Rozliczenie.ZaplataZaliczka1 + Rozliczenie.ZaplataZaliczka2 + Rozliczenie.ZaplataZaliczka3;
                Rozliczenie.ZaplataFundusz = Rozliczenie.FunduszRemontowy + Rozliczenie.FunduszRemontowyParagraf4270Korekta + Rozliczenie.ZaplataFundusz1 + Rozliczenie.ZaplataFundusz2 + Rozliczenie.ZaplataFundusz3;
                Rozliczenie.SumaParagraf4260 = Rozliczenie.Paragraf4260 + Rozliczenie.Paragraf4260Korekta;
                Rozliczenie.SumaParagraf4270 = Rozliczenie.Paragraf4270 + Rozliczenie.Paragraf4270Korekta;
                Rozliczenie.SumaParagraf4300 = Rozliczenie.Paragraf4300 + Rozliczenie.Paragraf4300Korekta;
                Rozliczenie.SumaParagraf4270FR = Rozliczenie.FunduszRemontowyParagraf4270 + Rozliczenie.FunduszRemontowyParagraf4270Korekta;

                trans.Commit();
            }


        }


    }
}



