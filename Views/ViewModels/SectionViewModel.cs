using System;
using NErgy.Building;
using SimpleMvvmToolkit;
using NErgy.Silverlight.Web.Models;

namespace NErgy.Silverlight.Views.ViewModels
{
    public class SectionViewModel : ViewModelDetailBase<SectionViewModel,Section>
    {
        public SectionViewModel(IThermalSection _ThermalSection)
        {
            ThermalSection = _ThermalSection;
            base.Model = _thermalSection as Section;
           // AssociateProperties(m=>m.D, vm=>vm.D);
 
            // this.MaterialsViewModel = new MaterialsViewModel(_ThermalSection.Material);
        }

        #region Properties

        private MaterialsViewModel _materialsViewModel;
        private IThermalSection _thermalSection;

        public IThermalSection ThermalSection
        {
            get { return _thermalSection; }
            set
            {
                _thermalSection = value;
               // MaterialsViewModel = new MaterialsViewModel(value.ThermalMaterial);
                NotifyPropertyChanged(vm=>vm.ThermalSection);
            }
        }

        public MaterialsViewModel MaterialsViewModel
        {
            get { return _materialsViewModel; }
            set
            {
                _materialsViewModel = value;
                NotifyPropertyChanged(vm => vm.MaterialsViewModel);
            }
        }

        public Double? U
        {
            get { return _thermalSection.U; }
            set
            {
                _thermalSection.U = value;
                NotifyPropertyChanged(vm => vm.U);
            }
        }

        public string Name
        {
            get { return Model.Name; }
            set
            {
                Model.Name = value;
                NotifyPropertyChanged(vm => vm.U);
            }
        }


        //public IEquatable<ThermalSection> Layers
        //{
        //    get { return this ; }
        //    set
        //    {
        //        _ThermalSection.D = value;
        //        RaisePropertyChanged("D");
        //    }
        //}

        #endregion //Properties

        #region Notifications

        // Events to notify the view or obtain data from the view
        public event EventHandler<NotificationEventArgs<Exception>> ErrorNotice;
        public event EventHandler<NotificationEventArgs<object, bool>> SaveChangesNotice;
        public event EventHandler<NotificationEventArgs> ItemsSavedNotice;

        #endregion
 
        #region Helpers


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
            CanEdit = !IsBusy  ;
            CanRemove = !IsBusy ;
        }

        #endregion
    }
}