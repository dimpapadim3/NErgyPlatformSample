using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Controls;
using NErgy.Building;
using NErgy.Silverlight.Web.Models;
using SimpleMvvmToolkit;

namespace NErgy.Silverlight.Views.ViewModels
{
    public class MaterialsViewModel : ViewModelDetailBase<MaterialsViewModel,Material>
    {

        public event EventHandler<NotificationEventArgs<Material>> MaterialChanged;

        private IThermalMaterial _thermalMaterial;
  
        protected MaterialsViewModel()
        {
           
        }
 
        public void OnMaterialSelectionChanged(object sender,  NotificationEventArgs<Material> e)
        {
            ThermalMaterial = e.Data;
        }

        public MaterialsViewModel(IThermalMaterial iThermalMaterial):this()
        {
            _thermalMaterial = iThermalMaterial;
            base.Model = iThermalMaterial as Material;
            LibrariesListViewModel = new LibrariesListViewModel();
            LibrariesListViewModel.MaterialChanged += OnMaterialSelectionChanged;
            AssociateProperties(m => m.TheramlCapacityCoef, vm => vm.TheramlCapacityCoef);
            AssociateProperties(m => m.TheramlConductivityCoef, vm => vm.TheramlConductivityCoef);
        }

        public IThermalMaterial ThermalMaterial
        {
            get { return _thermalMaterial; }
            set
            {
                _thermalMaterial = value;
               NotifyPropertyChanged(vm=>vm.ThermalMaterial);
               NotifyPropertyChanged(vm=>vm.TheramlCapacityCoef);
               NotifyPropertyChanged(vm => vm.TheramlConductivityCoef);
            }
        }


        public Double TheramlCapacityCoef
        {
            get { return ThermalMaterial.TheramlCapacityCoef.Value; }
            set
            {
                ThermalMaterial.TheramlCapacityCoef = value;
                NotifyPropertyChanged(vm => vm.TheramlCapacityCoef);
            }
        }
 
        public Double TheramlConductivityCoef
        {
            get { return ThermalMaterial.TheramlConductivityCoef.Value; }
            set
            {
                ThermalMaterial.TheramlConductivityCoef = value;
                NotifyPropertyChanged(vm => vm.TheramlConductivityCoef);
            }
        }
 
        private IThermalMaterial _selectedMaterial;
        public IThermalMaterial SelectedMaterial
        {
            get { return _selectedMaterial; }
            set
            {
                _selectedMaterial = value;
                NotifyPropertyChanged(vm => vm.SelectedMaterial);
            }
        }


        #region Libraries

        public LibrariesListViewModel LibrariesListViewModel { get; set; }

        #endregion
    }
}