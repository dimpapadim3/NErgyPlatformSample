using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using Liquid;
using NCad.Utilities;
using NCad.Views.ViewModels3D;
using NErgy.Building;
using NErgy.Building.BuildingShell;
using NErgy.Building.Systems;
using NErgy.Building.ThermalZones;
using NErgy.Silverlight.Models;
using NErgy.Silverlight.Views.BuildingViews;
using NErgy.Silverlight.Views.Controllers;
using SimpleMvvmToolkit;
using NErgy.Silverlight.Web.Models;

namespace NErgy.Silverlight.Views.ViewModels
{


    public class DesignViewViewModel
    {

        private readonly PropertyGrid properyGrid = new PropertyGrid();
        private ThermalBuildingModel Model { get { return ThermalBuildingModel.Instance; } }
        private MenuManager _menuManager;
        private KeyboardManager _keyboardManager;

        public IDesignView View { get; set; }
        public TreeView MainTreeView { get; set; }
        public DesignViewViewModel(IDesignView view)
        {
            View = view;
            View3dMenuItems = new List<MenuItem>();
            Model.SelectedItem += Model_SelectedItem;

            Model.Initialize();

            var mediator = Mediator.Instance; //new Mediator(Model);
            mediator.DesignView = View;
            // mediator.Initialize3DController();
            ////var MenuManager = new MenuManager(mediator.Controller3D);

            ////MenuManager.AttachMenuToView(mainView3D);
            ////mediator.Add3DView(mainView3D);
            //mediator.AddTreeView(this.MainTreeView);
            //mediator.SectionEditorController.Initialize(_ThermalSectionEditor);
            //mediator.SectionEditorController.Create3DController(_ThermalSectionEditor.BalderView);
            //mediator.SectionEditorController.SelectedSection=SectionEditorController.TestCompositeThermalSection();
            //mediator.SectionEditorController.SetUpScene(); 

            //mediator.Add2DView(View.MainView2D);

            TreeViewdMenuItems = new List<MenuItem>();

        }


        private void Model_SelectedItem(object sender, EventArgs e)
        {
            //properyGrid.Show(sender,ActionManager);
        }

        public IList<MenuItem> View3dMenuItems
        {
            get;
            set;
        }
        public IList<MenuItem> TreeViewdMenuItems
        {
            get;
            set;
        }


        public PropertyGrid ProperyGrid
        {
            get { return properyGrid; }
        }

        public MenuManager MenuManager
        {
            get { return _menuManager; }
            set { _menuManager = value; }
        }

        public KeyboardManager KeyboardManager
        {
            get { return _keyboardManager; }
            set { _keyboardManager = value; }
        }

        internal void UpdateDataContextItems()
        {
            TreeViewdMenuItems.Clear();
            if (SelectedTreeViewItem != null)
            {
                var item = ((Node)SelectedTreeViewItem).Tag;

                if (((Node)SelectedTreeViewItem).Title == "NErgy Ενεργειακή Μελέτη")
                {
                    TreeViewdMenuItems.Add(new MenuItem()
                    {
                        Header = "Προσθήκη Νέου Έργου",
                        Command = new DelegateCommand(() =>
                                                          {
                                                              Mediator.Instance.CreateNewProject();
                                                              UpdateTreeView();
                                                          })
                    });

                }

                if (item is ProjectViewModel)
                {
                    TreeViewdMenuItems.Add(new MenuItem()
                    {
                        Header = "Διαγραφή Έργου",
                        Command = new DelegateCommand(() =>
                        {
                            Mediator.Instance.DeleteProject(((ProjectViewModel)item).Model);
                            UpdateTreeView();
                        })
                    });
                }

                #region Building

                if (item is Web.Models.Building)
                {
                    TreeViewdMenuItems.Add(new MenuItem()
                    {
                        Header = "Προσθήκη Νέας Ζώνης",
                        Command = new DelegateCommand(() =>
                                                          {
                                                              if (((Web.Models.Building)item).BuildingModel == null)
                                                              {
                                                                  ((Web.Models.Building) item).BuildingModel =
                                                                      new BuildingModel();
                                                                //  Mediator.Instance.SaveBuildingData();
                                                                //  ThermalBuildingModel.Instance.DataAccesLayerService.SaveChanges(s=>{});
                                                              }
                                                              if (((Web.Models.Building)item).BuildingModel.ThermalZones == null)
                                                                  ((Web.Models.Building)item).BuildingModel.ThermalZones = new List<ThermalZone>();
                                                              var newZone = CreateEmptyThermalZone();

                                                              ((Web.Models.Building)item).BuildingModel.ThermalZones.Add(newZone);
                                                              if (!(SelectedTreeViewItem as Node).HasChildren)
                                                              {
                                                                  (SelectedTreeViewItem as Node).Nodes =
                                                                      new ObservableCollection<Node>() { TreeViewItemsGenerator.CreateThermalZoneTreeViewItem(newZone) };


                                                              }
                                                              else
                                                              {
                                                                  (SelectedTreeViewItem as Node).Nodes.Add(TreeViewItemsGenerator.CreateThermalZoneTreeViewItem(newZone));
                                                              }
                                                              Mediator.Instance.SaveBuildingData();
                                                              ThermalBuildingModel.Instance.DataAccesLayerService.SaveChanges(e =>
                                                              {
                                                                  if (e != null)
                                                                      Mediator.Instance.StatusBar.Display("Σφαλμα κατα την αποθυκευση ΔΕΝ Προστέθηκε Νέος  Νέα Ζώνη");
                                                                  Mediator.Instance.StatusBar.Display("Προστέθηκε Νέος Νέα Ζώνη");
                                                              });


                                                              View.UpdateLayout();

                                                              //UpdateTreeView();
                                                          })
                    });

                    TreeViewdMenuItems.Add(new MenuItem()
                    {
                        Header = "Προσθήκη Νέου Μη Θερμενόμενου Χώρου",
                        Command = new DelegateCommand(() =>
                                                          {
                                                              var building = ((Web.Models.Building)item);
                                                              if (((Web.Models.Building)item).BuildingModel.NonHeatedArea == null)
                                                                  ((Web.Models.Building)item).BuildingModel.NonHeatedArea = new List<NonHeatedArea>();
                                                              var newNonHeatedArea = CreateNonHeatedArea(building);

                                                              ((Web.Models.Building)item).BuildingModel.NonHeatedArea.Add(newNonHeatedArea);
                                                              if (!(SelectedTreeViewItem as Node).HasChildren)
                                                              {
                                                                  (SelectedTreeViewItem as Node).Nodes =
                                                                      new ObservableCollection<Node>() { TreeViewItemsGenerator.CreateNonHeatedAreaTreeViewItem(newNonHeatedArea) };


                                                              }
                                                              else
                                                              {
                                                                  (SelectedTreeViewItem as Node).Nodes.Add(TreeViewItemsGenerator.CreateNonHeatedAreaTreeViewItem(newNonHeatedArea));
                                                              }
                                                              Mediator.Instance.SaveBuildingData();
                                                              ThermalBuildingModel.Instance.DataAccesLayerService.SaveChanges(e =>
                                                              {
                                                                  if (e != null)
                                                                      Mediator.Instance.StatusBar.Display("Σφαλμα κατα την αποθυκευση ΔΕΝ Προστέθηκε Νέος  Μη Θερμενόμενος Χώρος");
                                                                  Mediator.Instance.StatusBar.Display("Προστέθηκε Νέος  Μη Θερμενόμενος Χώρος");
                                                              });


                                                              View.UpdateLayout();

                                                              //UpdateTreeView();
                                                          })
                    });
                    TreeViewdMenuItems.Add(new MenuItem()
                    {
                        Header = "Προσθήκη Νέου Ηλιακού Χώρου",
                        Command = new DelegateCommand(() =>
                        {
                            var building = ((Web.Models.Building)item);
                            if (((Web.Models.Building)item).BuildingModel.SunAreas == null)
                                ((Web.Models.Building)item).BuildingModel.SunAreas = new List<SunArea>();
                            var newNonHeatedArea = CreateSunArea(building);

                            ((Web.Models.Building)item).BuildingModel.SunAreas.Add(newNonHeatedArea);
                            if (!(SelectedTreeViewItem as Node).HasChildren)
                            {
                                (SelectedTreeViewItem as Node).Nodes =
                                    new ObservableCollection<Node>() { TreeViewItemsGenerator.CreateSunAreaTreeViewItem(newNonHeatedArea) };


                            }
                            else
                            {
                                (SelectedTreeViewItem as Node).Nodes.Add(TreeViewItemsGenerator.CreateSunAreaTreeViewItem(newNonHeatedArea));
                            }
                            Mediator.Instance.SaveBuildingData();
                            ThermalBuildingModel.Instance.DataAccesLayerService.SaveChanges(e =>
                            {
                                if (e != null)
                                    Mediator.Instance.StatusBar.Display("Σφαλμα κατα την αποθυκευση ΔΕΝ Προστέθηκε Νέος  Ηλιακοs Χώροs");
                                Mediator.Instance.StatusBar.Display("Προστέθηκε Νέος  Ηλιακοs Χώροs");
                            });


                            View.UpdateLayout();

                            //UpdateTreeView();
                        })
                    });

                    TreeViewdMenuItems.Add(new MenuItem()
                    {
                        Header = "Διαγραφή Κτιρίου",
                        Command = new DelegateCommand(() => { })
                    });


                }

                #endregion

                #region ThermalZone


                if (item is ThermalZone)
                {
                    TreeViewdMenuItems.Add(new MenuItem()
                    {
                        Header = "Προσθήκη Νέας Διαχωριστικής Επιφάνειας",
                        Command = new DelegateCommand(() => CreateAndAddNewSeparatingSurface(item))
                    });


                }

                #endregion

                #region Shell
                if (item is Shell)
                {
                    TreeViewdMenuItems.Add(new MenuItem()
                    {
                        Header = "Προσθήκη Νέας Διαχωριστικής Επιφάνειας",
                        Command = new DelegateCommand(() => CreateAndAddNewSeparatingSurfaceFromShellNode(item))
                    });
                }
                #endregion


            }

        }

        private void CreateAndAddNewSeparatingSurface(object item)
        {
            if (((ThermalZone)item).Shell.SeparatingSurfuces == null)
            {
                ((ThermalZone)item).Shell.SeparatingSurfuces =
                    new List<SeparatingSurfuce>();
            }

            var newSeperateting = CreateEmptySeparatingSerfuce();

            ((ThermalZone)item).Shell.SeparatingSurfuces.Add(newSeperateting);
            if (!(SelectedTreeViewItem as Node).Nodes[0].HasChildren)
            {
                (SelectedTreeViewItem as Node).Nodes[0].Nodes =
                    new ObservableCollection<Node>() { TreeViewItemsGenerator.CreateSeparatingSurfuceTreeViewItem(newSeperateting) };


            }
            else
            {
                (SelectedTreeViewItem as Node).Nodes[0].Nodes.Add(TreeViewItemsGenerator.CreateSeparatingSurfuceTreeViewItem(newSeperateting));
            }
            Mediator.Instance.SaveBuildingData();
            ThermalBuildingModel.Instance.DataAccesLayerService.SaveChanges(e =>
                                                                                {
                                                                                    if (e != null)
                                                                                        Mediator.Instance.StatusBar.Display("Σφαλμα κατα την αποθυκευση ΔΕΝ Νέα Διαχωριστική Επιφάνεια");
                                                                                    Mediator.Instance.StatusBar.Display("Προστέθηκε Νέα Διαχωριστική Επιφάνεια");
                                                                                });

            View.UpdateLayout();
            View.TreeView.UpdateLayout();
        }

        //todo:if ia have time refactor 
        /// <summary>
        /// doublicate code 
        /// </summary>
        /// <param name="item"></param>
        private void CreateAndAddNewSeparatingSurfaceFromShellNode(object item)
        {
            if (((Shell)item).SeparatingSurfuces == null)
            {
                ((Shell)item).SeparatingSurfuces =
                    new List<SeparatingSurfuce>();
            }

            var newSeperateting = CreateEmptySeparatingSerfuce();

            ((Shell)item).SeparatingSurfuces.Add(newSeperateting);
            if (!(SelectedTreeViewItem as Node).HasChildren)
            {
                (SelectedTreeViewItem as Node).Nodes =
                    new ObservableCollection<Node>() { TreeViewItemsGenerator.CreateSeparatingSurfuceTreeViewItem(newSeperateting) };


            }
            else
            {
                (SelectedTreeViewItem as Node).Nodes.Add(TreeViewItemsGenerator.CreateSeparatingSurfuceTreeViewItem(newSeperateting));
            }
            Mediator.Instance.SaveBuildingData();
            ThermalBuildingModel.Instance.DataAccesLayerService.SaveChanges(e =>
            {
                if (e != null)
                    Mediator.Instance.StatusBar.Display("Σφαλμα κατα την αποθυκευση ΔΕΝ Νέα Διαχωριστική Επιφάνεια");
                Mediator.Instance.StatusBar.Display("Προστέθηκε Νέα Διαχωριστική Επιφάνεια");
            });

            View.UpdateLayout();
            View.TreeView.UpdateLayout();
        }


        private SunArea CreateSunArea(Web.Models.Building building)
        {
            return new SunArea() { Id = ++EntitiesIdCounters.SunAreaCounter };
        }

        private NonHeatedArea CreateNonHeatedArea(Web.Models.Building building)
        {
            return new NonHeatedArea() { Id = ++EntitiesIdCounters.NonHeatedAreaCounter };
        }


        public object SelectedTreeViewItem { get; set; }

        public void DisplaySlectedElement(object selectedItem)
        {

            selectedItem = (selectedItem as Node).Tag;
            if (selectedItem is Web.Models.Building)
            {
                View.MainRegion.Content = new BuildingView() { ViewModel = new BuildingViewModel(selectedItem as Web.Models.Building) };
            }
            if (selectedItem is ProjectViewModel)
            {
                View.MainRegion.Content = new ProjectView() { ViewModel = selectedItem as ProjectViewModel };
            }
            if (selectedItem is SunArea)
            {
                View.MainRegion.Content = new SunAreaShellView() { ViewModel = new SunAreaShellViewModel((selectedItem as SunArea)) };
            }
            if (selectedItem is NonHeatedArea)
            {
                View.MainRegion.Content = new SunAreaShellView() { DataContext = new NonHeatedAreaShellViewModel((selectedItem as NonHeatedArea)) };
            }
            else if (selectedItem is Shell)
            {
                View.MainRegion.Content = new ShellView() { ViewModel = new ShellViewModel<Shell>((selectedItem as Shell)) };
            }
            if (selectedItem is SeparatingSurfuce)
            {
                var viewModel = new SeparetingSerfuceShellViewModel(selectedItem as SeparatingSurfuce);
                viewModel.SetUpSeparatingSurfuceAreasList(Model.Project);
                View.MainRegion.Content = new SeparetingSerfuceView()
                                              {
                                                  ViewModel = viewModel

                                              };
            }
            if (selectedItem is NErgy.Building.Systems.ThermalSystem)
            {
                View.MainRegion.Content = new ThermalSystemsView() { ViewModel = new SystemsViewModel(selectedItem as NErgy.Building.Systems.ThermalSystem) };
            }
            if (selectedItem is NErgy.Building.ThermalZone)
            {
                View.MainRegion.Content = new ThermalZoneView() { ViewModel = new ThermalZoneViewModel(selectedItem as NErgy.Building.ThermalZone) };
            }

        }
        public List<ProjectViewModel> Projects
        {
            get
            {
                return InitializeProjectList();
            }
        }

        public Tree TreeViewd { get; set; }

        private List<ProjectViewModel> InitializeProjectList()
        {
            if (Model.Project != null)
            {
                var building = Model.Project.Building.FirstOrDefault();

                // CreateEmptyBuilding(building);
                //  CreateEmptyThermalZone(building);
                //  CreateEmptySystems(building);
                //  CreateSunnyAreaList(building);
                // CreateNonHeatedAreaList(building);

                return new List<ProjectViewModel>() { new ProjectViewModel(Model.Project) };
            }
            else return null;
        }


        private void CreateEmptyThermalZone(Web.Models.Building building)
        {
            if (building.BuildingModel.ThermalZones == null)
                building.BuildingModel.ThermalZones = new List<ThermalZone>();
            if (building.BuildingModel.ThermalZones.Count == 0)
                building.BuildingModel.ThermalZones.Add(CreateEmptyThermalZone());
        }

        private void CreateEmptySystems(Web.Models.Building building)
        {
            building.BuildingModel.ThermalZones.ToList()
                .ForEach(tz =>
                             {
                                 if (tz.Systems == null)
                                     tz.Systems = new List<ThermalSystem>();
                                 if (tz.Systems.Count == 0)
                                     tz.Systems.Add(CreateNewEmptySystem());

                             });
        }

        private void CreateSunnyAreaList(Web.Models.Building building)
        {
            if (building.BuildingModel.SunAreas == null)
                building.BuildingModel.SunAreas = new List<SunArea>() { new SunArea() { TotalArea = 200 } };
        }

        private void CreateNonHeatedAreaList(Web.Models.Building building)
        {
            if (building.BuildingModel.NonHeatedArea == null)
                building.BuildingModel.NonHeatedArea = new List<NonHeatedArea>()
                                          {
                                              new NonHeatedArea()
                                                  {
                                                      ContactWithGroundElement =
                                                          new List<ContactWithGroundElement>()
                                                              {new ContactWithGroundElement()}
                                                  }
                                          };
        }

        private ThermalSystem CreateNewEmptySystem()
        {
            return new ThermalSystem();
        }

        private ThermalZone CreateEmptyThermalZone()
        {
            return new ThermalZone()
            {
                Shells = new List<Shell>() 
          { new Shell() { SeparatingSurfuces = new List<SeparatingSurfuce>() ,
              PassiveDirectSolarGainSystems = new List<PassiveDirectSolarGainSystem>( )}
          },
                Systems = new List<ThermalSystem>() { new ThermalSystem() }
            };
        }

        private SeparatingSurfuce CreateEmptySeparatingSerfuce()
        {
            return new SeparatingSurfuce() { };
        }

        private TreeViewItemsGeneratorLiquid TreeViewItemsGenerator = new TreeViewItemsGeneratorLiquid();

        public IEnumerable CreateRootTreeViewItem()
        {
            return null; // return new List<TreeViewItem>() { .CreateRootTreeViewItem(this.Projects) };
        }

        public ObservableCollection<Node> CreateRootTreeViewItemNodes()
        {
            return TreeViewItemsGenerator.CreateRootTreeViewItem(this.Projects);
        }

        public void UpdateTreeView()
        {
            View.TreeView.Nodes.Clear();
            CreateRootTreeViewItemNodes().ToList().ForEach(View.TreeView.Nodes.Add);
        }

        public void KeyDownPressed(object sender, Key key)
        {
            if (key == Key.Delete)
            {
                if (SelectedTreeViewItem != null)
                {
                    var type = SelectedTreeViewItem as Node;

                    if (type != null && (type.Tag is Web.Models.Building))
                    {
                        var msgResult = MessageBox.Show("Θέλετε να Διαγράψετε το κτίριο", "Διαγραφή", MessageBoxButton.OKCancel);
                        if (msgResult == MessageBoxResult.OK)
                        {
                            Mediator.Instance.RemoveBuildingFromProject((type.Tag as Web.Models.Building));
                            type.ParentNode.Nodes.Remove(type);
                            View.UpdateLayout();

                        }
                    }
                    if (type != null && (type.Tag is ThermalZone))
                    {
                        var msgResult = MessageBox.Show("Θέλετε να Διαγράψετε την Ζώνη", "Διαγραφή", MessageBoxButton.OKCancel);
                        if (msgResult == MessageBoxResult.OK)
                        {
                            Mediator.Instance.RemoveThermalZoneFromBuilding((type.Tag as ThermalZone), (type.ParentNode.Tag as Web.Models.Building));
                            type.ParentNode.Nodes.Remove(type);
                            View.UpdateLayout();

                        }
                    }


                    if (type != null && (type.Tag is NonHeatedArea))
                    {
                        var msgResult = MessageBox.Show("Θέλετε να Διαγράψετε την Μη Θερμενόμενη Επιφάνεια", "Διαγραφή", MessageBoxButton.OKCancel);
                        if (msgResult == MessageBoxResult.OK)
                        {
                            Mediator.Instance.RemoveNonHeatedAreaFromBuilding((type.Tag as NonHeatedArea), (type.ParentNode.Tag as Web.Models.Building));
                            type.ParentNode.Nodes.Remove(type);
                            View.UpdateLayout();

                        }
                    }

                    if (type != null && (type.Tag is SunArea))
                    {
                        var msgResult = MessageBox.Show("Θέλετε να Διαγράψετε τον ηλιακό Χώρο", "Διαγραφή", MessageBoxButton.OKCancel);
                        if (msgResult == MessageBoxResult.OK)
                        {
                            Mediator.Instance.RemoveSunAreaFromBuilding((type.Tag as SunArea), (type.ParentNode.Tag as Web.Models.Building));
                            type.ParentNode.Nodes.Remove(type);
                            View.UpdateLayout();

                        }
                    }

                    if (type != null && (type.Tag is SeparatingSurfuce))
                    {
                        var msgResult = MessageBox.Show("Θέλετε να Διαγράψετε την Διαχωριστική επιφάνεια", "Διαγραφή", MessageBoxButton.OKCancel);
                        if (msgResult == MessageBoxResult.OK)
                        {
                            Mediator.Instance.RemoveSeparatingSurfuceFromShell((type.Tag as SeparatingSurfuce), (type.ParentNode.Tag as Shell));
                            type.ParentNode.Nodes.Remove(type);
                            View.UpdateLayout();
                            View.TreeView.UpdateLayout();
                        }
                    }

                }
            }
        }
    }

    public class TreeViewItemsGenerator
    {

        public TreeViewItem CreateRootTreeViewItem(List<ProjectViewModel> projects)
        {
            var rootTreeViewItem = new TreeViewItem() { Tag = null, Header = "NErgy Ενεργειακή Μελέτη" };
            projects.ForEach(p => rootTreeViewItem.Items.Add(CreateProjectTreeViewItem(p)));

            return rootTreeViewItem;
        }

        public TreeViewItem CreateProjectTreeViewItem(ProjectViewModel project)
        {
            var rootTreeViewItem = new TreeViewItem() { Tag = project, Header = "Έργο" };
            project.Buildings.ToList().ForEach(p =>
            {
                rootTreeViewItem.Items.Add(CreateBuildingTreeViewItem(p));
            });


            return rootTreeViewItem;
        }

        private object CreateBuildingTreeViewItem(Web.Models.Building building)
        {
            var rootTreeViewItem = new TreeViewItem() { Tag = building, Header = "Κτήριο" };
            building.BuildingModel.ThermalZones.ToList().ForEach(p =>
            {
                rootTreeViewItem.Items.Add(CreateThermalZoneTreeViewItem(p));
            });


            return rootTreeViewItem;
        }

        private object CreateThermalZoneTreeViewItem(ThermalZone thermalZone)
        {
            if (thermalZone == null) return null;
            var rootTreeViewItem = new TreeViewItem() { Tag = thermalZone, Header = "Ζώνη" };

            rootTreeViewItem.Items.Add(CreateShellTreeViewItem(thermalZone.Shell));
            if (thermalZone.Systems != null) rootTreeViewItem.Items.Add(CreateSystemsTreeViewItem(thermalZone.Systems));

            return rootTreeViewItem;
        }

        private object CreateSystemsTreeViewItem(IList<NErgy.Building.Systems.ThermalSystem> systems)
        {
            if (systems == null) return null;
            var rootTreeViewItem = new TreeViewItem() { Tag = systems.FirstOrDefault(), Header = "Συστήματα" };


            return rootTreeViewItem;
        }

        private object CreateShellTreeViewItem(Shell shell)
        {
            var rootTreeViewItem = new TreeViewItem() { Tag = shell, Header = "Κέλυφος" };
            if (shell.SeparatingSurfuces != null)
                shell.SeparatingSurfuces.ToList().ForEach(p =>
                {
                    rootTreeViewItem.Items.Add(CreateSeparatingSurfuceTreeViewItem(p));
                });


            return rootTreeViewItem;
        }

        private object CreateSeparatingSurfuceTreeViewItem(SeparatingSurfuce separatingSurfuce)
        {
            return new TreeViewItem() { Tag = separatingSurfuce, Header = "Διαχωριστική Επιφάνεια" };
        }

    }
    public class TreeViewItemsGeneratorLiquid
    {

        public ObservableCollection<Node> CreateRootTreeViewItem(List<ProjectViewModel> projects)
        {
            var rootTreeViewItem = new ObservableCollection<Node>();// new Node() { Tag = null, Title = "NErgy Ενεργειακή Μελέτη" };
            if (projects != null) projects.ForEach(p => rootTreeViewItem.Add(CreateProjectTreeViewItem(p)));

            return rootTreeViewItem;
        }

        public Node CreateProjectTreeViewItem(ProjectViewModel project)
        {
            var rootTreeViewItem = new Node() { Tag = project, Title = "Έργο", Icon = "../../Assets/projecticon.jpg" };
            project.Buildings.ToList().ForEach(p =>
            {
                rootTreeViewItem.Nodes.Add(CreateBuildingTreeViewItem(p));
            });


            return rootTreeViewItem;
        }

        public Node CreateBuildingTreeViewItem(Web.Models.Building building)
        {
            var rootTreeViewItem = new Node() { Tag = building, Title = "Κτήριο", Icon = "../../Assets/buildingicon.jpg" };
            if (building.BuildingModel != null)
            {


                if (building.BuildingModel.ThermalZones != null)
                    building.BuildingModel.ThermalZones.ToList().ForEach(p =>
                                                                             {
                                                                                 rootTreeViewItem.Nodes.Add(CreateThermalZoneTreeViewItem(p));
                                                                             });

                if (building.BuildingModel.SunAreas != null)
                    building.BuildingModel.SunAreas.ToList().ForEach(p =>
                                                                         {
                                                                             rootTreeViewItem.Nodes.Add(CreateSunAreaTreeViewItem(p));
                                                                         });
                if (building.BuildingModel.NonHeatedArea != null)
                    building.BuildingModel.NonHeatedArea.ToList().ForEach(p =>
                                                                              {
                                                                                  rootTreeViewItem.Nodes.Add(CreateNonHeatedAreaTreeViewItem(p));
                                                                              });
            }

            return rootTreeViewItem;
        }

        public Node CreateNonHeatedAreaTreeViewItem(NonHeatedArea NonHeatedArea)
        {
            if (NonHeatedArea == null) return null;
            var rootTreeViewItem = new Node() { Tag = NonHeatedArea, Title = "Μη Θερμενόμενος Χώρος" + NonHeatedArea.Id };
            return rootTreeViewItem;
        }

        public Node CreateSunAreaTreeViewItem(SunArea sunArea)
        {
            if (sunArea == null) return null;
            var rootTreeViewItem = new Node() { Tag = sunArea, Title = "Ηλιακός Χώρος" + sunArea.Id, Icon = "../../Assets/solar-energy-home-icon.jpg" };
            return rootTreeViewItem;


        }

        public Node CreateThermalZoneTreeViewItem(ThermalZone thermalZone)
        {
            if (thermalZone == null) return null;
            var rootTreeViewItem = new Node() { Tag = thermalZone, Title = "Ζώνη" };

            if (thermalZone.Shell != null) rootTreeViewItem.Nodes.Add(CreateShellTreeViewItem(thermalZone.Shell));
            if (thermalZone.Systems != null) rootTreeViewItem.Nodes.Add(CreateSystemsTreeViewItem(thermalZone.Systems));

            return rootTreeViewItem;
        }

        public Node CreateSystemsTreeViewItem(IList<NErgy.Building.Systems.ThermalSystem> systems)
        {
            if (systems == null) return null;
            var rootTreeViewItem = new Node() { Tag = systems.FirstOrDefault(), Title = "Συστήματα", Icon = "../../Assets/mechanicsicon.jpg" };


            return rootTreeViewItem;
        }

        public Node CreateShellTreeViewItem(Shell shell)
        {
            var rootTreeViewItem = new Node() { Tag = shell, Title = "Κέλυφος" };
            if (shell != null)
                if (shell.SeparatingSurfuces != null)
                    shell.SeparatingSurfuces.ToList().ForEach(p =>
                                                                  {
                                                                      rootTreeViewItem.Nodes.Add(CreateSeparatingSurfuceTreeViewItem(p));
                                                                  });


            return rootTreeViewItem;
        }

        public Node CreateSeparatingSurfuceTreeViewItem(SeparatingSurfuce separatingSurfuce)
        {
            return new Node() { Tag = separatingSurfuce, Title = "Διαχωριστική Επιφάνεια" };
        }

    }


    public class IconConverter : IValueConverter
    {
        public object Convert(
            object value,
            Type targetType,
            object parameter,
            CultureInfo culture)
        {
            if (value is Web.Models.Building)
            {
                return new Image()
                {
                    Source =
                        new BitmapImage(
                        new Uri(
                            "/NErgy.Silverlight;component/Assets/buildingicon.jpg", UriKind.Relative))
                };
            }

            if (value is ProjectViewModel)
            {
                return new Image()
                     {
                         Source =
                             new BitmapImage(
                             new Uri(
                                 "/NErgy.Silverlight;component/Assets/projecticon.jpg", UriKind.Relative))
                     };
            }


            return null;
        }

        public object ConvertBack(
            object value,
            Type targetType,
            object parameter,
            CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

}
