using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Balder;
using Balder.Materials;
using Balder.Objects.Geometries;
using CommonTypes.Math;
using NCad.Application.Views.Controllers;
using NCad.Views.Views3D;
using NErgy.Building;
using BuildingStructuralElements;
using NCad.Models;
using System.Collections.Generic;
using NErgy.Silverlight.Models;
//using SL4PopupMenu;
using Balder.Animation.Silverlight;
using SimpleMvvmToolkit;

namespace NErgy.Silverlight.Views.Controllers
{
    public class Controller3DBalder : BalderViewController
    {
        private ResourceWrapper resourceWrapper;
        public Controller3DBalder()
        {
            resourceWrapper = new ResourceWrapper();
        }
        protected IThermalBuildingModel ThermalModel
        {
            get
            {
                return (IThermalBuildingModel)base.Model;
            }
        }

        protected Mediator NErgyMediator { get { return Mediator as Mediator; } }

        protected override System.Collections.Generic.List<Balder.Objects.Geometries.Geometry> CreateElementGeometriesBase(object modelElement)
        {
            List<Balder.Objects.Geometries.Geometry> geometries = new List<Balder.Objects.Geometries.Geometry>();

            if (modelElement is NErgy.Building.NeighborBuilding)
            {
                NErgy.Building.NeighborBuilding building = (NErgy.Building.NeighborBuilding)modelElement;
                geometries.Add(CreateNeigbourBuildingGeometry(building));
            }

            if (modelElement is BuildingStory)
            {
                BuildingStory buildingStory = (BuildingStory)modelElement;
                geometries.Add(CreateStoryGeometry(buildingStory));
            }
            if (modelElement is ShaderElement)
            {
                geometries.Add(CreateShaderGeometry(modelElement as ShaderElement));
            }

            base.CreateElementGeometriesBase(modelElement).ForEach(geometries.Add);

            return geometries;
        }

        private Geometry CreateShaderGeometry(ShaderElement shaderElement)
        {
            var geometry = CreateSlabGeometry(shaderElement);
            geometry.InitialMaterial = new Material
                                           {
                                               Diffuse = Colors.Orange,
                                               Shade = MaterialShade.Gouraud
                                           };
            return geometry;
        }

        private Balder.Objects.Geometries.Geometry CreateNeigbourBuildingGeometry(NeighborBuilding building)
        {
            Geometry renderedGeometry = new Balder.Objects.Geometries.Geometry();
            renderedGeometry.Children.Add(new Box()
            {
                Dimension = new Balder.Math.Coordinate(building.Breadth, building.Height, building.Width),
                Position = new Balder.Math.Coordinate(building.X - building.Breadth / 2, 0, building.Y - building.Width / 2),

                InitialMaterial = new Material
                {
                    Diffuse = Colors.Gray,
                    Shade = MaterialShade.Gouraud
                }
            });

            renderedGeometry.IsVisible = true;

            return renderedGeometry;
        }

        private Balder.Objects.Geometries.Geometry CreateStoryGeometry(BuildingStory buildingStory)
        {
            StructuralArea layer = new BuildingBuilder().CreateSlab(
             new BuildingBasicDomain.Line(
             new BuildingBasicDomain.Point(0, buildingStory.EndingStoryHeightLevel, 0),
             new BuildingBasicDomain.Point(0, buildingStory.EndingStoryHeightLevel, 1)),
             1, -BuildingBuilder.xDirection);

            return CreateSlabGeometry(layer);
        }

        protected override void OnItemSelected(object sender, EventArgs e)
        {
            base.OnItemSelected(sender, e);
        }

        public void AnimateCamera()
        {

            // // Create a duration of 2 seconds.
            // Duration duration = new Duration(TimeSpan.FromSeconds(2));

            // // Create two DoubleAnimations and set their properties.
            // CoordinateAnimation myDoubleAnimation1 = new CoordinateAnimation();
            // myDoubleAnimation1.BeginTime = TimeSpan.FromSeconds(2) ;
            // myDoubleAnimation1.Duration = new Duration(TimeSpan.FromSeconds(2));
            // myDoubleAnimation1.TargetName = "View.Position";

            // //myDoubleAnimation1.Duration = duration;


            // Storyboard sb = new Storyboard();
            // sb.Duration = duration;

            // StoryboardExtensions.SetCoordinateAnimation(sb, myDoubleAnimation1);

            // //Storyboard.SetTarget(myDoubleAnimation1, View.Camera);

            // // Set the attached properties of Canvas.Left and Canvas.Top
            // // to be the target properties of the two respective DoubleAnimations.
            // //Storyboard.SetTargetProperty(myDoubleAnimation1, new PropertyPath("Target.X"));
            // //Storyboard.SetTargetProperty(myDoubleAnimation2, new PropertyPath("(Canvas.Top)"));

            //// myDoubleAnimation1.To = 200;

            // // Make the Storyboard a resource.
            // this.View.Resources.Add("unique_id", sb);

            // // Begin the animation.
            // sb.Begin();
        }

        static Material phasedOutMaterial = new Material
        {
            Diffuse = Colors.White,
            Shade = MaterialShade.Gouraud
        };

        public override IList<MenuItem> GenerateMenuItems()
        {
            var menuItems = new List<MenuItem>();
            menuItems.Add(new MenuItem()
                              {
                                  Header = ApplicationStrings.CopyLevel,
                                  Command = new DelegateCommand(() => { (Model as ThermalBuildingModel).CopyStory(); })
                              });
            menuItems.Add(new MenuItem()
            {
                Header = ApplicationStrings.CreateNewLevel,
                Command = new DelegateCommand(() => { (Model as ThermalBuildingModel).CreateNewStory(); })
            });
            menuItems.Add(new MenuItem()
            {
                Header = ApplicationStrings.CreateRoof,
                Command = new DelegateCommand(() => { (Model as ThermalBuildingModel).CreateRoof(); })
            });


          
            if (Model.CurrentSelectedItem is Slab)
            {
                menuItems.Add(new MenuItem()
                {
                    Header = "Open Section Editor",
                    Command = new DelegateCommand(() => { NErgyMediator.OpenNewSectionEditor(Model.CurrentSelectedItem); })
                });

                menuItems.Add(new MenuItem()
                {
                    Header = "Create Walls On Slab Edges",
                    Command = new DelegateCommand(() => { (Model as ThermalBuildingModel).CreateWallsOnSlabEdges(); })
                });

                menuItems.Add(new MenuItem()
                {
                    Header = ApplicationStrings.CreateRoof,
                    Command = new DelegateCommand(() =>
                    {
                        var area = (Model.CurrentSelectedItem as Slab);
                        var midle = area.GetMiddle();
                        var actorposition = midle.V + new Vector3D(area.normal.X, area.normal.Y, area.normal.Z) * 2;
                        (this.View as BalderView).AnimateCamera(actorposition);
                    })
                });

            }
            menuItems.Add(new MenuItem()
            {
                Header = "Edit Story",
                Command = new DelegateCommand(() => { NErgyMediator.EditStory(Model.CurrentSelectedItem as BuildingStory); })
            });
         
          
            if (Model.CurrentSelectedItem is ShearWall)
            {
                menuItems.Add(new MenuItem()
                {
                    Header = "Create Door",
                    Command = new DelegateCommand(() => { ThermalModel.CreateDefaultDoorOpeningOnWall(); })
                });

                menuItems.Add(new MenuItem()
                {
                    Header = "Create Window",
                    Command = new DelegateCommand(() => { ThermalModel.CreateWindowOpeningOnWall(); })
                });
                menuItems.Add(new MenuItem()
                {
                    Header = "Edit Surface",
                    Command = new DelegateCommand(() => { NErgyMediator.EditSurface(Model.CurrentSelectedItem as StructuralArea); })
                });

                 
                //addMenuItem("Create Beam On Top", delegate { ThermalModel.CreateBeamElementOnTop(Model.CurrentSelectedItem as StructuralArea); });
                //addMenuItem("Create Side Columns", delegate { ThermalModel.CreateSideColumn(Model.CurrentSelectedItem as StructuralArea); });
                //addMenuItem("Open Section Editor", delegate { NErgyMediator.OpenNewSectionEditor(Model.CurrentSelectedItem); });
                //addMenuItem("Add Shading Element", delegate
                //{
                //    ThermalModel.AddCantiliverShadingForArea(Model.CurrentSelectedItem as StructuralArea);
                //});

            }
            if (Model.CurrentSelectedItem is Opening)
            {
            //    var area = (Model.CurrentSelectedItem as Opening);
            //    var midle = area.GetMiddle();
            //    var actorposition = midle.V + new Vector3D(area.normal.X, area.normal.Y, area.normal.Z) * 5;

            //    addMenuItem("take a look in the door", delegate
            //    {
            //        //(this.View as BalderView).MovingActor += (s, e) =>
            //        //                                             {
            //        //                                                 this.
            //        //                                                     RegisteredGeoemetries
            //        //                                                     [
            //        //                                                         Model
            //        //                                                             .
            //        //                                                             CurrentSelectedItem
            //        //                                                     ].
            //        //                                                     Material
            //        //                                                     =
            //        //                                                     phasedOutMaterial;


            //        //                                             };


            //        (this.View as BalderView).AnimateCamera(actorposition, midle.V);
            //    });
            //    addMenuItem("Open Openings Editor", delegate { NErgyMediator.OpenNewOpeningsEditor(Model.CurrentSelectedItem); });

            }
            menuItems.AddRange(base.GenerateMenuItems());
            return menuItems;
        }

        //public  override void GenerateMenuItems(Action<string, RoutedEventHandler> addMenuItem)
        //{

        //    addMenuItem("Copy Level", delegate { (Model as ThermalBuildingModel).CopyStory(); });
        //    addMenuItem("Create New Level", delegate { (Model as ThermalBuildingModel).CreateNewStory(); });
        //    addMenuItem("Create Roof", delegate { (Model as ThermalBuildingModel).CreateRoof(); });

        //    if (Model.CurrentSelectedItem is Slab)
        //    {
        //        addMenuItem("Open Section Editor", delegate { NErgyMediator.OpenNewSectionEditor(Model.CurrentSelectedItem); });
        //        addMenuItem("Create Walls On Slab Edges", delegate { (Model as ThermalBuildingModel).CreateWallsOnSlabEdges(); });
        //        addMenuItem("Get In The room", delegate
        //                                           {
        //                                               var area = (Model.CurrentSelectedItem as Slab);
        //                                               var midle = area.GetMiddle();
        //                                               var actorposition = midle.V + new Vector3D(area.normal.X, area.normal.Y, area.normal.Z) * 2;
        //                                               (this.View as BalderView).AnimateCamera(actorposition);

        //                                           });
        //    }
        //    if (Model.CurrentSelectedItem is BuildingStory)
        //    {
        //        addMenuItem("Edit Story", delegate { NErgyMediator.EditStory(Model.CurrentSelectedItem as BuildingStory); });
        //    }
        //    if (Model.CurrentSelectedItem is ShearWall)
        //    {
        //        addMenuItem("Create Door", delegate { ThermalModel.CreateDefaultDoorOpeningOnWall(); });
        //        addMenuItem("Create Window", delegate { ThermalModel.CreateWindowOpeningOnWall(); });
        //        addMenuItem("Edit Surface", delegate { NErgyMediator.EditSurface(Model.CurrentSelectedItem as StructuralArea); });
        //        addMenuItem("Create Beam On Top", delegate { ThermalModel.CreateBeamElementOnTop(Model.CurrentSelectedItem as StructuralArea); });
        //        addMenuItem("Create Side Columns", delegate { ThermalModel.CreateSideColumn(Model.CurrentSelectedItem as StructuralArea); });
        //        addMenuItem("Open Section Editor", delegate{ NErgyMediator.OpenNewSectionEditor(Model.CurrentSelectedItem); });
        //        addMenuItem("Add Shading Element", delegate
        //                                               {
        //                                                   ThermalModel.AddCantiliverShadingForArea(Model.CurrentSelectedItem as StructuralArea);
        //                                               });

        //    }
        //    if (Model.CurrentSelectedItem is Opening)
        //    {
        //        var area = (Model.CurrentSelectedItem as Opening);
        //        var midle = area.GetMiddle();
        //        var actorposition = midle.V +  new Vector3D(area.normal.X,area.normal.Y,area.normal.Z)*5;

        //        addMenuItem("take a look in the door", delegate
        //                                                   {
        //                                                       //(this.View as BalderView).MovingActor += (s, e) =>
        //                                                       //                                             {
        //                                                       //                                                 this.
        //                                                       //                                                     RegisteredGeoemetries
        //                                                       //                                                     [
        //                                                       //                                                         Model
        //                                                       //                                                             .
        //                                                       //                                                             CurrentSelectedItem
        //                                                       //                                                     ].
        //                                                       //                                                     Material
        //                                                       //                                                     =
        //                                                       //                                                     phasedOutMaterial;


        //                                                       //                                             };


        //                                                       (this.View as BalderView).AnimateCamera(actorposition,midle.V);
        //                                                   });
        //        addMenuItem("Open Openings Editor", delegate { NErgyMediator.OpenNewOpeningsEditor(Model.CurrentSelectedItem); });

        //    }
        //    base.GenerateMenuItems(addMenuItem);
        //}


    }
}
