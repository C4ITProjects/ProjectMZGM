using System;
using System.Linq;

using Soneta.Business;
using Soneta.Business.UI;
using ProjectMZGM.Czynsze;
using ProjectMZGM.UI;
using Soneta.Types;

[assembly: FolderView("MZGM/Czynsze/Należności",
    Priority = 15,
    Description = "Należności Miesięcznie",
    TableName = "Rozliczenia",
    ViewType = typeof(RozliczeniaMiesiecznieViewInfo),
    BrickColor = FolderViewAttribute.BlueBrick
)]

[assembly: Worker(typeof(ProjectMZGM.UI.RozliczeniaMiesiecznieViewInfo))]

namespace ProjectMZGM.UI
{
    public class RozliczeniaMiesiecznieViewInfo : ViewInfo
    {
        public RozliczeniaMiesiecznieViewInfo()
        {
            // View wiążemy z odpowiednią definicją viewform.xml poprzez property ResourceName
            ResourceName = "RozliczeniaMiesiecznie";

            // Inicjowanie contextu
            InitContext += RozliczeniaMiesiecznieViewInfo_InitContext;

            // Tworzenie view zawierającego konkretne dane
            CreateView += RozliczeniaMiesiecznieViewInfo_CreateView;
        }

        void RozliczeniaMiesiecznieViewInfo_InitContext(object sender, ContextEventArgs args)
        {
            args.Context.Remove(typeof(WParams));
            args.Context.TryAdd(() => new WParams(args.Context));

        }

        void RozliczeniaMiesiecznieViewInfo_CreateView(object sender, CreateViewEventArgs args)
        {
            var parameters = new WParams(args.Context);
            args.Context.Get(out parameters);
            args.View = ViewCreate(parameters);
            var cond = RowCondition.Empty;
            if (parameters.Zarzadca != null)
                cond &= new FieldCondition.Equal("Zarzadca", parameters.Zarzadca);
            if (parameters.Nieruchomosc != null)
                cond &= new FieldCondition.Equal("AdresPelnyNieruchomosci", parameters.Nieruchomosc.AdresPelnyNieruchomosci);
            if (parameters.Data != Date.Empty)
            {
                cond &= new FieldCondition.LessEqual("Data", parameters.Data.LastDayMonth());
                cond &= new FieldCondition.GreaterEqual("Data", parameters.Data.FirstDayMonth());
            }
            args.View.Condition = cond;
        }

        public class WParams : ContextBase
        {
            public WParams(Context context) : base(context)
            { }
            private Date _data = Date.Today;
            public Date Data
            {
                get => _data;
                set
                {
                    _data = value;
                    Context.Set(this);
                }
            }

            private Zarzadca _zarzadca = null;
            public Zarzadca Zarzadca
            {
                get => _zarzadca;
                set
                {
                    _zarzadca = value;
                    Context.Set(this);
                }
            }
            private Nieruchomosc _nieruchomosc = null;
            public Nieruchomosc Nieruchomosc
            {
                get => _nieruchomosc;
                set
                {
                    _nieruchomosc = value;
                    Context.Set(this);
                }
            }
        }
        protected View ViewCreate(WParams pars)
        {
            View view = CzynszeModule.GetInstance(pars.Session).Rozliczenia.CreateView();
            return view;
        }

    }
}

