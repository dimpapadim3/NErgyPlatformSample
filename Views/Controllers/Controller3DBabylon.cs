using System;
using System.Collections.Generic;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using Babylon.Primitives;
using Babylon.Toolbox;
using BuildingStructuralElements;
using CommonTypes.Math;
using Microsoft.Xna.Framework;
using NCad.Application.Views.Controllers;
using NErgy.Building;
using NErgy.Silverlight.Models;
using SimpleMvvmToolkit;
using System.Windows.Graphics;
using Color = Microsoft.Xna.Framework.Color;

namespace NErgy.Silverlight.Views.Controllers
{
    public class Controller3DBabylon :NCad.Application.Views.Controllers. Controller3DBabylon
    {
        protected IThermalBuildingModel ThermalModel
        {
            get
            {
                return (IThermalBuildingModel)base.Model;
            }
        }

        protected Mediator NErgyMediator { get { return Mediator as Mediator; } }
 
        public override IList<MenuItem> GenerateMenuItems()
        {
            var menuItems = new List<MenuItem>();
            menuItems.Add(new MenuItem()
            {
                Header = ApplicationStrings.CopyLevel,
                Command = new DelegateCommand(() => { ThermalModel.CopyStory(); })
            });
            menuItems.Add(new MenuItem()
            {
                Header = ApplicationStrings.CreateNewLevel,
                Command = new DelegateCommand(() => { ThermalModel.CreateNewStory(); })
            });
            menuItems.Add(new MenuItem()
            {
                Header = ApplicationStrings.CreateRoof,
                Command = new DelegateCommand(() => { ThermalModel.CreateRoof(); })
            });
            menuItems.Add(new MenuItem()
            {
                Header = "Test SectionEditor",
                Command = new DelegateCommand(() => { NErgyMediator.OpenNewSectionEditor(SectionEditorController.TestCompositeThermalSection()); })
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
                    Command = new DelegateCommand(() => { ThermalModel.CreateWallsOnSlabEdges(); })
                });

                menuItems.Add(new MenuItem()
                {
                    Header = ApplicationStrings.CreateRoof,
                    Command = new DelegateCommand(() =>
                    {
                        var area = (Model.CurrentSelectedItem as Slab);
                        var midle = area.GetMiddle();
                        var actorposition = midle.V + new Vector3D(area.normal.X, area.normal.Y, area.normal.Z) * 2;
                      //  (this.View as BalderView).AnimateCamera(actorposition);
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

        protected override List<IDrawableShape> CreateElementGeometriesBase(object modelElement)
        {
            var geometries  =  base.CreateElementGeometriesBase(modelElement);
            if(modelElement is INeighborBuilding)
                geometries .Add(CreateNeighbourBuildingGeometry(modelElement as INeighborBuilding));
            return geometries;
        }
        protected  IDrawableShape CreateNeighbourBuildingGeometry(INeighborBuilding neighborBuilding)
        {
            var midle = neighborBuilding.Height/2;
     
            var angle = -Math.Atan(neighborBuilding.Angle);

            ColoredCube _coloredCube;
            _coloredCube = new ColoredCube((float)neighborBuilding.Breadth, (float)neighborBuilding.Height, (float)neighborBuilding.Width, new Color(0.0f, 1.0f, .0f, 1.0f));

            var gd = GraphicsDeviceManager.Current.GraphicsDevice;
            _coloredCube.Device = gd;
            _coloredCube.LoadContent();
            VertexColorEffect _coloredCubeEffect;
            _coloredCubeEffect = new VertexColorEffect(gd)
            {
                Alpha = 1,
                AmbientColor = new Color(.2f, .2f, .2f, 1),
                SceneAmbientColor = new Color(.2f, .2f, .2f, 1),
                SpecularPower = 16,
                SpecularColor = new Color(1.0f, 1, 1, 1),
            };
            _coloredCube.Effect = _coloredCubeEffect;
            Matrix worldMatrix = Matrix.CreateTranslation((float)neighborBuilding.X, (float)neighborBuilding.Height / 2, (float)neighborBuilding.Y) *
                                       Matrix.CreateRotationY((float)angle);
            (_coloredCube.Effect as VertexColorEffect).World = worldMatrix;

            return _coloredCube;
        }
    

    }
}
