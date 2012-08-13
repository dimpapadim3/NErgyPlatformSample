using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

using CommonTypes.Events;

namespace MVCFramework.Presenter.States
{
    public class SelectState : ControllState
    {
        private static ISelectable selected;
        public static ISelectable Selected
        {
            get { return selected; }
            set { selected = value; }
        }

        private static IMenuHandlerContainer menuHandlerContainer;
        public static IMenuHandlerContainer MenuHandlerContainer
        {
            get { return menuHandlerContainer; }
            set { menuHandlerContainer = value; }
        }

        log4net.ILog log = log4net.LogManager.GetLogger(typeof(IPresenter));

        # region " IViewActionListener Implementation "

        public override bool MouseDownHandler(object sender, ActionEventArgs e)
        {
            if (e.List == null) return false;
            if (Filter<BuildingBasicDomain.IPoint>(e.List)) return true ;
            if (Filter<BuildingBasicDomain.ILine>(e.List)) return true ;
            if (Filter<BuildingBasicDomain.IArea>(e.List)) return true ;
            return false;
        }

        private bool Filter<T>(ArrayList list)
        {
            bool val = false ;
            foreach (object _object in list)
                if (_object is T)
                    val = val || DoObjectSelection(_object);
           return val ;
        }

        protected override bool DoObjectSelection(object _object)
        {
            if (_object is ISelectable)
            {
                Selected = _object as ISelectable;
                Handle(_object as ISelectable);
            //    _presenter.UpadateModels();
                _presenter.NotifyViews();
                return true;
            }
            return false;
        }

        public override bool MouseMoveHandler(object sender, ActionEventArgs e)
        {
            return false;
        }

        public override bool MouseUpHandler(object sender, ActionEventArgs e)
        {
            ResolveSelection(sender, e);
            selected = null;
            return false;
        }

        #endregion 

        #region "Menu Managing "

        public override ArrayList ParseContextMenu()
        {
            if ( MenuHandlerContainer != null)
                return MenuHandlerContainer.Menulist;
        return new ArrayList();
        }

        protected override IMenuHandlerContainer DoGetMenuHandlerContainer()
        {
            return MenuHandlerContainer;
        }

        #endregion

        public virtual void Handle(ISelectable SelectedObject)
        {
            if (SelectedObject != null)
            {
                if (SelectedObject.IsSelected)
                {
                    SelectedObject.IsSelected = false;
                    _presenter.RemoveSelection(SelectedObject);
                    return;
                }
                if (!SelectedObject.IsSelected)
                {
                    SelectedObject.IsSelected = true;
                    _presenter.AddSelection(SelectedObject);
                    return;
                }
                
                log.Info(SelectedObject.ToString() + " IsSelected = " + SelectedObject.IsSelected);
            }
        }

        public SelectState()
        {

        }
    }
    public class MultiSelectState : ControllState
    {
        private static ISelectable selected;
        public static ISelectable Selected
        {
            get { return selected; }
            set { selected = value; }
        }

        private static IMenuHandlerContainer menuHandlerContainer;
        public static IMenuHandlerContainer MenuHandlerContainer
        {
            get { return menuHandlerContainer; }
            set { menuHandlerContainer = value; }
        }

        log4net.ILog log = log4net.LogManager.GetLogger(typeof(IPresenter));

        # region " IViewActionListener Implementation "

        public override bool MouseDownHandler(object sender, ActionEventArgs e)
        {
            ResolveSelection(sender, e);
            _presenter.NotifyViews<MVCFramework.View.IViewForm>();
            return true ;
        }

        protected override bool DoObjectSelection(object _object)
        {
            if (_object is ISelectable)
            {
                Selected = _object as ISelectable;
                Handle(_object as ISelectable);

                return true;
            }
            return false;
        }

        public override bool MouseMoveHandler(object sender, ActionEventArgs e)
        {
            return false;
        }

        public override bool MouseUpHandler(object sender, ActionEventArgs e)
        {
            ResolveSelection(sender, e);
            _presenter.NotifyViews<MVCFramework.View.IViewForm>();
            selected = null;
            return false;
        }

        #endregion

        #region "Menu Managing "

        public override ArrayList ParseContextMenu()
        {
            if (MenuHandlerContainer != null)
                return MenuHandlerContainer.Menulist;
            return new ArrayList();
        }

        protected override IMenuHandlerContainer DoGetMenuHandlerContainer()
        {
            return MenuHandlerContainer;
        }

        #endregion

        public virtual void Handle(ISelectable SelectedObject)
        {
            if (SelectedObject != null)
            {
                if (SelectedObject.IsSelected)
                {
                    SelectedObject.IsSelected = false;
                    _presenter.RemoveSelection(SelectedObject);
                    return;
                }
                if (!SelectedObject.IsSelected)
                {
                    SelectedObject.IsSelected = true;
                    _presenter.AddSelection(SelectedObject);
                    return;
                }

                log.Info(SelectedObject.ToString() + " IsSelected = " + SelectedObject.IsSelected);
            }
        }

        public MultiSelectState()
        {

        }
    }
}
