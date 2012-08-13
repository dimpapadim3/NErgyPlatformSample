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
    public partial class BuildingView : UserControl
    {
        public BuildingView()
        {
            InitializeComponent();
        }
        public BuildingViewModel ViewModel
        {
            get
            {
                return this.DataContext as BuildingViewModel;
            }
            set
            {
                this.DataContext = value;
            }
        }

        public Web.Models.Building Building
        {
            get { return (Web.Models.Building)GetValue(MaterialProperty); }
            set { SetValue(MaterialProperty, value); }
        }

        public static readonly DependencyProperty MaterialProperty =
    DependencyProperty.Register("Building", typeof(Web.Models.Building),
                                typeof(BuildingView),
                                new PropertyMetadata(null, BuildingPropertyChangedCallBack));

        private static void BuildingPropertyChangedCallBack(DependencyObject d,
                                                                      DependencyPropertyChangedEventArgs e)
        {
            var editor = d as BuildingView;
            if (e.NewValue == null) return;
            editor.ViewModel =new BuildingViewModel( e.NewValue as  Web.Models.Building) ;
    
           
        }
        private bool _StartingDateignoreNextUpdate = true;

        private void OnStartingDateSelected(object sender, SelectionChangedEventArgs e)
        {
            var startingDtatepicker  =  sender as DatePicker;
         if (_StartingDateignoreNextUpdate)
            {
                _StartingDateignoreNextUpdate = false;
             
                if (startingDtatepicker.SelectedDate != null)
                {
                  //this.ViewModel.StartingDispayDate
                }
                else
                {
                
                }
            }
            else
            {
                _StartingDateignoreNextUpdate = true;
            }
        }
 

        private void OnEndingDateSelected(object sender, SelectionChangedEventArgs e)
        {
           
        }
    }
}
