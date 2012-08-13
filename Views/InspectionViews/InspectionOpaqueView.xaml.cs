using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using NErgy.Building;
using NErgy.Silverlight.Views.ViewModels;

namespace NErgy.Silverlight.Views
{
    public partial class InspectionOpaqueView : UserControl
    {

        public InspectionOpaqueView()
        {
            InitializeComponent();
            this.DataContext = new InspectionTranslusentViewModel(new TranslusentElement());
        }
 
        private void libraryListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           //ViewModel.LoadDefaultUg();
        }

        public InspectionTranslusentViewModel ViewModel
        {
            get { return this.DataContext as InspectionTranslusentViewModel; }
            set { this.DataContext = value; }
        }

        public static readonly DependencyProperty TranslusentElementProperty =
      DependencyProperty.Register("TranslusentElement", typeof(IThermalSection),
      typeof(InspectionOpaqueView), new PropertyMetadata(null, TranslusentElementPropertyChangedCallBack));

        private static void TranslusentElementPropertyChangedCallBack(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var editor = d as InspectionOpeningView;
            //if (e.NewValue == null) return;
            //editor.DataContext = new InspectionTranslusentViewModel(e.NewValue as TranslusentElement); ;
        }

        public TranslusentElement TranslusentElement
        {
            get { return (TranslusentElement)GetValue(TranslusentElementProperty); }
            set { SetValue(TranslusentElementProperty, value); }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void SelectedConstructionLicencePeriodType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ViewModel.UpdateSelectedConstructionLicencePeriodType(e.AddedItems);
        }

        private void ThermalProtectionTypes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           var description =  ViewModel.UpdateSelectedThermalProtectionTypese(e.AddedItems);
           UCalculationStategyActualBuildingLabel .Content= description;
            this._MainRegion.Content = ViewModel.OpaqueDetailsView;
        }

        private void expander1_Expanded(object sender, RoutedEventArgs e)
        {

        }


        private void StructuralElementTypes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
         }
    }
}