using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using NErgy.Silverlight.Views.ViewModels;
using NErgy.Silverlight.Web.Models;

namespace NErgy.Silverlight.Views.BuildingViews
{
    public partial class ThermalZoneBuildingUsageControl : UserControl
    {


        public ZoneUsageViewModel ViewModel
        {
            get { return DataContext as ZoneUsageViewModel; }
            set { this.DataContext = value; }
        }

        public ThermalZoneBuildingUsageControl()
        {
            InitializeComponent();
            ViewModel = new ZoneUsageViewModel();


        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            ViewModel.IsbuttonVisibility = false;
            ViewModel.IsTreeViewVisibility = true;
        }

        public static readonly DependencyProperty MaterialsViewModelProperty =
          DependencyProperty.Register("UsageId", typeof(int?),
                                      typeof(ThermalZoneBuildingUsageControl),
                                      new PropertyMetadata(null, MaterialsViewModelPropertyChangedCallBack));

        private static void MaterialsViewModelPropertyChangedCallBack(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var editor = d as ThermalZoneBuildingUsageControl;
            if (e.NewValue == null) return;
            editor.ViewModel .SetNewUsageById( (int)e.NewValue) ;
            //editor.ViewModel.UsageViewModel.LoadLibraries();
        }

        public int? UsageId
        {
            get { return (int)GetValue(MaterialsViewModelProperty); }
            set { SetValue(MaterialsViewModelProperty, value); }
        }

        private void TreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            var item = e.NewValue as TreeViewItem;
            if (item != null)
                if (item.Tag is EnumObject<object>)
                {
                    ViewModel.SelectedUsageType = item.Tag as EnumObject<object>;
                    this.UsageId = (item.Tag as EnumObject<object>).Id;
                    ViewModel.IsbuttonVisibility = true;
                    ViewModel.IsTreeViewVisibility = false;



                }
        }
    }
}
