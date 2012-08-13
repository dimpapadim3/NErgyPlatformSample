using System;
using System.Windows;
using System.Windows.Controls;
using GuiLabs.Undo;
using Liquid;
using NCad.Application.Views.Controllers;
using NCad.Utilities;
using NCad.Views.ViewModels3D;
using NErgy.Building;
using NErgy.Silverlight.Models;
using NErgy.Silverlight.Views.Controllers;
using NErgy.Silverlight.Views.ViewModels;
using Mediator = NErgy.Silverlight.Views.Controllers.Mediator;
//using MenuManager = NCad.Application.Views.Controllers.SLPopupMenuManager;
using NCad.Views;
using Controller3DBabylon = NErgy.Silverlight.Views.Controllers.Controller3DBabylon;

namespace NErgy.Silverlight.Views
{
    public partial class DesignView : UserControl, IDesignView
    {
        private readonly ActionManager ActionManager = new ActionManager();

        private ThermalBuildingModel Model { get { return ThermalBuildingModel.Instance; } }

        private readonly PropertyGrid properyGrid = new PropertyGrid();
        private MenuManager MenuManager;
        private KeyboardManager _keyboardManager;

        /// <summary>
        /// Creates a new <see cref="Home"/> instance.
        /// </summary>
        public DesignView()
        {
            InitializeComponent();
            // ServiceReference1.Service1 service1=new 
            // properyGridScrollViewer.Content = properyGrid;
            _keyboardManager = new KeyboardManager(this);

        }

        public Tree TreeView { get { return null; } set { } }

        public DesignViewViewModel ViewModel
        {
            get { return this.DataContext as DesignViewViewModel; }
            set { this.DataContext = value; }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            ViewModel = new DesignViewViewModel(this);

            Model.SelectedItem += Model_SelectedItem;

            InitialBuildingConfiguration();

            Model.Initialize();

            var mediator = Mediator.Instance; //new Mediator(Model);
            mediator.DesignView = this;
            mediator.Initialize3DController(new Controller3DBabylon() { ActionManager = mediator.ActionManager });
            var MenuManager = new Controllers.MenuManager(mediator.Controller3D);

            //MenuManager.AttachMenuToView(mainView3D);
            mediator.Add3DView(mainView3D);
            //mediator.AddTreeView(this.MainTreeView);
            //mediator.SectionEditorController.Initialize(_ThermalSectionEditor);
            //mediator.SectionEditorController.Create3DController(_ThermalSectionEditor.BalderView);
            //mediator.SectionEditorController.SelectedSection=SectionEditorController.TestCompositeThermalSection();
            //mediator.SectionEditorController.SetUpScene(); 

            mediator.Add2DView(mainview2D);
            //mainview2D.ShowToolbar = false;

            //mediator.AddTreeView(mainTreeView);

        }

        private void InitialBuildingConfiguration()
        {
            //Model.Project.Buildings[0].TypicalStoryHeight = 3.5;
            //Model.Project.Buildings[0].FirstStoryHeight = 4;
        }

        private void Model_SelectedItem(object sender, EventArgs e)
        {
            properyGrid.Show(sender, ActionManager);
        }

        public IView3D MainView3D { get { return null; } }
        public ContentControl DispalyRegion { get { return this.mainview2D2SV; } }
        public ContentControl MainRegion { get { return null;/*this._MainRegion; */} }
        public ContentControl SecondaryRegion { get { return this.mainview2D1SV; } }
        public ContentControl SectionRegion { get { return this.sectionEditorRegion; } }
        private void ContextMenu_Opened(object sender, RoutedEventArgs e)
        {
            ViewModel.UpdateDataContextItems();
        }

        public void SetSectionView(UserControl thermalSectionEditor)
        {
            SectionRegion.Content = thermalSectionEditor;
        }



    }
}