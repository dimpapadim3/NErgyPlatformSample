using System;
using System.Windows;
using System.Windows.Controls;
using NErgy.Silverlight.Views.Controllers;
using NErgy.Silverlight.Views.ViewModels;

namespace NErgy.Silverlight.Views
{
    public partial class SectionLibrariesView : UserControl
    {
        public SectionEditorController SectionEditorController { get; set; }

        public SectionLibrariesView()
        {
            InitializeComponent();
          
        }

        public SectionLibrariesListAddSectionViewModel ViewModel
        {
            get { return DataContext as SectionLibrariesListAddSectionViewModel; }
            set { DataContext = value;
            
            }
        }

     

        public Object DrawingView2D
        {
            get
            {
                return null;
            }
        }

       // public ThermalSectionView SectionLayerView { get { return _ThermalSectionView; } }

     //   public DataGrid LayersDataGridView{get { return this.LayersDataGrid; }}
  
        private void button3_Click(object sender, RoutedEventArgs e)
        {
            SectionEditorController.AddMaterialLayerToSection();
        }

        private void expander1_Expanded(object sender, RoutedEventArgs e)
        {
            ViewModel.LoadLibraries();
        }

        private void button4_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.OpenNewLibraryEditor();
        }

        private void libraryListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ViewModel.LoadSectionsForLibrary();
        }

  
 
    }
}
