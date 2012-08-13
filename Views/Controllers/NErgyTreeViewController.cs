using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using BuildingStructuralElements;
using CommonTypes.Math;
using NCad.Application.Views.Controllers;
using NErgy.Silverlight.Models;

namespace NErgy.Silverlight.Views.Controllers
{
    public class NErgyTreeViewController : TreeViewController
    {
        protected Mediator NErgyMediator { get { return Mediator as Mediator; } }

        protected ThermalBuildingModel ThermalModel
        {
            get
            {
                return (ThermalBuildingModel)base.Model;
            }
        }

        public override void GenerateMenuItems(Action<string, RoutedEventHandler> addMenuItem)
        {

            addMenuItem("Copy Level", delegate { (Model as ThermalBuildingModel).CopyStory(); });
            addMenuItem("Create New Level", delegate { (Model as ThermalBuildingModel).CreateNewStory(); });
            addMenuItem("Create Roof", delegate { (Model as ThermalBuildingModel).CreateRoof(); });

            if (Model.CurrentSelectedItem is Slab)
            {
                addMenuItem("Create Walls On Slab Edges", delegate { (Model as ThermalBuildingModel).CreateWallsOnSlabEdges(); });

                addMenuItem("Get In The room", delegate
                {
                    //var area = (Model.CurrentSelectedItem as Slab);
                    //var midle = area.GetMiddle();
                    //var actorposition = midle.V + new Vector3D(area.normal.X, area.normal.Y, area.normal.Z) * 2;
                    //(this.View as BalderView).AnimateCamera(actorposition);

                });
            }
            if (Model.CurrentSelectedItem is BuildingStory)
            {
                addMenuItem("Edit Story", delegate { NErgyMediator.EditStory(Model.CurrentSelectedItem as BuildingStory); });
            }
            if (Model.CurrentSelectedItem is ShearWall)
            {
                addMenuItem("Create Door", delegate { ThermalModel.CreateDefaultDoorOpeningOnWall(); });
                addMenuItem("Create Window", delegate { ThermalModel.CreateWindowOpeningOnWall(); });
                addMenuItem("Edit Surface", delegate { NErgyMediator.EditSurface(Model.CurrentSelectedItem as StructuralArea); });
                addMenuItem("Create Beam On Top", delegate { ThermalModel.CreateBeamElementOnTop(Model.CurrentSelectedItem as StructuralArea); });
                addMenuItem("Create Side Columns", delegate { ThermalModel.CreateSideColumn(Model.CurrentSelectedItem as StructuralArea); });
                addMenuItem("Open Section Editor", delegate { NErgyMediator.OpenNewSectionEditor(Model.CurrentSelectedItem); });
                addMenuItem("Add Shading Element", delegate
                {
                    ThermalModel.AddCantiliverShadingForArea(Model.CurrentSelectedItem as StructuralArea);
                });

            }
            if (Model.CurrentSelectedItem is Opening)
            {
                var area = (Model.CurrentSelectedItem as Opening);
                var midle = area.GetMiddle();
                var actorposition = midle.V + new Vector3D(area.normal.X, area.normal.Y, area.normal.Z) * 5;

                addMenuItem("take a look in the door", delegate
                {
            
                });
                addMenuItem("Open Openings Editor", delegate { NErgyMediator.OpenNewOpeningsEditor(Model.CurrentSelectedItem); });

            }
            base.GenerateMenuItems(addMenuItem);
        }

    }
}
