using System;
using System.Collections.Generic;
using NErgy.Building;
using NErgy.Building.Systems;
using NErgy.Silverlight.Models;
using NErgy.Silverlight.Views.Controllers;

namespace NErgy.Silverlight.Views.ViewModels
{
    public class SystemsViewModel : ViewModelMasterDetailsBase<SystemsViewModel, ThermalSystem>
    {
        #region strings 

        public static List<string> ProductionTypesStrings = new List<string>  {
				"Λέβητας",
				"Κεντρική υδρόψυκτη Α.Θ.",
				"Κεντρική αερόψυκτη Α.Θ.",
				"Τοπική αερόψυκτη Α.Θ.",
				"Γεωθερμική Α.Θ. με οριζόντιο εναλλάκτη",
				"Γεωθερμική Α.Θ. με κατακόρυφο εναλλάκτη",
				"Κεντρική άλλου τύπου Α.Θ.",
				"Τοπικές ηλεκτρικές μονάδες (καλοριφέρ ή θερμοπομποί ή άλλο)",
				"Τοπικές μονάδες αερίου (σόμπες υγραερίου)",
				"Ανοικτές εστίες καύσης",
				"Τηλεθέρμανση",
				"ΣΗΘ",
				"Μονάδα παραγωγής άλλου τύπου"
			};


        public static List<string> CoolingProductionTypesStrings = new List<string>
                                                                {
                                                                    "Αερόψυκτος ψύκτης",
                                                                    "Υδρόψυκτος ψύκτης",
                                                                    "Υδρόψυκτη Α.Θ.",
                                                                    "Αερόψυκτη Α.Θ.",
                                                                    "Γεωθερμική Α.Θ. με οριζόντιο εναλλάκτη",
                                                                    "Γεωθερμική Α.Θ. με κατακόρυφο εναλλάκτη",
                                                                    "Προσρόφησης απορρόφησης Α.Θ.",
                                                                    "Κεντρική άλλου τύπου Α.Θ.",
                                                                    "Μονάδα παραγωγής άλλου τύπου"
                                                                };

        public static List<string> ZnxProductionTypesStrings = new List<string>
                                                           
        {
				"Λέβητας",
				"Τηλεθέρμανση",
				"ΣΗΘ",
				"Αντλία Θερμότητας (Α.Θ.)",
				"Τοπικός ηλεκτρικός θερμαντήρας",
				"Τοπική μονάδα φυσικού αερίου",
				"Μονάδα παραγωγής (κεντρική) άλλου τύπου"
			};


        public static List<string> YgransiProductionTypesStrings = new List<string>

                                                                   {
                                                                       "Ατμολέβητας κεντρικής παροχής",
                                                                       "Τοπική μονάδα ψεκασμού",
                                                                       "Τοπική μονάδα παραγωγής ατμού",
                                                                       "Τοπική μονάδα άλλου τύπου"
                                                                   };



        public static List<string> SolarSystemsTypes = new List<string>  {
				"Χωρίς κάλυμμα",
				"Απλός επίπεδος",
				"Επιλεκτικός επίπεδος",
				"Κενού",
				"Συγκεντρωτικός"
			};

        public static List<string> EnergySourceTypesStrings = new List<string>  {
				"Υγραέριο (LPG)",
				"Φυσικό αέριο",
				"Ηλεκτρισμός",
				"Πετρέλαιο θέρμανσης",
				"Πετρέλαιο κίνησης",
				"Τηλεθέρμανση",
				"Βιομάζα"
			};

        public static List<string> TransmitionTypesStrings = new List<string>
                                                                  {

                                                                      "Εσωτερικοί  ή έως και 20% σε εξωτερικούς",
                                                                      "Πάνω απο  20% σε εξωτερικούς"
                                                                  };
        public static List<string> HelpingUnitsTypesStrings = new List<string>
                                                                  {
	
				"Αντλίες",
				"Κυκλοφορητές",
				"Ηλεκτροβάνες",
				"Ανεμιστήρες"
			};

        public static List<string> LightingSystemMotionDetectionStrings = new List<string>
                                                               {
                                                                   "1. Χειροκίνητος διακόπτης (αφής/σβέσης)",
                                                                   "2. Χειροκίνητος διακόπτης (αφής/σβέσης) και αισθητήρας παρουσίας",
                                                                   "3. Ανίχνευση με αυτόματη έναυση / ρύθμιση φωτεινής ροής (dimming)",
                                                                   "4. Ανίχνευση με αυτόματο έναυση και σβέση",
                                                                   "5. Ανίχνευση με χειροκίνητη έναυση / ρύθμιση φωτεινής ροής (dimming)",
                                                                   "6. Ανίχνευση με χειροκίνητη έναυση / αυτόματη σβέση"
                                                               };
        public static List<string> LightingSystemControllersTypesStrings = new List<string>
                                                              	{
				"1. Αυτόματος",
				"2. Χειροκίνητος"
			};



        #endregion

        public HeatSystemsViewModel HeatSystemsViewModel { get; set; }
        public CoolingSystemsViewModel CoolingSystemsViewModel { get; set; }
        public ZnxSystemsViewModel ZnxSystemsViewModel { get; set; }
        public YgransiSystemsViewModel YgransiSystemsViewModel { get; set; }
        public KlimatisticUnitSystemsCollectionViewModel KlimatisticUnitSystemsCollectionViewModel { get; set; }
        public SolarSystemsCollectionViewModel SolarSystemsCollectionViewModel { get; set; }
        public LightingSystem LightingSystem { get; set; }

        #region 
        public IEditingController _lightingEditingController { get; set; }
        class LightingEditingController:IEditingController
        {
            #region Implementation of IEditingController

            void IEditingController.HandleBeginEdit()
            {
                 
            }

            void IEditingController.HandleEndEdit()
            {
                Mediator.Instance.SaveBuildingData();
                ThermalBuildingModel.Instance.DataAccesLayerService.SaveChanges(s=>{});
            }

            void IEditingController.HandleCancelEdit()
            {
                
            }

            #endregion
        }
        #endregion


        private IDataAccesLayerServiceAgent DataAccesLayerService { get; set; }

        public SystemsViewModel(ThermalSystem system)
        {
            if (system.HeatSystems == null) system.HeatSystems = new HeatSystem();
            if (system.CoolingSystems == null) system.CoolingSystems = new CoolingSystem();
            if (system.ZnxSystem == null) system.ZnxSystem = new ZnxSystem();
            if (system.YgransiSystem == null) system.YgransiSystem = new YgransiSystem();

            HeatSystemsViewModel = new HeatSystemsViewModel(system.HeatSystems);
            CoolingSystemsViewModel = new CoolingSystemsViewModel(system.CoolingSystems);
            ZnxSystemsViewModel = new ZnxSystemsViewModel(system.ZnxSystem);
            YgransiSystemsViewModel = new YgransiSystemsViewModel(system.YgransiSystem);

            KlimatisticUnitSystemsCollectionViewModel = new KlimatisticUnitSystemsCollectionViewModel(system);
            SolarSystemsCollectionViewModel = new SolarSystemsCollectionViewModel(system);

            if (system.LightingSystem == null) system.LightingSystem = new LightingSystem();
            this._lightingEditingController = new LightingEditingController();
            system.LightingSystem.IEditingController = _lightingEditingController;

            LightingSystem = system.LightingSystem;
            Model = system;

            DataAccesLayerService = ThermalBuildingModel.Instance.DataAccesLayerService;
            LightingSystemMotionDetectionTypes  = new List<EnumObject<object>>(GetEnumListFromArray<object>(SystemsViewModel.LightingSystemMotionDetectionStrings));
            LightingSystemControllersTypes = new List<EnumObject<object>>(GetEnumListFromArray<object>(SystemsViewModel.LightingSystemControllersTypesStrings));
        
        }

        public static List<EnumObject<object>> LightingSystemMotionDetectionTypes { get; set; }
        public static  List<EnumObject<object>> LightingSystemControllersTypes { get; set; }

        #region Properties

       private bool _HeatingIsSelected;
       // private bool _IsHliakosEnabled;
       // private bool _isFotismosEnabled;
      //  private bool _isKkmEnabled;

        private bool _isYgransiEnabled;

        public bool HeatingIsSelected
        {
            get { return _HeatingIsSelected; }
            set
            {
               
                _HeatingIsSelected = value;
                NotifyPropertyChanged(vm => vm.HeatingIsSelected);
            }
        }

        public bool IsYgransiEnabled
        {
            get { return Model.IsYgransiEnabled; }
            set
            {
                Model.IsYgransiEnabled = value;
                if (value == false) HeatingIsSelected = true;
                Mediator.Instance.SaveBuildingData();
                SaveChanges();
                NotifyPropertyChanged(vm => vm.IsYgransiEnabled);
            }
        }

        public bool IsKkmEnabled
        {
            get { return Model.IsKkmEnabled; }
            set
            {
                Model.IsKkmEnabled = value; Mediator.Instance.SaveBuildingData();
                SaveChanges();
                if (value == false) HeatingIsSelected = true;
                NotifyPropertyChanged(vm => vm.IsKkmEnabled);
            }
        }


        public bool IsHliakosEnabled
        {
            get { return Model.IsHliakosEnabled; }
            set
            {
                Model.IsHliakosEnabled = value; Mediator.Instance.SaveBuildingData();
                SaveChanges();
                if (value == false) HeatingIsSelected = true;
                NotifyPropertyChanged(vm => vm.IsHliakosEnabled);
            }
        }


        public bool IsFotismosEnabled
        {
            get { return Model.IsFotismosEnabled; }
            set
            {
                Model.IsFotismosEnabled = value; Mediator.Instance.SaveBuildingData();
                SaveChanges();
                if (value == false) HeatingIsSelected = true;
                NotifyPropertyChanged(vm => vm.IsFotismosEnabled);
            }
        }

     

        #endregion //Properties

        #region Methods

        // Add methods that will be called by the view


        // Save changes on the domain content if there are any
        public void SaveChanges()
        {
            DataAccesLayerService.SaveChanges(e=>{});
 

        }

        // Call RejectChanged on the service agent then reload items
        public void RejectChanges()
        {
            DataAccesLayerService.RejectChanges();
        }

        #endregion

        #region Completion Callbacks

        // Add callback methods for async calls to the service agent


        protected override void OnEndEdit()
        {
            SaveChanges();
            base.OnEndEdit();
        }

        #endregion

    }
}