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
    //public class KlimatisticUnitSystemsViewModel : ViewModelMasterDetailsBase<KlimatisticUnitSystemsViewModel,KlimatisticUnit>
    //{
    //    public KlimatisticUnitSystemsCollection KlimatisticUnitSystemsCollection { get; set; }
 
    //    public KlimatisticUnitSystemsViewModel(KlimatisticUnit klimatisticUnit)
    //    {
    //        this.Model = klimatisticUnit;

    //        KlimatisticUnitSystemsCollection = new KlimatisticUnitSystemsCollection(klimatisticUnit);
         

    //        DataAccesLayerService = ThermalBuildingModel.Instance.DataAccesLayerService;
    //        KlimatisticUnitSystemsCollection.DataAccesLayerService = this.DataAccesLayerService;
    //    }

    //    protected IDataAccesLayerServiceAgent DataAccesLayerService { get; set; }
    //}

    public class KlimatisticUnitSystemsCollectionViewModel:CollectionViewModelBase<KlimatisticUnit>
    {
        public ThermalSystem ThermalSystem { get; set; }

        public KlimatisticUnitSystemsCollectionViewModel(ThermalSystem ThermalSystem)
            : base(new KlimatisticUnit() )
        {
            if (ThermalSystem.KlimatisticUnits == null) ThermalSystem.KlimatisticUnits = new List<KlimatisticUnit>();
            this.ThermalSystem = ThermalSystem;
        }


        public override List<KlimatisticUnit> GetCollection()
        {
            return  ThermalSystem.KlimatisticUnits.ToList();
        }

        public override void SetCollection(IList<KlimatisticUnit> collection)
        {
            ThermalSystem.KlimatisticUnits = collection;
        }

        
    }
 
}
      