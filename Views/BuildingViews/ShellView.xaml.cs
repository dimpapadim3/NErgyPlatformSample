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
using NErgy.Silverlight.Views.Controllers;

namespace NErgy.Silverlight.Views
{
    public partial class ShellView : UserControl
    {
        public ShellView()
        {
            InitializeComponent();
            Mediator.Instance.ActivePageType = (int)HelpPageTypeId.ShellPage;
        }
        public ShellViewModel<Shell> ViewModel
        {
            get
            {
                return this.DataContext as ShellViewModel<Shell>;
            }
            set
            {
                this.DataContext = value;
            }
        }

        public Shell Shell
        {
            get { return (Shell)GetValue(MaterialProperty); }
            set { SetValue(MaterialProperty, value); }
        }

        public static readonly DependencyProperty MaterialProperty =
    DependencyProperty.Register("Shell", typeof(Shell),
                                typeof(ShellView),
                                new PropertyMetadata(null, BuildingPropertyChangedCallBack));

        private static void BuildingPropertyChangedCallBack(DependencyObject d,
                                                                      DependencyPropertyChangedEventArgs e)
        {
            var editor = d as ShellView;
            if (e.NewValue == null) return;
            editor.ViewModel =new ShellViewModel<Shell>( e.NewValue as  Shell) ;
    
           
        }

        public void ContactWithGroundElementsContextMenu_Opened(object sender, RoutedEventArgs e)
        {
            ViewModel.UpdateContactWithGroundElementsContextMenu();
   
        }


        public void TanslusentContextMenuOpenedCommand(object sender, RoutedEventArgs e)
        {
            ViewModel.UpdateTransparentContextMenu();
            (sender as ContextMenu).ItemsSource = ViewModel.TranslusentElementsMenuItems;
        }

        private void DataGrid_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(e.AddedItems!=null && e.AddedItems.Count>0)
            ViewModel.SelectedTranslusentElement=e.AddedItems[0] as NErgy.Silverlight.Models.StructuralElements.TranslusentElement;
        }

        private void DataGrid_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete) ViewModel.DeleteTransparentElement.Execute(null);
        }

        private void TranslusentDataGrid_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete) ViewModel.DeleteTranslusentElement.Execute(null);
        }
 
        private void ContactWithGroundDataGrid_KeyDown(object sender, KeyEventArgs e)
        {
          if(e.Key==Key.Delete) ViewModel.DeleteContactWithGroundElementElement.Execute(null);
        }
    }
}
