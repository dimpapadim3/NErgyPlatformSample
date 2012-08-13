using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using NErgy.Building;
using NErgy.Silverlight.Views.ViewModels;

namespace NErgy.Silverlight.Views
{
    public partial class InspectionOpaqueKTHKStandardesView : UserControl
    {

        public InspectionOpaqueKTHKStandardesView()
        {
            InitializeComponent();
           // this.DataContext = new InspectionTranslusentViewModel(new TranslusentElement());
        }



        public InspectionTranslusentKTHKVerticalElementsViewModel ViewModel
        {
            get { return this.DataContext as InspectionTranslusentKTHKVerticalElementsViewModel; }
            set { this.DataContext = value; }
        }

        public static readonly DependencyProperty TranslusentElementProperty =
      DependencyProperty.Register("TranslusentElement", typeof(IThermalSection),
      typeof(InspectionOpaqueKTHKStandardesView), new PropertyMetadata(null, TranslusentElementPropertyChangedCallBack));

        private static void TranslusentElementPropertyChangedCallBack(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var editor = d as InspectionOpaqueKTHKStandardesView;
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

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }


     
    }
}