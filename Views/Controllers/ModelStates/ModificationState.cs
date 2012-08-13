using System;
using System.Collections.Generic;
using System.Text;
using BuildingBasicDomain;
using MVCFramework.Presenter.States;
using MVCFramework.Presenter;
using MVCFramework;

using BuildingStructuralElements;


namespace BuildingAplication.States
{
    public abstract class LineModificationState: ControllState 
    {
      protected  INodeLine firstLine;
      protected  ILine  secondLine;
      protected void Clear()
        {
            if (firstLine is ISelectable )
                ((ISelectable)firstLine).IsSelected = false;
            if (secondLine is ISelectable)
                ((ISelectable)secondLine).IsSelected = false;


            firstLine = null;
            secondLine = null;
        }

      protected  void Handle(ILine _object)
        {
            if (firstLine == null && _object is INodeLine)
            {
                firstLine = _object as INodeLine;
                if (_object is ISelectable)
                    ((ISelectable)_object).IsSelected = true;
            }
            
            else if (firstLine != null && secondLine == null )
            {
                secondLine = _object ;
                if ( _object is ISelectable )
                ((ISelectable)secondLine).IsSelected = true;

                Modification();

                Clear();
            }
        }

      protected virtual  void Modification()
        {
            
        }
    }

    public class ExtendState:LineModificationState 
    {

        # region "Singleton Implementation"
        private static ControllState instance;

        public static ControllState Instance
        {
            get
            {
                if (instance == null)
                    instance = new ExtendState();


                return instance;
            }

        }
        #endregion

        # region "IViewActionListener Implementation"
        public override bool MouseDownHandler(object sender, ActionEventArgs e)
        {
            base.ResolveSelection(sender, e);
            return true;
        }
        protected override bool  DoObjectSelection(object _object)
        {
            if (_object is ILine )
            {
                Handle(_object as ILine );
            }
            return false;
        }

        protected override void Modification()
        {
            ModificationWorkBench bench = new ModificationWorkBench();
            bench.extendLine(secondLine, ref firstLine);
        }

        public override bool MouseMoveHandler(object sender,ActionEventArgs e)
        {
            return false;
        }

        public override bool MouseUpHandler(object sender, ActionEventArgs e)
        {
            return false;
        }
        #endregion
    }

    public class TrimState:LineModificationState 
    {
        # region "Singleton Implementation"
        private static ControllState instance;

        public static ControllState Instance
        {
            get
            {
                if (instance == null)
                    instance = new TrimState();


                return instance;
            }

        }
        #endregion
        public override bool MouseDownHandler(object sender,ActionEventArgs e)
        {
            base.ResolveSelection(sender, e);
            return true;
        }
        protected override bool DoObjectSelection(object _object)
        {
            if (_object is ILine )
            {
                Handle(_object as ILine );
            }
            return false;
        }

        protected override void Modification()
        {
            ModificationWorkBench bench = new ModificationWorkBench();
            bench.trimLine(secondLine, ref firstLine);
        }

        public override bool MouseMoveHandler(object sender, ActionEventArgs e)
        {
            return false;
        }

        public override bool MouseUpHandler(object sender, ActionEventArgs e)
        {
            return false;
        }
      
    }
}
