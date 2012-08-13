using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using NErgy.Building;
using NErgy.Silverlight.Views.ViewModels;

namespace NErgy.Silverlight.Views
{
    public partial class InspectionShadingView : UserControl
    {
 
        public InspectionShadingView()
        {
            InitializeComponent();
            this.ViewModel = new InspectionShadingViewModel(new HorizonShader());
        }
 
        private void libraryListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        
        }

        public InspectionShadingViewModel ViewModel
        {
            get { return this.DataContext as InspectionShadingViewModel; }
            set { this.DataContext = value; }
        }

        public static readonly DependencyProperty TransparentElementProperty =
      DependencyProperty.Register("IShadedElement", typeof(IShadedElement),
      typeof(InspectionShadingView), new PropertyMetadata(null, TransparentElementPropertyChangedCallBack));

        private static void TransparentElementPropertyChangedCallBack(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var editor = d as InspectionOpeningView;
            if (e.NewValue == null) return;
            editor.DataContext = new InspectionShadingViewModel(e.NewValue as IShadedElement); ;
        }

        public IShadedElement ShadedElement
        {
            get { return (IShadedElement)GetValue(TransparentElementProperty); }
            set { SetValue(TransparentElementProperty, value); }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {

        }



 
    }
}