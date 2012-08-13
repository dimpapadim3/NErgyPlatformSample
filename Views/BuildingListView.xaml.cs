using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using NErgy.Building;
using NErgy.Silverlight.Models;
using NErgy.Silverlight.Views.ViewModels;

namespace NErgy.Silverlight.Views
{
    public partial class BuildingListView : UserControl
    {
        public BuildingListView()
        {
            InitializeComponent();

            this.ViewModel = new BuildingListViewModel(ThermalBuildingModel.Instance.Project);
        }
        public BuildingListViewModel ViewModel
        {
            get
            {
                return this.DataContext as BuildingListViewModel;
            }
            set
            {
                this.DataContext = value;
            }
        }
  private ObservableCollection<NeighborBuilding> neigbourBuildings;
        public ObservableCollection<NeighborBuilding> NeigbourBuildings
        {
            get { return neigbourBuildings; }
            set
            {
                neigbourBuildings = value;
               
            }
        }
      private ObservableCollection<Web.Models.Building> buildings;
        public ObservableCollection<Web.Models.Building> Buildings
        {
            get { return buildings; }
            set
            {
                buildings = value;
            
            }
        }
        private void buildingCategoryDomainDataSource_LoadedData(object sender, System.Windows.Controls.LoadedDataEventArgs e)
        {

            if (e.HasError)
            {
                System.Windows.MessageBox.Show(e.Error.ToString(), "Load Error", System.Windows.MessageBoxButton.OK);
                e.MarkErrorAsHandled();
            }
        }

    }
}
