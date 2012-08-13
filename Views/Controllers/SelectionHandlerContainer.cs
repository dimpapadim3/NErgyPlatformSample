using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using BuildingBasicDomain;

using MVCFramework.Presenter.States;
using MVCFramework.Presenter;
using BuildingStructuralElements;

namespace BuildingAplication
{
    public class SelectHandlersContainer : MenuHandlerContainer
    {
        ApplicationPresenter presenter;

        public SelectHandlersContainer(ApplicationPresenter presenter)
        {
            this.presenter = presenter;
        }
        public void AddLoadHandler()
        {
            try
            {
              foreach (object Selected in  presenter .SelectedObjects )
                  if (Selected is ILine && Selected != null) 
                  {
                      presenter._creator .CreateLoad(Selected as ILine);
                  }
               
            }
            catch { }
        }
        public void SplitLineHandler()
        {
            try
            {
     
            }
            catch { }
        }
        public void SplitBeamHandler()
        {

        }
        public void AddSurfaceLoadHandler()
        {
            object Selected = SelectState.Selected;
            try
            {

                if (Selected is IArea)
                {
               //    state._creator.CreateLoad(Selected as IArea);
                }
            }
            catch { }
        }

        #region IMenuHandlerContainer Members
        protected override void ParseMenu()
        {
            object Selected = SelectState.Selected;
            menulist = new ArrayList();
/*
            if (Selected is IPoint)
            {
                menulist.Add("AddLoad");
                menulist.Add("Function2");
                menulist.Add("Function3");
                menulist.Add("Function4");
            }
            if (Selected is ILine)
            {
                menulist.Add("AddLoad");
            }
            if (Selected is NodeLine)
            {
                menulist.Add("SplitLine");
            }

            if (Selected is IArea)
            {
                menulist.Add("AddSurfaceLoad");
            }
*/
            if (Selected is IPoint || Selected is IGeometry)
            {
                menulist.Add(new LoadCommand (Selected,presenter));
             
            }
            if (Selected is INodeLine)
            {
                menulist.Add(new SplitCommand(Selected as INodeLine));
            }

            base.ParseMenu();
        }

        #endregion

        public override IList<MVCFramework.Menus.IMenuCommand> GetCommands()
        {
            throw new Exception("The method or operation is not implemented.");
        }
    }

    public class LoadCommand :MVCFramework .Menus .IMenuCommand 
    {
        object  Geometry;
        ApplicationPresenter _presenter;

        public LoadCommand(object geometry,ApplicationPresenter  _presenter )
        {
            this._presenter = _presenter;
            this.Geometry = geometry;
        }

        #region IMenuCommand Members

        public void Execute()
        {

            if (Geometry is ILine)
            { _presenter._creator.CreateLoad(Geometry as ILine); }
            if (Geometry is IArea)
            { _presenter._creator.CreateLoad(Geometry as IArea); }      
           
        }

        public string MenuCaption
        {
            get
            {
                return "AddLoad";
            }
            set
            {
                throw new Exception("The method or operation is not implemented.");
            }
        }

        #endregion
    }
    public class SplitCommand : MVCFramework.Menus.IMenuCommand
    {
        INodeLine line;
        public SplitCommand(INodeLine line)
        {
            this.line = line;
        }
        #region IMenuCommand Members

        public void Execute()
        {
            line.SplitLine(5);
        }

        public string MenuCaption
        {
            get
            {
                return "Split";
            }
            set
            {
                throw new Exception("The method or operation is not implemented.");
            }
        }

        #endregion
    }
}
