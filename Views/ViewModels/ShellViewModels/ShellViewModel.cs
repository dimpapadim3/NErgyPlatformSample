using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;
using NErgy.Building;
using NErgy.Building.BuildingShell;
using NErgy.Building.ThermalZones;
using NErgy.Silverlight.Assets.Resources;
using NErgy.Silverlight.Helpers;
using NErgy.Silverlight.Models;
using NErgy.Silverlight.Views.Controllers;
using NErgy.Silverlight.Web.Models;
using SimpleMvvmToolkit;
using TranslusentElement = NErgy.Silverlight.Models.StructuralElements.TranslusentElement;
using TransparentElement = NErgy.Silverlight.Models.StructuralElements.TransparentElement;
namespace NErgy.Silverlight.Views.ViewModels
{

    public class TranslusentElementCollectionViewModel : CollectionViewModelBase<TranslusentElement>
    {
        public Shell Shell { get; set; }

        public TranslusentElementCollectionViewModel(Shell Shell)
            : base(new TranslusentElement())
        {
            this.Shell = Shell;
            this.DataAccesLayerService = ThermalBuildingModel.Instance.DataAccesLayerService;
        }

        public override List<TranslusentElement> GetCollection()
        {
            return Shell.OpaqueElements.Cast<TranslusentElement>().ToList();
        }

        public override void SetCollection(IList<TranslusentElement> collection)
        {
            Shell.OpaqueElements = collection.Cast<NErgy.Building.TranslusentElement>().ToList();
        }

    }

    public class TransparentElementCollectionViewModel : CollectionViewModelBase<TransparentElement>
    {
        public Shell Shell { get; set; }

        public TransparentElementCollectionViewModel(Shell Shell)
            : base(new TransparentElement())
        {
            this.Shell = Shell;
            this.DataAccesLayerService = ThermalBuildingModel.Instance.DataAccesLayerService;
        }

        public override List<TransparentElement> GetCollection()
        {
            return Shell.TransparentElements.Cast<TransparentElement>().ToList();
        }

        public override void SetCollection(IList<TransparentElement> collection)
        {
            Shell.TransparentElements = collection.Cast<NErgy.Building.TransparentElement>().ToList();
        }

    }

    public class ContactWithGroundElementCollectionViewModel : CollectionViewModelBase<ContactWithGroundElement>
    {
        public Shell Shell { get; set; }

        public ContactWithGroundElementCollectionViewModel(Shell Shell)
            : base(new ContactWithGroundElement())
        {
            this.Shell = Shell;
            this.DataAccesLayerService = ThermalBuildingModel.Instance.DataAccesLayerService;
        }

        public override List<ContactWithGroundElement> GetCollection()
        {
            return Shell.ContactWithGroundElement.ToList();
        }

        public override void SetCollection(IList<ContactWithGroundElement> collection)
        {
            if (collection != null)
                Shell.ContactWithGroundElement = collection.ToList();
        }
    }



    public class PassiveDirectSolarGainSystemViewModel : CollectionViewModelBase<PassiveDirectSolarGainSystem>
    {
        public Shell Shell { get; set; }

        public PassiveDirectSolarGainSystemViewModel(Shell Shell)
            : base(new PassiveDirectSolarGainSystem())
        {
            this.Shell = Shell;
            this.DataAccesLayerService = ThermalBuildingModel.Instance.DataAccesLayerService;
        }

        public override List<PassiveDirectSolarGainSystem> GetCollection()
        {
            return Shell.PassiveDirectSolarGainSystems.ToList();
        }

        public override void SetCollection(IList<PassiveDirectSolarGainSystem> collection)
        {
            Shell.PassiveDirectSolarGainSystems = collection;
        }

    }
    public class ShellViewModel<T> : ViewModelMasterDetailsBase<ShellViewModel<T>, T> where T : Shell
    {
        public List<string> TranslusentElementTypesStrings = new List<string>   {
				"Τοίχος",
				"Οροφή",
				"Πυλωτή",
				"Πόρτα"
			};

        public List<string> TransparentElementTypesStrings = new List<string>   {
				"Ανοιγόμενο κούφωμα",
				"Μη ανοιγόμενο κούφωμα",
				"Ανοιγόμενη πρόσοψη",
				"Μη ανοιγόμενη πρόσοψη"
			};


        public List<string> ContactWithGroundElementTypesStrings = new List<string>  {
				"Τοίχος",
				"Δάπεδο"
			};


        public List<string> PassiveTypesStrings = new List<string>  {
			"Ανοιγόμενο κούφωμα" ,
            "Μη ανοιγόμενο κούφωμα",
           "Ανοιγόμενη πρόσοψη",
           "Μη ανοιγόμενη πρόσοψη"
			};



        public ShellViewModel(T shell)
        {
            ContactWithGroundElementsMenuItems = new List<MenuItem>();
            TranslusentElementsMenuItems = new List<MenuItem>();

            DataAccesLayerService = ThermalBuildingModel.Instance.DataAccesLayerService;

            this.Model = shell;

            PassiveDirectSolarGainSystemViewModel = new PassiveDirectSolarGainSystemViewModel(Model);
            TranslusentElementCollectionViewModel = new TranslusentElementCollectionViewModel(Model);
            TransparentElementCollectionViewModel = new TransparentElementCollectionViewModel(Model);
            ContactWithGroundElementCollectionViewModel = new ContactWithGroundElementCollectionViewModel(Model);

            TranslusentElementTypes = new List<EnumObject<TranslusentElementType>>(GetEnumListWithArraySupport<TranslusentElementType>(TranslusentElementTypesStrings));
            TransparentElementTypes = new List<EnumObject<TransparentElementTypes>>(GetEnumListWithArraySupport<TransparentElementTypes>(TransparentElementTypesStrings));
            ContactWithGroundElementTypes = new List<EnumObject<ContactWithGroundElementTypes>>(GetEnumListWithArraySupport<ContactWithGroundElementTypes>(ContactWithGroundElementTypesStrings));

            PassiveElementsTypes = new List<EnumObject<PassiveTypes>>(GetEnumListWithArraySupport<PassiveTypes>(PassiveTypesStrings));
        }

        public TranslusentElementCollectionViewModel TranslusentElementCollectionViewModel { get; set; }
        public PassiveDirectSolarGainSystemViewModel PassiveDirectSolarGainSystemViewModel { get; set; }
        public TransparentElementCollectionViewModel TransparentElementCollectionViewModel { get; set; }
        public ContactWithGroundElementCollectionViewModel ContactWithGroundElementCollectionViewModel { get; set; }

        public string ThermalBridgesString
        {
            get { return Model.ThermalBridgesString; }
            set
            {
                Model.ThermalBridgesString = value;
                NotifyPropertyChanged(vm => vm.ThermalBridgesString);
                Mediator.Instance.SaveBuildingData();
                DataAccesLayerService.SaveChanges(e=>
                                                      {
                                                          Mediator.Instance.StatusBar.Display("Οι αλλαγες στις θερμογέφυρες αποθηκευτηκαν");
                                                      });
            }
        }

        public double ThermalBridgesFactor
        {
            get { return Model.ThermalBridgesFactor; }
            set
            {
                Model.ThermalBridgesFactor = value;
                NotifyPropertyChanged(vm => vm.ThermalBridgesFactor);
                Mediator.Instance.SaveBuildingData(); 
                DataAccesLayerService.SaveChanges(e =>
                {
                    Mediator.Instance.StatusBar.Display("Οι αλλαγες στις θερμογέφυρες αποθηκευτηκαν");
                });
            }
        }

        public int NumberOfSeparatingSurfauces
        {
            get { return Model.SeparatingSurfuces != null ? Model.SeparatingSurfuces.Count : 0; }
            set
            {
                Mediator.Instance.RequestSeparatingSurfaceByNumericUdDown(Model, value);
                NotifyPropertyChanged(m => m.NumberOfSeparatingSurfauces);
                Mediator.Instance.SaveBuildingData();
                DataAccesLayerService.SaveChanges(e =>
                {
                    Mediator.Instance.StatusBar.Display("Ο αρηθμός των διαχωρήστικώνεπιφανείών  αποθηκευτηκε");
                });  
              
            }
        }


        public bool HasPassiveSolarSystems
        {
            get { return Model.IsPassiveSystemsEnabled; }
            set
            {
                Model.IsPassiveSystemsEnabled = value;
                Mediator.Instance.SaveBuildingData();
                DataAccesLayerService.SaveChanges((e) => { });
                NotifyPropertyChanged(m => m.HasPassiveSolarSystems);
            }
        }


        private void NotifyOpaqueBlankRowCollectionCollectionChanged()
        {
            NotifyPropertyChanged(m => m.OpaqueElements);
            IsEnabledTranslusentEndEdit = true;
            OriginalOpaqueElements = SimpleMvvmToolkit.ModelExtensions.Extensions.Clone(Model.OpaqueElements, new List<Type>() { typeof(TranslusentElement) });
        }

        private void NotifyTransparentBlankRowCollectionCollectionChanged()
        {
            NotifyPropertyChanged(m => m.TransparentElements);
            IsEnabledTransparentEndEdit = true;
            OriginalTransparents = SimpleMvvmToolkit.ModelExtensions.Extensions.Clone(Model.TransparentElements, new List<Type>() { typeof(TransparentElement) });
        }

        private void NotifyContactWithGroundCollectionCollectionChanged()
        {
            NotifyPropertyChanged(m => m.ContactWithGroundElements);
            IsEnabledContactWithGroundElementEndEdit = true;
            OriginalContactWithGrounds = SimpleMvvmToolkit.ModelExtensions.Extensions.Clone(Model.ContactWithGroundElement, new List<Type>() { typeof(TranslusentElement) });
        }

        #region Properties

        private BlankRowCollection<TranslusentElement> _opaqueElementsblankRowCollection;
        private object OriginalOpaqueElements { get; set; }
        private object OriginalTransparents { get; set; }
        private object OriginalContactWithGrounds { get; set; }
        public IList<TranslusentElement> OpaqueElements
        {
            get
            {
                if (_opaqueElementsblankRowCollection == null || _opaqueElementsblankRowCollection.Count == 0)
                {
                    InitializeOpaqueElementCollection();
                }
                return _opaqueElementsblankRowCollection;
            }

        }

        private void InitializeOpaqueElementCollection()
        {
            var coll = new List<TranslusentElement>();
            if (Model.OpaqueElements != null)
            {
                (Model.OpaqueElements.ToList())
                    .ForEach(c =>
                                 {
                                     if (c is TranslusentElement) coll.Add(c as TranslusentElement);
                                     c.PropertyChanged += (s, e) => NotifyOpaqueBlankRowCollectionCollectionChanged();
                                 });

            }
            _opaqueElementsblankRowCollection = new BlankRowCollection<TranslusentElement>(coll);
            _opaqueElementsblankRowCollection.CollectionChanged += (s, e) => NotifyOpaqueBlankRowCollectionCollectionChanged();
        }

        private TranslusentElement _selectedTranslusentElement;
        public TranslusentElement SelectedTranslusentElement
        {
            get { return _selectedTranslusentElement; }
            set
            {
                _selectedTranslusentElement = value;
                NotifyPropertyChanged(m => m.SelectedTranslusentElement);
            }
        }

        private BlankRowCollection<TransparentElement> _transparentElementblankRowCollection;
        public IList<TransparentElement> TransparentElements
        {
            get
            {
                if (_transparentElementblankRowCollection == null || _transparentElementblankRowCollection.Count == 0)
                {
                    InitializeTranspantElements();
                }
                return _transparentElementblankRowCollection;
            }
        }

        private void InitializeTranspantElements()
        {
            var coll = new List<TransparentElement>();
            if (Model.TransparentElements != null)
                (Model.TransparentElements.ToList())
                    .ForEach(c =>
                                 {
                                     if (c is TransparentElement) coll.Add(c as TransparentElement);
                                     c.PropertyChanged += (s, e) => NotifyTransparentBlankRowCollectionCollectionChanged();
                                 });

            _transparentElementblankRowCollection = new BlankRowCollection<TransparentElement>(coll);
            _transparentElementblankRowCollection.CollectionChanged += (s, e) => NotifyTransparentBlankRowCollectionCollectionChanged();
        }


        private TransparentElement _selectedTransparentElement;
        public TransparentElement SelectedTransparentElement
        {
            get { return _selectedTransparentElement; }
            set
            {
                _selectedTransparentElement = value;
                NotifyPropertyChanged(m => m.SelectedTransparentElement);
            }
        }

        private BlankRowCollection<ContactWithGroundElement> _contactWithGroundElementskRowCollection;
        public IList<ContactWithGroundElement> ContactWithGroundElements
        {
            get
            {
                if (_contactWithGroundElementskRowCollection == null || _contactWithGroundElementskRowCollection.Count == 0)
                {
                    InitializeContactWithGroundElements();
                }
                return _contactWithGroundElementskRowCollection;

            }

        }

        private void InitializeContactWithGroundElements()
        {
            var coll = new List<ContactWithGroundElement>();
            if (Model.ContactWithGroundElement != null)
                (Model.ContactWithGroundElement.ToList())
                    .ForEach(c =>
                                 {
                                     if (c is ContactWithGroundElement) coll.Add(c as ContactWithGroundElement);
                                     c.PropertyChanged += (s, e) => NotifyContactWithGroundCollectionCollectionChanged();
                                 });

            _contactWithGroundElementskRowCollection = new BlankRowCollection<ContactWithGroundElement>(coll);
            _contactWithGroundElementskRowCollection.CollectionChanged += (s, e) => NotifyContactWithGroundCollectionCollectionChanged();
        }

        private ContactWithGroundElement _selectedContactWithGroundElement;
        public ContactWithGroundElement SelectedContactWithGroundElement
        {
            get { return _selectedContactWithGroundElement; }
            set
            {
                _selectedContactWithGroundElement = value;
                NotifyPropertyChanged(m => m.SelectedContactWithGroundElement);
            }
        }

        public ICommand EndEditTranslucentCommand
        {
            get
            {
                return new DelegateCommand(SaveTranslucentElementsChanges);
            }
        }
        public ICommand CancelEditTranslucentCommand
        {
            get
            {

                return new DelegateCommand(RejectChangesTranslusentCollection);
            }
        }

        public ICommand EndEditTransparentCommand
        {
            get
            {
                return new DelegateCommand(SaveTransparentElementsChanges);
            }
        }
        public ICommand CancelEditTransparentCommand
        {
            get
            {

                return new DelegateCommand(RejectChangesTransparentElmentCollection);
            }
        }

        public ICommand EndEditContactWithGroundElementCommand
        {
            get
            {
                return new DelegateCommand(SaveContactWithGroundElementChanges);
            }
        }
        public ICommand CancelEditContactWithGroundElementCommand
        {
            get
            {
                return new DelegateCommand(RejectChangesContactWithGroundElmentCollection);
            }
        }

        private void RejectChangesTranslusentCollection()
        {
            this.Model.OpaqueElements = OriginalOpaqueElements as IList<NErgy.Building.TranslusentElement>;
            InitializeOpaqueElementCollection();
            NotifyPropertyChanged(m => m.OpaqueElements);
        }
        private void RejectChangesContactWithGroundElmentCollection()
        {
            this.Model.ContactWithGroundElement = OriginalContactWithGrounds as IList<ContactWithGroundElement>;
            InitializeContactWithGroundElements();
            NotifyPropertyChanged(m => m.ContactWithGroundElements);
        }
        private void RejectChangesTransparentElmentCollection()
        {
            this.Model.TransparentElements = OriginalTransparents as IList<NErgy.Building.TransparentElement>;
            InitializeTranspantElements();
            NotifyPropertyChanged(m => m.TransparentElements);
        }

        private bool _isEnabledTranslusentEndEdit;
        public bool IsEnabledTranslusentEndEdit
        {
            get { return _isEnabledTranslusentEndEdit; }
            set
            {
                _isEnabledTranslusentEndEdit = value;
                NotifyPropertyChanged(m => m.IsEnabledTranslusentEndEdit);
            }
        }

        private bool _isEnabledTransparentEndEdit;
        public bool IsEnabledTransparentEndEdit
        {
            get { return _isEnabledTransparentEndEdit; }
            set
            {
                _isEnabledTransparentEndEdit = value;
                NotifyPropertyChanged(m => m.IsEnabledTransparentEndEdit);
            }
        }

        private bool _isEnabledContactWithGroundElementEndEdit;
        public bool IsEnabledContactWithGroundElementEndEdit
        {
            get { return _isEnabledContactWithGroundElementEndEdit; }
            set
            {
                _isEnabledContactWithGroundElementEndEdit = value;
                NotifyPropertyChanged(m => m.IsEnabledContactWithGroundElementEndEdit);
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


        // Save changes on the domain content if there are any
        public void SaveChanges()
        {
            Mediator.Instance.SaveBuildingData();
            //this.IsEnabledTranslusentEndEdit = false;
        }

        public void SaveTranslucentElementsChanges()
        {
            Model.OpaqueElements = _opaqueElementsblankRowCollection.RealItems.Cast<NErgy.Building.TranslusentElement>().ToList();
            Mediator.Instance.SaveBuildingData();
            DataAccesLayerService.SaveChanges(e =>
            {
                if (e != null)
                {
                }
            });
            IsEnabledTranslusentEndEdit = false;
        }
        public void SaveTransparentElementsChanges()
        {
            Model.TransparentElements = _transparentElementblankRowCollection.RealItems.Cast<NErgy.Building.TransparentElement>().ToList();
            Mediator.Instance.SaveBuildingData();
            DataAccesLayerService.SaveChanges(e =>
            {
                if (e != null)
                {
                }
            });
            IsEnabledTransparentEndEdit = false;
        }


        public void SaveContactWithGroundElementChanges()
        {
            Model.ContactWithGroundElement = _contactWithGroundElementskRowCollection.RealItems.ToList();
            Mediator.Instance.SaveBuildingData();
            DataAccesLayerService.SaveChanges(e =>
            {
                if (e != null)
                {
                }
            });
            IsEnabledContactWithGroundElementEndEdit = false;
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

        private static List<EnumObject<TranslusentElementType>> _translusentElementTypes;
        public static List<EnumObject<TranslusentElementType>> TranslusentElementTypes
        {
            get { return _translusentElementTypes; }
            set
            {
                _translusentElementTypes = value;

            }
        }

        private static List<EnumObject<ContactWithGroundElementTypes>> _contactWithGroundElementTypes;
        public static List<EnumObject<ContactWithGroundElementTypes>> ContactWithGroundElementTypes
        {
            get { return _contactWithGroundElementTypes; }
            set
            {
                _contactWithGroundElementTypes = value;

            }
        }

        private static List<EnumObject<TransparentElementTypes>> _transparentElementTypes;
        public static List<EnumObject<TransparentElementTypes>> TransparentElementTypes
        {
            get { return _transparentElementTypes; }
            set
            {
                _transparentElementTypes = value;

            }
        }


        private static List<EnumObject<PassiveTypes>> _PassiveElementsTypes;
        public static List<EnumObject<PassiveTypes>> PassiveElementsTypes
        {
            get { return _PassiveElementsTypes; }
            set
            {
                _PassiveElementsTypes = value;

            }
        }




        public void UpdateTransparentContextMenu()
        {
            TranslusentElementsMenuItems.Clear();
            TranslusentElementsMenuItems.Add(new MenuItem()
            {
                Header = "ΤΟΤΕ",
                Command = new DelegateCommand(() => Mediator.Instance.OpenStandardsOpaqueElement(this.SelectedTranslusentElement))
            });
            TranslusentElementsMenuItems.Add(new MenuItem()
            {
                Header = "Εισόδος απο Βιβλιοθήκη",
                Command = new DelegateCommand(() => Mediator.Instance.OpenSectionEditorInWindowStrategy(this.SelectedTranslusentElement))
            });

            TranslusentElementsMenuItems.Add(new MenuItem()
            {
                Header = "Προσθηκή σε βιβλιοθήκη",
                Command = new DelegateCommand(() => Mediator.Instance.OpenDialogAddTRanslusentElementToLibrary(this.SelectedTranslusentElement))
            });
 
        }



        #region translusentElemet Cs contextmenu Commands  
        public ICommand OpenTOTEEForTranslusent
        {
            get
            {
                return
                    new DelegateCommand(
                        () => Mediator.Instance.OpenStandardsOpaqueElement(this.SelectedTranslusentElement));
            }
        }

        #endregion
        public void UpdateContactWithGroundElementsContextMenu()
        {
            ContactWithGroundElementsMenuItems.Clear();
            ContactWithGroundElementsMenuItems.Add(new MenuItem()
                                                       {
                                                           Header = "Διαγραφή",
                                                           //Command = new DelegateCommand(() => ((Web.Models.Building)SelectedTreeViewItem).BuildingModel.ThermalZones.Add(new ThermalZone()))
                                                       });
        }

        public IList<MenuItem> ContactWithGroundElementsMenuItems { get; set; }

        public IList<MenuItem> TranslusentElementsMenuItems { get; set; }

        public ICommand DeleteContactWithGroundElementElement
        {
            get
            {
                return new DelegateCommand(RemoveContactWithGroundElementFromCollection);
            }
        }

        public void ContactWithGroundElementKeyDown(Key key)
        {
            if (key == Key.Delete)
            {
                RemoveTranslusentElementFromCollection();
            }
        }

        private void RemoveContactWithGroundElementFromCollection()
        {
            if (_contactWithGroundElementskRowCollection.Contains(SelectedContactWithGroundElement))
            {
                _contactWithGroundElementskRowCollection.Remove(SelectedContactWithGroundElement);
            }
        }

        public ICommand DeleteTransparentElement
        {
            get
            {
                return new DelegateCommand(RemoveTransparentElementFromCollection);
            }
        }

        public void TransparentKeyDown(Key key)
        {
            if (key == Key.Delete)
            {
                RemoveTranslusentElementFromCollection();
            }
        }

        private void RemoveTransparentElementFromCollection()
        {
            if (_transparentElementblankRowCollection.Contains(SelectedTransparentElement))
            {
                _transparentElementblankRowCollection.Remove(SelectedTransparentElement);
            }
        }

        public ICommand DeleteTranslusentElement
        {
            get
            {
                return new DelegateCommand(RemoveTranslusentElementFromCollection);
            }
        }

        public void TranslusentKeyDown(Key key)
        {
            if (key == Key.Delete)
            {
                RemoveTranslusentElementFromCollection();
            }
        }

        private void RemoveTranslusentElementFromCollection()
        {
            if (_opaqueElementsblankRowCollection.Contains(SelectedTranslusentElement))
            {
                _opaqueElementsblankRowCollection.Remove(SelectedTranslusentElement);
            }
        }
    }

    public class SunAreaShellViewModel : ViewModelMasterDetailsBase<SunAreaShellViewModel, SunArea>
    {
        public SunAreaShellViewModel(SunArea sunArea)
        {
            this.Model = sunArea;
            ShellViewModel = new ShellViewModel<Shell>(sunArea);
        }

        public ShellViewModel<Shell> ShellViewModel { get; set; }


        public double TotalArea
        {
            get { return Model.TotalArea; }
            set
            {
                Model.TotalArea = value;
                Mediator.Instance.SaveBuildingData();
                Mediator.Instance.Model.DataAccesLayerService
                    .SaveChanges(e => Mediator.Instance.StatusBar
                        .Display(StatusBarStrings.SavedCompletedSucesfully));
                NotifyPropertyChanged(vm => vm.TotalArea);
            }
        }

        public double AirPenetrattion
        {
            get { return Model.AirPenetrattion; }
            set
            {
                Model.AirPenetrattion = value;
                Mediator.Instance.SaveBuildingData();
                Mediator.Instance.Model.DataAccesLayerService
                    .SaveChanges(e => Mediator.Instance.StatusBar
                        .Display(StatusBarStrings.SavedCompletedSucesfully));
                NotifyPropertyChanged(vm => vm.AirPenetrattion);
            }
        }
    }
    public class NonHeatedAreaShellViewModel : ViewModelMasterDetailsBase<NonHeatedAreaShellViewModel, NonHeatedArea>
    {
        public NonHeatedAreaShellViewModel(NonHeatedArea nonHeatedArea)
        {
            this.Model = nonHeatedArea;
            ShellViewModel = new ShellViewModel<Shell>(nonHeatedArea);
        }

        public ShellViewModel<Shell> ShellViewModel { get; set; }


        public double TotalArea
        {
            get { return Model.TotalArea; }
            set
            {
                Model.TotalArea = value;
                Mediator.Instance.SaveBuildingData();
                Mediator.Instance.Model.DataAccesLayerService
                    .SaveChanges(e => Mediator.Instance.StatusBar
                        .Display(StatusBarStrings.SavedCompletedSucesfully));
                NotifyPropertyChanged(vm => vm.TotalArea);
            }
        }

        public double AirPenetrattion
        {
            get { return Model.AirPenetrattion; }
            set
            {
                Model.AirPenetrattion = value;
                Mediator.Instance.SaveBuildingData();
                Mediator.Instance.Model.DataAccesLayerService
                    .SaveChanges(e => Mediator.Instance.StatusBar
                        .Display(StatusBarStrings.SavedCompletedSucesfully));
                NotifyPropertyChanged(vm => vm.AirPenetrattion);
            }
        }
    }

    public class SeparetingSerfuceShellViewModel : ViewModelMasterDetailsBase<SeparetingSerfuceShellViewModel, SeparatingSurfuce>
    {
        public class SeparatingSefuceObject
        {

            public SeparatingSefuceObject(object _object)
            {
                Object = _object;
            }

            public object Object { get; set; }
            public string Id
            {
                get
                {
                    if (Object is NonHeatedArea)
                    {
                        return "NonHeatedArea" + (Object as NonHeatedArea).Id;
                    }
                    if (Object is SunArea)
                    {
                        return "SunArea" + (Object as SunArea).Id;
                    }
                    return null;
                }
            }
            public string Name
            {
                get
                {
                    if (Object is NonHeatedArea)
                    {
                        return "Μη Θερμενόμενος Χώρος" + (Object as NonHeatedArea).Id;
                    }
                    if (Object is SunArea)
                    {
                        return "Ηλιακός Χώρος" + (Object as SunArea).Id;
                    }
                    return null;
                }
            }

        }
        public SeparetingSerfuceShellViewModel(SeparatingSurfuce nonHeatedArea)
        {
            Model = nonHeatedArea;
            ShellViewModel = new ShellViewModel<Shell>(nonHeatedArea);
            SeparatingAreas = new List<SeparatingSefuceObject>();
        }

        private IList<SeparatingSefuceObject> _separatingAreas;
        public IList<SeparatingSefuceObject> SeparatingAreas
        {
            get { return _separatingAreas; }
            set
            {
                _separatingAreas = value;
                NotifyPropertyChanged(vm => vm.SeparatingAreas);
            }
        }

        public ShellViewModel<Shell> ShellViewModel { get; set; }

        private double _airCirculation;
        public double AirPenetrattion
        {
            get { return Model.AirCirculation; }
            set
            {
                Model.AirCirculation = value;
                Mediator.Instance.SaveBuildingData();
                Mediator.Instance.Model.DataAccesLayerService
                    .SaveChanges(e => Mediator.Instance.StatusBar
                        .Display(StatusBarStrings.SavedCompletedSucesfully));
                NotifyPropertyChanged(vm => vm.AirPenetrattion);
            }
        }

        private SeparatingSefuceObject _separatingArea;
        public SeparatingSefuceObject SelectedSeparatingArea
        {
            get { return new SeparatingSefuceObject(Model.SeparatingArea); }
            set
            {
                _separatingArea = value;
                if (value != null)
                {
                    Model.SeparatingArea = value.Object;
                    Mediator.Instance.SaveBuildingData();
                    Mediator.Instance.Model.DataAccesLayerService
                    .SaveChanges(e => Mediator.Instance.StatusBar
                        .Display(StatusBarStrings.SavedCompletedSucesfully));
                }
                NotifyPropertyChanged(vm => vm.SelectedSeparatingArea);
            }
        }


        private string _separatingAreaId;
        public string SelectedSeparatingAreaId
        {
            get
            {
                return SelectedSeparatingArea != null ? SelectedSeparatingArea.Id : null;
            }
            set
            {
                _separatingAreaId = value;
                SelectedSeparatingArea = SeparatingAreas.FirstOrDefault(s => s.Id == value);
                NotifyPropertyChanged(vm => vm.SelectedSeparatingAreaId);

            }
        }

        private Project _activeProject;
        public void SetUpSeparatingSurfuceAreasList(Project project)
        {
            _activeProject = project;
            project.OnCreateSparatingNewArea += project_OnCreateSparatingNewArea;

            var building = project.Building.FirstOrDefault();
            if (building != null)
            {
                if (building.BuildingModel.NonHeatedArea != null)
                    building.BuildingModel.NonHeatedArea.ToList().ForEach(a => this.SeparatingAreas.Add(new SeparatingSefuceObject(a)));
                if (building.BuildingModel.SunAreas != null)
                    building.BuildingModel.SunAreas.ToList().ForEach(a => this.SeparatingAreas.Add(new SeparatingSefuceObject(a)));
            }
            NotifyPropertyChanged(vm => vm.SeparatingAreas);
        }

        private void project_OnCreateSparatingNewArea(object sender, EventArgs e)
        {
            this.SeparatingAreas.Add(new SeparatingSefuceObject(sender));
            NotifyPropertyChanged(vm => vm.SeparatingAreas);
        }
    }



}