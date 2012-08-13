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
using Section = NErgy.Silverlight.Web.Models.Section;

namespace NErgy.Silverlight.Views.ViewModels
{
    public class SectionLibrariesListViewModel : ViewModelBase<SectionLibrariesListViewModel>
    {

        #region Fields

  
        public int userID { get; set; }

        #endregion

        public SectionLibrariesListViewModel()
        {
            userID = 1;
        }

        #region Notifications

        // Events to notify the view or obtain data from the view
        public event EventHandler<NotificationEventArgs<Exception>> ErrorNotice;
        public event EventHandler<NotificationEventArgs<object, bool>> SaveChangesNotice;
        public event EventHandler<NotificationEventArgs> ItemsSavedNotice;

        public event EventHandler<NotificationEventArgs<Section>> SectionChanged;

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

        private ObservableCollection<Section> _Sections;
        public ObservableCollection<Section> Sections
        {
            get { return _Sections; }
            set
            {
                _Sections = value;
                NotifyPropertyChanged(vm => vm.Sections);
            }
        }

        private Section _selectedSection;
        public Section SelectedSection
        {
            get { return _selectedSection; }
            set
            {
                _selectedSection = value;
                NotifyPropertyChanged(vm => vm.SelectedSection);
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
 
        private DelegateCommand<Section> setSectionCommand;
        private IDataAccesLayerServiceAgent DataAccesLayerService = ThermalBuildingModel.Instance.DataAccesLayerService;

        public DelegateCommand<Section> SetSectionCommand
        {
            get { return setSectionCommand?? new DelegateCommand<Section>(NotifySectionSelected); }
            set
            {
                setSectionCommand = value;
                NotifyPropertyChanged(m => m.SetSectionCommand);
            }
        }

        #endregion

        #region Methods
 
        public void NotifySectionSelected(Section Section)
        {
            Notify(SectionChanged, new NotificationEventArgs<Section>(null, Section));
        }
 
        public void LoadLibraries()
        {
            // Load items
            DataAccesLayerService.GetSectionLibrariesForUser(
                (entities, error) => LibrariesLoaded(entities, error),userID);

            // Reset property
            Libraries = null;

            // Flip busy flag
            IsBusy = true;
        }
 
        public void LoadSectionsForLibrary()
        {
            if(SelectedLibrary==null) return;
            // Load items
            DataAccesLayerService.GetSectionsForLibrary(
                 selectedLibrary.LibraryID,(entities, error) => SectionsLoaded(entities, error));

            // Reset property
            Sections = null;

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
            if (item != null) 
            {
                DataAccesLayerService.AddLibrary(item);
                item.UserID = userID.ToString();
                SelectedLibrary = item; 
                LoadLibraries();
            }
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

        private void SectionsLoaded(IList<Section> entities, Exception error)
        {
            // If no error is returned, set the model to entities
            if (error == null)
            {
                Sections = new ObservableCollection<Section>(entities);
            }
            // Otherwise notify view that there's an error
            else
            {
                NotifyError("Unable to retrieve Matials", error);
            }

            // Set SelectedItem to the first item
            if (Libraries.Count > 0)
            {
                SelectedSection = Sections[0];
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
