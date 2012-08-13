using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using NErgy.Building;
using NErgy.Silverlight.Models;
using NErgy.Silverlight.Web.Models;
using SimpleMvvmToolkit;
using BuildingCategory = NErgy.Building.BuildingCategory;
using BuildingUsage = NErgy.Building.BuildingUsage;


namespace NErgy.Silverlight.Views.ViewModels
{
    public class BuildingListViewModel : ViewModelBase<BuildingListViewModel>
    {
             private IDataAccesLayerServiceAgent DataAccesLayerService { get; set; }
        private readonly Project _project;

     
        public BuildingListViewModel(Project project)
        {
            BuildingExposures = new List<EnumObject<BuildingExposure>>(GetEnumList<BuildingExposure>());

            _project = project;
            DataAccesLayerService = new MockDataAccesLayerServiceAgent();
            Buildings = new ObservableCollection<Web.Models.Building>(project.Building);
           if(Buildings.Any())
            SelectedBuilding = Buildings[0];

            NeigbourBuildings = new ObservableCollection<NeighborBuilding>(project.NeighborBuildings);
            if(NeigbourBuildings.Any())
            SelectedNeigbourBuilding = NeigbourBuildings[0];

            Initialize();
        }

        public BuildingListViewModel(Web.Models.Building building)
        {
        }

        private List<EnumObject<BuildingExposure>> _buildingExposures;
        public List<EnumObject<BuildingExposure>> BuildingExposures
        {
            get { return _buildingExposures; }
            set
            {
                _buildingExposures = value;
                NotifyPropertyChanged(m => m.BuildingExposures);
            }
        }

        private void Initialize()
        {
          
        }

        public BuildingListViewModel()
        {
        }

 
        #region Properties

        private Web.Models.Building _selectedBuilding;
        public Web.Models.Building SelectedBuilding
        {
            get { return _selectedBuilding; }
            set
            {
                _selectedBuilding = value;
                this.BuildingViewModel = new BuildingViewModel(value);
            }
        }

        private ObservableCollection<Web.Models.Building> buildings;
        public ObservableCollection<Web.Models.Building> Buildings
        {
            get { return buildings; }
            set
            {
                buildings = value;
                NotifyPropertyChanged(m => m.Buildings);
            }
        }

        public BuildingViewModel BuildingViewModel { get; set; }
        //public IList<NErgy.Building.BuildingStory> BuildingStories
        //{
        //    get { return Model.BuildingModel.BuildingStories; }
        //    set
        //    {
        //        Model.BuildingModel.BuildingStories = value;
        //        NotifyPropertyChanged(vm => vm.BuildingStories);
        //    }
        //}

     
        private NeighborBuilding selectedNeigbourBuilding;
        public NeighborBuilding SelectedNeigbourBuilding
        {
            get { return selectedNeigbourBuilding; }
            set
            {
                selectedNeigbourBuilding = value;
                NotifyPropertyChanged(vm => vm.SelectedNeigbourBuilding);
            }
        }

       
   

        private ObservableCollection<NeighborBuilding> neigbourBuildings;
        public ObservableCollection<NeighborBuilding> NeigbourBuildings
        {
            get { return neigbourBuildings; }
            set
            {
                neigbourBuildings = value;
                NotifyPropertyChanged(m => m.NeigbourBuildings);
            }
        }

        public ICommand AddNewNeighbourBuilding
        {
            get
            {
                return new DelegateCommand(() =>
                                               {
                                                   NeigbourBuildings.Add(new NeighborBuilding());
                                               });
            }
        }

        public ICommand SubmitChangesNeighbourBuilding
        {
            get
            {
                return new DelegateCommand(() =>
                {

                });
            }
        }

        public ICommand AddBuildingCommand
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    Buildings.Add(new Web.Models.Building()
                                      {
                                         // Project = _project
                                      });
                    
                  
                });
            }
        }
         
      

        //public override void BeginEdit()
        //{
        //    base.BeginEdit();
        //}
        //public override void EndEdit()
        //{
        //    //this.DataLayer.NErgyContext.SubmitChanges();
        //    base.EndEdit();
        //}
        #endregion //Properties

        #region Notifications

        // Events to notify the view or obtain data from the view
        public event EventHandler<NotificationEventArgs<Exception>> ErrorNotice;
        public event EventHandler<NotificationEventArgs<object, bool>> SaveChangesNotice;
        public event EventHandler<NotificationEventArgs> ItemsSavedNotice;

        #endregion

        #region Helpers

        // Helper method to notify View of an error
        private void NotifyError(string message, Exception error)
        {
            // Notify view of an error
            Notify(ErrorNotice, new NotificationEventArgs<Exception>(message, error));
        }

        private bool isBusy;
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

        private bool canLoad = true;
        public bool CanLoad
        {
            get { return canLoad; }
            set
            {
                canLoad = value;
                NotifyPropertyChanged(m => m.CanLoad);
            }
        }

        private bool canAdd;
        public bool CanAdd
        {
            get { return canAdd; }
            set
            {
                canAdd = value;
                NotifyPropertyChanged(m => m.CanAdd);
            }
        }

        private bool canEdit;
        public bool CanEdit
        {
            get { return canEdit; }
            set
            {
                canEdit = value;
                NotifyPropertyChanged(m => m.CanEdit);
            }
        }

        private bool canRemove;
        public bool CanRemove
        {
            get { return canRemove; }
            set
            {
                canRemove = value;
                NotifyPropertyChanged(m => m.CanRemove);
            }
        }


        private void SetCanProperties()
        {
            CanLoad = !IsBusy;
            CanAdd = !IsBusy;
            CanEdit = !IsBusy;
            CanRemove = !IsBusy;
        }

        #endregion

        #region Methods

        // Add methods that will be called by the view

      
        // Save changes on the domain content if there are any
        public void SaveChanges()
        {
            // Prompt the user to save changes if there are any
            Notify(SaveChangesNotice, new NotificationEventArgs<object, bool>
                ("There are unsaved changes. Do you wish to save?", null,
                confirm =>
                {
                    if (confirm)
                    {
                        // Save changes
                        DataAccesLayerService.SaveChanges
                            (error => ItemsSaved(error));
                    }
                }));
        }

        // Call RejectChanged on the service agent then reload items
        public void RejectChanges()
        {
            DataAccesLayerService.RejectChanges();
 
        }

        #endregion

        #region Completion Callbacks

        // Add callback methods for async calls to the service agent

      
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

        //TODO:new UserControl
        #region DataGrid Extensions

        private INeighborBuilding emptyNeighborBuilding;


        #endregion
    }
}
