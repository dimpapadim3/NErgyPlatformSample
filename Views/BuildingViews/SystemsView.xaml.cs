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
    public partial class SystemsView : UserControl
    {
        public SystemsView()
        {
            InitializeComponent();
        }
        public SystemsViewModel ViewModel
        {
            get
            {
                return this.DataContext as SystemsViewModel;
            }
            set
            {
                this.DataContext = value;
            }
        }

        public NErgy.Building.Systems.ThermalSystem System
        {
            get { return (NErgy.Building.Systems.ThermalSystem)GetValue(MaterialProperty); }
            set { SetValue(MaterialProperty, value); }
        }

        public static readonly DependencyProperty MaterialProperty =
    DependencyProperty.Register("System", typeof(NErgy.Building.Systems.ThermalSystem),
                                typeof(SystemsView),
                                new PropertyMetadata(null, BuildingPropertyChangedCallBack));

        private static void BuildingPropertyChangedCallBack(DependencyObject d,
                                                                      DependencyPropertyChangedEventArgs e)
        {
            var editor = d as SystemsView;
            if (e.NewValue == null) return;
            if (editor != null) editor.ViewModel =new SystemsViewModel( e.NewValue as  NErgy.Building.Systems.ThermalSystem) ;
        }

    }
}
