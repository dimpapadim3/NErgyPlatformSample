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
using NErgy.Building;
using NErgy.Building.BuildingShell;
using NErgy.Silverlight.Views.ViewModels;

namespace NErgy.Silverlight.Views.BuildingViews
{
    public partial class SeparetingSerfuceView : UserControl
    {
        public SeparetingSerfuceView()
        {
            InitializeComponent();
        }

        public SeparetingSerfuceShellViewModel ViewModel
        {
            get
            {
                return this.DataContext as SeparetingSerfuceShellViewModel;
            }
            set
            {
                this.DataContext = value;
            }
        }

    //    public sepa SunArea
    //    {
    //        get { return (SunArea)GetValue(MaterialProperty); }
    //        set { SetValue(MaterialProperty, value); }
    //    }

    //    public static readonly DependencyProperty MaterialProperty =
    //DependencyProperty.Register("SunArea", typeof(SunArea),
    //                            typeof(SunAreaShellView),
    //                            new PropertyMetadata(null, BuildingPropertyChangedCallBack));

    //    private static void BuildingPropertyChangedCallBack(DependencyObject d,
    //                                                                  DependencyPropertyChangedEventArgs e)
    //    {
    //        var editor = d as SunAreaShellView;
    //        if (e.NewValue == null) return;
    //        editor.ViewModel = new SunAreaShellViewModel(e.NewValue as SunArea);


    //    }

        public void ContactWithGroundElementsContextMenu_Opened(object sender, RoutedEventArgs e)
        {
            ViewModel.ShellViewModel.UpdateContactWithGroundElementsContextMenu();

        }


        public void TanslusentContextMenuOpenedCommand(object sender, RoutedEventArgs e)
        {
            ViewModel.ShellViewModel.UpdateTransparentContextMenu();
        }

        private void DataGrid_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //if (e.AddedItems != null && e.AddedItems.Count > 0)
           //     ViewModel.ShellViewModel.SelectedTranslusentElement = e.AddedItems[0] as Models.StructuralElements.TranslusentElement;
        }

        private void DataGrid_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete) ViewModel.ShellViewModel.DeleteTransparentElement.Execute(null);
        }

        private void TranslusentDataGrid_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete) ViewModel.ShellViewModel.DeleteTranslusentElement.Execute(null);
        }

        private void ContactWithGroundDataGrid_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete) ViewModel.ShellViewModel.DeleteContactWithGroundElementElement.Execute(null);
        }
    }
}
