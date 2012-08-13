using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.ServiceModel.DomainServices.Client.ApplicationServices;
using System.Windows.Controls;
using System.Windows.Input;
using NErgy.Building;
using NErgy.Silverlight.Assets.Resources;
using NErgy.Silverlight.Helpers;
using NErgy.Silverlight.Models;
using NErgy.Silverlight.Views.Controllers;
using NErgy.Silverlight.Web.Models;
using SimpleMvvmToolkit;

namespace NErgy.Silverlight.Views.ViewModels
{

    public class PoleodomiesCollectionViewModel : DataBaseEntityCollectionViewModelBase<Poleodomia>
    {
        public IList<Poleodomia> Entities { get; set; }
        public Web.Models.Project Project { get; set; }


        public PoleodomiesCollectionViewModel(IList<Poleodomia> entities)
        {
            Entities = entities;
            DataAccesLayerService = ThermalBuildingModel.Instance.DataAccesLayerService;
        }

        #region Overrides of DataBaseEntityCollectionViewModelBase<HeatSource>

        private IList<Poleodomia> TempEntities = new List<Poleodomia>();
        public override void AddToTableEntity(Poleodomia entity)
        {
            DataAccesLayerService.AddPoleodomiaToProject(entity, Project);

            if (AddedEntityObjects.Contains(entity))
                AddedEntityObjects.Remove(entity);
        }


        protected override void DoDataAccessSaveChanges()
        {
            NotifySavingChanges();
            DataAccesLayerService.SaveChanges(e =>
            {
                if (e != null)
                {
                    NotifyError(StatusBarStrings.SavedCompletedFalied, e);
                }
                NotifySavedChanges();
            });
        }


        public override void RemoveFromTableEntity(Poleodomia entity)
        {
            DataAccesLayerService.RemovePoleodomia(entity, Project);
        }

        public override void GetCollectionAsync(Action<List<Poleodomia>> completed)
        {
            DataAccesLayerService.GetPoleodomiaForProject(Project, (systems, e) =>
            {

                completed(systems);
                NotifyPropertyChanged(vm => vm.EntityObjects);
            });
        }



        public virtual void SetCollection(IList<Poleodomia> collection)
        {

        }

        #endregion
    }

    public class ProjectViewModel : ViewModelMasterDetailsBase<ProjectViewModel, Project>
    {
        #region Declarations

        public IStatusBar StatusBar = Mediator.Instance.StatusBar;

        private List<string> GreeceLocationsStrings = new List<string>()
                                                          {
		"Αθήνα (Ελληνικό)",
		"Αθήνα (Ν. Φιλαδέλφεια)",
		"Αγρίνιο",
		"Αγχίαλος",
		"Αλεξανδρούπολη",
		"Αλίαρτος",
		"Ανδραβίδα",
		"Άραξος",
		"Άργος (Πυργέλα)",
		"Αργοστόλι",
		"Άρτα",
		"Βέλος",
		"Δράμα",
		"Έδεσσα",
		"Ζάκυνθος",
		"Ηράκλειο",
		"Θεσσαλονίκη (Μίκρα)",
		"Ιεράπετρα",
		"Ιωάννινα",
		"Καλαμάτα",
		"Καρδίτσα",
		"Καρπενήσι",
		"Κάρυστος",
		"Καστοριά",
		"Κέρκυρα",
		"Κοζάνη",
		"Κομοτηνή",
		"Κόνιτσα",
		"Κύθηρα",
		"Κως",
		"Λαμία",
		"Λάρισα",
		"Λευκάδα",
		"Λήμνος",
		"Μεθώνη",
		"Μήλος",
		"Μυτιλήνη",
		"Νάξος",
		"Ξάνθη",
		"Πάρος",
		"Πάτρα",
		"Πολύγυρος",
		"Πύργος",
		"Ρέθυμνο",
		"Ρόδος",
		"Σάμος",
		"Σέρρες",
		"Σητεία",
		"Σκύρος",
		"Σούδα",
		"Σπάρτη",
		"Σύρος",
		"Τανάγρα",
		"Τρίκαλα (Ημαθίας)",
		"Τρίκαλα (Θεσσαλίας)",
		"Τρίπολη",
		"Τυμπάκι",
		"Φλώρινα",
		"Χαλκίδα",
		"Χανιά",
		"Χίος",
		"Χρυσούπολη (Καβάλας)"
	};

        private List<string> KlimaticZonesStrings = new List<string>()
                                                 {
                                                     "Ζώνη Α",
                                                     "Ζώνη Β",
                                                     "Ζώνη Γ",
                                                     "Ζώνη Δ"
                                                 };

        public static List<string> UsageTitleStrings = new List<string>()
                                                 {
                                                    "Κατοικίας",
                                                     "Προσωρινής διαμονής",
                                                     "Συνάθροισης κοινού",
                                                     "Εκπαίδευσης",
                                                     "Υγείας και Κοινωνικής Πρόνοιας",
                                                     "Σωφρονισμού"
                                                     ,"Εμπορίου",
                                                     "Γραφείων"

                                                 };
        private List<string> UsageStrings = new List<string>()
                                                 {
                                                     "Μονοκατοικία"
, "Πολυκατοικία"
 ,"Ξενοδοχείο - Ετήσιας λειτουργίας"
 , "Ξενοδοχείο - Θερινής λειτουργίας"
 , "Ξενοδοχείο - Χειμερινής λειτουργίας"
 , "Ξενώνες - Ετήσιας λειτουργίας"
 , "Ξενώνες - Θερινής λειτουργίας"
 , "Ξενώνες - Χειμερινής λειτουργίας"
,	 "Οικοτροφεία"
,	 "Κοιτώνες"
,	 "Εστιατόρια"
,	 "Ζαχαροπλαστεία"
,	 "Καφενεία"
,	 "Νυχτερινά κέντρα διασκέδασης"
,	 "Μουσικές σκηνές"
,	 "Θέατρα"
,	 "Κινηματογράφοι"
,	 "Χώροι συναυλιών"
,	 "Χώροι εκθέσεων"
,	 "Μουσεία"
,	 "Χώροι συνεδρίων"
,	 "Αμφιθέατρα"
,	 "Αίθουσες δικαστηρίων"
,	 "Τράπεζες"
,	 "Αίθουσες πολλαπλών χρήσεων"
,	 "Κλειστό γυμναστήριο"
,	 "Κλειστό κολυμβητήριο"
,	 "Νηπιαγωγεία"
,	 "Πρωτοβάθμιας εκπαίδευσης"
,	 "Δευτεροβάθμιας εκπαίδευσης"
,	 "Τριτοβάθμιας εκπαίδευσης"
,	 "Αίθουσες διδασκαλίας"
,	 "Φροντιστήρια"
,	 "Ωδεία"
,	 "Νοσοκομεία"
,	 "Κλινικές"
,	 "Αγροτικά ιατρεία"
,	 "Υγειονομικοί σταθμοί"
,	 "Κέντρα υγείας"
,	 "Ιατρεία"
,	 "Ψυχιατρεία"
,	 "Ιδρύματα"
,	 "Οίκοι ευγηρίας"
,	 "Βρεφοκομεία"
,	 "Βρεφικοί σταθμοί"
,	 "Παιδικοί σταθμοί"
,	 "Κρατητήρια"
,	 "Αναμορφωτήρια"
,	 "Φυλακές"
,	 "Αστυνομικές διευθύνσεις"
,	 "Εμπορικά κέντρα"
,	 "Αγορές"
,	 "Υπεραγορές"
,	 "Καταστήματα"
,	 "Φαρμακεία"
,	 "Ινστιτούτα γυμναστικής"
,	 "Κουρεία"
,	 "Κομμωτήρια"
,	 "Γραφεία"
,	 "Βιβλιοθήκες"



                                                 };

        private List<string> OwnershipTypesStrings = new List<string>()
                                                        {
                                                            "Δημόσιο",
                                                            "Ιδιωτικό",
                                                            "Δημόσιο ιδιωτικού ενδιαφέροντος",
                                                            "Ιδιωτικό δημοσίου ενδιαφέροντος"
                                                        };

        private List<string> OwnershipContactInformationsStrings = new List<string>()
                                                                       {
                                                                           "Ιδιοκτήτης",
                                                                           "Διαχειριστής",
                                                                           "Ενοικιαστής",
                                                                           "Τεχνικός Υπεύθυνος",
                                                                           "Άλλο"
                                                                       };



        private List<string> PoleodomiaTypesStrings = new List<string>()
                                                                       {
                                                                           "Νέο",
                                                                           "Παλαιό",
                                                                           "Ριζικά ανακαινιζόμενο"
                                                                       };
        public PoleodomiesCollectionViewModel PoleodomiesCollectionViewModel { get; set; }

        public static List<EnumObject<object>> UsageTypes { get; set; }

        public ProjectViewModel(Project project)
        {
            ProjectListViewModel.EngineersInProjectLoaded += new EventHandler<NotificationEventArgs<Web.Models.Project>>(ProjectListViewModel_EngineersInProjectLoaded);
            if (DataAccesLayerServiceAgent == null)
                DataAccesLayerServiceAgent = ThermalBuildingModel.Instance.DataAccesLayerService;
            GreeceLocations = new List<EnumObject<object>>(GetEnumListFromArray<object>(GreeceLocationsStrings));
            UsageTypes = new List<EnumObject<object>>(GetEnumListFromArray<object>(UsageStrings));

            OwnershipTypes = new List<EnumObject<OwnershipType>>(GetEnumListWithArraySupport<OwnershipType>(OwnershipTypesStrings));
            OwnershipContactInformations =
                new List<EnumObject<OwnershipContactInformation>>(GetEnumListWithArraySupport<OwnershipContactInformation>(OwnershipContactInformationsStrings));
            // KlimaticZoneDataType =
            //new List<EnumObject<KlimaticZoneDataType>>(GetEnumList<KlimaticZoneDataType>());

            KlimaticZones = new List<EnumObject<KlimaticZone>>(GetEnumListWithArraySupport<KlimaticZone>(KlimaticZonesStrings, 1));
            PoleodomiaTypes = new List<EnumObject<PoleodomiaType>>(GetEnumListWithArraySupport<PoleodomiaType>(PoleodomiaTypesStrings));
            //Model = project;
            //Project = project;
            Model = project;
            PoleodomiesCollectionViewModel = new PoleodomiesCollectionViewModel(new List<Poleodomia>()) { Project = Model };

            PoleodomiesCollectionViewModel.SavingItemNotice += PoleodomiesCollectionViewModel_SavingItemNotice;
            PoleodomiesCollectionViewModel.ItemsSavedNotice += PoleodomiesCollectionViewModel_ItemsSavedNotice;
            PoleodomiesCollectionViewModel.ErrorNotice += PoleodomiesCollectionViewModel_ErrorNotice;

            this.engineerEditor = engineerEditor;

            AssociateProperties(m => m.ProjectTitle, vm => vm.ProjectTitle);
            AssociateProperties(m => m.ProjectUseageId, vm => vm.ProjectUseageId);


            WebContext.Current.Authentication.LoggedIn += new EventHandler<System.ServiceModel.DomainServices.Client.ApplicationServices.AuthenticationEventArgs>(Authentication_LoggedIn);
        }

        private void Authentication_LoggedIn(object sender, AuthenticationEventArgs e)
        {

        }


        public List<EnumObject<object>> GreeceLocations { get; set; }

        private void PoleodomiesCollectionViewModel_ErrorNotice(object sender, NotificationEventArgs<Exception> e)
        {
            StatusBar.Display(e.Message);

        }

        private void PoleodomiesCollectionViewModel_ItemsSavedNotice(object sender, NotificationEventArgs e)
        {
            IsBusy = false;
            StatusBar.Display(StatusBarStrings.SavedCompletedSucesfully);
        }

        private void PoleodomiesCollectionViewModel_SavingItemNotice(object sender, NotificationEventArgs e)
        {
            IsBusy = true;
            StatusBar.Display(StatusBarStrings.SavingInProgress);

        }

        private void ProjectListViewModel_EngineersInProjectLoaded(object sender, NotificationEventArgs<Project> e)
        {
        //    if (e.Data.Engineers != null) Model.Engineers = new ObservableCollection<Engineers>(e.Data.Engineers);
         //   NotifyPropertyChanged(m => m.Engineers);
        }

        public bool IsBusy
        {
            get { return isBusy; }
            set
            {
                isBusy = value;

                NotifyPropertyChanged(m => m.IsBusy);
            }
        }

        #endregion //Declarations

        #region Events

        // public override event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

        public IDataAccesLayerServiceAgent DataAccesLayerServiceAgent
        {
            get;
            set;
        }

        #endregion //Events

        #region Properties

        private List<EnumObject<OwnershipType>> _ownershipTypes;
        public List<EnumObject<OwnershipType>> OwnershipTypes
        {
            get { return _ownershipTypes; }
            set
            {
                _ownershipTypes = value;
                NotifyPropertyChanged(m => m.OwnershipTypes);
            }
        }
        public int? OwnerShiptype
        {
            get { return Model.OwnerShipType; }
            set
            {
                Model.OwnerShipType = value;
                NotifyPropertyChanged(m => m.OwnerShiptype);
            }
        }

        public int? ProjectUseageId
        {
            get { return Model.ProjectUseageId; }
            set
            {
                Model.ProjectUseageId = value;
                NotifyPropertyChanged(m => m.ProjectUseageId);
            }
        }
        public int? OwnershipContactInfoId
        {
            get { return Model.OwnershipContactInfoID; }
            set
            {
                Model.OwnershipContactInfoID = value;
                NotifyPropertyChanged(m => m.OwnershipContactInfoId);
            }
        }
        private List<EnumObject<OwnershipContactInformation>> _ownershipContactInformations;
        public List<EnumObject<OwnershipContactInformation>> OwnershipContactInformations
        {
            get { return _ownershipContactInformations; }
            set
            {
                _ownershipContactInformations = value;
                NotifyPropertyChanged(m => m.OwnershipTypes);
            }
        }

        private Project _project;
   //     public Engineers _selectedEngineer;

        public Project Project
        {
            get { return _project; }
            set
            {
                _project = value;
                NotifyPropertyChanged(m => m.Project);
            }
        }

        public IList<Web.Models.Building> Buildings
        {
            get { return Model.Building.ToList(); }
            set
            {
                //Model.Buildings = value;
                NotifyPropertyChanged(m => m.Buildings);
            }
        }


        public IList<NeighborBuilding> NeighborBuildings
        {
            get { return Model.NeighborBuildings; }
            set
            {
                Model.NeighborBuildings = value;
                NotifyPropertyChanged(m => m.NeighborBuildings);
            }
        }

        public String ProjectCode
        {
            get { return Model.ProjectCode; }
            set
            {
                Model.ProjectCode = value;
                NotifyPropertyChanged(m => m.ProjectCode);
            }
        }

        public Nullable<bool> IsBuildingMember
        {
            get { return Model.IsBuildingMember; }
            set
            {
                Model.IsBuildingMember = value;
                NotifyPropertyChanged(m => m.IsBuildingMember);
            }
        }

        public String OwnerName
        {
            get { return Model.OwnerName; }
            set
            {
                Model.OwnerName = value;
                NotifyPropertyChanged(m => m.OwnerName);
            }
        }


        public String OwnerShipNumber
        {
            get { return Model.OwnerShipNumber; }
            set
            {
                Model.OwnerShipNumber = value;
                NotifyPropertyChanged(m => m.OwnerShipNumber);
            }
        }

        public String KAEK
        {
            get { return Model.KAEK; }
            set
            {
                Model.KAEK = value;
                NotifyPropertyChanged(m => m.KAEK);
            }
        }

        public String OwnerEmail
        {
            get { return Model.OwnerEmail; }
            set
            {
                Model.OwnerEmail = value;
                NotifyPropertyChanged(m => m.OwnerEmail);
            }
        }
        private List<EnumObject<KlimaticZoneDataType>> _klimaticZoneDataType;
        public List<EnumObject<KlimaticZoneDataType>> KlimaticZoneDataType
        {
            get { return _klimaticZoneDataType; }
            set
            {
                _klimaticZoneDataType = value;
                NotifyPropertyChanged(m => m.OwnershipTypes);
            }
        }
        public int? ProjectKlimaticData
        {
            get { return Model.KlimaticDataID; }
            set
            {
                Model.KlimaticDataID = value;
                NotifyPropertyChanged(m => m.ProjectKlimaticData);
            }
        }

        public String ProjectDescription
        {
            get { return Model.ProjectDescription; }
            set
            {
                Model.ProjectDescription = value;
                NotifyPropertyChanged(m => m.ProjectDescription);
            }
        }

        public bool? AltitudeOver500m
        {
            get { return Model.AltitudeOver500m; }
            set
            {
                Model.AltitudeOver500m = value;
                NotifyPropertyChanged(m => m.AltitudeOver500m);
            }
        }

        public String OwnersNames
        {
            get { return null; }
            set
            {
                //_project.OwnersNames = value;
                NotifyPropertyChanged(m => m.OwnersNames);
            }
        }

        #region datasources
        public bool DataSourceArcitectural
        {
            get { return Model.DataSourceArcitectural.HasValue ? Model.DataSourceArcitectural.Value : false; }
            set
            {
                Model.DataSourceArcitectural = value;
                NotifyPropertyChanged(m => m.DataSourceArcitectural);
            }
        }

        public bool DataSourceDeltiaAgorasYlikwn
        {
            get { return Model.DataSourceDeltiaAgorasYlikwn.HasValue ? Model.DataSourceDeltiaAgorasYlikwn.Value : false; }
            set
            {
                Model.DataSourceDeltiaAgorasYlikwn = value;
                NotifyPropertyChanged(m => m.DataSourceDeltiaAgorasYlikwn);
            }
        }



        public bool DataSourceDiaxiristis
        {
            get { return Model.DataSourceDiaxiristis.HasValue ? Model.DataSourceDiaxiristis.Value : false; }
            set
            {
                Model.DataSourceDiaxiristis = value;
                NotifyPropertyChanged(m => m.DataSourceDiaxiristis);
            }
        }




        public bool DataSourceEnergeiakiEpithewrisiLevita
        {
            get { return Model.DataSourceEnergeiakiEpithewrisiLevita.HasValue ? Model.DataSourceEnergeiakiEpithewrisiLevita.Value : false; }
            set
            {
                Model.DataSourceEnergeiakiEpithewrisiLevita = value;
                NotifyPropertyChanged(m => m.DataSourceEnergeiakiEpithewrisiLevita);
            }
        }






        public bool DataSourceEpithewrisisKlimatismou
        {
            get { return Model.DataSourceEpithewrisisKlimatismou.HasValue ? Model.DataSourceEpithewrisisKlimatismou.Value : false; }
            set
            {
                Model.DataSourceEpithewrisisKlimatismou = value;
                NotifyPropertyChanged(m => m.DataSourceEpithewrisisKlimatismou);
            }
        }




        public bool DataSourceEpithewrisiSystimatosThermansis
        {
            get { return Model.DataSourceEpithewrisiSystimatosThermansis.HasValue ? Model.DataSourceEpithewrisiSystimatosThermansis.Value : false; }
            set
            {
                Model.DataSourceEpithewrisiSystimatosThermansis = value;
                NotifyPropertyChanged(m => m.DataSourceEpithewrisiSystimatosThermansis);
            }
        }



        public bool DataSourcefylosisntirisisKlimatismou
        {
            get { return Model.DataSourcefylosisntirisisKlimatismou.HasValue ? Model.DataSourcefylosisntirisisKlimatismou.Value : false; }
            set
            {
                Model.DataSourcefylosisntirisisKlimatismou = value;
                NotifyPropertyChanged(m => m.DataSourcefylosisntirisisKlimatismou);
            }
        }


        public bool DataSourcefyloSyntirisisLevita
        {
            get { return Model.DataSourcefyloSyntirisisLevita.HasValue ? Model.DataSourcefyloSyntirisisLevita.Value : false; }
            set
            {
                Model.DataSourcefyloSyntirisisLevita = value;
                NotifyPropertyChanged(m => m.DataSourcefyloSyntirisisLevita);
            }
        }


        public bool DataSourceHM
        {
            get { return Model.DataSourceHM.HasValue ? Model.DataSourceHM.Value : false; }
            set
            {
                Model.DataSourceHM = value;
                NotifyPropertyChanged(m => m.DataSourceHM);
            }
        }



        public bool DataSourceTimologiaEnergeiakvnKataNalwsewn
        {
            get { return Model.DataSourceTimologiaEnergeiakvnKataNalwsewn.HasValue ? Model.DataSourceTimologiaEnergeiakvnKataNalwsewn.Value : false; }
            set
            {
                Model.DataSourceTimologiaEnergeiakvnKataNalwsewn = value;
                NotifyPropertyChanged(m => m.DataSourceTimologiaEnergeiakvnKataNalwsewn);
            }
        }



        #endregion



        //private IList<Poleodomia> _Poleodomies;
        //public IList<Poleodomia> Poleodomies
        //{
        //    get { return _Poleodomies; }
        //    set
        //    {
        //        _Poleodomies= value;
        //        NotifyPropertyChanged(m => m.Poleodomies);
        //    }
        //}


        private static List<EnumObject<PoleodomiaType>> _poleodomiaTypes;
        public static List<EnumObject<PoleodomiaType>> PoleodomiaTypes
        {
            get { return _poleodomiaTypes; }
            set
            {
                _poleodomiaTypes = value;
              //  NotifyPropertyChanged(m => m.PoleodomiaTypes);
            }
        }


        //public IList<Engineers> Engineers
        //{
        //    get { return Model.Engineers; }
        //    set
        //    {
        //        Model.Engineers = value;
        //        NotifyPropertyChanged(m => m.Engineers);
        //    }
        //}

        //public Engineers SelectedEngineer
        //{
        //    get { return _selectedEngineer; }
        //    set
        //    {
        //        _selectedEngineer = value;
        //        NotifyPropertyChanged(m => m.SelectedEngineer);
        //    }
        //}


        public IList<Poleodomia> Poleodomies
        {
            get { return Model.Poleodomia_1.ToList(); }
            set
            {
                //Model.Poleodomies = value;
                NotifyPropertyChanged(m => m.Poleodomies);
            }
        }



        public String ProjectTitle
        {
            get { return Model.ProjectTitle; }
            set
            {
                Model.ProjectTitle = value;
                NotifyPropertyChanged(m => m.ProjectTitle);
            }
        }

        public String Address
        {
            get { return Model.Address; }
            set
            {
                Model.Address = value;
                NotifyPropertyChanged(m => m.Address);
            }
        }


        public String AddressNumber
        {
            get { return Model.AddressNumber; }
            set
            {
                Model.AddressNumber = value;
                NotifyPropertyChanged(m => m.AddressNumber);
            }
        }


        public String AddressTK
        {
            get { return Model.AddressTK; }
            set
            {
                Model.AddressTK = value;
                NotifyPropertyChanged(m => m.AddressTK);
            }
        }

        //public String Email
        //{
        //    get { return Model.Email; }
        //    set
        //    {
        //        Project.Email = value;
        //        NotifyPropertyChanged(m => m.Email);
        //    }
        //}


        private List<EnumObject<KlimaticZone>> _klimaticZones;
        public List<EnumObject<KlimaticZone>> KlimaticZones
        {
            get { return _klimaticZones; }
            set
            {
                _klimaticZones = value;
                NotifyPropertyChanged(m => m.KlimaticZones);
            }
        }
        public int? KlimaticZoneID
        {
            get { return Model.KlimaticZoneID; }
            set
            {
                Model.KlimaticZoneID = value;
                NotifyPropertyChanged(m => m.KlimaticZoneID);
            }
        }


        #endregion //Properties

        private object engineerEditor;
        private bool isBusy;
        //public ProjectListViewModel ProjectListViewModel { get; set; }
        public ICommand EditEngineerCommand
        {
            get
            {
                return new DelegateCommand(
                    () =>
                    {
                        ChildWindow dialog = Mediator.OpenWindow(engineerEditor);

                        dialog.Closed += (s, e) => { };
                    });
            }
        }

        protected override void OnEndEdit()
        {
            this.DataAccesLayerServiceAgent.SaveChanges((e) =>
                                                         {
                                                             if (e != null)
                                                                 StatusBar.Display(e.Message);
                                                             else
                                                             {
                                                                 StatusBar.Display("Οι αλλαγές στο έργο αποθηκέυτηκαν ");
                                                             }
                                                         });
            base.OnEndEdit();
        }

        public override void CancelEdit()
        {
            base.CancelEdit();
        }

        public override void EndEdit()
        {


        }




        #region TreeViewViewModel


        #endregion
    }
}