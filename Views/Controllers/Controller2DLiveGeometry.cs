using System;
using System.Collections.Generic;
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
using NCad.Models;
using NErgy.Silverlight.Models;
using SimpleMvvmToolkit;

namespace NErgy.Silverlight.Views.Controllers
{
    public class Controller2DLiveGeometry : LiveGeometryController
    {
        protected override NCad.Models.BuildingBuilder CreateBuildingBuilder(NCad.Models.IModel model)
        {
            return new TrermalBuildingBuilder(model);
        }

        protected ThermalBuildingModel ThermalModel
        {
            get
            {
                return (ThermalBuildingModel)base.Model;
            }
        }

        protected Mediator NErgyMediator { get { return Mediator as Mediator; } }

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
                        //(this.View as BalderView).AnimateCamera(actorposition);
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

       

    }
}
