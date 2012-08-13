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
    public class HeatSystemsViewModel : ViewModelMasterDetailsBase<HeatSystemsViewModel,HeatSystem>
    {
        public HeatingSystemsProductionViewModel HeatingSystemsProductionViewModel { get; set; }
        public HeatingSystemsTransmitionNetworkViewModel HeatingSystemsTransmitionNetworkViewModel { get; set; }
        public HeatingSystemsTerminalUnitViewModel HeatingSystemsTerminalUnitViewModel { get; set; }
        public HeatingSystemsHelpingUnitViewModel HeatingSystemsHelpingUnitViewModel { get; set; }

        public HeatSystemsViewModel(HeatSystem heatSystem)
        {
            this.Model = heatSystem;

            if (heatSystem.Produntions == null) heatSystem.Produntions = new List<Production>();
            if (heatSystem.TransmitionNetworks == null) heatSystem.TransmitionNetworks = new List<TransmitionNetwork>()  ;
            if (heatSystem.TerminalUnits == null) heatSystem.TerminalUnits = new List<TerminalUnit>();
            if (heatSystem.HelpingUnits == null) heatSystem.HelpingUnits = new List<HelpingUnit>();


            HeatingSystemsProductionViewModel = new HeatingSystemsProductionViewModel(heatSystem);
            HeatingSystemsTransmitionNetworkViewModel = new HeatingSystemsTransmitionNetworkViewModel(heatSystem);
            HeatingSystemsTerminalUnitViewModel = new HeatingSystemsTerminalUnitViewModel(heatSystem);
            HeatingSystemsHelpingUnitViewModel = new HeatingSystemsHelpingUnitViewModel(heatSystem);

            DataAccesLayerService = ThermalBuildingModel.Instance.DataAccesLayerService;
            HeatingSystemsProductionViewModel.DataAccesLayerService = this.DataAccesLayerService;
        }

        protected IDataAccesLayerServiceAgent DataAccesLayerService { get; set; }
    }

    public class HeatingSystemsProductionViewModel :CollectionViewModelBase<Production>
    {

        public HeatSystem HeatSystem { get; set; }

        public HeatingSystemsProductionViewModel(HeatSystem heatSystem):base(new Production() )
        {
            ProductionTypes = new List<EnumObject<object>>(GetEnumListFromArray<object>(SystemsViewModel.ProductionTypesStrings));
            EnergySourceTypes = new List<EnumObject<EnergySourceTypes>>(GetEnumListWithArraySupport<EnergySourceTypes>(SystemsViewModel.EnergySourceTypesStrings));
            HeatSystem = heatSystem;
           
        }

        public static List<EnumObject<object>> ProductionTypes { get; set; }
        public static List<EnumObject<EnergySourceTypes>> EnergySourceTypes { get; set; }

        public override List<Production> GetCollection()
        {
            return HeatSystem.Produntions.ToList();
        }

        public override void SetCollection(IList<Production> collection)
        {
            this.HeatSystem.Produntions = collection;
        }

        
    }
    public class HeatingSystemsTransmitionNetworkViewModel : CollectionViewModelBase<TransmitionNetwork>
    {
        public HeatSystem HeatSystem { get; set; }
        public static List<EnumObject<object >> TransmitionTypes { get; set; }
        public HeatingSystemsTransmitionNetworkViewModel(HeatSystem heatSystem)
            : base(new TransmitionNetwork())
        {
            TransmitionTypes = new List<EnumObject<object>>(GetEnumListFromArray<object>(SystemsViewModel.TransmitionTypesStrings));
            HeatSystem = heatSystem;

        }


        public override List<TransmitionNetwork> GetCollection()
        {
            return HeatSystem.TransmitionNetworks.ToList();
        }

        public override void SetCollection(IList<TransmitionNetwork> collection)
        {
            this.HeatSystem.TransmitionNetworks = collection;
        }


    }
    public class HeatingSystemsTerminalUnitViewModel : CollectionViewModelBase<TerminalUnit>
    {
        public HeatSystem HeatSystem { get; set; }

        public HeatingSystemsTerminalUnitViewModel(HeatSystem heatSystem)
            : base(new TerminalUnit())
        {

            HeatSystem = heatSystem;

        }


        public override List<TerminalUnit> GetCollection()
        {
            return HeatSystem.TerminalUnits.ToList();
        }

        public override void SetCollection(IList<TerminalUnit> collection)
        {
            this.HeatSystem.TerminalUnits = collection;
        }


    }

    public class HeatingSystemsHelpingUnitViewModel : CollectionViewModelBase<HelpingUnit>
    {
        public HeatSystem HeatSystem { get; set; }

        public HeatingSystemsHelpingUnitViewModel(HeatSystem heatSystem)
            : base(new HelpingUnit())
        {

            HelpingUnitsTypes = new List<EnumObject<object>>(GetEnumListFromArray<object>(SystemsViewModel.HelpingUnitsTypesStrings));
            HeatSystem = heatSystem;

        }

        public static  List<EnumObject<object>> HelpingUnitsTypes { get; set; }

        public override List<HelpingUnit> GetCollection()
        {
            return HeatSystem.HelpingUnits.ToList();
        }

        public override void SetCollection(IList<HelpingUnit> collection)
        {
            this.HeatSystem.HelpingUnits = collection;
        }


    }

}
      