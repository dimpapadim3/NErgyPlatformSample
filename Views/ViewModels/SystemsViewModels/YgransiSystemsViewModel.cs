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
    public class YgransiSystemsViewModel : ViewModelMasterDetailsBase<YgransiSystemsViewModel,YgransiSystem>
    {
        public YgransiSystemsProductionViewModel YgransiSystemsProductionViewModel { get; set; }
        public YgransiSystemsTransmitionNetworkViewModel YgransiSystemsTransmitionNetworkViewModel { get; set; }
        public YgransiSystemsTerminalUnitViewModel YgransiSystemsTerminalUnitViewModel { get; set; }
       // public YgransiSystemsHelpingUnitViewModel YgransiSystemsHelpingUnitViewModel { get; set; }

        public YgransiSystemsViewModel(YgransiSystem ygransiSystem)
        {
            this.Model = ygransiSystem;


            if (ygransiSystem.Produntions == null) ygransiSystem.Produntions = new List<Production>();
            if (ygransiSystem.TransmitionNetworks == null) ygransiSystem.TransmitionNetworks = new List<TransmitionNetwork>();
            if (ygransiSystem.TerminalUnits == null) ygransiSystem.TerminalUnits = new List<TerminalUnit>();
            //if (YgransiSystem.HelpingUnits == null) YgransiSystem.HelpingUnits = new List<HelpingUnit>();

            YgransiSystemsProductionViewModel = new YgransiSystemsProductionViewModel(ygransiSystem);
            YgransiSystemsTransmitionNetworkViewModel = new YgransiSystemsTransmitionNetworkViewModel(ygransiSystem);
            YgransiSystemsTerminalUnitViewModel = new YgransiSystemsTerminalUnitViewModel(ygransiSystem);
          //  YgransiSystemsHelpingUnitViewModel = new YgransiSystemsHelpingUnitViewModel(YgransiSystem);

            DataAccesLayerService = ThermalBuildingModel.Instance.DataAccesLayerService;
            YgransiSystemsProductionViewModel.DataAccesLayerService = this.DataAccesLayerService;
        }

        protected IDataAccesLayerServiceAgent DataAccesLayerService { get; set; }
    }

    public class YgransiSystemsProductionViewModel :CollectionViewModelBase<Production>
    {
        public YgransiSystem YgransiSystem { get; set; }

        public YgransiSystemsProductionViewModel(YgransiSystem ygransiSystem):base(new Production() )
        {
            ProductionTypes = new List<EnumObject<object>>(GetEnumListFromArray<object>(SystemsViewModel.YgransiProductionTypesStrings));
            EnergySourceTypes = new List<EnumObject<EnergySourceTypes>>(GetEnumListWithArraySupport<EnergySourceTypes>(SystemsViewModel.EnergySourceTypesStrings));
            YgransiSystem = ygransiSystem;
           
        }

        public static List<EnumObject<object>> ProductionTypes { get; set; }
        public static List<EnumObject<EnergySourceTypes>> EnergySourceTypes { get; set; }

        public override List<Production> GetCollection()
        {
            return YgransiSystem.Produntions.ToList();
        }

        public override void SetCollection(IList<Production> collection)
        {
            this.YgransiSystem.Produntions = collection;
        }

        
    }
    public class YgransiSystemsTransmitionNetworkViewModel : CollectionViewModelBase<TransmitionNetwork>
    {
        public YgransiSystem YgransiSystem { get; set; }
        public static List<EnumObject<object>> TransmitionTypes { get; set; }
        public YgransiSystemsTransmitionNetworkViewModel(YgransiSystem heatSystem)
            : base(new TransmitionNetwork())
        {
            TransmitionTypes = new List<EnumObject<object>>(GetEnumListFromArray<object>(SystemsViewModel.TransmitionTypesStrings));
            YgransiSystem = heatSystem;

        }


        public override List<TransmitionNetwork> GetCollection()
        {
            return YgransiSystem.TransmitionNetworks.ToList();
        }

        public override void SetCollection(IList<TransmitionNetwork> collection)
        {
            this.YgransiSystem.TransmitionNetworks = collection;
        }


    }
    public class YgransiSystemsTerminalUnitViewModel : CollectionViewModelBase<TerminalUnit>
    {
        public YgransiSystem YgransiSystem { get; set; }
        public YgransiSystemsTerminalUnitViewModel(YgransiSystem heatSystem)
            : base(new TerminalUnit())
        {

            YgransiSystem = heatSystem;

        }


        public override List<TerminalUnit> GetCollection()
        {
            return YgransiSystem.TerminalUnits.ToList();
        }

        public override void SetCollection(IList<TerminalUnit> collection)
        {
            this.YgransiSystem.TerminalUnits = collection;
        }


    }
    //public class YgransiSystemsHelpingUnitViewModel : CollectionViewModelBase<HelpingUnit>
    //{
    //    public YgransiSystem YgransiSystem { get; set; }

    //    public YgransiSystemsHelpingUnitViewModel(YgransiSystem heatSystem)
    //        : base(new HelpingUnit())
    //    {

    //        HelpingUnitsTypes = new List<EnumObject<HelpingUnitsType>>(GetEnumList<HelpingUnitsType>());
    //        YgransiSystem = heatSystem;

    //    }

    //    protected List<EnumObject<HelpingUnitsType>> HelpingUnitsTypes { get; set; }
        
    //    public virtual List<HelpingUnit> GetCollection()
    //    {
    //        return YgransiSystem.HelpingUnits.ToList();
    //    }

    //    public virtual void SetCollection(IList<HelpingUnit> collection)
    //    {
    //        YgransiSystem.HelpingUnits = collection;
    //    }


    //}

}
      