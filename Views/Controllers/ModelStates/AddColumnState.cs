using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;


using BuildingBasicDomain;
using MVCFramework.Models;
using BuildingStructuralElements;
using CommonTypes.Events;
using MVCFramework.Presenter.States;
using MVCFramework.Presenter;



namespace BuildingAplication.States
{

    public class AddColumnFree : AddBeamState
    {
        public AddColumnFree()
        {

        }

        public override void StateChangeHandler(object sender, EventArgs e)
        {
            Clear();
        }

        private void Clear()
        {
        }

        # region " IViewActionListener Implementation "

        public override bool MouseDownHandler(object sender, ActionEventArgs e)
        {
            //ResolveSelection(sender, e);
            return false;
        }

        protected override bool DoObjectSelection(object _object)
        {
            if (_object is IPoint)
            {
                ProcessPoint(_object as IPoint );
                return true;
            }
            return false;
        }

        private void ProcessPoint(IPoint  _object)
        {

            if (_model is ApplicationModel)
            {
                IList<StructuralElement> elementsToBeAdded = ((ApplicationModel)_model).GetColumnsAndSupports(_object as IPoint);
                _presenter.RequestAddition<StructuralElement>(elementsToBeAdded);
            }
         //   _presenter.UpadateModels();

        }

        protected override void DoHandleCoordinates(double[] coords)
        {
            IPoint freepoint = new Node(coords[0], coords[1], coords[2]);
            ProcessPoint(freepoint);

        }

        public override bool MouseMoveHandler(object sender, ActionEventArgs e)
        {
            _presenter.NotifyViews<MVCFramework.View.ViewForm>();
            return true;
        }

        public override bool MouseUpHandler(object sender, ActionEventArgs e)
        {
            ResolveSelection(sender, e);
            return true;
        }

        #endregion
    }
     
}
