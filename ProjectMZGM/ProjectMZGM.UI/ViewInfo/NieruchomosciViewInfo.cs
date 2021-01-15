using System;
using System.Linq;

using Soneta.Business;
using Soneta.Business.UI;
using ProjectMZGM.Czynsze;
using ProjectMZGM.UI;
using Soneta.Types;

[assembly: FolderView("MZGM/Czynsze/Wspólnoty",
    Priority = 1,
    Description = "Wspólnoty",
    TableName = "Nieruchomosci",
    ViewType = typeof(NieruchomosciViewInfo),
   BrickColor = FolderViewAttribute.GreenBrick
)]

namespace ProjectMZGM.UI
{
    public class NieruchomosciViewInfo : ViewInfo
    {
        public NieruchomosciViewInfo()
        {
            // View wiążemy z odpowiednią definicją viewform.xml poprzez property ResourceName
            ResourceName = "Nieruchomosci";

            // Inicjowanie contextu
            InitContext += NieruchomosciViewInfo_InitContext;

            // Tworzenie view zawierającego konkretne dane
            CreateView += NieruchomosciViewInfo_CreateView;
        }

        void NieruchomosciViewInfo_InitContext(object sender, ContextEventArgs args)
        {
            args.Context.Remove(typeof(WParams));
            args.Context.TryAdd(() => new WParams(args.Context));//do filtrów, usuwa stare, dodaje nowe

            if (!args.Context.Contains(typeof(WParams)))
            {
                args.Context.Set((object)new ActualDate(args.Context, Date.Today));
                args.Context.Set(new WParams(args.Context));
            }
        }

        void NieruchomosciViewInfo_CreateView(object sender, CreateViewEventArgs args)
        {
            var parameters = new WParams(args.Context);
            args.Context.Get(out parameters);
            args.View = ViewCreate(parameters);
            var cond = RowCondition.Empty;
            if (parameters.Zarzadca != null)
                cond &= new FieldCondition.Equal("Zarzadca", parameters.Zarzadca);
            if (parameters.Zablokowany)
                cond &= new FieldCondition.In("Nieaktywny", parameters.Zablokowany);

            args.View.Condition = cond;
        }

        public class WParams : ContextBase
        {
            public WParams(Context context) : base(context)
            {
            }
            /*
            public Date Data
            {
                get
                {
                    if (!Context.Contains(typeof(ActualDate)))
                        return Date.Today;
                    ActualDate actualDate = (ActualDate)Context[typeof(ActualDate)];
                    return actualDate == null || actualDate.Actual == Soneta.Types.Date.Empty ? Soneta.Types.Date.Empty : actualDate.Actual;
                }
                set
                {

                    Context.Set((object)new ActualDate(Context, value));
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
             */

            private Boolean _Zablokowany;
            public Boolean Zablokowany
            {
                get => _Zablokowany;
                set
                {
                    _Zablokowany = value;
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
        }

        [Context]
        public Context Cx_context { get; set; }
        public Soneta.Business.View GetList
        {
            get
            {
                return this.Cx_context.Session.GetCzynsze().Nieruchomosci.WgAdresuNieruchomosci.CreateView();
            }
        }
        protected View ViewCreate(WParams pars)
        {
            View view = null;
            return view;
        }

    }
}
