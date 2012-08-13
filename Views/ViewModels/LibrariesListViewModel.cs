using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using NErgy.Silverlight.Models;
using SimpleMvvmToolkit;
using NErgy.Silverlight.Web.Models;

namespace NErgy.Silverlight.Views.ViewModels
{
    public class LibrariesListViewModel : ViewModelBase<LibrariesListViewModel>
    {
        private IDataAccesLayerServiceAgent DataAccesLayerService = ThermalBuildingModel.Instance.DataAccesLayerService;
        #region Fields

  
        public int userID { get; set; }

        #endregion
 
        public LibrariesListViewModel()
        {
            userID = 1;
        }

        #region Notifications

        // Events to notify the view or obtain data from the view
        public event EventHandler<NotificationEventArgs<Exception>> ErrorNotice;
        public event EventHandler<NotificationEventArgs<object, bool>> SaveChangesNotice;
        public event EventHandler<NotificationEventArgs> ItemsSavedNotice;

        public event EventHandler<NotificationEventArgs<Material>> MaterialChanged;

        #endregion

        #region Properties

        // Add properties using the mvvmprop code snippet

        private ObservableCollection<Library> _libraries;
        public ObservableCollection<Library> Libraries
        {
            get { return _libraries; }
            set
            {
                _libraries = value;
                NotifyPropertyChanged(vm => vm.Libraries);
            }
        }

        private Library selectedLibrary;
        public Library SelectedLibrary
        {
            get { return selectedLibrary; }
            set
            {
                selectedLibrary = value;
                NotifyPropertyChanged(vm => vm.SelectedLibrary);
            }
        }

        private ObservableCollection<Material> _materials;
        public ObservableCollection<Material> Materials
        {
            get { return _materials; }
            set
            {
                _materials = value;
                NotifyPropertyChanged(vm => vm.Materials);
            }
        }

        private Material _selectedMaterial;
        public Material SelectedMaterial
        {
            get { return _selectedMaterial; }
            set
            {
                _selectedMaterial = value;
                NotifyPropertyChanged(vm => vm.SelectedMaterial);
            }
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


        private DelegateCommand<Material> setMaterialCommand;
        public DelegateCommand<Material> SetMaterialCommand
        {
            get { return setMaterialCommand?? new DelegateCommand<Material>(NotifyMaterialSelected); }
            set
            {
                setMaterialCommand = value;
                NotifyPropertyChanged(m => m.SetMaterialCommand);
            }
        }

        #endregion

        #region Methods
 
        public void NotifyMaterialSelected(Material material)
        {
            Notify(MaterialChanged, new NotificationEventArgs<Material>(null, material));
        }
 
        public void LoadLibraries()
        {
            // Load items
            DataAccesLayerService.GetMaterialLibrariesForUser(
                (entities, error) => LibrariesLoaded(entities, error),userID);

            // Reset property
            Libraries = null;

            // Flip busy flag
            IsBusy = true;
        }
 
        public void LoadMaterialsForLibrary()
        {
            // Load items
            DataAccesLayerService.GetMaterialsForLibrary(
                 selectedLibrary.LibraryID,(entities, error) => MaterialsLoaded(entities, error));

            // Reset property
            Materials = null;

            // Flip busy flag
            IsBusy = true;
        }
 
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

        // Add item
        public void Add(Library item)
        {
            
        }

        // Remove selected item
        public void Remove()
        {
            if (SelectedLibrary != null)
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

        private void LibrariesLoaded(IList<Library> entities, Exception error)
        {
            // If no error is returned, set the model to entities
            if (error == null)
            {
                Libraries = new ObservableCollection<Library>(entities);
            }
            // Otherwise notify view that there's an error
            else
            {
                NotifyError("Unable to retrieve items", error);
            }

            // Set SelectedItem to the first item
            if (Libraries.Count > 0)
            {
                SelectedLibrary = Libraries[0];
            }

            // We're done
            IsBusy = false;
        }

        private void MaterialsLoaded(IList<Material> entities, Exception error)
        {
            // If no error is returned, set the model to entities
            if (error == null)
            {
                Materials = new ObservableCollection<Material>(entities);
            }
            // Otherwise notify view that there's an error
            else
            {
                NotifyError("Unable to retrieve Matials", error);
            }

            // Set SelectedItem to the first item
            if (Libraries.Count > 0)
            {
                SelectedMaterial = Materials[0];
            }

            // We're done
            IsBusy = false;
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
            CanEdit = !IsBusy && selectedLibrary != null;
            CanRemove = !IsBusy && selectedLibrary != null;
        }

        // Set SelectedItem
        private void SelectItem(object item)
        {
          
        }

        #endregion
 
    }
}
