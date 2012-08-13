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
using NErgy.Building.Systems;
using NErgy.Silverlight.Models;

namespace NErgy.Silverlight.Views.ViewModels 
{
    public class CoolingSystemsViewModel : ViewModelMasterDetailsBase<CoolingSystemsViewModel,CoolingSystem>
    {
        public CoolingSystemsProductionViewModel CoolingSystemsProductionViewModel { get; set; }
        public CoolingSystemsTransmitionNetworkViewModel CoolingSystemsTransmitionNetworkViewModel { get; set; }
        public CoolingSystemsTerminalUnitViewModel CoolingSystemsTerminalUnitViewModel { get; set; }
        public CoolingSystemsHelpingUnitViewModel CoolingSystemsHelpingUnitViewModel { get; set; }

        public CoolingSystemsViewModel(CoolingSystem heatSystem)
        {
            this.Model = heatSystem;

            if (heatSystem.Produntions == null) heatSystem.Produntions = new List<Production>();
            if (heatSystem.TransmitionNetworks == null) heatSystem.TransmitionNetworks = new List<TransmitionNetwork>();
            if (heatSystem.TerminalUnits == null) heatSystem.TerminalUnits = new List<TerminalUnit>();
            if (heatSystem.HelpingUnits == null) heatSystem.HelpingUnits = new List<HelpingUnit>();

            CoolingSystemsProductionViewModel = new CoolingSystemsProductionViewModel(heatSystem);
            CoolingSystemsTransmitionNetworkViewModel = new CoolingSystemsTransmitionNetworkViewModel(heatSystem);
            CoolingSystemsTerminalUnitViewModel = new CoolingSystemsTerminalUnitViewModel(heatSystem);
            CoolingSystemsHelpingUnitViewModel = new CoolingSystemsHelpingUnitViewModel(heatSystem);

            DataAccesLayerService = ThermalBuildingModel.Instance.DataAccesLayerService;
            CoolingSystemsProductionViewModel.DataAccesLayerService = this.DataAccesLayerService;
        }

        protected IDataAccesLayerServiceAgent DataAccesLayerService { get; set; }
    }

    public class CoolingSystemsProductionViewModel :CollectionViewModelBase<Production>
    {
        public CoolingSystem CoolingSystem { get; set; }

        public CoolingSystemsProductionViewModel(CoolingSystem coolingSystem):base(new Production() )
        {
            ProductionTypes = new List<EnumObject<object>>(GetEnumListFromArray<object>(SystemsViewModel.CoolingProductionTypesStrings));
            EnergySourceTypes = new List<EnumObject<EnergySourceTypes>>(GetEnumListWithArraySupport<EnergySourceTypes>(SystemsViewModel.EnergySourceTypesStrings));
            CoolingSystem = coolingSystem;
           
        }

        public static List<EnumObject<object >> ProductionTypes { get; set; }
        public static List<EnumObject<EnergySourceTypes>> EnergySourceTypes { get; set; }

        public override List<Production> GetCollection()
        {
            return CoolingSystem.Produntions.ToList();
        }

        public override void SetCollection(IList<Production> collection)
        {
            this.CoolingSystem.Produntions = collection;
        }

        
    }
    public class CoolingSystemsTransmitionNetworkViewModel : CollectionViewModelBase<TransmitionNetwork>
    {
        public CoolingSystem CoolingSystem { get; set; }
        public static List<EnumObject<object>> TransmitionTypes { get; set; }
        public CoolingSystemsTransmitionNetworkViewModel(CoolingSystem heatSystem)
            : base(new TransmitionNetwork())
        {
            TransmitionTypes = new List<EnumObject<object>>(GetEnumListFromArray<object>(SystemsViewModel.TransmitionTypesStrings));
            CoolingSystem = heatSystem;

        }


        public override List<TransmitionNetwork> GetCollection()
        {
            return CoolingSystem.TransmitionNetworks.ToList();
        }

        public override void SetCollection(IList<TransmitionNetwork> collection)
        {
            this.CoolingSystem.TransmitionNetworks = collection;
        }


    }
    public class CoolingSystemsTerminalUnitViewModel : CollectionViewModelBase<TerminalUnit>
    {
        public CoolingSystem CoolingSystem { get; set; }

        public CoolingSystemsTerminalUnitViewModel(CoolingSystem heatSystem)
            : base(new TerminalUnit())
        {

            CoolingSystem = heatSystem;

        }


        public override List<TerminalUnit> GetCollection()
        {
            return CoolingSystem.TerminalUnits.ToList();
        }

        public override void SetCollection(IList<TerminalUnit> collection)
        {
            this.CoolingSystem.TerminalUnits = collection;
        }


    }
    public class CoolingSystemsHelpingUnitViewModel : CollectionViewModelBase<HelpingUnit>
    {
        public CoolingSystem CoolingSystem { get; set; }

        public CoolingSystemsHelpingUnitViewModel(CoolingSystem heatSystem)
            : base(new HelpingUnit())
        {

            HelpingUnitsTypes = new List<EnumObject<object>>(GetEnumListFromArray<object>(SystemsViewModel.HelpingUnitsTypesStrings));
            CoolingSystem = heatSystem;

        }

        public static List<EnumObject<object>> HelpingUnitsTypes { get; set; }

        public override List<HelpingUnit> GetCollection()
        {
            return CoolingSystem.HelpingUnits.ToList();
        }

        public override void SetCollection(IList<HelpingUnit> collection)
        {
            this.CoolingSystem.HelpingUnits = collection;
        }


    }

}
      