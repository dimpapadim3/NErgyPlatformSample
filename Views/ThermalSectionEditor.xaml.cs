using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Graphics;
using System.Windows.Input;

using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Balder.Objects.Geometries;
using NCad.Views.Views3D;
using NErgy.Silverlight.Views.Controllers;
using NCad.Views;
using NCad.Application.Views.Views2D;
using NErgy.Silverlight.Views.ViewModels;
using NErgy.Silverlight.Web.Models;
using SimpleMvvmToolkit;
 
namespace NErgy.Silverlight.Views
{
    public partial class ThermalSectionEditor : UserControl
    {
        public SectionEditorController SectionEditorController { get; set; }

        public ThermalSectionEditor()
        {
            InitializeComponent();
          
        }

        public ThermalEditorViewModel ViewModel
        {
            get { return DataContext as ThermalEditorViewModel; }
            set { DataContext = value;
            
            }
        }

        private IView3D<Babylon.Primitives.IDrawableShape> _sectionDarwingView;
        public IView3D<Babylon.Primitives.IDrawableShape> SectionDarwingView
        {
            get { return this.mainView3D; }
    
        }

        public Object DrawingView2D
        {
            get
            {
                return null;
            }
        }

        public ThermalSectionView SectionLayerView { get { return _ThermalSectionView; } }

        public DataGrid LayersDataGridView{get { return this.LayersDataGrid; }}
  
        private void button3_Click(object sender, RoutedEventArgs e)
        {
            SectionEditorController.AddMaterialLayerToSection();
        }

        private void expander1_Expanded(object sender, RoutedEventArgs e)
        {
            ViewModel.SectionLibrariesListViewModel.LoadLibraries();
        }

        private void button4_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.OpenNewLibraryEditor();
        }

        private void libraryListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ViewModel.SectionLibrariesListViewModel.LoadSectionsForLibrary();
        }

  
 
    }
}
