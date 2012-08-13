using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Babylon.Primitives;
using Balder.Objects.Geometries;
using BuildingStructuralElements;
using NCad.Application.Views.Controllers;
using NCad.Models;
using NCad.Views;
using NErgy.Building;
using NErgy.Silverlight.Views.ViewModels;
using NErgy.Silverlight.Web.Models;
using SimpleMvvmToolkit;
using Line = BuildingBasicDomain.Line;
using Page = LiveGeometry.Page;
using Point = BuildingBasicDomain.Point;

namespace NErgy.Silverlight.Views.Controllers
{
    public class Section3DViewControler : Controller3DBabylon
    {
        public override IList<MenuItem> GenerateMenuItems()
        {
            return new List<MenuItem>()
                       {
                           new MenuItem()
                               {
                                   Header = "Delete Material Layer",Command =new DelegateCommand(() => Model.RemoveModelElement(Model.CurrentSelectedItem))
                               }
                       };

        }
    }

    public class SectionEditorController
    {
        private readonly Dictionary<object, object> RegisteredGeoemetries = new Dictionary<object, object>();
        public double SectionsDistane = 0.5;
        private Controller3DBabylon _Controller3D;
        private IThermalElement _SelectedElement;
        private ThermalSectionEditor _ThermalSectionEditor;

        public SectionEditorController()
        {
            SectionModel = new Model();
            SectionModel.SelectedItem += SectionModel_SelectedItem;

            //SectionModel.RemovedItem += new EventHandler(SectionModel_RemovedItem);
        }


        private Model SectionModel { get; set; }


        public Controller3DBabylon Controller3D
        {
            get { return _Controller3D; }
            set
            {
                _Controller3D = value;
                _Controller3D.Model = SectionModel;
            }
        }

        private IThermalSection _SelectedCompositeSection;
        public IThermalSection SelectedCompositeSection
        {
            get
            {
                return _SelectedCompositeSection;
            }
            set
            {
                //_ThermalSectionEditor.ViewModel.SelectedSection = value;
                _SelectedCompositeSection = value;
                SectionModel.ModelElements.Clear();
                Model2D.ModelElements.Clear();
                CreateModel3DElements(value);
                //CreateModel2DElements(value);
                FillDataGrid(value);
                //_ThermalSectionEditor.LayersDataGridView.SelectionChanged += new SelectionChangedEventHandler(LayersDataGridView_SelectionChanged);
            }
        }
  
        public ThermalSectionEditor ThermalSectionEditor
        {
            get { return _ThermalSectionEditor; }
            set
            {
                _ThermalSectionEditor = value;
                _ThermalSectionEditor.ViewModel = new ThermalEditorViewModel();
                //_ThermalSectionEditor.EditMaterial += _ThermalSectionEditor_EditMaterial;
                //_ThermalSectionEditor.LayersDataGridView.SelectionChanged += LayersDataGridView_SelectionChanged;
                _ThermalSectionEditor.SectionEditorController = this;
            }
        }

        public ActionManager ActionManager { get; set; }

    
        private void SectionModel_SelectedItem(object sender, EventArgs e)
        {
            KeyValuePair<object, object> section = RegisteredGeoemetries.FirstOrDefault(c => c.Key.Equals(sender));
        }


        private void CreateModel2DElements(CompositeThermalSection compositeThermalSection)
        {
            StructuralArea baseLayer = GetBaseLayer();

            StructuralArea templayer = baseLayer;
            if (compositeThermalSection is CompositeThermalSection)
            {
                foreach (IThermalSection section in (compositeThermalSection).Sections)
                {
                   // StructuralArea area = CreateAreaOnSideOf(templayer, section.D.Value);
                  //  templayer = area;

//BuildingStructuralElements.Slab slab = new BuildingBuilder().CreateSlab(area.Area);
                   // slab.T = section.D.Value;
               //     AddSectionTo2DModel(slab, section);
                }
            }
        }

        private void AddSectionTo2DModel(BuildingStructuralElements.Slab slab, IThermalSection section)
        {
            Model2D.AddModelElement(slab);
        }

        private StructuralArea CreateAreaOnSideOf(StructuralArea layer, double D)
        {
            var parallel_area = new StructuralArea {T = D};

            layer.Polygon
                .ForEach(p =>
                             {
                                 var parallel_node = new Node(p.X, p.Y, p.Z);
                                 parallel_area.Add(parallel_node);
                             }
                );

            double dist = layer.T/2; // +SectionsDistane;

            parallel_area
                .Move(
                    0,
                    0,
                    layer.GetMiddle().Z + dist
                );

            return parallel_area;
        }

        private void FillDataGrid(IThermalSection value)
        {
            //if (value is CompositeThermalSection) {
            //    _ThermalSectionEditor.LayersDataGridView.ItemsSource = (value as CompositeThermalSection).Sections;
            //     _ThermalSectionEditor.LayersDataGridView.UpdateLayout();
            //}

            if (value is CompositeThermalSection)
            {
                _ThermalSectionEditor.ViewModel.ThermalSectionsLayers = new ObservableCollection<IThermalSection>((value as CompositeThermalSection).Sections);
            }
        }

        private void RegisterGeometry(object element, object geometryObject)
        {
            if (RegisteredGeoemetries.Keys.Contains(element)) throw new Exception("Element Already Registered");
            RegisteredGeoemetries.Add(element, geometryObject);
        }

        private void UnRegisterGeometry(object element, object geometryObject)
        {
            if (!RegisteredGeoemetries.Keys.Contains(element)) throw new Exception("Element Is Not Registered");
            RegisteredGeoemetries.Remove(element);
        }
 
        public static CompositeThermalSection TestCompositeThermalSection()
        {
            return new CompositeThermalSection
                       {
                           Sections = new List<IThermalSection>
                                          {
                                              //new Web.Models.Section() {D = 0.2,Material  = new Web.Models. Material(){TheramlCapacityCoef = 0.5,TheramlConductivityCoef = 0.1} },
                                              //new Web.Models.Section() {D = 0.2,Material  = new Web.Models. Material(){TheramlCapacityCoef = 0.6,TheramlConductivityCoef = 0.6}},
                                              //new Web.Models.Section() {D = 0.3,Material  = new Web.Models. Material(){TheramlCapacityCoef = 0.3,TheramlConductivityCoef = 0.5}},
                                          }
                       };
        }

        private void CreateModel3DElements(object compositeThermalSection)
        {
            CreateModel3DElementsFromArea(compositeThermalSection, GetBaseLayer());
        }

        private void CreateModel3DElementsFromArea(object compositeThermalSection, StructuralArea structuralArea)
        {
            StructuralArea baseLayer = structuralArea;
            baseLayer = ReduceSize(baseLayer, 3);
            StructuralArea templayer = baseLayer;
            if (compositeThermalSection is CompositeThermalSection)
            {
                foreach (IThermalSection section in ((CompositeThermalSection) compositeThermalSection).Sections)
                {
                    //StructuralArea area = CreateAreaOnTopOf(templayer, section.D.Value);
                    //templayer = area;

                    //BuildingStructuralElements.Slab slab = new BuildingBuilder().CreateSlab(area.Area);
                    //slab.T = section.D.Value;
                    //AddSectionToModel(slab, section);
                }
            }
        }

        private StructuralArea ReduceSize(StructuralArea baseLayer, double rate)
        {
            var reduced = new StructuralArea();
            baseLayer.Polygon
                .ForEach(p =>
                             {
                                 var parallel_node = new Node(p.X/rate, p.Y/rate, p.Z/rate);
                                 reduced.Add(parallel_node);
                             }
                );
            return reduced;
        }

        private void AddSectionToModel(BuildingStructuralElements.Slab slab, IThermalSection section)
        {
            SectionModel.AddModelElement(slab);
            RegisteredGeoemetries.Add(slab, section);
            
            
        }

        private StructuralArea GetBaseLayer()
        {
            return new BuildingBuilder().CreateSlab(
                new Line(
                    new Point(0, 0, 0),
                    new Point(0, 0, 50)),
                50, BuildingBuilder.xDirection);
        }

        private StructuralArea CreateAreaOnTopOf(StructuralArea layer, double D)
        {
            var parallel_area = new StructuralArea {T = D};

            layer.Polygon
                .ForEach(p =>
                             {
                                 var parallel_node = new Node(p.X, p.Y, p.Z);
                                 parallel_area.Add(parallel_node);
                             }
                );

            double dist = layer.T + SectionsDistane;

            parallel_area
                .Move(
                    -layer.normal.X*dist,
                    -layer.normal.Y*dist,
                    -layer.normal.Z*dist
                );

            return parallel_area;
        }

        //public IView3D<IDrawableShape> View3D { get; set; }
        public virtual void Create3DController()
        {
            Controller3D = new Section3DViewControler
                                       {
                                           View = ThermalSectionEditor.SectionDarwingView
                                       };
            //BalderViewController.SelectElement();
        }

        public void SetUpScene()
        {
            if (Controller3D != null)
                Controller3D.SetUpScene();
            if (LiveGeometryController != null)
                LiveGeometryController.SetUpScene();
        }

        //public void DisplaySelectedSectionLayer(IThermalSection section)
        //{
        //    ThermalSectionEditor.SectionLayerView.ViewModel = new SectionViewModel(section);
        //}

        public void Initialize(ThermalSectionEditor _ThermalSectionEditor)
        {
            ThermalSectionEditor = _ThermalSectionEditor;
           // Create3DController(_ThermalSectionEditor.SectionDarwingView);
        }

        #region View2D

        private readonly Model Model2D = new Model();

        private LiveGeometryController _LiveGeometryController;

        private LiveGeometryController LiveGeometryController
        {
            get { return _LiveGeometryController; }
            set
            {
                _LiveGeometryController = value;
                _LiveGeometryController.Model = Model2D;
                LiveGeometryController.PlaneType = PLANE_TYPE.elevation;
            }
        }

        internal void Create2DController(Page view2D)
        {
            LiveGeometryController = new LiveGeometryController();

            //LiveGeometryController.View = ThermalSectionEditor.View2D.DrawingControl.Drawing;
        }

        #endregion

        internal   void AddMaterialLayerToSection()
        {
            //(SelectedSection as CompositeThermalSection).Sections.Add(new ThermalSection());
            //FillDataGrid(this.SelectedSection);
        }
    }
}