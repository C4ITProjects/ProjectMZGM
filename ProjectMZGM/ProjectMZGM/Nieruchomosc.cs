using Soneta.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[assembly: NewRow(typeof(ProjectMZGM.Nieruchomosc))]

namespace ProjectMZGM
{
   public class Nieruchomosc : ProjectMZGM.Czynsze.CzynszeModule.NieruchomoscRow
    {

        protected override void OnAdded()
        {
            base.OnAdded();

            // w tym miejscu można zainicjować interesujące nas pola nowego wiersza
        }


        public override string ToString()
        {
            /*string wynik2 = " / ";
            if (NumerMieszkaniaNieruchomosci != null)
                wynik2 += NumerMieszkaniaNieruchomosci;
            string wynik = UlicaNieruchomosci + " " + NumerBramyNieruchomosci + wynik2;
            return wynik;*/
            return AdresPelnyNieruchomosci;
        }
    }
}
