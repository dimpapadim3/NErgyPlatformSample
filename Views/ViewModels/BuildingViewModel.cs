using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using NErgy.Building;
using NErgy.Building.ThermalZones;
using NErgy.Silverlight.Helpers;
using NErgy.Silverlight.Models;
using NErgy.Silverlight.Views.Controllers;
using NErgy.Silverlight.Web.Models;
using SimpleMvvmToolkit;
using BuildingCategory = NErgy.Silverlight.Web.Models.BuildingCategory;
using BuildingUsage = NErgy.Silverlight.Web.Models.BuildingUsage;
using HeatSource = NErgy.Silverlight.Web.Models.HeatSource;

namespace NErgy.Silverlight.Views.ViewModels
{
    public class HeatSourcesCollectionViewModel : DataBaseEntityCollectionViewModelBase<HeatSource>
    {
        

        public IList<HeatSource> Entities { get; set; }
        public Web.Models.Building Building { get; set; }

        private IDataAccesLayerServiceAgent DataAccesLayerServiceAgent = ThermalBuildingModel.Instance.DataAccesLayerService;

        public HeatSourcesCollectionViewModel(IList<HeatSource> entities)
        {
            Entities = entities; DataAccesLayerService = ThermalBuildingModel.Instance.DataAccesLayerService;
        }

        #region Overrides of DataBaseEntityCollectionViewModelBase<HeatSource>

        public override void AddToTableEntity(HeatSource entity)
        {
            DataAccesLayerServiceAgent.AddHeatSource(entity, Building);
            if (AddedEntityObjects.Contains(entity))
                AddedEntityObjects.Remove(entity);
        }

        public override void RemoveFromTableEntity(HeatSource entity)
        {
            DataAccesLayerServiceAgent.RemoveHeatSource(entity, Building);
        }

        public virtual List<HeatSource> GetCollection()
        {
            return new List<HeatSource>();
        }
        public override void GetCollectionAsync(Action<List<HeatSource>> completed)
        {
            if (SetIsBusy != null) SetIsBusy(true);

            DataAccesLayerService.GetbuildingHeatSources(Building, (systems, e) =>
            {
                if (SetIsBusy != null) SetIsBusy(false);
                completed(systems);
                NotifyPropertyChanged(vm => vm.EntityObjects);
            });
        }

        public virtual void SetCollection(IList<HeatSource> collection)
        {

        }

        #endregion
    }
    public class ElectricalHeatSystemsCollectionViewModel : DataBaseEntityCollectionViewModelBase<SystemElectricalHeat>
    {
        public IList<SystemElectricalHeat> Entities { get; set; }
        public Web.Models.Building Building { get; set; }


        public ElectricalHeatSystemsCollectionViewModel(IList<SystemElectricalHeat> entities)
        {
            Entities = entities;
            DataAccesLayerService = ThermalBuildingModel.Instance.DataAccesLayerService;
        }

        #region Overrides of DataBaseEntityCollectionViewModelBase<HeatSource>

        private IList<SystemElectricalHeat> TempEntities = new List<SystemElectricalHeat>();
        public override void AddToTableEntity(SystemElectricalHeat entity)
        {
            DataAccesLayerService.AddElectricalHeatSystem(entity, Building);

            // TempEntities.Add(entity);

            if (AddedEntityObjects.Contains(entity))
                AddedEntityObjects.Remove(entity);
        }


        protected override void DoDataAccessSaveChanges()
        {
            if (SetIsBusy != null) SetIsBusy(true);
            DataAccesLayerService.SaveChanges(e =>
            {
                if (SetIsBusy != null) SetIsBusy(false);
                if (e == null)
                {
                   
                }
            });
        }


        public override void RemoveFromTableEntity(SystemElectricalHeat entity)
        {
            DataAccesLayerService.RemoveElectricalHeatSystem(entity, Building);
        }

        public override void GetCollectionAsync(Action<List<SystemElectricalHeat>> completed)
        {
            if (SetIsBusy != null) SetIsBusy(true);
            DataAccesLayerService.GetbuildingElectricalHeatSystem(Building, (systems, e) =>
            {
                if (SetIsBusy != null) SetIsBusy(false);
                completed(systems);
                NotifyPropertyChanged(vm => vm.EntityObjects);
            });
        }



        public virtual void SetCollection(IList<HeatSource> collection)
        {

        }

        #endregion
    }
    public class PlumpingSystemsCollectionViewModel : DataBaseEntityCollectionViewModelBase<PlumpingSystem>
    {
        public IList<PlumpingSystem> Entities { get; set; }
        public Web.Models.Building Building { get; set; }


        public PlumpingSystemsCollectionViewModel(IList<PlumpingSystem> entities)
        {
            Entities = entities;
            DataAccesLayerService = ThermalBuildingModel.Instance.DataAccesLayerService;
        }

        #region Overrides of DataBaseEntityCollectionViewModelBase<HeatSource>

        // private IList<PlumpingSystem> TempEntities = new List<PlumpingSystem>();
        public override void AddToTableEntity(PlumpingSystem entity)
        {
            DataAccesLayerService.AddPlumpingSystem(entity, Building);

            // TempEntities.Add(entity);

            if (AddedEntityObjects.Contains(entity))
                AddedEntityObjects.Remove(entity);
        }


        protected override void DoDataAccessSaveChanges()
        {
            if (SetIsBusy != null) SetIsBusy(true);
            DataAccesLayerService.SaveChanges(e =>
            {
                if (SetIsBusy != null) SetIsBusy(false);
                if (e == null)
                {

                    Mediator.Instance.HandleException(e);
                }
            });
        }


        public override void RemoveFromTableEntity(PlumpingSystem entity)
        {
            DataAccesLayerService.RemovePlumpingSystem(entity, Building);
        }

        public override void GetCollectionAsync(Action<List<PlumpingSystem>> completed)
        {
            if (SetIsBusy != null) SetIsBusy(true);
            DataAccesLayerService.GetbuildingPlumpingSystem(Building, (systems, e) =>
            {
                if (SetIsBusy != null) SetIsBusy(false);
                completed(systems);
                NotifyPropertyChanged(vm => vm.EntityObjects);
            });
        }



        public virtual void SetCollection(IList<HeatSource> collection)
        {

        }

        #endregion
    }
    public class ElevatorSystemsCollectionViewModel : DataBaseEntityCollectionViewModelBase<Elavetor>
    {
        public IList<Elavetor> Entities { get; set; }
        public Web.Models.Building Building { get; set; }


        public ElevatorSystemsCollectionViewModel(IList<Elavetor> entities)
        {
            Entities = entities;
            DataAccesLayerService = ThermalBuildingModel.Instance.DataAccesLayerService;
        }

        #region Overrides of DataBaseEntityCollectionViewModelBase<HeatSource>

        // private IList<PlumpingSystem> TempEntities = new List<PlumpingSystem>();
        public override void AddToTableEntity(Elavetor entity)
        {
            DataAccesLayerService.AddElavetorSystem(entity, Building);

            // TempEntities.Add(entity);

            if (AddedEntityObjects.Contains(entity))
                AddedEntityObjects.Remove(entity);
        }


        protected override void DoDataAccessSaveChanges()
        {
            if (SetIsBusy != null) SetIsBusy(true);
            DataAccesLayerService.SaveChanges(e =>
            {
                if (SetIsBusy != null) SetIsBusy(false);
                if (e == null)
                {
                    //  TempEntities.ToList().ForEach(hs => DataAccesLayerService.AddElectricalHeatSystem_Building(hs, Building));

                    //   DataAccesLayerService.SaveChanges(e2 => { });

                }
            });
        }


        public override void RemoveFromTableEntity(Elavetor entity)
        {
            DataAccesLayerService.RemoveElavetorSystem(entity, Building);
        }

        public override void GetCollectionAsync(Action<List<Elavetor>> completed)
        {
            DataAccesLayerService.GetbuildingElavetorSystem(Building, (systems, e) =>
            {
                completed(systems);
                NotifyPropertyChanged(vm => vm.EntityObjects);
            });
        }



        public virtual void SetCollection(IList<HeatSource> collection)
        {

        }

        #endregion
    }
    public class WindGeneratorsSystemsCollectionViewModel : DataBaseEntityCollectionViewModelBase<WindGenerators>
    {
        public IList<WindGenerators> Entities { get; set; }
        public Web.Models.Building Building { get; set; }


        public WindGeneratorsSystemsCollectionViewModel(IList<WindGenerators> entities)
        {
            Entities = entities;
            DataAccesLayerService = ThermalBuildingModel.Instance.DataAccesLayerService;
        }

        #region Overrides of DataBaseEntityCollectionViewModelBase<HeatSource>

        // private IList<PlumpingSystem> TempEntities = new List<PlumpingSystem>();
        public override void AddToTableEntity(WindGenerators entity)
        {
            DataAccesLayerService.AddWindGeneratorsSystem(entity, Building);

            // TempEntities.Add(entity);

            if (AddedEntityObjects.Contains(entity))
                AddedEntityObjects.Remove(entity);
        }


        protected override void DoDataAccessSaveChanges()
        {
            if (SetIsBusy != null) SetIsBusy(true);
            DataAccesLayerService.SaveChanges(e =>
            {
                if (SetIsBusy != null) SetIsBusy(false);
                if (e == null)
                {
                    //  TempEntities.ToList().ForEach(hs => DataAccesLayerService.AddElectricalHeatSystem_Building(hs, Building));

                    //   DataAccesLayerService.SaveChanges(e2 => { });

                }
            });
        }


        public override void RemoveFromTableEntity(WindGenerators entity)
        {
            DataAccesLayerService.RemoveWindGeneratorsSystem(entity, Building);
        }

        public override void GetCollectionAsync(Action<List<WindGenerators>> completed)
        {
            if (SetIsBusy != null) SetIsBusy(true);
            DataAccesLayerService.GetbuildingWindGeneratorsSystem(Building, (systems, e) =>
            {
                if (SetIsBusy != null) SetIsBusy(false);
                completed(systems);
                NotifyPropertyChanged(vm => vm.EntityObjects);
            });
        }



        public virtual void SetCollection(IList<WindGenerators> collection)
        {

        }

        #endregion
    }
    public class PhotovoltaicTypeSystemsCollectionViewModel : DataBaseEntityCollectionViewModelBase<PhotovoltaicSystem>
    {
        public IList<PhotovoltaicSystem> Entities { get; set; }
        public Web.Models.Building Building { get; set; }


        public PhotovoltaicTypeSystemsCollectionViewModel(IList<PhotovoltaicSystem> entities)
        {
            Entities = entities;
            DataAccesLayerService = ThermalBuildingModel.Instance.DataAccesLayerService;
        }

        #region Overrides of DataBaseEntityCollectionViewModelBase<HeatSource>

        // private IList<PlumpingSystem> TempEntities = new List<PlumpingSystem>();
        public override void AddToTableEntity(PhotovoltaicSystem entity)
        {
            DataAccesLayerService.AddPhotovoltaicTypeSystem(entity, Building);

            // TempEntities.Add(entity);

            if (AddedEntityObjects.Contains(entity))
                AddedEntityObjects.Remove(entity);
        }


        protected override void DoDataAccessSaveChanges()
        {
            if (SetIsBusy != null) SetIsBusy(true);
            DataAccesLayerService.SaveChanges(e =>
            {
                if (SetIsBusy != null) SetIsBusy(false);
                if (e == null)
                {
                    //  TempEntities.ToList().ForEach(hs => DataAccesLayerService.AddElectricalHeatSystem_Building(hs, Building));

                    //   DataAccesLayerService.SaveChanges(e2 => { });

                }
            });
        }


        public override void RemoveFromTableEntity(PhotovoltaicSystem entity)
        {
            DataAccesLayerService.RemovePhotovoltaicTypeSystem(entity, Building);
        }

        public override void GetCollectionAsync(Action<List<PhotovoltaicSystem>> completed)
        {
            if (SetIsBusy != null) SetIsBusy(true);
            DataAccesLayerService.GetbuildingPhotovoltaicTypeSystem(Building, (systems, e) =>
            {
                if (SetIsBusy != null) SetIsBusy(false);
                completed(systems);
                NotifyPropertyChanged(vm => vm.EntityObjects);
            });
        }



        public virtual void SetCollection(IList<WindGenerators> collection)
        {

        }

        #endregion
    }


    public class BuildingViewModel : ViewModelMasterDetailsBase<BuildingViewModel, Web.Models.Building>
    {
        public DateTime StartingDispayDate { get; set; }

        public List<string> BuildingExposuresStrings = new List<string>   {
                                                           "Εκτεθειμένο",
                                                           "Ενδιάμεσο",
                                                           "Προστατευμένο"
                                                       };

        public List<string> ElevatorTypesStrings = new List<string> {
        "Μηχανικός ανελκυστήρας",
        "Υδραυλικός ανελκυστήρας",
        "Κυλιόμενες Σκάλες",
        "Κυλιόμενοι Διάδρομοι"
           };

        public List<string> HeatSourceTypesStrings = new List<string>
                                                       {
                                                           "Ηλεκτρική",
                                                           "Πετρέλαιο θέρμανσης",
                                                           "Πετρέλαιο κίνησης",
                                                           "Φυσικό αέριο",
                                                           "Υγραέριο",
                                                           "Βιομάζα",
                                                           "Τηλεθέρμανση"
                                                       };


        public static List<string> PlumpingSystemsTypesStrings = new List<string>
                                                       {
                                                           "Ύδρευση",
                                                           "Αποχέτευση",
                                                           "Άρδευση"
                                                       };


        public static List<string> ElectricHeatSystemsTypesStrings = new List<string>
                                                              {
                                                                	"Κυψέλες καυσίμου",
				"Μηχανή Stirling",
				"Μηχανή OTTO",
				"Μηχανή DIESEL",
				"Μικροτουρμπίνα",
				"Ατμοστρόβιλος Απομάστευσης",
				"Αεριοστρόβιλος με λέβητα ανάκτησης θερμότητας"
                                                              };


        public static List<string> ElectriclaHeatSystemFuelTypesStrings = new List<string>
                                                              {
                                                             
					"Υγραέριο (LPG)",
				"Φυσικό αέριο",
				"Ηλεκτρισμός",
				"Πετρέλαιο θέρμανσης",
				"Πετρέλαιο κίνησης",
				"Τηλεθέρμανση",
				"Βιομάζα"
                                                              };


        public static List<string> PhotovoltaicTypesStrings = new List<string>
                                                                       {
                                                                           "Μονοκρυσταλλικό",
                                                                           "Πολυκρυσταλλικό",
                                                                           "Λεπτού υμένα άμορφο a-S",
                                                                           "Λεπτού υμένα μικρομορφικό μ-Si.",
                                                                           "Λλεπτού υμένα CIS-CIGS",
                                                                           "Λεπτού υμένα CdTe",
                                                                           "Τριπλής επαφής (triple junction)"
                                                                       };

        public static List<string> WindgeneratorTypesStrings = new List<string>
                                                           {
                                                               "Αυτόνομο",
                                                               "Διασυνδεδεμένο"
                                                           };

        private readonly Project _project;

        private static List<EnumObject<BuildingExposure>> _buildingExposures;
        private static List<EnumObject<HeatSourceType>> _heatSourceTypes;
        private static List<EnumObject<NetworkType>> _systemPlumpingTypes;

        public HeatSourcesCollectionViewModel HeatSourcesCollectionViewModel { get; set; }
        public ElectricalHeatSystemsCollectionViewModel ElectricalHeatSystemsCollectionViewModel { get; set; }
        public PlumpingSystemsCollectionViewModel PlumpingSystemsCollectionViewModel { get; set; }
        public ElevatorSystemsCollectionViewModel ElevatorSystemsCollectionViewModel { get; set; }
        public WindGeneratorsSystemsCollectionViewModel WindGeneratorsSystemsCollectionViewModel { get; set; }
        public PhotovoltaicTypeSystemsCollectionViewModel PhotovoltaicTypeSystemsCollectionViewModel { get; set; }


        [Obsolete("", true)]
        public BuildingViewModel(Project project)
        {
            _project = project;

        }

        public BuildingViewModel(Web.Models.Building building)
        {

            StartingDispayDate = DateTime.Now.Date;

            if (building.NumberOfSunnyArea == null) building.NumberOfSunnyArea = 0;
            if (building.NumberOfZones == null) building.NumberOfZones = 0;
            if (building.NumberOfHeatedAreas == null) building.NumberOfHeatedAreas = 0;

            DataAccesLayerService = ThermalBuildingModel.Instance.DataAccesLayerService;
            BuildingExposures = new List<EnumObject<BuildingExposure>>(GetEnumListWithArraySupport<BuildingExposure>(BuildingExposuresStrings));
            HeatSourceTypes = new List<EnumObject<HeatSourceType>>(GetEnumListWithArraySupport<HeatSourceType>(HeatSourceTypesStrings));

            SystemPlumpingTypes = new List<EnumObject<NetworkType>>(GetEnumListWithArraySupport<NetworkType>(PlumpingSystemsTypesStrings));
            PhotovoltaicTypes = new List<EnumObject<PhotovoltaicType>>(GetEnumListFromArray<PhotovoltaicType>(PhotovoltaicTypesStrings));
            WindgeneratorTypes = new List<EnumObject<WindgeneratorType>>(GetEnumListFromArray<WindgeneratorType>(WindgeneratorTypesStrings));

            ElectriclaHeatSystemUnitTypes = new List<EnumObject<ElectriclaHeatSystemUnitTypeId>>(GetEnumListFromArray<ElectriclaHeatSystemUnitTypeId>(ElectricHeatSystemsTypesStrings));
            ElectriclaHeatSystemFuelTypes = new List<EnumObject<ElectriclaHeatSystemFuelTypeId>>(GetEnumListFromArray<ElectriclaHeatSystemFuelTypeId>(ElectriclaHeatSystemFuelTypesStrings));
            ElevatorTypes = new List<EnumObject<object>>(GetEnumListFromArray<object>(ElevatorTypesStrings));


            Model = building;
            LoadCategories();
            if (Model.BuildingCategory != null)
            {
                LoadBuildingUsages();
            }

            HeatSourcesCollectionViewModel = new HeatSourcesCollectionViewModel(new List<HeatSource>()) { Building = Model, SetIsBusy = (b) => IsBusy = b };

            ElectricalHeatSystemsCollectionViewModel = new ElectricalHeatSystemsCollectionViewModel(new List<SystemElectricalHeat>()) { Building = Model, SetIsBusy = (b) => IsBusy = b };
            PlumpingSystemsCollectionViewModel = new PlumpingSystemsCollectionViewModel(new List<PlumpingSystem>()) { Building = Model, SetIsBusy = (b) => IsBusy = b };
            ElevatorSystemsCollectionViewModel = new ElevatorSystemsCollectionViewModel(new List<Elavetor>()) { Building = Model, SetIsBusy = (b) => IsBusy = b };

            WindGeneratorsSystemsCollectionViewModel = new WindGeneratorsSystemsCollectionViewModel(new List<WindGenerators>()) { Building = Model, SetIsBusy = (b) => IsBusy = b };
            PhotovoltaicTypeSystemsCollectionViewModel = new PhotovoltaicTypeSystemsCollectionViewModel(new List<PhotovoltaicSystem>()) { Building = Model, SetIsBusy = (b) => IsBusy = b };
            Model.PropertyChanged += BuildingViewModel_PropertyChanged;

            HeatSourcesCollectionViewModel.PropertyChanged += HeatSourcesCollectionViewModel_PropertyChanged;
        }

        private void HeatSourcesCollectionViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            //NotifyPropertyChanged(vm=>vm.HeatSourcesCollectionViewModel);
        }

        private void BuildingViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            //if (new List<string>() { 
            //    "IsShthEnabled", 
            //    "IsPhotovoltaicEnabled" ,"IsWindGenaratorsEnabled"}
            //    .Contains(e.PropertyName)) return;


            //if (IsEnabledBuildingEndEdit) return;

            //   this.IsEnabledBuildingEndEdit = true;
            //  BeginEdit();
            //  OnBeginEdit();

        }

        public static List<EnumObject<ElectriclaHeatSystemFuelTypeId>> ElectriclaHeatSystemFuelTypes { get; set; }
        public static List<EnumObject<ElectriclaHeatSystemUnitTypeId>> ElectriclaHeatSystemUnitTypes { get; set; }
        public static List<EnumObject<object>> ElevatorTypes { get; set; }


        public static List<EnumObject<HeatSourceType>> HeatSourceTypes
        {
            get { return _heatSourceTypes; }
            set
            {
                _heatSourceTypes = value;
                //NotifyPropertyChanged(m => m.HeatSourceTypes);
            }
        }

        public List<EnumObject<BuildingExposure>> BuildingExposures
        {
            get { return _buildingExposures; }
            set
            {
                _buildingExposures = value;
                NotifyPropertyChanged(m => m.BuildingExposures);
            }
        }

        public static List<EnumObject<NetworkType>> SystemPlumpingTypes
        {
            get { return _systemPlumpingTypes; }
            set
            {
                _systemPlumpingTypes = value;
                //  NotifyPropertyChanged(m => m.SystemPlumpingTypes);
            }
        }

        private static List<EnumObject<PhotovoltaicType>> _photovoltaicTypes;
        public static List<EnumObject<PhotovoltaicType>> PhotovoltaicTypes
        {
            get { return _photovoltaicTypes; }
            set
            {
                _photovoltaicTypes = value;

            }
        }

        private static List<EnumObject<WindgeneratorType>> _windgeneratorType;
        private bool _IsPhotovoltaicEnabled;
        private bool _IsWindGenaratorsEnabled;

        public static List<EnumObject<WindgeneratorType>> WindgeneratorTypes
        {
            get { return _windgeneratorType; }
            set
            {
                _windgeneratorType = value;

            }
        }

        public BuildingViewModel()
        {
        }

        #region Properties

        public ICommand EndEditBuildingCommand
        {
            get
            {
                return new DelegateCommand(() =>
                                               {
                                                   //if (IsBusy) return;
                                                   ////EndEdit();
                                                   //// OnEndEdit();
                                                   //IsBusy = true;
                                                   //DataAccesLayerService.SaveChanges(e =>
                                                   //                                      {
                                                   //                                          IsBusy = false;
                                                   //                                          IsEnabledBuildingEndEdit = false;
                                                   //                                      });

                                               });
            }
        }


        public ICommand CancelBuildingCommand
        {
            get
            {
                return new DelegateCommand(() =>
                                               {
                                                   //Model = this.Copy;
                                                   //IsEnabledBuildingEndEdit = false;
                                                   //CancelEdit();
                                               });
            }
        }

        private bool _IsEnabledBuildingEndEdit;

        public bool IsEnabledBuildingEndEdit
        {
            get
            {

                return _IsEnabledBuildingEndEdit;
            }
            set
            {
                _IsEnabledBuildingEndEdit = value;
                NotifyPropertyChanged(vm => vm.IsEnabledBuildingEndEdit);
                Mediator.Instance.SaveBuildingData();
                DataAccesLayerService.SaveChanges(e =>{});
            }
        }

        //TODO:

        //   private bool _IsShthEnabled;

        public bool IsExistsElectricalSystems
        {
            get
            {
                if (IsEnergyStudy) return true;
                return Model.IsExistsElectricalSystems.HasValue ? Model.IsExistsElectricalSystems.Value : false;
            }
            set
            {
                Model.IsExistsElectricalSystems = value;
                NotifyPropertyChanged(vm => vm.IsExistsElectricalSystems);
                Mediator.Instance.SaveBuildingData();
                DataAccesLayerService.SaveChanges(e => { });
            }
        }

        public bool IsPhotovoltaicEnabled
        {
            get
            {
                if (IsEnergyStudy) return true;
                return Model.IsExistsPhotovoltaicsSystems.HasValue ? Model.IsExistsPhotovoltaicsSystems.Value : false;
            }
            set
            {
                Model.IsExistsPhotovoltaicsSystems = value;
                // if (value == false) HeatingIsSelected = true;
                NotifyPropertyChanged(vm => vm.IsPhotovoltaicEnabled);
                Mediator.Instance.SaveBuildingData();
                DataAccesLayerService.SaveChanges(e => { });
            }
        }

        public bool IsWindGenaratorsEnabled
        {
            get
            {
                if (IsEnergyStudy) return false;
                return Model.IsExistsWindGeneratorsSystems.HasValue ? Model.IsExistsWindGeneratorsSystems.Value : false;
            }
            set
            {
                Model.IsExistsWindGeneratorsSystems = value;
                //if (value == false) HeatingIsSelected = true;
                NotifyPropertyChanged(vm => vm.IsWindGenaratorsEnabled);
                Mediator.Instance.SaveBuildingData();
                DataAccesLayerService.SaveChanges(e => { });
            }
        }


        private ObservableCollection<BuildingCategory> _buildingCategories;
        private ObservableCollection<BuildingUsage> _buildingUsages;
        private NeighborBuilding _selectedNeigbourBuilding;

        public BuildingCategory SelectedCategory
        {
            get { return Model.BuildingCategory; }
            set
            {
                Model.BuildingCategory = value;
                NotifyPropertyChanged(vm => vm.SelectedCategory);
            }
        }

        //TODO:
        public int? BuildingExposure
        {
            get { return Model.buildingExposureID; }
            set
            {
                Model.buildingExposureID = value;
                NotifyPropertyChanged(vm => vm.BuildingExposure);
            }
        }


        public BuildingUsage SelectedUsage
        {
            get { return Model.BuildingUsage; }
            set
            {
                Model.BuildingUsage = value;
                NotifyPropertyChanged(vm => vm.SelectedUsage);
            }
        }

        public string Description
        {
            get { return Model.Description; }
            set
            {
                Model.Description = value;
                NotifyPropertyChanged(vm => vm.Description);
            }
        }

        public float? TotalArea
        {
            get { return Model.TotalArea; }
            set
            {
                Model.TotalArea = value;
                NotifyPropertyChanged(vm => vm.TotalArea);
            }
        }

        public float? TotalVolume
        {
            get { return Model.TotalVolume; }
            set
            {
                Model.TotalVolume = value;
                NotifyPropertyChanged(vm => vm.TotalVolume);
            }
        }

        public float? HeatedVolume
        {
            get { return Model.HeatedVolume; }
            set
            {
                Model.HeatedVolume = value;
                NotifyPropertyChanged(vm => vm.HeatedVolume);
            }
        }

        //TODO:COmplete this
        public float? HeatedArea
        {
            get { return Model.HeatedArea; }
            set
            {
                Model.HeatedArea = value;
                NotifyPropertyChanged(vm => vm.HeatedArea);
            }
        }


        public float? CoolingVolume
        {
            get { return Model.CoolingVolume; }
            set
            {
                Model.CoolingVolume = value;
                NotifyPropertyChanged(vm => vm.CoolingVolume);
            }
        }

        public float? CoolingSerfuce
        {
            get { return Model.CoolingSerfuce; }
            set
            {
                Model.CoolingSerfuce = value;
                NotifyPropertyChanged(vm => vm.CoolingSerfuce);
            }
        }

        public int? NumberOfHeatedAreas
        {
            get { return Model.NumberOfHeatedAreas; }
            set
            {
                Model.NumberOfHeatedAreas = value;
                NotifyPropertyChanged(vm => vm.NumberOfHeatedAreas);
            }
        }

        public int? NumberOfSunnyArea
        {
            get { return Model.NumberOfSunnyArea; }
            set
            {
                Model.NumberOfSunnyArea = value;
                NotifyPropertyChanged(vm => vm.NumberOfSunnyArea);
            }
        }

        public int? NumberOfZones
        {
            get { return Model.NumberOfZones; }
            set
            {
                Model.NumberOfZones = value;
                NotifyPropertyChanged(vm => vm.NumberOfZones);
            }
        }

        public int? NumberOfStories
        {
            get { return Model.NumberOfStories; }
            set
            {
                Model.NumberOfStories = value;
                NotifyPropertyChanged(vm => vm.NumberOfStories);
            }
        }


        public bool? IsVeritcalElementsProtected
        {
            get { return Model.IsVeritcalElementsProtected; }
            set
            {
                Model.IsVeritcalElementsProtected = value;
                NotifyPropertyChanged(vm => vm.IsVeritcalElementsProtected);
            }
        }

        public bool? ConditioInternalAir
        {
            get { return Model.ConditioInternalAir; }
            set
            {
                Model.ConditioInternalAir = value;
                NotifyPropertyChanged(vm => vm.ConditioInternalAir);
            }
        }

        public bool? ConditionsAcousticComfort
        {
            get { return Model.ConditionsAcousticComfort; }
            set
            {
                Model.ConditionsAcousticComfort = value;
                NotifyPropertyChanged(vm => vm.ConditionsAcousticComfort);
            }
        }

        public bool? ConditionsOpticComfort
        {
            get { return Model.ConditionsOpticComfort; }
            set
            {
                Model.ConditionsOpticComfort = value;
                NotifyPropertyChanged(vm => vm.ConditionsOpticComfort);
            }
        }

        public bool? ConditionsHeatComfort
        {
            get { return Model.ConditionsHeatComfort; }
            set
            {
                Model.ConditionsHeatComfort = value;
                NotifyPropertyChanged(vm => vm.ConditionsHeatComfort);
            }
        }

        public NeighborBuilding SelectedNeigbourBuilding
        {
            get { return _selectedNeigbourBuilding; }
            set
            {
                _selectedNeigbourBuilding = value;
                NotifyPropertyChanged(vm => vm.SelectedNeigbourBuilding);
            }
        }

        public ObservableCollection<BuildingCategory> BuildingCategories
        {
            get { return _buildingCategories; }
            set
            {
                _buildingCategories = value;
                NotifyPropertyChanged(m => m.BuildingCategories);
            }
        }

        public ObservableCollection<BuildingUsage> BuildingUsages
        {
            get { return _buildingUsages; }
            set
            {
                _buildingUsages = value;
                NotifyPropertyChanged(m => m.BuildingUsages);
            }
        }

        public Double FirstStoryHeight
        {
            get { return Model.FirstStoryHeight; }
            set
            {
                Model.FirstStoryHeight = value;
                NotifyPropertyChanged(vm => vm.FirstStoryHeight);
            }
        }

        public KlimaticZone KlimaticZone
        {
            get { return Model.KlimaticZone; }
            set
            {
                Model.KlimaticZone = value;
                NotifyPropertyChanged(vm => vm.KlimaticZone);
            }
        }

        public ObservableCollection<ThermalZone> ThermalZones
        {
            get { throw new NotImplementedException(); }
        }

        public Double TypicalStoryHeight
        {
            get { return Model.TypicalStoryHeight; }
            set
            {
                Model.TypicalStoryHeight = value;
                NotifyPropertyChanged(vm => vm.TypicalStoryHeight);
            }
        }


        #endregion //Properties

        #region Notifications

        // Events to notify the view or obtain data from the view
        public event EventHandler<NotificationEventArgs<Exception>> ErrorNotice;
        public event EventHandler<NotificationEventArgs<object, bool>> SaveChangesNotice;
        public event EventHandler<NotificationEventArgs> ItemsSavedNotice;

        #endregion

        #region Helpers

        // Helper method to notify View of an error

        private bool canAdd;
        private bool canEdit;
        private bool canLoad = true;
        private bool canRemove;
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

        private void NotifyError(string message, Exception error)
        {
            // Notify view of an error
            Notify(ErrorNotice, new NotificationEventArgs<Exception>(message, error));
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

        public void LoadCategories()
        {
            // Reset property
            BuildingCategories = null;

            // Flip busy flag
            IsBusy = true;
            // Load items
            DataAccesLayerService.GetCategories
                (
                    CategoriesLoaded
                );
        }

        public void LoadBuildingUsages()
        {
            // Load items
            DataAccesLayerService.GetBuildingUsagesForCategory
                (
                    SelectedCategory.CategoryID,
                    UsagesLoaded
                );

            // Reset property
            //BuildingUsages = null;

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

        // Call RejectChanged on the service agent then reload items
        public void RejectChanges()
        {
            DataAccesLayerService.RejectChanges();
            LoadBuildingUsages();
        }

        #endregion

        #region Completion Callbacks

        // Add callback methods for async calls to the service agent

        private void CategoriesLoaded(IList<BuildingCategory> entities, Exception error)
        {
            // If no error is returned, set the model to entities
            if (error == null)
            {
                BuildingCategories = new ObservableCollection<BuildingCategory>(entities);
            }
            // Otherwise notify view that there's an error
            else
            {
                NotifyError("Unable to retrieve items", error);
            }

            // Set SelectedItem to the first item
            if (BuildingCategories.Count > 0)
            {
                //SelectedCategory = BuildingCategories[0];
                NotifyPropertyChanged(m => m.SelectedCategory);
            }

            // We're done
            IsBusy = false;
        }

        private void UsagesLoaded(List<BuildingUsage> entities, Exception error)
        {
            // If no error is returned, set the model to entities
            if (error == null)
            {
                BuildingUsages = new ObservableCollection<BuildingUsage>(entities);
            }
            // Otherwise notify view that there's an error
            else
            {
                NotifyError("Unable to retrieve items", error);
            }

            // Set SelectedItem to the first item
            if (BuildingUsages.Count > 0)
            {
                //SelectedUsage = BuildingUsages[0];
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


        protected override void OnEndEdit()
        {
            DataAccesLayerService.SaveChanges(e =>
                                                  {
                                                      if (e != null)
                                                      {
                                                      }
                                                  });
            base.OnEndEdit();
        }

        #endregion

        private IDataAccesLayerServiceAgent DataAccesLayerService { get; set; }

        private static void HeatSourcesCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
        }
    }
}