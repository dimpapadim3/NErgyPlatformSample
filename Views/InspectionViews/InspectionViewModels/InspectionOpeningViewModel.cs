using System;
using System.Collections.Generic;
using System.Linq;
using NErgy.Building;
using NErgy.Building.BuildingShell.ConductivityStandards;
using NErgy.Silverlight.Models;
using NErgy.Silverlight.Web.Models;
using FrameType = NErgy.Building.BuildingShell.ConductivityStandards.FrameType;

namespace NErgy.Silverlight.Views.ViewModels
{
    public class InspectionOpeningViewModel : ViewModelBase<InspectionOpeningViewModel>
    {
        /// <summary>
        /// 
        /// </summary>
        /// 3.2.3.2. Συντελεστής πλαισίου
        private double defaultUf;

        /// <summary>
        /// 
        /// </summary>
        /// 3.2.3.1. Συντελεστής θερμοπερατότητας υαλοπίνακα
        private double defaultUg;

        /// <summary>
        /// 
        /// </summary>
        ///Πίνακας 3.12. Τυπικές τιμές συντελεστή θερμοπερατότητας κουφωμάτων Uw [W/(m2K)] ανάλογα με τον τύπο
        /// του πλαισίου, τον τύπο του υαλοπίνακα και το ποσοστό πλαισίου.
        private double defaultUw;

        public InspectionOpeningViewModel(ITransparentElement opening)
        {
            OpeningViewModel = new OpeningViewModel(opening);

            FrameTypes = new List<EnumObject<FrameType>>(GetEnumList<FrameType>());
            GlassTypes =new List<EnumObject<DefaultUgConductivityStandard.OpeningsGlassType>>(
                    GetEnumList<DefaultUgConductivityStandard.OpeningsGlassType>());

            GlassFrameJunctionTypes = new List<EnumObject<DefaultPsigConductivityStandard.GlassFrameJunctionType>>(
                    GetEnumList<DefaultPsigConductivityStandard.GlassFrameJunctionType>());

            LayersNumbers =new List<EnumObject<DefaultUwConductivityStandard.LayersNumber>>(
                    GetEnumList<DefaultUwConductivityStandard.LayersNumber>());
            FramePersentageTypes =new List<EnumObject<DefaultUwConductivityStandard.FramePersentageType>>(
                    GetEnumList<DefaultUwConductivityStandard.FramePersentageType>());
            AirGapTypes =new List<EnumObject<DefaultUwConductivityStandard.AirGap>>(
                    GetEnumList<DefaultUwConductivityStandard.AirGap>());
            GlassUwTypes =new List<EnumObject<DefaultUwConductivityStandard.GlassType>>(
                    GetEnumList<DefaultUwConductivityStandard.GlassType>());
        }

        #region Properties

        private OpeningViewModel OpeningViewModel { get; set; }

        #endregion //Properties

        #region DefaultUgConductivityStandard

  
        private EnumObject<DefaultUgConductivityStandard.OpeningsGlassType> selectedglassType;
        public EnumObject<DefaultUgConductivityStandard.OpeningsGlassType> SelectedglassType
        {
            get { return selectedglassType; }
            set
            {
                selectedglassType = value;
                NotifyPropertyChanged(m => m.SelectedglassType);
            }
        }

        private List<EnumObject<DefaultUgConductivityStandard.OpeningsGlassType>> glassTypes;
        public List<EnumObject<DefaultUgConductivityStandard.OpeningsGlassType>> GlassTypes
        {
            get { return glassTypes; }
            set
            {
                glassTypes = value;
                NotifyPropertyChanged(m => m.GlassTypes);
            }
        }


        #endregion

        #region DefaultpsigConductivityStandard
 
        private EnumObject<DefaultPsigConductivityStandard.GlassFrameJunctionType> selectedGlassFrameJunctionType;
        public EnumObject<DefaultPsigConductivityStandard.GlassFrameJunctionType> SelectedGlassFrameJunctionType
        {
            get { return selectedGlassFrameJunctionType; }
            set
            {
                selectedGlassFrameJunctionType = value;
                NotifyPropertyChanged(m => m.SelectedGlassFrameJunctionType);
            }
        }

        private List<EnumObject<DefaultPsigConductivityStandard.GlassFrameJunctionType>> glassFrameJunctionTypes;
        public List<EnumObject<DefaultPsigConductivityStandard.GlassFrameJunctionType>> GlassFrameJunctionTypes
        {
            get { return glassFrameJunctionTypes; }
            set
            {
                glassFrameJunctionTypes = value;
                NotifyPropertyChanged(m => m.GlassFrameJunctionTypes);
            }
        }


        #endregion
 
        #region DefaultUfConductivityStandard

        private List<EnumObject<FrameType>> frameTypes;
        private EnumObject<FrameType> selectedFrameType;

        public EnumObject<FrameType> SelectedFrameType
        {
            get { return selectedFrameType; }
            set
            {
                selectedFrameType = value;
                NotifyPropertyChanged(m => m.SelectedFrameType);
            }
        }

        public List<EnumObject<FrameType>> FrameTypes
        {
            get { return frameTypes; }
            set
            {
                frameTypes = value;
                NotifyPropertyChanged(m => m.FrameTypes);
            }
        }

        #endregion

        #region DefaultUwConductivityStandard

        #region LayersNumbers

        private List<EnumObject<DefaultUwConductivityStandard.LayersNumber>> layersNumber;
        private EnumObject<DefaultUwConductivityStandard.LayersNumber> selectedLayersNumber;

        public EnumObject<DefaultUwConductivityStandard.LayersNumber> SelectedLayersNumber
        {
            get { return selectedLayersNumber; }
            set
            {
                selectedLayersNumber = value;
                NotifyPropertyChanged(m => m.SelectedLayersNumber);
            }
        }

        public List<EnumObject<DefaultUwConductivityStandard.LayersNumber>> LayersNumbers
        {
            get { return layersNumber; }
            set
            {
                layersNumber = value;
                NotifyPropertyChanged(m => m.LayersNumbers);
            }
        }

        #endregion

        #region FramePersentageTypes

        private List<EnumObject<DefaultUwConductivityStandard.FramePersentageType>> framePersentageTypes;
        private EnumObject<DefaultUwConductivityStandard.FramePersentageType> selectedframePersentageType;

        public EnumObject<DefaultUwConductivityStandard.FramePersentageType> SelectedFramePersentageType
        {
            get { return selectedframePersentageType; }
            set
            {
                selectedframePersentageType = value;
                NotifyPropertyChanged(m => m.SelectedLayersNumber);
            }
        }

        public List<EnumObject<DefaultUwConductivityStandard.FramePersentageType>> FramePersentageTypes
        {
            get { return framePersentageTypes; }
            set
            {
                framePersentageTypes = value;
                NotifyPropertyChanged(m => m.FramePersentageTypes);
            }
        }

        #endregion

        #region AirGapTypes

        private List<EnumObject<DefaultUwConductivityStandard.AirGap>> airGapTypes;
        private EnumObject<DefaultUwConductivityStandard.AirGap> selectedAirGapType;

        public EnumObject<DefaultUwConductivityStandard.AirGap> SelectedAirGapType
        {
            get { return selectedAirGapType; }
            set
            {
                selectedAirGapType = value;
                NotifyPropertyChanged(m => m.SelectedAirGapType);
            }
        }

        public List<EnumObject<DefaultUwConductivityStandard.AirGap>> AirGapTypes
        {
            get { return airGapTypes; }
            set
            {
                airGapTypes = value;
                NotifyPropertyChanged(m => m.AirGapTypes);
            }
        }

        #endregion

        #region GlassUwTypes

        private List<EnumObject<DefaultUwConductivityStandard.GlassType>> glassUwTypes;
        private EnumObject<DefaultUwConductivityStandard.GlassType> selectedUwglassType;

        public EnumObject<DefaultUwConductivityStandard.GlassType> SelectedUwglassType
        {
            get { return selectedUwglassType; }
            set
            {
                selectedUwglassType = value;
                NotifyPropertyChanged(m => m.SelectedglassType);
            }
        }

        public List<EnumObject<DefaultUwConductivityStandard.GlassType>> GlassUwTypes
        {
            get { return glassUwTypes; }
            set
            {
                glassUwTypes = value;
                NotifyPropertyChanged(m => m.GlassTypes);
            }
        }

        #endregion

        #endregion

        public double DefaultUg
        {
            get { return defaultUg; }
            set
            {
                defaultUg = value;
                NotifyPropertyChanged(m => m.DefaultUg);
            }
        }

        public double DefaultUf
        {
            get { return defaultUg; }
            set
            {
                defaultUg = value;
                NotifyPropertyChanged(m => m.DefaultUg);
            }
        }

        private double defaultPsig;
        public double DefaultPsig
        {
            get { return defaultPsig; }
            set
            {
                defaultPsig = value;
                NotifyPropertyChanged(m => m.DefaultPsig);
            }
        }

        public double DefaultUw
        {
            get { return defaultUw; }
            set
            {
                defaultUw = value;
                NotifyPropertyChanged(m => m.DefaultUw);
            }
        }

        private double af;
        public double Af
        {
            get { return af; }
            set
            {
                af = value;
                NotifyPropertyChanged(m => m.Af);
            }
        }

        private double ag;
        public double DefaultAg
        {
            get { return ag; }
            set
            {
                ag = value;
                NotifyPropertyChanged(m => m.DefaultAg);
            }
        }

        private double lg;
        private IStandardsDataAccesServiceAgent StandardsServiceLayer = ThermalBuildingModel.Instance.StandardsServiceLayer;

        public double Lg
        {
            get { return lg; }
            set
            {
                lg = value;
                NotifyPropertyChanged(m => m.Lg);
            }
        }
         

        private void DefaultUgconductivityStandartLoaded(List<DefaultUgStandard> entities, Exception error)
        {
            // If no error is returned, set the model to entities
            if (error == null)
            {
                DefaultUg = entities.FirstOrDefault().U.Value;
            }
     
            else
            {
            
            }

        
        }

        private void DefaultUfconductivityStandartLoaded(List<DefaultUfStandard> entities, Exception error)
        {
            // If no error is returned, set the model to entities
            if (error == null)
            {
                DefaultUg = entities.FirstOrDefault().U.Value;
            }
        }

        //TODO:Add Default PsiStandard to database 1part
        private void DefaultPsigconductivityStandartLoaded(List<DefaultPsigConductivityStandard.DefaultPsig> entities, Exception error)
        {
            // If no error is returned, set the model to entities
            if (error == null)
            {
                DefaultPsig = entities.FirstOrDefault().U;
            }
        }

        private void DefaultUwconductivityStandartLoaded(List<DefaultUwConductivityStandard.DefaultUw> entities,
                                                         Exception error)
        {
            // If no error is returned, set the model to entities
            if (error == null)
            {
                DefaultUw = entities.FirstOrDefault().U;
            }
        }

        public void LoadDefaultUg()
        {
            StandardsServiceLayer.GetDefaultUgStandard(DefaultUgconductivityStandartLoaded,
                                                       (int) SelectedglassType.Object);
        }

        public void LoadDefaultUf()
        {
            StandardsServiceLayer.GetDefaultUfStandard(DefaultUfconductivityStandartLoaded,
                                                       (int) SelectedFrameType.Object);
        }

        public void LoadDefaultPsig()
        {
            //TODO:Add Default PsiStandard to database 2part
            //StandardsServiceLayer.GetDefaultPsigStandard(DefaultPsigconductivityStandartLoaded, (int)SelectedFrameType.Id);
        }
    }
}