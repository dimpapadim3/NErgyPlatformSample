using System;
using System.Collections.Generic;
using System.Text;


using BuildingBasicDomain;
using BuildingStructuralElements;
using CommonTypes.Events;
using MVCFramework.Presenter.States;
using MVCFramework.Presenter;
using BuildingGrid;

namespace BuildingAplication.States
{
    public class AddBraceInRegion : ControllState 
    {     
        public AddBraceInRegion()
        {

        }
     
        private static void Clear()
        {

        }

        # region " IViewActionListener Implementation "

        public override bool MouseDownHandler(object sender, ActionEventArgs e)
        {
            IArea area = ((BuildingAplication.BuildingView.AppView)_presenter.ActiveView).GetLeastEnclosingArea((float)e.Coordinates[0], (float)e.Coordinates[1]);

            Bracing bracing = Bracing.CreateBracing(area);
            _presenter.RequestAddition<StructuralElement>(bracing);

            return false;
        }

        private static IList <BeamElement> NewMethod(IArea _area)
        {
            if (_area == null || _area.Polygon.Count == 0) return null;
           IList <BeamElement > bracing = new List <BeamElement >();
            //bracing.bracingBeams = new MVCFramework.DomainCollection<BeamElement>(false);
            bracing.Add(BeamElement.CreateElement(NodeLine.Create(_area.Polygon[0], _area.Polygon[2])));
            bracing.Add(BeamElement.CreateElement(NodeLine.Create(_area.Polygon[1], _area.Polygon[3])));
            return  bracing ;
        }

        public override bool MouseMoveHandler(object sender, ActionEventArgs e)
        {
            return false;
        }

        public override bool MouseUpHandler(object sender, ActionEventArgs e)
        {
            return false;
        }

        #endregion
    }
}
