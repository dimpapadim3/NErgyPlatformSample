using System;
using System.Collections.Generic;
using System.Text;

using CommonTypes;
using System.Collections;
using CommonTypes.Events;

using MVCFramework.Presenter;
using MVCFramework;


namespace MVCFramework.Presenter.States
{
    public class MoveElementState : ControllState
    {
        protected IModifiable _modiafiable;
        public virtual IModifiable Modiafiable
        {
            get
            {
                return _modiafiable;
            }

            set
            {
                _modiafiable = value;
            }
        }

        Vector c1,c2,diff;

        # region " IViewActionListener Implementation "
       
        public override bool MouseDownHandler(object sender, ActionEventArgs e)
        {
            if ( e.Worldcoord != null)
            c1 = (Vector)e.Worldcoord;
            base.ResolveSelection(sender, e);
            _presenter.NotifyViews();
            return true;
        }
        protected override bool DoObjectSelection(object _object)
        {
            if (_object is IModifiable)
            {
                if (Modiafiable == null)
                {
                    
                    Modiafiable = _object as IModifiable;
                    if (_object is ISelectable)
                        ((ISelectable)_object).IsSelected = true;
                }

            }
            return false;
            
        }
        public override bool MouseMoveHandler(object sender, ActionEventArgs e)
        {
     
          if (e.Worldcoord != null)
            {
                c2 = (Vector)e.Worldcoord;
                if (c1 != null)
                {
                    diff = c2 - c1;
                    diff.Y  = 0;

                    if (Modiafiable != null)
                    {
                       Modiafiable.Move(diff);
                     //  _presenter.UpadateModels();
                      _presenter.NotifyViews();
                    }
                    
                }
            }    
            c1 = c2;
            return true;
        }

        public override bool MouseUpHandler(object sender, ActionEventArgs e)
        {
         if (Modiafiable is ISelectable && Modiafiable != null)
            {
                try
                {
                   // ExecuteMoveCommand();
                }
                catch { }
                ((ISelectable)Modiafiable).IsSelected = false;
                c1 = null; c2 = null; diff = null; Modiafiable = null;
                
                _presenter.NotifyViews();
            }
            return true;
        }

        private void ExecuteMoveCommand()
        {
           diff = c2 - c1; diff.Y = 0;
            Modiafiable.Move(diff);
           MVCFramework.Presenter .Commands . Command move = new MVCFramework.Presenter .Commands. ModifyCommand(_model ,_modiafiable , diff);
            move.Execute();
           _presenter.AddCommand(move);
        }
        #endregion 

        public MoveElementState()
        {

        }

    }

}
