using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.ServiceModel.DomainServices.Client.ApplicationServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using NErgy.Building;
using NErgy.Silverlight.Helpers;
using NErgy.Silverlight.Models;
using NErgy.Silverlight.Views.Controllers;
using NErgy.Silverlight.Web.Models;
using SimpleMvvmToolkit;

namespace NErgy.Silverlight.Views.ViewModels
{
    public class ProjectListViewModel : ViewModelBase<ProjectListViewModel>
    {
        #region Fields

        public Web.User user { get; set; }

        #endregion

        private ProjectViewModel _projectViewModel;

        public ProjectListViewModel()
        {
            Logger.Instance.Log("ProjectListViewModel Constructor");
            WebContext.Current.Authentication.LoggedIn += Authentication_LoggedIn;
            Mediator.Instance.OnNewProjectCreated += (s, e) => { LoadProjectForUser(); };
            if (WebContext.Current.User != null && WebContext.Current.User.IsAuthenticated)
            {
                Authentication_LoggedIn(null, null);
            }
        }

        private void Authentication_LoggedIn(object sender, AuthenticationEventArgs e)
        {
            user = WebContext.Current.User;
            LoadProjectForUser();
            SelectedProjectChanged += Mediator.Instance.ProjectListViewModelSelectedProjectChanged;
        }

        #region Notifications

        // Events to notify the view or obtain data from the view
        public event EventHandler<NotificationEventArgs<Exception>> ErrorNotice;
        public event EventHandler<NotificationEventArgs<object, bool>> SaveChangesNotice;
        public event EventHandler<NotificationEventArgs> ItemsSavedNotice;

        public static event EventHandler<NotificationEventArgs<Project>> SelectedProjectChanged;
        public static event EventHandler<NotificationEventArgs<Project>> BuildingInProjectLoaded;
        public static event EventHandler<NotificationEventArgs<Project>> EngineersInProjectLoaded;
        #endregion

        #region Properties

        // Add properties using the mvvmprop code snippet

        private ObservableCollection<Project> _projects;
        private bool canAdd;
        private bool canEdit;
        private bool canLoad = true;
        private bool canRemove;
        private bool isBusy;

        private Project selectedProject;
        private IDataAccesLayerServiceAgent DataAccesLayerService = ThermalBuildingModel.Instance.DataAccesLayerService;

        public ObservableCollection<Project> Projects
        {
            get { return _projects; }
            set
            {
                _projects = value;
                NotifyPropertyChanged(vm => vm.Projects);
            }
        }

        public Project SelectedProject
        {
            get { return selectedProject; }
            set
            {
                if (value == null) return;
                Logger.Instance.Log("SelectedProject set to value:" + value);
                selectedProject = value;
                NotifyPropertyChanged(vm => vm.SelectedProject);

                Notify(SelectedProjectChanged, new NotificationEventArgs<Project>("", selectedProject));
                if (selectedProject.Building != null)
                {
                    if (selectedProject.Building.Any())
                        Notify(BuildingInProjectLoaded, new NotificationEventArgs<Project>("", selectedProject));
                    ProjectViewModel = new ProjectViewModel(selectedProject) { DataAccesLayerServiceAgent = DataAccesLayerService };
                }
            }
        }


        public bool IsBusy
        {
            get { return isBusy; }
            set
            {
                isBusy = value;
                SetCanProperties();
                NotifyPropertyChanged(m => m.IsBusy);
            }
        }

        public bool CanLoad
        {
            get { return canLoad; }
            set
            {
                canLoad = value;
                NotifyPropertyChanged(m => m.CanLoad);
            }
        }

        public bool CanAdd
        {
            get { return canAdd; }
            set
            {
                canAdd = value;
                NotifyPropertyChanged(m => m.CanAdd);
            }
        }

        public bool CanEdit
        {
            get { return canEdit; }
            set
            {
                canEdit = value;
                NotifyPropertyChanged(m => m.CanEdit);
            }
        }

        public bool CanRemove
        {
            get { return canRemove; }
            set
            {
                canRemove = value;
                NotifyPropertyChanged(m => m.CanRemove);
            }
        }

        #endregion

        #region Methods

        public void LoadProjectForUser()
        {

            // Projects = null;
            // Flip busy flag
            IsBusy = true;
            // Load items
            DataAccesLayerService.GetProjectsForUser(user.Name,
                                                     (entities, error) =>
                                                     {
                                                         AddNeighbourBuildings(entities);
                                                         ProjectsLoaded(entities, error);
                                                         //LoadEngineersForProjects();
                                                         //LoadPoleodomiesForProjects();
                                                         //LoadBuildingsForProject();
                                                         SetActiveProject();

                                                     });
        }


        //public void LoadAllPublicProjectsForUser()
        //{
        //    Projects = null;
        //    // Flip busy flag
        //    IsBusy = true;
        //    // Load items
        //    DataAccesLayerService.GetProjectsForUser(userID,
        //                                             (entities, error) =>
        //                                             {
        //                                                 AddNeighbourBuildings(entities);
        //                                                 ProjectsLoaded(entities, error);
        //                                                 LoadEngineersForProjects();
        //                                                 LoadBuildingsForProject();
        //                                                 SetActiveProject();
        //                                             });
        //}

        private void AddNeighbourBuildings(List<Project> entities)
        {
            entities.ForEach(p =>
                                 {
                                     p.NeighborBuildings = new List<NeighborBuilding>
                                                               {
                                                                   //new NeighborBuilding
                                                                   //    {
                                                                   //        Angle = 0,
                                                                   //        Breadth = 30,
                                                                   //        Height = 30,
                                                                   //        Width = 10,
                                                                   //        X = 0,
                                                                   //        Y = 50
                                                                   //    },
                                                                   //new NeighborBuilding
                                                                   //    {
                                                                   //        Angle = 0,
                                                                   //        Breadth = 30,
                                                                   //        Height = 30,
                                                                   //        Width = 10,
                                                                   //        X = 20,
                                                                   //        Y = 50
                                                                   //    }
                                                               };
                                 });
        }

        //private void LoadBuildingsForProject()
        //{
        //    IsBusy = true;
        //    // Load items

        //    foreach (Project project in Projects)
        //    {
        //        LoadBuildingForProject(project);
        //    }
        //}

        protected void LoadBuildingForProject(Project project)
        {
            DataAccesLayerService.GetProjectBuildings(project, (b, e) =>
                                                                   {
                                                                       if (project != null)
                                                                           if (project.Building != null)
                                                                               if (project.Building.Any())
                                                                               {
                                                                                   Web.Models.Building building =
                                                                                       project.Building.First();
                                                                                   LoadBuildingData(building);
                                                                                   Notify(BuildingInProjectLoaded,
                                                                                          new NotificationEventArgs
                                                                                              <Project>("", project));
                                                                               }

                                                                       IsBusy = false;
                                                                   });
        }


        internal void LoadBuildingData(Web.Models.Building building)
        {
            if (!string.IsNullOrEmpty(building.BuildingDataXml))
            {
                Logger.Instance.Log("LoadBuildingData" + building.BuildingDataXml);

                building.BuildingModel = XmlSerializer.Deserialize<BuildingModel>(building.BuildingDataXml);
               if (building.BuildingModel==null)
                building.BuildingModel = new BuildingModel(); 
                if (building.BuildingModel.NonHeatedArea != null && building.BuildingModel.NonHeatedArea.Count > 0)
                    EntitiesIdCounters.NonHeatedAreaCounter = building.BuildingModel.NonHeatedArea.Max(s => s.Id);
                if (building.BuildingModel.SunAreas != null && building.BuildingModel.SunAreas.Count > 0)
                    EntitiesIdCounters.SunAreaCounter = building.BuildingModel.SunAreas.Max(s => s.Id);

                ThermalBuildingModel.Instance.IsDesignModelLoaded = true;
            }
           
        }


        //public static  event EventHandler BuildingSLoaded;
        private void LoadEngineersForProjects()
        {
            //Projects = null;
            // Flip busy flag
            // IsBusy = true;
            // Load items

            foreach (Project project in Projects)
            {
                DataAccesLayerService.GetProjectEngineers(project, e =>
                                                                       {
                                                                           EngineersInProjectLoaded(this, new NotificationEventArgs<Project>("Engineers", project));
                                                                       });
            }


        }

        private void LoadPoleodomiesForProjects(Project project)
        {

            IsBusy = true;

            DataAccesLayerService.GetProjectPoleodomies(project, e =>
                                                                     {
                                                                         IsBusy = false;
                                                                     });

        }

        // Save changes on the domain content if there are any
        public void SaveChanges()
        {
            //// Prompt the user to save changes if there are any
            //Notify(SaveChangesNotice, new NotificationEventArgs<object, bool>
            //    ("There are unsaved changes. Do you wish to save?", null,
            //    confirm =>
            //    {
            //        if (confirm)
            //        {
            //            // Save changes
            //            DataAccesLayerService.SaveChanges
            //                (error => ItemsSaved(error));
            //        }
            //    }));

            DataAccesLayerService.SaveChanges(ItemsSaved);
        }



        // Add item
        public void Add(Library item)
        {
        }

        // Remove selected item
        public void Remove()
        {
            if (selectedProject != null)
            {
                SetCanProperties();
            }
        }

        // Call RejectChanged on the service agent then reload items
        public void RejectChanges()
        {
            DataAccesLayerService.RejectChanges();
            //LoadProducts();
        }

        #endregion

        #region Completion Callbacks

        // Add callback methods for async calls to the service agent

        private void ProjectsLoaded(IList<Project> entities, Exception error)
        {
            // If no error is returned, set the model to entities
            if (error == null)
            {
                Projects = new ObservableCollection<Project>(entities);
                Projects.ToList().ForEach(LoadBuildingForProject);
                Projects.ToList().ForEach(LoadPoleodomiesForProjects);

                if (Projects.Count > 0)
                {
                    var idCounter = Projects.ToList().Max(p => p.LocalIdCounter);
                    EntitiesIdCounters.ProjectsIdCounter = idCounter.HasValue ? idCounter.Value+1 : 1;
                }

                else
                {
                    EntitiesIdCounters.ProjectsIdCounter = 1;
                }
            }
            // Otherwise notify view that there's an error
            else
            {
                NotifyError("Unable to retrieve items", error);
            }

            // Set SelectedItem to the first item
            SetActiveProject();

            // We're done
            IsBusy = false;
        }

        private void SetActiveProject()
        {
            if (Projects.Count > 0)
            {
                SelectedProject = Projects[0];
            }
        }


        private void ItemsSaved(Exception error)
        {
            if (error == null)
            {
                // Notify view products were saved successfully
                Notify(ItemsSavedNotice, new NotificationEventArgs
                                             ("Items were successfully saved"));
            }
            else
            {
                // Notify view if there's an error
                NotifyError("Unable to save items", error);
            }

            // We're done
            IsBusy = false;
        }

        #endregion

        #region Helpers

        // Helper method to notify View of an error
        private void NotifyError(string message, Exception error)
        {
            // Notify view of an error
            Notify(ErrorNotice, new NotificationEventArgs<Exception>(message, error));
        }

        private void SetCanProperties()
        {
            CanLoad = !IsBusy;
            CanAdd = !IsBusy;
            CanEdit = !IsBusy && selectedProject != null;
            CanRemove = !IsBusy && selectedProject != null;
        }

        // Set SelectedItem
        private void SelectItem(object item)
        {
        }

        #endregion

        public ICommand DeleteProjectCommand
        {
            get
            {
                return new DelegateCommand<Project>((p) =>
                                                        {
                                                            MessageBoxResult result = MessageBox.Show("Θέλετε Να Διαγράψετε το Έργο;",
       "Διαγραφής", MessageBoxButton.OKCancel);

                                                            if (result == MessageBoxResult.OK)
                                                            {
                                                                var name = p.ProjectTitle;
                                                                DataAccesLayerService.RemoveProject(p);
                                                                this.Projects.Remove(p);
                                                                DataAccesLayerService.SaveChanges(e=>
                                                                {
                                                                    Mediator.Instance.StatusBar.Display(" το Έργο Διαγραφηκε" + name);
                                                                });
                                                            }


                                                        });
            }
        }

        public ICommand CreateNewProjectCommand
        {
            get
            {
                return new DelegateCommand(() =>
                                                        {
                                                            Mediator.Instance.CreateNewProject();

                                                        });
            }
        }


        public ProjectViewModel ProjectViewModel
        {
            get { return _projectViewModel; }
            set
            {
                _projectViewModel = value;
                NotifyPropertyChanged(vm => vm.ProjectViewModel);
            }
        }
    }
}