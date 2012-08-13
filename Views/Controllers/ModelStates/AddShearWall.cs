using System;

using BuildingBasicDomain;
using BuildingStructuralElements;
using MVCFramework.Presenter.States;

namespace BuildingAplication.States
{
    public class AddShearWall: ControllState 
    {
        public AddShearWall()
        {

        }

        public override bool MouseDownHandler(object sender, MVCFramework.Presenter.ActionEventArgs e)
        {
            IArea area = ((BuildingAplication.BuildingView.AppView)_presenter.ActiveView).GetLeastEnclosingArea((float)e.Coordinates[0], (float)e.Coordinates[1]);

            if (area.isVertical())
            {
                ShearWall wall = new ShearWall (area);
                _presenter.RequestAddition<StructuralElement>(wall);
            }
            return false;
        }

        public override bool MouseMoveHandler(object sender, MVCFramework.Presenter.ActionEventArgs e)
        {
            return false; 
        }

        public override bool MouseUpHandler(object sender, MVCFramework.Presenter.ActionEventArgs e)
        {
            return false;
        }
    }
}
