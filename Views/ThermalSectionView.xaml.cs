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
using NErgy.Silverlight.Views.ViewModels;

namespace NErgy.Silverlight.Views
{
    public partial class ThermalSectionView : UserControl
    {
        public ThermalSectionView()
        {
            InitializeComponent();
        }



        public SectionViewModel ViewModel
        {
            get
            {
                return this.DataContext as SectionViewModel;
            }
            set
            {
                this.DataContext = value;
            }
        }

     public static readonly DependencyProperty SectionProperty =
     DependencyProperty.Register("Section", typeof(IThermalSection),
     typeof(ThermalSectionView), new PropertyMetadata(null,SectionPropertyChangedCallBack));


        // .NET Pm wrapper
     public IThermalSection Section
        {
            get { return (IThermalSection)GetValue(SectionProperty); }
            set
            {
                SetValue(SectionProperty, value);
               
            }
        }


     private static void SectionPropertyChangedCallBack(DependencyObject d,
                                                                 DependencyPropertyChangedEventArgs e)
     {
         var editor = d as ThermalSectionView;
         if (e.NewValue == null) return;
         editor.ViewModel = new SectionViewModel(e.NewValue as IThermalSection);

     }

    }
}
