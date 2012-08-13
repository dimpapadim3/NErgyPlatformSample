using System;
using System.Collections.ObjectModel;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using NErgy.Building;
using NErgy.Silverlight.Models;
using NErgy.Silverlight.Web.Models;
using SimpleMvvmToolkit;
using NErgy.Silverlight.Views.Controllers;
using Section = NErgy.Silverlight.Web.Models.Section;

namespace NErgy.Silverlight.Views.ViewModels
{
    public class ThermalEditorViewModel : ViewModelBase<ThermalEditorViewModel>
    {
        public event EventHandler<NotificationEventArgs<Section>> SelectedSectionLayerChanged;

    

        public void NotifyMaterialSelected(Section section)
        {
            Notify(SelectedSectionLayerChanged, new NotificationEventArgs<Section>(null, section));
        }
        public ThermalEditorViewModel()
        {
            SectionLibrariesListViewModel = new SectionLibrariesListViewModel();
            SectionLibrariesListViewModel.SectionChanged += SectionLibrariesListViewModel_SectionChanged;

            InspectionTranslusentViewModel = new InspectionTranslusentViewModel(null);
        }

        private void SectionLibrariesListViewModel_SectionChanged(object sender, NotificationEventArgs<Section> e)
        {
            SelectedSectionLayer = e.Data;
        }

        public event EventHandler<NotificationEventArgs<Material, bool>> MaterialEditNotice;

        private ObservableCollection<IThermalSection> thermalSectionsLayers;
        public ObservableCollection<IThermalSection> ThermalSectionsLayers
        {
            get { return thermalSectionsLayers; }
            set
            {
                thermalSectionsLayers = value;
                NotifyPropertyChanged(m => m.ThermalSectionsLayers);
            }
        }

        private IThermalSection selectedSectionLayer;
        public IThermalSection SelectedSectionLayer
        {
            get { return selectedSectionLayer; }
            set
            {
                selectedSectionLayer = value;
                NotifyPropertyChanged(m => m.SelectedSectionLayer);
            }
        }

        private DelegateCommand<Section> editMaterialCommand;
        public DelegateCommand<Section> EditMaterialCommand
        {
            get { return editMaterialCommand ?? (editMaterialCommand = new DelegateCommand<Section>(OpenMaterialEditor)); }
            private set { editMaterialCommand = value; }
        }

        public void OpenMaterialEditor(Section section)
        {
            //if (section.Material == null) return;
            //var dialog = Mediator.OpenWindow(new MaterialEditor
            //{
            //    Material = section.Material
            //});

        }

        private DelegateCommand<ThermalEditorViewModel> openStandardsViewCommand;
        public DelegateCommand<ThermalEditorViewModel> OpenStandardsViewCommand
        {
            get { return openStandardsViewCommand ?? (openStandardsViewCommand = new DelegateCommand<ThermalEditorViewModel>((e) => OpenTranslusentStandardsView()) { }); }
            private set { openStandardsViewCommand = value; }

        }


        protected void OpenTranslusentStandardsView()
        {
            var standardsView = new InspectionOpaqueView();
            var dialog = Mediator.OpenWindow(standardsView);
            dialog.Closed += (s,e) =>
                                 {
                                     //standardsView.TranslusentElement //TODO: do something
                                 };
        }

        public SectionLibrariesListViewModel SectionLibrariesListViewModel { get; set; }
        public InspectionTranslusentViewModel InspectionTranslusentViewModel { get; set; }


        public void OpenNewLibraryEditor()
        {
            var libraryEditor = new LibraryEditorView()
                                    {
                                        DataContext = new LibraryViewModel()
                                    };

            var dialog = Mediator.OpenWindow(libraryEditor);

            dialog.Closed += (s, e) =>
                                 {
                                     this.SectionLibrariesListViewModel.Add(libraryEditor.ViewModel.Library);


                                 };



        }
    }
}
