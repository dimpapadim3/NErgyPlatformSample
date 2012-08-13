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
using NErgy.Silverlight.Web.Models;

namespace NErgy.Silverlight.Views.ViewModels
{
    public class LibraryViewModel : ViewModelBase<LibraryViewModel>
    {

        public Library Library { get; set; }

        public LibraryViewModel(Library library)
        {
            this.Library = library;
            this.LoadLibraryTypes();
        }

        public LibraryViewModel( )
        {
            this.Library = new Library();
            this.LoadLibraryTypes();
        }

        public  string Name
        {
            get { return Library.Name; }
            set
            {
                Library.Name = value;
                NotifyPropertyChanged(vm => vm.Name);
            }
        }

        public int LibraryTypeID
        {
            get { return Library.LibraryTypeID; }
            set
            {
                Library.LibraryTypeID = value;
                NotifyPropertyChanged(vm => vm.LibraryTypeID);
            }
        }

        public string Description
        {
            get { return Library.Description; }
            set
            {
                Library.Description = value;
                NotifyPropertyChanged(vm => vm.Description);
            }
        }



        private  LibraryType  selectedLibraryType;
        public LibraryType SelectedLibraryType
        {
            get { return selectedLibraryType; }
            set
            {
                selectedLibraryType = value;
                NotifyPropertyChanged(m => m.SelectedLibraryType);
            }
        }

        private ObservableCollection<LibraryType> libraryTypes;
        public ObservableCollection<LibraryType> LibraryTypes
        {
            get { return libraryTypes; }
            set
            {
                libraryTypes = value;
                NotifyPropertyChanged(m => m.LibraryTypes);
            }
        }


        public void LoadLibraryTypes()
        {
            // Load items
            DataAccesLayerService.GetBuildingLibraryTypes
                (
                    
                    (entities, error) => LibraryTypesLoaded(entities, error)
                );

            // Reset property
            //BuildingUsages = null;

            // Flip busy flag
            IsBusy = true;
        }

        private void LibraryTypesLoaded(IList<LibraryType> entities, Exception error)
        {
            // If no error is returned, set the model to entities
            if (error == null)
            {
                LibraryTypes = new ObservableCollection<LibraryType>(entities);
            }
            // Otherwise notify view that there's an error
            else
            {
               // NotifyError("Unable to retrieve items", error);
            }

            // Set SelectedItem to the first item
            if (libraryTypes.Count > 0)
            {
                SelectedLibraryType = libraryTypes[0];
            }

            // We're done
            IsBusy = false;
        }


        private bool isBusy;
        public bool IsBusy
        {
            get { return isBusy; }
            set
            {
                isBusy = value;
                //SetCanProperties();
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

    }
}
