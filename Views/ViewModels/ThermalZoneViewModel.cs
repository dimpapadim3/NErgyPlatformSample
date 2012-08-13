using System;
using System.Collections.Generic;
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
using NErgy.Silverlight.Helpers;
using NErgy.Silverlight.Models;
using NErgy.Silverlight.Views.Controllers;

namespace NErgy.Silverlight.Views.ViewModels
{
    
    public class ThermalZoneViewModel : ViewModelMasterDetailsBase<ThermalZoneViewModel, ThermalZone>
    {
        public IDataAccesLayerServiceAgent DataAccesLayerServiceAgent
        {
            get;
            set;
        }


        public ThermalZoneViewModel(ThermalZone thermalZone)
        {
            DataAccesLayerServiceAgent = ThermalBuildingModel.Instance.DataAccesLayerService;

            KnownTypes = new List<Type>() {
                typeof( Models.StructuralElements.TransparentElement),
                typeof( Models.StructuralElements.TranslusentElement)
            };
            _thermalZone = thermalZone;
            Model = thermalZone;
            AssociateProperties(m=>m.NumberOfAirOpenings,vm=>vm.NumberOfAirOpenings);
            AssociateProperties(m => m.NumberOfCelingFans, vm => vm.NumberOfCelingFans);
            AssociateProperties(m => m.NumberOfChimnies, vm => vm.NumberOfChimnies);
            AssociateProperties(m => m.ThermalZoneUsageTypeId, vm => vm.ThermalZoneUsageTypeId);
            AssociateProperties(m => m.ThermalCapacity, vm => vm.ThermalCapacity);
            AssociateProperties(m => m.TotalArea, vm => vm.TotalArea);

            ThermalZoneControlType = new List<EnumObject<ThermalZoneControlType>>(GetEnumList<ThermalZoneControlType>());
            ThermalCapacityCoefficients = ThermalCapacityCoefficient.ThermalCapacityCoefficients.ToList();

        }
 
        //TODO developers please add your constructors in the below constructor region.
        //     be sure to include an overloaded constructor that takes a model type.

        #region Declarations

        ThermalZone _thermalZone;
        private List<EnumObject<ThermalZoneControlType>> _ThermalZoneControlType;
        

        #endregion //Declarations

      

        public List<EnumObject<ThermalZoneControlType>> ThermalZoneControlType
        {
            get { return _ThermalZoneControlType; }
            set { _ThermalZoneControlType = value; }
        }

        public static List<ThermalCapacityCoefficient> ThermalCapacityCoefficients { get; set; }


        public ThermalZone ThermalZone
        {
            get { return _thermalZone; }
            set
            {
                _thermalZone = value;
                NotifyPropertyChanged(m => m.ThermalZone);
            }
        }

        public Double AirPenetrationFromOpenings
        {
            get { return Model.AirPenetrationFromOpenings; }
            set
            {
                Model.AirPenetrationFromOpenings = value;
                NotifyPropertyChanged(m => m.AirPenetrationFromOpenings);
            }
        }

        public Int32 ControlTypeId
        {
            get { return Model.ControlTypeId; }
            set
            {
                Model.ControlTypeId = value;
                NotifyPropertyChanged(m => m.ControlTypeId);
            }
        }


        public Int32 NumberOfAirOpenings
        {
            get { return Model.NumberOfAirOpenings; }
            set
            {
                Model.NumberOfAirOpenings = value;
                NotifyPropertyChanged(m => m.NumberOfAirOpenings);
            }
        }

        public Int32 NumberOfCelingFans
        {
            get { return Model.NumberOfCelingFans; }
            set
            {
                Model.NumberOfCelingFans = value;
                NotifyPropertyChanged(m => m.NumberOfCelingFans);
            }
        }

        public Int32 NumberOfChimnies
        {
            get { return Model.NumberOfChimnies; }
            set
            {
                Model.NumberOfChimnies = value;
                NotifyPropertyChanged(m => m.NumberOfChimnies);
            }
        }

        public Shell Shell
        {
            get { return Model.Shell; }
            set
            {
                Model.Shell = value; NotifyPropertyChanged(m => m.Shell);
            }
        }



        public int? ThermalCapacity
        {
            get
            {
                return (int)Model.ThermalCapacity;
            }
            set
            {
                if (value .HasValue) Model.ThermalCapacity = value.Value;
                NotifyPropertyChanged(m => m.ThermalCapacity);
            }
        }

        public int? ThermalZoneUsageTypeId
        {
            get { return Model.ThermalZoneUsageTypeId; }
            set
            {
                Model.ThermalZoneUsageTypeId =  value.HasValue?value.Value:0;
                NotifyPropertyChanged(m => m.ThermalZoneUsageTypeId);
            }
        }

        public Double TotalArea
        {
            get { return Model.TotalArea; }
            set
            {
                Model.TotalArea = value;
                NotifyPropertyChanged(m => m.TotalArea);
            }
        }

        public Double TotalEnergyLoss
        {
            get { return Model.TotalEnergyLoss; }
        }

        public Double TotalVolum
        {
            get { return Model.TotalVolum; }
            set
            {
                Model.TotalVolum = value;
                NotifyPropertyChanged(m => m.TotalVolum);
            }
        }

        private bool isBusy;
        public bool IsBusy
        {
            get { return isBusy; }
            set
            {
                isBusy = value;
                NotifyPropertyChanged(m => m.IsBusy);
            }
        }

        protected override void OnEndEdit()
        {
            Mediator.Instance.SaveBuildingData();
            IsBusy = true;

            this.DataAccesLayerServiceAgent.SaveChanges(e =>
            {
                if (e != null)
                { 
                }
                IsBusy = false;
                Logger.Instance.Log("SaveEntityObjectsChanges ThermalZone"  );
            });
            base.OnEndEdit();
        }


    }
}
