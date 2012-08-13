using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using NErgy.Building;
using NErgy.Silverlight.Views.ViewModels;

namespace NErgy.Silverlight.Views
{
    public partial class MaterialEditor : UserControl
    {
        public static readonly DependencyProperty MaterialProperty =
            DependencyProperty.Register("Material", typeof (IThermalMaterial),
                                        typeof (MaterialEditor),
                                        new PropertyMetadata(null, MaterialPropertyChangedCallBack));


        public static readonly DependencyProperty MaterialsViewModelProperty =
            DependencyProperty.Register("MaterialsViewModel", typeof (MaterialsViewModel),
                                        typeof (MaterialEditor),
                                        new PropertyMetadata(null, MaterialsViewModelPropertyChangedCallBack));


        public MaterialEditor()
        {
            InitializeComponent();
            DataForm.EditEnded += DataForm_EditEnded;
            DataForm.BeginningEdit += DataForm_BeginningEdit;
            
        }

        public MaterialsViewModel ExposedDataContext
        {
            get { return DataContext as MaterialsViewModel; }
            set { DataContext = value; }
        }

        public MaterialsViewModel MaterialsViewModel
        {
            get { return (MaterialsViewModel) GetValue(MaterialsViewModelProperty); }
            set { SetValue(MaterialsViewModelProperty, value); }
        }

        public IThermalMaterial Material
        {
            get { return (IThermalMaterial) GetValue(MaterialProperty); }
            set { SetValue(MaterialProperty, value); }
        }

        private void DataForm_BeginningEdit(object sender, CancelEventArgs e)
        {
            MaterialsViewModel.BeginEdit();
        }

        private void DataForm_EditEnded(object sender, DataFormEditEndedEventArgs e)
        {
            if (e.EditAction == DataFormEditAction.Cancel)
            {
                MaterialsViewModel.CancelEdit();
            }
            MaterialsViewModel.EndEdit();
        }


        private static void MaterialsViewModelPropertyChangedCallBack(DependencyObject d,
                                                                      DependencyPropertyChangedEventArgs e)
        {
            var editor = d as MaterialEditor;
            if (e.NewValue == null) return;
            editor.ExposedDataContext = e.NewValue as MaterialsViewModel;
            editor.ExposedDataContext.LibrariesListViewModel.LoadLibraries();
           
        }


        private static void MaterialPropertyChangedCallBack(
            DependencyObject d,
            DependencyPropertyChangedEventArgs e)
        {
            var editor = d as MaterialEditor;
            if (e.NewValue == null) return;
            editor.MaterialsViewModel = new MaterialsViewModel(e.NewValue as IThermalMaterial);

        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            InvokeCloseEditor(null);
        }

        private event EventHandler CloseEditor;

        private void InvokeCloseEditor(EventArgs e)
        {
            EventHandler handler = CloseEditor;
            if (handler != null) handler(this, e);
        }

        private void libraryListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.MaterialsViewModel.LibrariesListViewModel.LoadMaterialsForLibrary();
        }

        private void button1_Click_1(object sender, RoutedEventArgs e)
        {

        }

        //private void libraryListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    materialDataGrid.ItemsSource = MaterialsViewModel.GetLibraryMaterials((e.AddedItems[0] as Library).LibraryID);
        //}

        //private void button2_Click(object sender, RoutedEventArgs e)
        //{
        //    Library new_Library = MaterialsViewModel.AddNewLibrary();
        //    MaterialsViewModel.ActiveLibrary = new_Library;
        //    libraryListBox.UpdateLayout();
        //}
    }
}