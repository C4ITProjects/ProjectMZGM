using System;
using System.Linq;

using Soneta.Business;
using Soneta.Business.UI;
using ProjectMZGM.UI;
using Soneta.Types;
using ProjectMZGM.Czynsze;
/*
[assembly: FolderView("MZGM",
    Priority = 0,
    Description = "Zarzadcy",
   // TableName = "Zarzadcy",
    BrickColor = FolderViewAttribute.BlueBrick
)]
*/

[assembly: FolderView("MZGM/Czynsze",
    Priority = 1,
    Description = "Zarzadcy",
    //TableName = "Zarzadcy",
    BrickColor = FolderViewAttribute.BlueBrick
)]

[assembly: FolderView("MZGM/Czynsze/Zarzadcy",
    Priority = 2,
    Description = "Zarzadcy",
    TableName = "Zarzadcy",
    ViewType = typeof(ZarzadcyViewInfo),
    BrickColor = FolderViewAttribute.BlueBrick
)]
namespace ProjectMZGM.UI
{
    public class ZarzadcyViewInfo : ViewInfo
    {
        public ZarzadcyViewInfo()
        {
            // View wiążemy z odpowiednią definicją viewform.xml poprzez property ResourceName
            ResourceName = "Zarzadcy";

            // Inicjowanie contextu
            InitContext += ZarzadcyViewInfo_InitContext;

            // Tworzenie view zawierającego konkretne dane
            CreateView += ZarzadcyViewInfo_CreateView;
        }

        void ZarzadcyViewInfo_InitContext(object sender, ContextEventArgs args)
        {
            args.Context.Remove(typeof(WParams));
            args.Context.TryAdd(() => new WParams(args.Context));
        }

        void ZarzadcyViewInfo_CreateView(object sender, CreateViewEventArgs args)
        {
            ZarzadcyViewInfo.WParams parameters;
            if (!args.Context.Get(out parameters))
                return;
            args.View = ViewCreate(parameters);
        }

        public class WParams : ContextBase
        {
            public WParams(Context context) : base(context) { }
            
            private Date _data = Date.Empty;
            public Date Data
            {
                get => _data;
                set
                {
                    _data = value;
                    Context.Set(this);
                }
            }

            private Boolean _naleznosci;
            public Boolean Naleznosci
            {
                get => _naleznosci;
                set
                {
                    _naleznosci = value;
                    Context.Set(this);
                }
            }

            private Boolean _rozliczenie;
            public Boolean Rozliczenie
            {
                get => _rozliczenie;
                set
                {
                    _rozliczenie = value;
                    Context.Set(this);
                }
            }


        }




        protected View ViewCreate(WParams pars)
        {
            View view = CzynszeModule.GetInstance(pars.Session).Zarzadcy.CreateView();
            return view;
        }

    }
}
