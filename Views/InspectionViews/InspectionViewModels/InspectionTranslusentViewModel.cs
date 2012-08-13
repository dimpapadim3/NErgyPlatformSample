using System;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Controls;
using NErgy.Building.BuildingShell.ConductivityStandards.DefaultVericalU;
//using NErgy.Building.BuildingShell.MaxKENAKConductivityStandards;
using NErgy.Building.BuildingShell.MaxKENAKConductivityStandards;
using NErgy.Silverlight.Web.Models;
using MaxKENAKConductivityStandards = NErgy.Building.BuildingShell.MaxKENAKConductivityStandards;
using MaxKTHKConductivityStandards = NErgy.Building.BuildingShell.MaxKTHKConductivityStandards;
using ConcrateAreaPercentageStandard = NErgy.Building.BuildingShell.ConcrateAreaPercentageStandard;

using NErgy.Building;
using NErgy.Building.BuildingShell.ConductivityStandards;


namespace NErgy.Silverlight.Views.ViewModels
{
    public class InspectionTranslusentViewModel : ViewModelBase<InspectionTranslusentViewModel>
    {

        private static List<string> ConstructionLicencePeriodTypesString = new List<string>()
                                                            {"Πριν από το 1979 (ανυπαρξία κανονισμού)",
                                                                "Περίοδος 1979 - 2010 (ισχύς Κ.Θ.Κ.)",
                                                                "Μετά το 2010(ισχύς Κ.Εν.Α.Κ.)"


                                                                
    };



        private static List<string> ThermalProtectionTypesString = new List<string>()
                                                            {"Χωρίς,θερμομονωτική προστασία",
                                                                "Μερική πρόνοια θερμικής προστασίας",
                                                                "Μετέπειτα επέμβαση σύμφωνα με Κ.Θ.Κ.",
                                                                "Μετέπειτα επέμβαση σύμφωνα με Κεν.Α.Κ.",
                                                                "Χωρίς θερμομονωτική προστασία (μη εφαρμογή Κ.Θ.Κ.)"


                                                                
    };

        public InspectionTranslusentViewModel(TranslusentElement translucentElement)
        {

            KlimaZoneTypes = new List<EnumObject<KlimaticZone>>(GetEnumList<KlimaticZone>());
            StructuralElementTypes = new List<EnumObject<MaxKENAKConductivityStandards.StructuralElementType>>(GetEnumList<MaxKENAKConductivityStandards.StructuralElementType>());

            KTHKKlimaZoneTypes = new List<EnumObject<MaxKTHKConductivityStandards.KlimaticZone>>(GetEnumList<MaxKTHKConductivityStandards.KlimaticZone>());
            KTHKStructuralElementTypes = new List<EnumObject<MaxKTHKConductivityStandards.StructuralElementType>>(GetEnumList<MaxKTHKConductivityStandards.StructuralElementType>());

            BuildingTypes = new List<EnumObject<ConcrateAreaPercentageStandard.BuildingType>>(GetEnumList<ConcrateAreaPercentageStandard.BuildingType>());
            StoriesNumberTypes = new List<EnumObject<ConcrateAreaPercentageStandard.StoriesNumber>>(GetEnumList<ConcrateAreaPercentageStandard.StoriesNumber>());
            LicencedYearTypes = new List<EnumObject<ConcrateAreaPercentageStandard.LicencedYear>>(GetEnumList<ConcrateAreaPercentageStandard.LicencedYear>());
            StoriesNumberTypes = new List<EnumObject<ConcrateAreaPercentageStandard.StoriesNumber>>(GetEnumList<ConcrateAreaPercentageStandard.StoriesNumber>());

            //KTHKElementTypes = new List<EnumObject<ElementType>>(GetEnumList<ElementType>());
            //ContactTypes = new List<EnumObject<ContactType>>(GetEnumList<ContactType>());
            //ProtectionCategories = new List<EnumObject<ProtectionCategory>>(GetEnumList<ProtectionCategory>());

            //HorizontalElementNames = new List<EnumObject<HorizontalElementName>>(GetEnumList<HorizontalElementName>());
            //VerticalElementNames = new List<EnumObject<VerticalElementName>>(GetEnumList<VerticalElementName>());
            //Calculation strategy

            ConstructionLicencePeriodTypes = new List<EnumObject<ConstructionLicencePeriod>>(GetEnumListFromArray<ConstructionLicencePeriod>(ConstructionLicencePeriodTypesString));
            //ThermalProtectionTypes = new List<EnumObject<ThermalProtection>>(GetEnumList<ThermalProtection>());
            //LoadDefaultUg();
            LoadDefaultKENAKStandards();
        }




        #region  MaxKENAKConductivityStandard

        private EnumObject<KlimaticZone> selectedKlimaZoneType;
        public EnumObject<KlimaticZone> SelectedKlimaZoneType
        {
            get { return selectedKlimaZoneType; }
            set
            {
                selectedKlimaZoneType = value;
                NotifyPropertyChanged(m => m.SelectedKlimaZoneType);
            }
        }

        private List<EnumObject<KlimaticZone>> klimaZoneTypes;
        public List<EnumObject<KlimaticZone>> KlimaZoneTypes
        {
            get { return klimaZoneTypes; }
            set
            {
                klimaZoneTypes = value;
                NotifyPropertyChanged(m => m.KlimaZoneTypes);
            }
        }



        private EnumObject<MaxKENAKConductivityStandards.StructuralElementType> selectedStructuralElementType;
        public EnumObject<MaxKENAKConductivityStandards.StructuralElementType> SelectedStructuralElementType
        {
            get { return selectedStructuralElementType; }
            set
            {
                selectedStructuralElementType = value;
                NotifyPropertyChanged(m => m.SelectedStructuralElementType);
            }
        }

        private List<EnumObject<MaxKENAKConductivityStandards.StructuralElementType>> structuralElementTypes;
        public List<EnumObject<MaxKENAKConductivityStandards.StructuralElementType>> StructuralElementTypes
        {
            get { return structuralElementTypes; }
            set
            {
                structuralElementTypes = value;
                NotifyPropertyChanged(m => m.StructuralElementTypes);
            }
        }




        #endregion

        #region  MaxKTHKConductivityStandard

        private EnumObject<MaxKTHKConductivityStandards.KlimaticZone> _selectedKthkKlimaZoneType;
        public EnumObject<MaxKTHKConductivityStandards.KlimaticZone> SelectedKTHKKlimaZoneType
        {
            get { return _selectedKthkKlimaZoneType; }
            set
            {
                _selectedKthkKlimaZoneType = value;
                NotifyPropertyChanged(m => m.SelectedKTHKKlimaZoneType);
            }
        }

        private List<EnumObject<MaxKTHKConductivityStandards.KlimaticZone>> _kthKklimaZoneTypes;
        public List<EnumObject<MaxKTHKConductivityStandards.KlimaticZone>> KTHKKlimaZoneTypes
        {
            get { return _kthKklimaZoneTypes; }
            set
            {
                _kthKklimaZoneTypes = value;
                NotifyPropertyChanged(m => m.KlimaZoneTypes);
            }
        }



        private EnumObject<MaxKTHKConductivityStandards.StructuralElementType> _selectedKthkStructuralElementType;
        public EnumObject<MaxKTHKConductivityStandards.StructuralElementType> SelectedKTHKStructuralElementType
        {
            get { return _selectedKthkStructuralElementType; }
            set
            {
                _selectedKthkStructuralElementType = value;
                NotifyPropertyChanged(m => m.SelectedKTHKStructuralElementType);
            }
        }

        private List<EnumObject<MaxKTHKConductivityStandards.StructuralElementType>> _kThkStructuralElementTypes;
        public List<EnumObject<MaxKTHKConductivityStandards.StructuralElementType>> KTHKStructuralElementTypes
        {
            get { return _kThkStructuralElementTypes; }
            set
            {
                _kThkStructuralElementTypes = value;
                NotifyPropertyChanged(m => m.KTHKStructuralElementTypes);
            }
        }
        #endregion

        #region ConcrateAreaPercentageStandard


        private EnumObject<ConcrateAreaPercentageStandard.BuildingType> selectedBuildingType;
        public EnumObject<ConcrateAreaPercentageStandard.BuildingType> SelectedBuildingType
        {
            get { return selectedBuildingType; }
            set
            {
                selectedBuildingType = value;
                NotifyPropertyChanged(m => m.SelectedBuildingType);
            }
        }

        private List<EnumObject<ConcrateAreaPercentageStandard.BuildingType>> buildingTypes;
        public List<EnumObject<ConcrateAreaPercentageStandard.BuildingType>> BuildingTypes
        {
            get { return buildingTypes; }
            set
            {
                buildingTypes = value;
                NotifyPropertyChanged(m => m.BuildingTypes);
            }
        }

        private EnumObject<ConcrateAreaPercentageStandard.LicencedYear> selectedLicencedYear;
        public EnumObject<ConcrateAreaPercentageStandard.LicencedYear> SelectedLicencedYear
        {
            get { return selectedLicencedYear; }
            set
            {
                selectedLicencedYear = value;
                NotifyPropertyChanged(m => m.SelectedLicencedYear);
            }
        }

        private List<EnumObject<ConcrateAreaPercentageStandard.LicencedYear>> licencedYearTypes;
        public List<EnumObject<ConcrateAreaPercentageStandard.LicencedYear>> LicencedYearTypes
        {
            get { return licencedYearTypes; }
            set
            {
                licencedYearTypes = value;
                NotifyPropertyChanged(m => m.LicencedYearTypes);
            }
        }

        private EnumObject<ConcrateAreaPercentageStandard.StoriesNumber> selectedStoriesNumberType;
        public EnumObject<ConcrateAreaPercentageStandard.StoriesNumber> SelectedStoriesNumberType
        {
            get { return selectedStoriesNumberType; }
            set
            {
                selectedStoriesNumberType = value;
                NotifyPropertyChanged(m => m.SelectedStoriesNumberType);
            }
        }

        private List<EnumObject<ConcrateAreaPercentageStandard.StoriesNumber>> stroriesNumberTypes;
        public List<EnumObject<ConcrateAreaPercentageStandard.StoriesNumber>> StoriesNumberTypes
        {
            get { return stroriesNumberTypes; }
            set
            {
                stroriesNumberTypes = value;
                NotifyPropertyChanged(m => m.StoriesNumberTypes);
            }
        }

        #endregion

        #region CalculationstrategyConductivityStandard

        private EnumObject<ConstructionLicencePeriod> selectedConstructionLicencePeriodType;
        public EnumObject<ConstructionLicencePeriod> SelectedConstructionLicencePeriodType
        {
            get { return selectedConstructionLicencePeriodType; }
            set
            {
                selectedConstructionLicencePeriodType = value;
                NotifyPropertyChanged(m => m.SelectedConstructionLicencePeriodType);
            }
        }

        private List<EnumObject<ConstructionLicencePeriod>> constructionLicencePeriodTypes;
        public List<EnumObject<ConstructionLicencePeriod>> ConstructionLicencePeriodTypes
        {
            get { return constructionLicencePeriodTypes; }
            set
            {
                constructionLicencePeriodTypes = value;
                NotifyPropertyChanged(m => m.constructionLicencePeriodTypes);
            }
        }

        private EnumObject<ThermalProtection> selectedThermalProtectionType;
        public EnumObject<ThermalProtection> SelectedThermalProtectionType
        {
            get { return selectedThermalProtectionType; }
            set
            {
                selectedThermalProtectionType = value;
                NotifyPropertyChanged(m => m.SelectedThermalProtectionType);
            }
        }

        private List<EnumObject<ThermalProtection>> thermalProtectionTypes;
        public List<EnumObject<ThermalProtection>> ThermalProtectionTypes
        {
            get { return thermalProtectionTypes; }
            set
            {
                thermalProtectionTypes = value;
                NotifyPropertyChanged(m => m.ThermalProtectionTypes);
            }
        }

        #endregion

        public void LoadThermalProtectionTypes()
        {
            if (SelectedConstructionLicencePeriodType.Object == ConstructionLicencePeriod.PeriodBefore1979)
            {
                ThermalProtectionTypes = new List<EnumObject<ThermalProtection>>()
                                             {
                                                 new EnumObject<ThermalProtection>("Χωρίς θερμομονωτική προστασία",ThermalProtection.XwrisThermomonotikiProstasia,1),
                                                 new EnumObject<ThermalProtection>("Μερική πρόνοια θερμικής προστασίας (εξαρχής πρόνοια ή μετέπειτα πέμβαση)",ThermalProtection.MerikiPronia,2),
                                                 new EnumObject<ThermalProtection>("Μετέπειτα επέμβαση σύμφωνα με Κ.Θ.Κ.",ThermalProtection.MetepitaParemvasiKTHK,3),
                                                 new EnumObject<ThermalProtection>("Μετέπειτα επέμβαση σύμφωνα με Κεν.Α.Κ.",ThermalProtection.MetepitaParemvasiKENAK,4)
                                             };
            }
            else if (SelectedConstructionLicencePeriodType.Object == ConstructionLicencePeriod.Period1979To2010)
            {
                ThermalProtectionTypes = new List<EnumObject<ThermalProtection>>()
                                             {
                                                 new EnumObject<ThermalProtection>("Χωρίς θερμομονωτική προστασία (μη εφαρμογή Κ.Θ.Κ.)",ThermalProtection.XwrisThermomonotikiProstasia,1),
                                                 new EnumObject<ThermalProtection>("Πλημμελής εφαρμογή Κ.Θ.Κ.",ThermalProtection.PlimelisEfarmogiKTHK,2),
                                                 new EnumObject<ThermalProtection>("Σύμφωνα με απαιτήσεις Κ.Θ.Κ.",ThermalProtection.SymfwnaMeKTHK,3),
                                                 new EnumObject<ThermalProtection>("Σύμφωνα με απαιτήσεις Κ.Εν.Α.Κ. (εξαρχής πρόνοιαή μετέπειταεπέμβαση)",ThermalProtection.PlirisEfarmogiKENAK,4)
                                             };
            }
            else if (SelectedConstructionLicencePeriodType.Object == ConstructionLicencePeriod.PeriodAfter2010)
            {
                ThermalProtectionTypes = new List<EnumObject<ThermalProtection>>()
                                             {
                                                 new EnumObject<ThermalProtection>("Πλημμελής εφαρμογή Κ.Εν.Α.Κ.",ThermalProtection.PlimelisEfarmogiKenak,1),
                                                 new EnumObject<ThermalProtection>("Πλήρης εφαρμογή Κ.Εν.Α.Κ.",ThermalProtection.PlirisEfarmogiKENAK,2),
                                           
                                             };
            }

        }

        private UCalculationStategy uCalculationStategyActualBuilding;
        public UCalculationStategy UCalculationStategyActualBuilding
        {
            get { return uCalculationStategyActualBuilding; }
            set
            {
                uCalculationStategyActualBuilding = value;
                NotifyPropertyChanged(m => m.UCalculationStategyActualBuilding);
            }
        }

        private UCalculationStategy uCalculationStategyReferanceBuilding;
        public UCalculationStategy UCalculationStategyReferanceBuilding
        {
            get { return uCalculationStategyReferanceBuilding; }
            set
            {
                uCalculationStategyReferanceBuilding = value;
                NotifyPropertyChanged(m => m.UCalculationStategyReferanceBuilding);
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// Πινακας 3.3
        ///Μέγιστος επιτρεπόμενος συντελεστής θερμοπερατότητας δομικών στοιχείων, σύμφωνα με τον
        ///Κανονισμό Θερμομόνωσης Κτιρίων (1979) για τις τρεις κλιματικές ζώνες στην Ελλάδα
        private double elementUKENAKMax;
        public double ElementUKENAKMax
        {
            get { return elementUKENAKMax; }
            set
            {
                elementUKENAKMax = value;
                NotifyPropertyChanged(m => m.ElementUKENAKMax);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// Πινακας 3.3
        ///Μέγιστος επιτρεπόμενος συντελεστής θερμοπερατότητας δομικών στοιχείων, σύμφωνα με τον
        ///Κανονισμό Θερμομόνωσης Κτιρίων (1979) για τις τρεις κλιματικές ζώνες στην Ελλάδα
        private double elementUKTHKMax;
        public double ElementUKTHKMax
        {
            get { return elementUKTHKMax; }
            set
            {
                elementUKTHKMax = value;
                NotifyPropertyChanged(m => m.ElementUKTHKMax);
            }
        }



        private double defaultPercentage;
        public double DefaultPercentage
        {
            get { return defaultPercentage; }
            set
            {
                defaultPercentage = value;
                NotifyPropertyChanged(m => m.DefaultPercentage);
            }
        }

        public object OpaqueDetailsView
        {
            get
            {

                if (uCalculationStategyActualBuilding is KenakMaxUCalculationStategy)
                    return new InspectionOpaqueKENAKMaxUView() { ViewModel = new InspectionTranslusentMaxKENAKViewModel() };
                if (uCalculationStategyActualBuilding is DefaultUCalculationStategy)
                    return new InspectionOpaqueKTHKStandardesView() { ViewModel = new InspectionTranslusentKTHKVerticalElementsViewModel() };
                if (uCalculationStategyActualBuilding is KthkMaxUCalculationStategy)
                    return new InspectionOpaqueKTHKMaxUView() { ViewModel = new InspectionTranslusentMaxKTHKViewModel() };
                return null;
            }
        }

        private void DefaultMaxKENAKUconductivityStandartLoaded(List<MaxKENAKConductivityStandards.ElementUMax> entities, Exception error)
        {
            // If no error is returned, set the model to entities
            if (error == null)
            {
                ElementUKENAKMax = entities.FirstOrDefault().U;
            }
            // Otherwise notify view that there's an error
            else
            {
                //NotifyError("Unable to retrieve items", error);
            }

            //// Set SelectedItem to the first item
            //if (BuildingUsages.Count > 0)
            //{
            //    //SelectedUsage = BuildingUsages[0];
            //}

            //// We're done
            //IsBusy = false;
        }

        private void DefaulMaxKTHKtUconductivityStandartLoaded(List<MaxKTHKConductivityStandards.ElementUMax> entities, Exception error)
        {
            // If no error is returned, set the model to entities
            if (error == null)
            {
                ElementUKTHKMax = entities.FirstOrDefault().U;
            }

        }

        private void ConcrateAreaPercentageStandardLoaded(List<ConcrateAreaPercentageStandard.DefaultPercentage> entities, Exception error)
        {
            // If no error is returned, set the model to entities
            if (error == null)
            {
                DefaultPercentage = entities.FirstOrDefault().percentage;
            }
            // Otherwise notify view that there's an error
            else
            {
                //NotifyError("Unable to retrieve items", error);
            }

            //// Set SelectedItem to the first item
            //if (BuildingUsages.Count > 0)
            //{
            //    //SelectedUsage = BuildingUsages[0];
            //}

            //// We're done
            //IsBusy = false;
        }


        //  private List<DefaultUgStandard> DefaultUgStandards;
        private void DefaultUgconductivityStandartLoaded(List<DefaultUgStandard> entities, Exception error)
        {

            //if (error == null)
            //{
            //    DefaultUgStandards = entities;
            //    BindToEnumObjects(DefaultUgStandards, KTHKElementTypes);
            //}

        }

        private void BindToEnumObjects(List<DefaultUgStandard> defaultUgStandards, List<EnumObject<ElementType>> enumObjects)
        {
            enumObjects.ForEach(eo =>
                                    {
                                        eo.DataBaseObject = defaultUgStandards.FirstOrDefault(db => db.GlassTypeID == (int)eo.Object);
                                    });
        }

        public void LoadDefaultUg()
        {
            StandardsServiceLayer.GetDefaultUgStandard(DefaultUgconductivityStandartLoaded, 0);
        }

        private void LoadDefaultKENAKStandards()
        {
            if (selectedKlimaZoneType != null)
                if (selectedStructuralElementType != null)
                    StandardsServiceLayer.GetDefaultMaxKENAKUconductivityStandart(DefaultMaxKENAKUconductivityStandartLoaded,
                                                                                  (int)selectedKlimaZoneType.Object,
                                                                                  (int)selectedStructuralElementType.Object);
        }


        public void UpdateSelectedConstructionLicencePeriodType(IList addedItems)
        {
            LoadThermalProtectionTypes();

        }

        public string UpdateSelectedThermalProtectionTypese(IList addedItems)
        {
            if (SelectedConstructionLicencePeriodType != null)
                if (SelectedThermalProtectionType != null)
                {
                    uCalculationStategyActualBuilding =
                        ConstrcuctionLicenceList.UCalculationStategyActualBuilding(SelectedConstructionLicencePeriodType.Object,
                                                                                   SelectedThermalProtectionType.Object);
                    return uCalculationStategyActualBuilding.Description;
                }
            return "";
        }



    }

    public class InspectionTranslusentKTHKVerticalElementsViewModel : ViewModelBase<InspectionTranslusentKTHKVerticalElementsViewModel>
    {
        private static List<string> KTHKElementTypesStrings = new List<string>()
                                                            {"Στοιχείο φέροντος οργανισμού οπλισμένου σκυροδέματος (πάχους μικρότερου των 80 cm)",
                                                                "Οπτοπλινθοδομή, φέρουσα ή πλήρωσης (με ή χωρίς κλειστό διάκενο αέρος)",
                                                                "Δρομική οπτοπλινθοδομή",
                                                                "Αργολιθοδομή",
                                                                "Επιστεγάσεις (με ή χωρίς ψευδοροφή)",
                                                                "Δάπεδα με επικάλυψη παντός τύπου (ξύλο, μάρμαρο, πλακάκι, μωσαϊκό κ.τ.λ.)"


                                                                
    };

        private static List<string> VerticalElementNamesStrings = new List<string>()
                                                            {"Ανεπίχριστο από τη μία ή τις δύο όψεις.",
                                                                " Επιχρισμένο και από τις δύο όψεις.",
                                                                "Επενδεδυμένο με απλή ή διακοσμητική οπτοπλινθοδομή.",
                                                                "Επενδεδυμένο με αργολιθοδομή.",
                                                                "Επενδεδυμένο με μαρμάρινες πλάκες.",
                                                                "Επενδεδυμένο με γυψοσανίδα,τσιμεντοσανίδα, ξυλοσανίδα ή άλλες πλάκες."
                                                            
    };

        private static List<string> ContactTypesStrings = new List<string>()
                                                            {"Σε επαφή με αέρα",
                                                                " Σε επαφή με μη θερμαινόμ.χώρο",
                                                                "Σε επαφή με έδαφος",
                                                              };


        private static List<string> ProtectionCategoriesStrings = new List<string>()
                                                            {"Χωρίς θερμομονωτική προστασία",
                                                                "Με ανεπαρκή θερμομονωτική προστασία κατά Κ.Θ.Κ.",
                                                            };


        public InspectionTranslusentKTHKVerticalElementsViewModel()
        {
            KTHKElementTypes = new List<EnumObject<ElementType>>(GetEnumListWithArraySupport<ElementType>(KTHKElementTypesStrings));
            ContactTypes = new List<EnumObject<ContactType>>(GetEnumListWithArraySupport<ContactType>(ContactTypesStrings));
            ProtectionCategories = new List<EnumObject<ProtectionCategory>>(GetEnumListWithArraySupport<ProtectionCategory>(ProtectionCategoriesStrings));

            HorizontalElementNames = new List<EnumObject<HorizontalElementName>>(GetEnumList<HorizontalElementName>());
            VerticalElementNames = new List<EnumObject<VerticalElementName>>(GetEnumListWithArraySupport<VerticalElementName>(VerticalElementNamesStrings));
        }
        #region Conductivity Standards
        //        Πίνακας 3.4α. Τυπικές τιμές του συντελεστή θερμοπερατότητας για υφιστάμενα κατακόρυφα αδιαφανή δομικά
        //στοιχεία που συναντώνται σε κτήρια η οικοδομική άδεια των οποίων εκδόθηκε πριν από την
        //εφαρμογή του Κανονισμού Θερμομόνωσης Κτιρίων (1979).

        private EnumObject<ElementType> selectedElementType;
        public EnumObject<ElementType> SelectedKTHKElementType
        {
            get { return selectedElementType; }
            set
            {
                selectedElementType = value;

                NotifyPropertyChanged(m => m.SelectedKTHKElementType);
                TryLoadDefaultKTHKtandards();
            }
        }

        //public DefaultUgStandard SelectedDefaultUgStandard { get; set; }
        private List<EnumObject<ElementType>> elementTypes;
        public List<EnumObject<ElementType>> KTHKElementTypes
        {
            get { return elementTypes; }
            set
            {
                elementTypes = value;
                NotifyPropertyChanged(m => m.KTHKElementTypes);
            }
        }

        private EnumObject<ContactType> selectedKTHKContactType;
        public EnumObject<ContactType> SelectedKTHKContactType
        {
            get { return selectedKTHKContactType; }
            set
            {
                selectedKTHKContactType = value;

                NotifyPropertyChanged(m => m.SelectedKTHKContactType); TryLoadDefaultKTHKtandards();
            }
        }



        private List<EnumObject<ContactType>> contactTypes;
        public List<EnumObject<ContactType>> ContactTypes
        {
            get { return contactTypes; }
            set
            {
                contactTypes = value;
                NotifyPropertyChanged(m => m.SelectedKTHKContactType);
            }
        }

        private List<EnumObject<ProtectionCategory>> protectionCategories;
        public List<EnumObject<ProtectionCategory>> ProtectionCategories
        {
            get { return protectionCategories; }
            set
            {
                protectionCategories = value;
                NotifyPropertyChanged(m => m.SelectedKTHKContactType);
            }
        }

        private EnumObject<ProtectionCategory> selectedProtectionCategory;
        public EnumObject<ProtectionCategory> SelectedKTHKProtectionCategory
        {
            get { return selectedProtectionCategory; }
            set
            {
                selectedProtectionCategory = value;

                NotifyPropertyChanged(m => m.SelectedKTHKProtectionCategory); TryLoadDefaultKTHKtandards();
            }
        }

        private List<EnumObject<HorizontalElementName>> horizontalElementNames;
        public List<EnumObject<HorizontalElementName>> HorizontalElementNames
        {
            get { return horizontalElementNames; }
            set
            {
                horizontalElementNames = value;
                NotifyPropertyChanged(m => m.HorizontalElementNames);
            }
        }

        private EnumObject<HorizontalElementName> selectedHorizontalElementNames;
        public EnumObject<HorizontalElementName> SelectedHorizontalElementNames
        {
            get { return selectedHorizontalElementNames; }
            set
            {
                selectedHorizontalElementNames = value;
                NotifyPropertyChanged(m => m.SelectedHorizontalElementNames); TryLoadDefaultKTHKtandards();
            }
        }


        private List<EnumObject<VerticalElementName>> verticalElementNames;
        public List<EnumObject<VerticalElementName>> VerticalElementNames
        {
            get { return verticalElementNames; }
            set
            {
                verticalElementNames = value;
                NotifyPropertyChanged(m => m.VerticalElementNames);
            }
        }

        private EnumObject<VerticalElementName> selectedVerticalElementNames;
        public EnumObject<VerticalElementName> SelectedKTHKVerticalElementNames
        {
            get { return selectedVerticalElementNames; }
            set
            {
                selectedVerticalElementNames = value;
                NotifyPropertyChanged(m => m.SelectedKTHKVerticalElementNames); TryLoadDefaultKTHKtandards();
            }
        }

        #endregion


        #region Kthk
        //Πίνακας 3.4α. Τυπικές τιμές του συντελεστή θερμοπερατότητας για υφιστάμενα κατακόρυφα αδιαφανή δομικά
        //στοιχεία που συναντώνται σε κτήρια η οικοδομική άδεια των οποίων εκδόθηκε πριν από την
        //εφαρμογή του Κανονισμού Θερμομόνωσης Κτιρίων (1979).
        private double elementUKTHKVerticalElementU;
        public double ElementUKTHKVerticalElementU
        {
            get { return elementUKTHKVerticalElementU; }
            set
            {
                elementUKTHKVerticalElementU = value;
                NotifyPropertyChanged(m => m.ElementUKTHKVerticalElementU);
            }
        }



        private void TryLoadDefaultKTHKtandards()
        {
            if (SelectedKTHKContactType != null
                && SelectedKTHKElementType != null
                && SelectedKTHKProtectionCategory != null
                && SelectedKTHKVerticalElementNames != null)
            {
                StandardsServiceLayer.GetDefaultMaxKTHKUconductivityStandartd((entities, e) =>
                    // ReSharper disable PossibleInvalidOperationException
                    {
                        var u1 = entities.FirstOrDefault(c =>
                        c.ContactType == (int)SelectedKTHKContactType.Object &&
                        c.ElementType == (int)SelectedKTHKElementType.Object &&
                        c.ProtectionCategory == (int)SelectedKTHKProtectionCategory.Object &&
                        c.VerticalElementName == (int)SelectedKTHKVerticalElementNames.Object
                        );
                        var u = u1 != null ? u1.U : null;
                        if (u != null)
                            ElementUKTHKVerticalElementU = u.HasValue ? u.Value : 0;
                    },
                    // ReSharper restore PossibleInvalidOperationException
                                                                                (int)SelectedKTHKContactType.Object,
                                                                                (int)SelectedKTHKElementType.Object,
                                                                                (int)SelectedKTHKProtectionCategory.Object,
                                                                                (int)SelectedKTHKVerticalElementNames.Object);
            }

        }

        //private void DefaultMaxKENAKUconductivityStandartLoaded(List<DefaultUVerticalKTHK> entities, Exception error)
        //{
        //    // If no error is returned, set the model to entities
        //    if (error == null)
        //    {

        //        ElementUKTHKVerticalElementU = entities.FirstOrDefault(c=>c.).U ?? entities.FirstOrDefault().U.Value;
        //    }
        //    // Otherwise notify view that there's an error
        //    else
        //    {
        //        //NotifyError("Unable to retrieve items", error);
        //    }

        //}

        #endregion

    }
    public class InspectionTranslusentMaxKENAKViewModel : ViewModelBase<InspectionTranslusentMaxKENAKViewModel>
    {

        public InspectionTranslusentMaxKENAKViewModel(
            //TranslusentElement translucentElement
            )
        {

            KlimaZoneTypes = new List<EnumObject<KlimaticZone>>(GetEnumList<KlimaticZone>());
            StructuralElementTypes = new List<EnumObject<MaxKENAKConductivityStandards.StructuralElementType>>(GetEnumList<MaxKENAKConductivityStandards.StructuralElementType>());

        }




        #region  MaxKENAKConductivityStandard

        private EnumObject<KlimaticZone> selectedKlimaZoneType;
        public EnumObject<KlimaticZone> SelectedKlimaZoneType
        {
            get { return selectedKlimaZoneType; }
            set
            {
                selectedKlimaZoneType = value;
                NotifyPropertyChanged(m => m.SelectedKlimaZoneType);
                TryGetDefaultMaxU();
            }
        }

        private List<EnumObject<KlimaticZone>> klimaZoneTypes;
        public List<EnumObject<KlimaticZone>> KlimaZoneTypes
        {
            get { return klimaZoneTypes; }
            set
            {
                klimaZoneTypes = value;
                NotifyPropertyChanged(m => m.KlimaZoneTypes);
            }
        }



        private EnumObject<MaxKENAKConductivityStandards.StructuralElementType> selectedStructuralElementType;
        public EnumObject<MaxKENAKConductivityStandards.StructuralElementType> SelectedStructuralElementType
        {
            get { return selectedStructuralElementType; }
            set
            {
                selectedStructuralElementType = value;
                NotifyPropertyChanged(m => m.SelectedStructuralElementType);
                TryGetDefaultMaxU();
            }
        }

        private List<EnumObject<MaxKENAKConductivityStandards.StructuralElementType>> structuralElementTypes;
        public List<EnumObject<MaxKENAKConductivityStandards.StructuralElementType>> StructuralElementTypes
        {
            get { return structuralElementTypes; }
            set
            {
                structuralElementTypes = value;
                NotifyPropertyChanged(m => m.StructuralElementTypes);
            }
        }


        #endregion


        /// <summary>
        /// 
        /// </summary>
        /// Πινακας 3.3
        ///Μέγιστος επιτρεπόμενος συντελεστής θερμοπερατότητας δομικών στοιχείων, σύμφωνα με τον
        ///Κανονισμό Θερμομόνωσης Κτιρίων (1979) για τις τρεις κλιματικές ζώνες στην Ελλάδα
        private double elementUKENAKMax;
        public double ElementUKENAKMax
        {
            get { return elementUKENAKMax; }
            set
            {
                elementUKENAKMax = value;
                NotifyPropertyChanged(m => m.ElementUKENAKMax);
            }
        }

        public void TryGetDefaultMaxU()
        {
            if (SelectedKlimaZoneType != null && SelectedStructuralElementType != null)
            {

                var element = KenakThermalConductivitiesMaxStandard.Instant.ElementsU
                        .FirstOrDefault(e => (int)e.KlimaticZone == (int)SelectedKlimaZoneType.Object &&
                                             (int)e.StructuralElement == (int)SelectedStructuralElementType.Object
                        );
                ElementUKENAKMax = element != null ? element.U : 0;

            }
        }
    }
    public class InspectionTranslusentMaxKTHKViewModel : ViewModelBase<InspectionTranslusentMaxKTHKViewModel>
    {

        public InspectionTranslusentMaxKTHKViewModel(
            //TranslusentElement translucentElement
            )
        {

            KlimaZoneTypes = new List<EnumObject<MaxKTHKConductivityStandards.KlimaticZone>>(GetEnumList<MaxKTHKConductivityStandards.KlimaticZone>());
            StructuralElementTypes = new List<EnumObject<MaxKTHKConductivityStandards.StructuralElementType>>(GetEnumList<MaxKTHKConductivityStandards.StructuralElementType>());

        }




        #region  MaxKENAKConductivityStandard

        private EnumObject<MaxKTHKConductivityStandards.KlimaticZone> selectedKlimaZoneType;
        public EnumObject<MaxKTHKConductivityStandards.KlimaticZone> SelectedKlimaZoneType
        {
            get { return selectedKlimaZoneType; }
            set
            {
                selectedKlimaZoneType = value;
                NotifyPropertyChanged(m => m.SelectedKlimaZoneType);
                TryGetDefaultMaxU();
            }
        }

        private List<EnumObject<MaxKTHKConductivityStandards.KlimaticZone>> klimaZoneTypes;
        public List<EnumObject<MaxKTHKConductivityStandards.KlimaticZone>> KlimaZoneTypes
        {
            get { return klimaZoneTypes; }
            set
            {
                klimaZoneTypes = value;
                NotifyPropertyChanged(m => m.KlimaZoneTypes);
            }
        }



        private EnumObject<MaxKTHKConductivityStandards.StructuralElementType> selectedStructuralElementType;
        public EnumObject<MaxKTHKConductivityStandards.StructuralElementType> SelectedStructuralElementType
        {
            get { return selectedStructuralElementType; }
            set
            {
                selectedStructuralElementType = value;
                NotifyPropertyChanged(m => m.SelectedStructuralElementType);
                TryGetDefaultMaxU();
            }
        }

        private List<EnumObject<MaxKTHKConductivityStandards.StructuralElementType>> structuralElementTypes;
        public List<EnumObject<MaxKTHKConductivityStandards.StructuralElementType>> StructuralElementTypes
        {
            get { return structuralElementTypes; }
            set
            {
                structuralElementTypes = value;
                NotifyPropertyChanged(m => m.StructuralElementTypes);
            }
        }


        #endregion


        private double elementUKENAKMax;
        public double ElementUKENAKMax
        {
            get { return elementUKENAKMax; }
            set
            {
                elementUKENAKMax = value;
                NotifyPropertyChanged(m => m.ElementUKENAKMax);
            }
        }

        public void TryGetDefaultMaxU()
        {
            if (SelectedKlimaZoneType != null && SelectedStructuralElementType != null)
            {

                var element = KenakThermalConductivitiesMaxStandard.Instant.ElementsU
                        .FirstOrDefault(e => (int)e.KlimaticZone == (int)SelectedKlimaZoneType.Object &&
                                             (int)e.StructuralElement == (int)SelectedStructuralElementType.Object
                        );
                ElementUKENAKMax = element != null ? element.U : 0;

            }
        }
    }

}
