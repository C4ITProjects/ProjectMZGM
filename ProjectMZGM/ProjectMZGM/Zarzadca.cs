using Soneta.Business;
using Soneta.Types;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[assembly: NewRow(typeof(ProjectMZGM.Zarzadca))]

namespace ProjectMZGM
{
    public class Zarzadca : Czynsze.CzynszeModule.ZarzadcaRow
    {
        
        public override string ToString()
        {
            return NazwaZarzadcy;
        }
        
        protected override void OnAdded()
        {
            base.OnAdded();

        }

     
    }
}
