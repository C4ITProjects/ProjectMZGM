using Soneta.Business;
using Soneta.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[assembly: NewRow(typeof(ProjectMZGM.Rozliczenie))]
namespace ProjectMZGM
{
    public class Rozliczenie : Czynsze.CzynszeModule.RozliczenieRow
    {
        [Context]
        public Nieruchomosc Nieruchomosc { get; set; }
        protected override void OnAdded()
        {
            base.OnAdded();
            //Data = DateTime.Today;
            //AdresPelnyNieruchomosci = Nieruchomosc.AdresPelnyNieruchomosci;
        }

        public override string ToString()
        {
            return AdresPelnyNieruchomosci.ToString() + " " + Data.ToString();
        }

    }
}
