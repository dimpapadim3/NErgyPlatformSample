using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Xml.Linq;
using NCad.Application.Views.Controllers;
using NCad.Models;
using NCad.Views;
using NCad.Views.Controllers;
using NErgy.Building;
using NErgy.Building.BuildingShell;
using NErgy.Building.Calculations;
using NErgy.Building.ThermalZones;
using NErgy.Silverlight.Building;
using NErgy.Silverlight.Helpers;
using NErgy.Silverlight.Models;
using NErgy.Silverlight.Views.ViewModels;
using NErgy.Silverlight.Web.Models;
using Silverlight.Wizard;
using SimpleMvvmToolkit;
using TranslusentElement = NErgy.Silverlight.Models.StructuralElements.TranslusentElement;
using System.IO;
using System.Text;

namespace NErgy.Silverlight.Views.Controllers
{

    public enum HelpPageTypeId
    {

        SystemsPage = 1,
        ThermalZonePage = 2,
        ShellPage = 3,
        ProjectsPage = 4,


    }



    public class Mediator : NCad.Application.Views.Controllers.Mediator
    {

        private static Mediator instance;
        public int ActivePageType { get; set; }
        public static User User;
        private readonly CalculationEngine _CalculationEngine = new CalculationEngine();
        public SectionEditorController SectionEditorController;
        private ThermalSectionEditor _ThermalSectionEditor; // = new ThermalSectionEditor();
        private OpeningEditorController openingcontroller;

        #region Views region

        private ToolbarView _ToolbarView;
        public IDesignView DesignView { get; set; }

        public ToolbarView ToolbarView
        {
            get { return _ToolbarView; }
            set
            {
                _ToolbarView = value;
                _ToolbarView.CommandSelected += _ToolbarView_CommandSelected;
            }
        }


        public ProjectView ProjectView { get; set; }

        #endregion

        public Mediator(ThermalBuildingModel Model)
            : base(Model)
        {
            ApplicationUser = WebContext.Current.User;
            SectionEditorController = new SectionEditorController { ActionManager = ActionManager };
            //MainView3D = new BalderView() {Height = 100, Width = 200};
        }

        public Web.User ApplicationUser { get; set; }

        public IView3D MainView3D { get; set; }

        public static Mediator Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Mediator(ThermalBuildingModel.Instance);
                    instance.ActionManager = new ActionManager();
                }
                return instance;
            }
        }

        private ControllState ConstrolState { get; set; }

        public ThermalBuildingModel Model
        {
            get { return base.Model as ThermalBuildingModel; }
        }

        public IStatusBar StatusBar { get; set; }

        public bool IsEnergyStudy
        {
            get {
                if (Model.Project != null)
                    return Model.Project.IsProjectEnergyStudy.HasValue? Model.Project.IsProjectEnergyStudy.Value: false;
                return false;
            }
        }

        public void ProjectListViewModelSelectedProjectChanged(object sender, NotificationEventArgs<Project> e)
        {
            ThermalBuildingModel.Instance.Project = e.Data;
        }


        private void DisplaySlectedElement(object e)
        {
            if (e is Web.Models.Building)
            {
                DesignView.MainRegion.Content = new BuildingView() { ViewModel = new BuildingViewModel(e as Web.Models.Building) };
            }
            if (e is Project)
            {
                DesignView.MainRegion.Content = new ProjectView() { ViewModel = e as ProjectViewModel };
            }
        }

        public void SerialzeModel()
        {
            string serilizedData = XmlSerializer.Serialize(Model);
        }

        private void _ToolbarView_CommandSelected(object sender, EventArgs e)
        {
            ConstrolState = sender as ControllState;
            if (ConstrolState is AddBeamFree)
            {
                Instance.SaveBuildingData();
            }
            if (ConstrolState is AddSlabRectangular)
            {

            }

            if (ConstrolState is SelectionState)
            {

            }
        }

        internal void EditStory(BuildingStory buildingStory)
        {
            Controller2D.ClearView();
            Controller2D.SetStoryPlane(buildingStory.StoryPlane);

            Controller2D.Model = Model;
            Controller2D.SetUpScene();
        }

        public override void Add3DView(IView3D mainView3D)
        {
            base.Add3DView(mainView3D);
        }

        protected override NCad.Application.Views.Controllers.TreeViewController CreateTreeViewController()
        {
            return new TreeViewControllerKenak() { Mediator = this };
        }

        public void CalculateEnergyNeeds()
        {
            //_CalculationEngine.CaluclateEnergyNeeds(Model.Project.Buildings);
        }

        public void ValidateThermalElementsMaxU()
        {
            //_CalculationEngine.CheckMaxU(Model.Project.Buildings);
        }

        internal void SaveBuildingData()
        {
            Model.SelectedBuilding.BuildingDataXml = XmlSerializer.Serialize(Model.SelectedBuilding.BuildingModel);
            //ProjectView.ViewModel.SaveChanges();
        }


        // public void Add3DView()
        //{
        //    Controller3D.Model = Model;
        //    var view = new BalderView { Width = 500, Height = 300 };
        //    DesignView.MainRegion.Content = view;
        //    Controller3D.View = view;
        //    Controller3D.SetUpScene();
        //}

        protected override IController CreateController3D()
        {
            // return new Controller3DBabylon() { ActionManager = ActionManager };
            return new Controller3DBalder { ActionManager = ActionManager };
        }

        public void Initialize3DController(IController controller = null)
        {
            if (controller == null)
            {
                Controller3D = CreateController3D();
            }
            else
            {
                Controller3D = controller;
            }
        }

        protected override LiveGeometryController CreateController2D()
        {
            return new Controller2DLiveGeometry { ActionManager = ActionManager };
        }

        internal void OpenNewSectionEditor(object p)
        {
            //OpenSectionEditorInWindowStrategy(p);
            OpenSectionEditorInRegionStrategy(p);
        }

        private void OpenSectionEditorInRegionStrategy(object p)
        {
            _ThermalSectionEditor = new ThermalSectionEditor();
            DesignView.SetSectionView(_ThermalSectionEditor);
            SectionEditorController.Initialize(_ThermalSectionEditor);
            SectionEditorController.Create3DController();
            if (Model.CurrentSelectedItem is IThermalElement)
            {
                if ((Model.CurrentSelectedItem as IThermalElement).Section == null)
                    SectionEditorController.SelectedCompositeSection =
                        SectionEditorController.TestCompositeThermalSection();
                else
                    SectionEditorController.SelectedCompositeSection =
                        (Model.CurrentSelectedItem as IThermalElement).Section;
            }
            if (p is IThermalSection)
            {
                SectionEditorController.SelectedCompositeSection = ((IThermalSection)p);
            }
            SectionEditorController.SetUpScene();
        }

        public void OpenSectionEditorInWindowStrategy(object p)
        {
            _ThermalSectionEditor = new ThermalSectionEditor();
            DesignView.SetSectionView(_ThermalSectionEditor);
            SectionEditorController.Initialize(_ThermalSectionEditor);
            SectionEditorController.Create3DController();
            if (Model.CurrentSelectedItem is IThermalElement)
            {
                if ((Model.CurrentSelectedItem as IThermalElement).Section == null)
                    SectionEditorController.SelectedCompositeSection =
                        SectionEditorController.TestCompositeThermalSection();
                else
                    SectionEditorController.SelectedCompositeSection =
                        (Model.CurrentSelectedItem as IThermalElement).Section;
            }
            if (p is IThermalSection)
            {
                SectionEditorController.SelectedCompositeSection = ((IThermalSection)p);
            }
            SectionEditorController.SetUpScene();

            //_ThermalSectionEditor = new ThermalSectionEditor();

            //SectionEditorController.Initialize(_ThermalSectionEditor);
            //SectionEditorController.Create3DController(); //_ThermalSectionEditor.SectionDarwingView);
            ////SectionEditorController.Create2DController(_ThermalSectionEditor.DrawingView2D);
            ////SectionEditorController.SelectedSection = SectionEditorController.TestCompositeThermalSection();
            //if (Model.CurrentSelectedItem is IThermalElement)
            //{
            //    if ((Model.CurrentSelectedItem as IThermalElement).Section == null)
            //        throw new Exception("Structural Element Section Not Set");
            //    SectionEditorController.SelectedCompositeSection =
            //        (Model.CurrentSelectedItem as IThermalElement).Section;
            //}
            //if (p is IThermalSection)
            //{
            //    SectionEditorController.SelectedCompositeSection = ((IThermalSection)p);
            //}
            //SectionEditorController.SetUpScene();


            ////_ThermalSectionEditor.SectionDarwingView.IsPaused = false;

            ShowAsChildWindow(_ThermalSectionEditor);
        }

        private void ShowAsChildWindow(object ContentControl)
        {
            var SectionWindow = new ChildWindow();
            SectionWindow.Height = 500;
            SectionWindow.Width = 800;

            SectionWindow.Content = ContentControl;
            SectionWindow.Visibility = Visibility.Visible;
            SectionWindow.Show();
            //SectionWindow.Closing += new EventHandler<System.ComponentModel.CancelEventArgs>(SectionWindow_Closing);
        }

        public static ChildWindow OpenWindow(object ContentControl,double _Width = 700, double _Height = 500)
        {
            var SectionWindow = new ChildWindow {Width=_Width,Height=_Height};

            SectionWindow.Content = ContentControl;
            SectionWindow.Visibility = Visibility.Visible;
            // SectionWindow.Title = ApplicationStrings.MaterialTitle;
            SectionWindow.Show();
            return SectionWindow;
        }

        public static void OpenInRegion(ContentControl region, object contentControl)
        {
            region.Content = contentControl;
        }


        internal void OpenNewOpeningsEditor(object p)
        {
            openingcontroller = new OpeningEditorController();
            openingcontroller.View = DesignView.MainView3D;
            openingcontroller.AddOpeningToView(p as Opening);

            var _OpeningEditor = new OpeningEditor();
            //_OpeningEditor.TransparentElement = (p as Opening).ThermalOpening as KoufomaMono;
            //var window = OpenWindow(_OpeningEditor);
            //window.Closing += new EventHandler<System.ComponentModel.CancelEventArgs>(window_Closing);

            OpenInRegion(DesignView.SecondaryRegion, _OpeningEditor);
        }

        private void window_Closing(object sender, CancelEventArgs e)
        {
            openingcontroller.ClearOpeningFromView();
        }

        internal void OpenShadingElementEditor(IStructuralArea iStructuralArea)
        {
            throw new NotImplementedException();
        }

        internal void CreateNewDesignModel()
        {
            Model.NewDesignModel();
        }

        //private List<IController> Controllers = new List<IController>();
        //public void GenerateMenuItems()
        //{
        //   Controllers.ForEach(c=>c.GenerateMenuItems());
        //}
        public void OpenStandardsOpaqueElement(TranslusentElement selectedTranslusentElement)
        {
            OpenWindow(new InspectionOpaqueView()
                           {
                               ViewModel = new InspectionTranslusentViewModel(selectedTranslusentElement)
                           });
        }

        public void OpenLibrariesOpaqueElement(TranslusentElement selectedTranslusentElement)
        {
            var viewmodel = new ThermalEditorViewModel() { };
            OpenWindow(new ThermalSectionEditor()
            {
                ViewModel = new ThermalEditorViewModel()
            });
        }

        public void OpenDialogAddTRanslusentElementToLibrary(TranslusentElement selectedTranslusentElement)
        {
            if (selectedTranslusentElement != null)
                if (selectedTranslusentElement.U > 0)
                {

                    //new SectionLibrariesView()
                    //               {
                    //                   ViewModel = new SectionLibrariesListAddSectionViewModel() { U = selectedTranslusentElement.U }
                    //               }

                    var sharedViewModel = new SectionLibrariesListAddSectionViewModel() { U = selectedTranslusentElement.U };
                    var wizard = new WizardControl();
                    wizard.manager.Add(new WizardStep() { Content = new SectionLibrariesView() { ViewModel = sharedViewModel }, StepIndex = 1, StepHeaderText = "Επιλεξτε Βιβλιοθήκη", StepName = "LibrarySelectionStep" });
                    wizard.manager.Add(new WizardStep() { Content = new SectionNameView() { DataContext = sharedViewModel }, StepIndex = 2, StepHeaderText = "Επιλεξτε Ονομα Διατομής", StepName = "SectionSelectionStep" });

                    var window = OpenWindow(wizard);
                }
        }

        public void OpenHelpWindow()
        {
            OpenWindow(new HelpUserControl(),1000,500);
        }

        public void SaveProjectAsXml()
        {
            var TeeExporter = new ProjectTeeXmlConverter();



            SaveFileDialog save = new SaveFileDialog();
            save.Filter = "*.xml|*.*";

            save.FilterIndex = 1;

            bool? saveClicked = save.ShowDialog();

            if (saveClicked == true)
            {
                System.IO.Stream stream = save.OpenFile();
                // StreamWriter writer = new StreamWriter(stream);
                //     writer.Write(projectxml);
                // var projectxml = 
                TeeExporter.CreateTeeXml(this.Model.Project, stream);
                stream.Close();
            }
        }

        public void LoadProjectFromXml()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "*.xml|*.*";

            if (openFileDialog.ShowDialog() == true)
            {
                System.IO.FileStream fileStream = openFileDialog.File.OpenRead();
                //  byte[] textBytes = new byte[fileStream.Length];
                //   fileStream.Read(textBytes, 0, textBytes.Length);
             
                //  var text = UTF8Encoding.UTF8.GetString(textBytes, 0, textBytes.Length);
                var xDoc = XDocument.Load(fileStream);   
                var TeeExporter = new ProjectTeeXmlBackConverted();
                var newProject  = TeeExporter.CreateProject(xDoc);
                AddProject(newProject);
            }
        }

        public EventHandler OnNewProjectCreated;
        private Action<Exception> _action;

        public void CreateNewProjectStudy()
        {
            var newProject = new Project() { IsProjectEnergyStudy = true};
            CreateEmptyBuilding(newProject);
            AddProject(newProject);
        }

        public void CreateNewProject()
        {
            var newProject = new Project() { IsProjectEnergyStudy = false }; 
            CreateEmptyBuilding(newProject);
            AddProject(newProject);
        }

        private void AddProject(Project newProject)
        {
            newProject.UserID = (string) WebContext.Current.User.GetIdentity();
            newProject.LocalIdCounter = EntitiesIdCounters.GetProjectIdCounterForProject(newProject);
            Model.DataAccesLayerService.AddProject(newProject);
            Model.DataAccesLayerService.SaveChanges(e =>
                                                        {
                                                            if (OnNewProjectCreated != null)
                                                                OnNewProjectCreated(newProject, new EventArgs());
                                                        });
        }

        private void CreateEmptyBuilding(Project project)
        {

            // project.Building = new List<Web.Models.Building>();
            if (project.Building != null)
                if (project.Building.Count == 0)
                {

                    var newBuilding = new Web.Models.Building()
                                          {
                                              BuildingUsageID = project.ProjectUseageId,
                                              BuildingModel = new BuildingModel()
                                                                  {
                                                                      BuildingStories = new List<BuildingStory>(),
                                                                      ThermalZones = new List<ThermalZone>(),
                                                                      SunAreas = new List<SunArea>(),
                                                                      NonHeatedArea = new List<NonHeatedArea>(),

                                                                  }
                                          };
                    Model.DataAccesLayerService.AddbuildingToProject(project, newBuilding);

                    Model.DataAccesLayerService.SaveChanges(e =>
                                                                {

                                                                });
                    // Model.Project.Building.Add(newBuilding);
                }
        }



        public void RemoveBuildingFromProject(Web.Models.Building building)
        {
            Model.DataAccesLayerService.RemoveBuildingFromProject(building.Project, building);
            _action = e =>
                          {
                              if (e == null) StatusBar.Display("Το Κτιριο διαγράφηκε");
                          };
            Model.DataAccesLayerService.SaveChanges(_action);
        }

        public void RemoveThermalZoneFromBuilding(ThermalZone thermalZone, Web.Models.Building building)
        {
            if (building.BuildingModel.ThermalZones.Contains(thermalZone))
            {
                building.BuildingModel.ThermalZones.Remove(thermalZone);
                SaveBuildingData();
                _action = e =>
                {
                    if (e == null) StatusBar.Display("η Ζώνη διαγράφηκε");
                };
                Model.DataAccesLayerService.SaveChanges(_action);

            }
        }

        public void RemoveNonHeatedAreaFromBuilding(NonHeatedArea nonHeatedArea, Web.Models.Building building)
        {
            if (building.BuildingModel.NonHeatedArea.Contains(nonHeatedArea))
            {
                building.BuildingModel.NonHeatedArea.Remove(nonHeatedArea);
                SaveBuildingData();
                _action = e =>
                {
                    if (e == null) StatusBar.Display("o χώρος διαγράφηκε");
                };
                Model.DataAccesLayerService.SaveChanges(_action);

            }
        }

        public void RemoveSunAreaFromBuilding(SunArea sunArea, Web.Models.Building building)
        {
            if (building.BuildingModel.SunAreas.Contains(sunArea))
            {
                building.BuildingModel.SunAreas.Remove(sunArea);
                SaveBuildingData();
                _action = e =>
                {
                    if (e == null) StatusBar.Display("o χώρος διαγράφηκε");
                };
                Model.DataAccesLayerService.SaveChanges(_action);

            }
        }


        public void RemoveSeparatingSurfuceFromShell(SeparatingSurfuce separatingSurfuce, Shell shell)
        {
            if (shell.SeparatingSurfuces.Contains(separatingSurfuce))
            {
                shell.SeparatingSurfuces.Remove(separatingSurfuce);
                SaveBuildingData();
                _action = e =>
                {
                    if (e == null) StatusBar.Display("o χώρος διαγράφηκε");
                };
                Model.DataAccesLayerService.SaveChanges(_action);

            }
        }

        public void DeleteProject(Project project)
        {
          

           
            _action = e =>
              {
                  if (e == null) StatusBar.Display("Το Έργο διαγράφηκε");
                 project.IsProjectHidden = true;  
                  EntitiesIdCounters.ProjectRemoved (project);
              };
            Model.DataAccesLayerService.SaveChanges(_action);

        }



        public void RequestSeparatingSurfaceByNumericUdDown(Shell value, int i)
        {
            if (value.SeparatingSurfuces != null)
            {
                if (value.SeparatingSurfuces.Count < i)
                {
                    // value.SeparatingSurfuces.Add();
                }

            }
        }


        public void HandleException(Exception exception)
        {
            if (exception != null) Logger.Instance.Log(exception.Message);
        }
    }
}