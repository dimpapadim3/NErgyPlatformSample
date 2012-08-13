using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using NErgy.Building;
using NErgy.Silverlight.Views.ViewModels;

namespace NErgy.Silverlight.Views
{
    public partial class InspectionOpeningOveralConductivityView : UserControl
    {

        public InspectionOpeningOveralConductivityView()
        {
            InitializeComponent();
            this.DataContext = new InspectionOpeningViewModel(new KoufomaMono());
        }
 
        private void libraryListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ViewModel.LoadDefaultUg();
        }

        public InspectionOpeningViewModel ViewModel
        {
            get { return this.DataContext as InspectionOpeningViewModel; }
            set { this.DataContext = value; }
        }

        public static readonly DependencyProperty TransparentElementProperty =
      DependencyProperty.Register("TransparentElement", typeof(ITransparentElement),
      typeof(InspectionOpeningOveralConductivityView), new PropertyMetadata(null, TransparentElementPropertyChangedCallBack));

        private static void TransparentElementPropertyChangedCallBack(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var editor = d as InspectionOpeningOveralConductivityView;
            if (e.NewValue == null) return;
            editor.DataContext = new InspectionOpeningViewModel(e.NewValue as ITransparentElement); ;
        }

        public ITransparentElement TransparentElement
        {
            get { return (ITransparentElement)GetValue(TransparentElementProperty); }
            set { SetValue(TransparentElementProperty, value); }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {

        }



 
    }
}