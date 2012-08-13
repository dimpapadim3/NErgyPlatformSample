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
    public class ZnxSystemsViewModel : ViewModelMasterDetailsBase<ZnxSystemsViewModel,ZnxSystem>
    {
        public ZnxSystemsProductionViewModel ZnxSystemsProductionViewModel { get; set; }
        public ZnxSystemsTransmitionNetworkViewModel ZnxSystemsTransmitionNetworkViewModel { get; set; }
        public ZnxSystemsTerminalUnitViewModel ZnxSystemsTerminalUnitViewModel { get; set; }
       // public ZnxSystemsHelpingUnitViewModel ZnxSystemsHelpingUnitViewModel { get; set; }

        public ZnxSystemsViewModel(ZnxSystem znxSystem)
        {
            this.Model = znxSystem;
            if (znxSystem.Produntions == null) znxSystem.Produntions = new List<Production>();
            if (znxSystem.TransmitionNetworks == null) znxSystem.TransmitionNetworks = new List<TransmitionNetwork>();
            if (znxSystem.TerminalUnits == null) znxSystem.TerminalUnits = new List<TerminalUnit>();

            ZnxSystemsProductionViewModel = new ZnxSystemsProductionViewModel(znxSystem);
            ZnxSystemsTransmitionNetworkViewModel = new ZnxSystemsTransmitionNetworkViewModel(znxSystem);
            ZnxSystemsTerminalUnitViewModel = new ZnxSystemsTerminalUnitViewModel(znxSystem);
          //  ZnxSystemsHelpingUnitViewModel = new ZnxSystemsHelpingUnitViewModel(znxSystem);

            DataAccesLayerService = ThermalBuildingModel.Instance.DataAccesLayerService;
            ZnxSystemsProductionViewModel.DataAccesLayerService = this.DataAccesLayerService;
        }

        protected IDataAccesLayerServiceAgent DataAccesLayerService { get; set; }
    }

    public class ZnxSystemsProductionViewModel :CollectionViewModelBase<Production>
    {
        public ZnxSystem ZnxSystem { get; set; }

        public ZnxSystemsProductionViewModel(ZnxSystem znxSystem):base(new Production() )
        {
            ProductionTypes = new List<EnumObject<object>>(GetEnumListFromArray<object>(SystemsViewModel.ZnxProductionTypesStrings));
            EnergySourceTypes = new List<EnumObject<EnergySourceTypes>>(GetEnumListWithArraySupport<EnergySourceTypes>(SystemsViewModel.EnergySourceTypesStrings));
            ZnxSystem = znxSystem;
           
        }

        public static List<EnumObject<object>> ProductionTypes { get; set; }
        public static List<EnumObject<EnergySourceTypes>> EnergySourceTypes { get; set; }

        public override List<Production> GetCollection()
        {
            return ZnxSystem.Produntions.ToList();
        }

        public override void SetCollection(IList<Production> collection)
        {
            this.ZnxSystem.Produntions = collection;
        }

        
    }
    public class ZnxSystemsTransmitionNetworkViewModel : CollectionViewModelBase<TransmitionNetwork>
    {
        public ZnxSystem ZnxSystem { get; set; }
        public static List<EnumObject<object>> TransmitionTypes { get; set; }
        public ZnxSystemsTransmitionNetworkViewModel(ZnxSystem heatSystem)
            : base(new TransmitionNetwork())
        {
            TransmitionTypes = new List<EnumObject<object>>(GetEnumListFromArray<object>(SystemsViewModel.TransmitionTypesStrings));
            ZnxSystem = heatSystem;

        }


        public override List<TransmitionNetwork> GetCollection()
        {
            if (ZnxSystem.TransmitionNetworks != null) return ZnxSystem.TransmitionNetworks.ToList();
            return null;
        }

        public override void SetCollection(IList<TransmitionNetwork> collection)
        {
            this.ZnxSystem.TransmitionNetworks = collection;
        }


    }
    public class ZnxSystemsTerminalUnitViewModel : CollectionViewModelBase<TerminalUnit>
    {
        public ZnxSystem ZnxSystem { get; set; }
        public ZnxSystemsTerminalUnitViewModel(ZnxSystem heatSystem)
            : base(new TerminalUnit())
        {

            ZnxSystem = heatSystem;

        }


        public override List<TerminalUnit> GetCollection()
        {
            return ZnxSystem.TerminalUnits.ToList();
        }

        public override void SetCollection(IList<TerminalUnit> collection)
        {
            this.ZnxSystem.TerminalUnits = collection;
        }


    }
    //public class ZnxSystemsHelpingUnitViewModel : CollectionViewModelBase<HelpingUnit>
    //{
    //    public ZnxSystem ZnxSystem { get; set; }

    //    public ZnxSystemsHelpingUnitViewModel(ZnxSystem heatSystem)
    //        : base(new HelpingUnit())
    //    {

    //        HelpingUnitsTypes = new List<EnumObject<HelpingUnitsType>>(GetEnumList<HelpingUnitsType>());
    //        ZnxSystem = heatSystem;

    //    }

    //    protected List<EnumObject<HelpingUnitsType>> HelpingUnitsTypes { get; set; }
        
    //    public virtual List<HelpingUnit> GetCollection()
    //    {
    //        return ZnxSystem.HelpingUnits.ToList();
    //    }

    //    public virtual void SetCollection(IList<HelpingUnit> collection)
    //    {
    //        ZnxSystem.HelpingUnits = collection;
    //    }


    //}

}
      