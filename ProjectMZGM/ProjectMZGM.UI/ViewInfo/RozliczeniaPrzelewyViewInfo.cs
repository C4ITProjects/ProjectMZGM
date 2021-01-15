using System;
using System.Linq;

using Soneta.Business;
using Soneta.Business.UI;
using ProjectMZGM.Czynsze;
using ProjectMZGM.UI;
using Soneta.Types;

[assembly: FolderView("MZGM/Czynsze/Przelewy",
    Priority = 15,
    Description = "Przelewy wg rozliczeń",
    TableName = "Rozliczenia",
    ViewType = typeof(RozliczeniaPrzelewyViewInfo),
    BrickColor = FolderViewAttribute.BlueBrick
)]

[assembly: Worker(typeof(ProjectMZGM.UI.RozliczeniaPrzelewyViewInfo))]

namespace ProjectMZGM.UI
{
    public class RozliczeniaPrzelewyViewInfo : ViewInfo
    {
        public RozliczeniaPrzelewyViewInfo()
        {
            // View wiążemy z odpowiednią definicją viewform.xml poprzez property ResourceName
            ResourceName = "RozliczeniaPrzelewy";

            // Inicjowanie contextu
            InitContext += RozliczeniaPrzelewyViewInfo_InitContext;

            // Tworzenie view zawierającego konkretne dane
            CreateView += RozliczeniaPrzelewyViewInfo_CreateView;
        }

        void RozliczeniaPrzelewyViewInfo_InitContext(object sender, ContextEventArgs args)
        {
            args.Context.Remove(typeof(WParams));
            args.Context.TryAdd(() => new WParams(args.Context));

        }

        void RozliczeniaPrzelewyViewInfo_CreateView(object sender, CreateViewEventArgs args)
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
        [Context]
        public Context Cx_context { get; set; }

        [Context]
        public Nieruchomosc Nieruchomosc { get; set; }


        public Soneta.Business.View GetList
        {
            get
            {
                RowCondition cond = new FieldCondition.In("AdresPelnyNieruchomosci", Nieruchomosc.AdresPelnyNieruchomosci);
                return this.Cx_context.Session.GetCzynsze().Rozliczenia.WgAdresuNieruchomosci[cond].CreateView();
            }
        }

        protected View ViewCreate(WParams pars)
        {
            View view = CzynszeModule.GetInstance(pars.Session).Rozliczenia.CreateView();
            return view;
        }

    }
}

