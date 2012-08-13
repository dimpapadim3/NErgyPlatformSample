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
   
    public class SolarSystemsCollectionViewModel:CollectionViewModelBase<SolarCollector>
    {
        public ThermalSystem ThermalSystem { get; set; }
        public static List<EnumObject<object >> ProductionTypes { get; set; }
        public SolarSystemsCollectionViewModel(ThermalSystem ThermalSystem)
            : base(new SolarCollector())
        {
          if(ThermalSystem.SolarCollectors==null)
              ThermalSystem.SolarCollectors = new List<SolarCollector>();
            this.ThermalSystem = ThermalSystem;

            ProductionTypes = new List<EnumObject<object>>(GetEnumListFromArray<object>(SystemsViewModel.SolarSystemsTypes));
        }
   
        public override List<SolarCollector> GetCollection()
        {
            return  ThermalSystem.SolarCollectors.ToList();
        }

        public override void SetCollection(IList<SolarCollector> collection)
        {
            ThermalSystem.SolarCollectors = collection;
        }

        
    }
 
}
      