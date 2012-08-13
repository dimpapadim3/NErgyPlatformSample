using System;
using System.Windows;
using System.Windows.Browser;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using Liquid;
using NCad.Models;
using NCad.Utilities;
using NCad.Views;
using NCad.Views.ViewModels3D;
using NErgy.Silverlight.Models;
using NErgy.Silverlight.Views.Controllers;
using NErgy.Silverlight.Views.Providers;
using NErgy.Silverlight.Views.ViewModels;
using NErgy.Silverlight.Web.Models;
using NErgy.Silverlight.Web.Services;
using SimpleMvvmToolkit;
using System.Collections.Generic;

namespace NErgy.Silverlight.Views
{
    public partial class EngineersGlobalView2 : UserControl, IDesignView
    {
        public EngineersGlobalView2()
        {
            InitializeComponent();
            ViewModel = new ProjectListViewModel();
            ProjectListViewModel.SelectedProjectChanged += ProjectListViewModel_SelectedProjectChanged;
            //this.thnxTetx.Visibility = System.Windows.Visibility.Collapsed;
            this.MyScroller.SetIsMouseWheelScrollingEnabled(true);
            //Mediator.Instance.ProjectView = this; 
            //this.ViewModel.BuildingInProjectLoaded += projectPreview.BuildingLoaded;
            //this.ViewModel = new ProjectViewModel(ThermalBuildingModel.Instance.Project);
            //this.scrollview.SetIsMouseWheelScrollingEnabled(true);
            // mainview2D2SV.Visibility = System.Windows.Visibility.Collapsed;
            try
            {
                ProviderAdds = new List<ProviderAdd>()
                                   {
                                       new ProviderAdd()
                                           {
                                               AddText = "Προμηθευτής",
                                               ProviderWebSite = new Uri("http://nergy.somee.com/"),
                                               ProviderAddImage =
                                                   new Image()
                                                       {
                                                           Source =
                                                               new BitmapImage(
                                                               new Uri(
                                                                   "/NErgy.Silverlight;component/Assets/house_ad-w_b90.png",UriKind.Relative))
                                                       }
                                           },
                                       new ProviderAdd()
                                           {
                                               AddText = "Προμηθευτής",
                                               ProviderWebSite = new Uri("http://nergy.somee.com/"), ProviderAddImage =
                                                   new Image()
                                                       {
                                                           Source =
                                                               new BitmapImage(
                                                               new Uri(
                                                                   "/NErgy.Silverlight;component/Assets/house_ad-w_b90.png",UriKind.Relative))
                                                       }
                                           },
                                       new ProviderAdd()
                                           {
                                               AddText = "Προμηθευτής",
                                               ProviderWebSite = new Uri("http://nergy.somee.com/"),
                                                ProviderAddImage =
                                                   new Image()
                                                       {
                                                           Source =
                                                               new BitmapImage(
                                                               new Uri(
                                                                   "/NErgy.Silverlight;component/Assets/house_ad-w_b90.png",UriKind.Relative))
                                                       }
                                           },
                                          
                                   };
            }
            catch (Exception exception)
            {
                
            }
        }

        public ProjectListViewModel ViewModel
        {
            get { return DataContext as ProjectListViewModel; }
            set { DataContext = value; }
        }

        private IList<ProviderAdd> _providerAdds;
        public IList<ProviderAdd> ProviderAdds
        {
            get { return _providerAdds; }
            set { _providerAdds = value; }
        }


        private void ProjectListViewModel_SelectedProjectChanged(object sender, NotificationEventArgs<Project> e)
        {
            // var preview = new ProjectsPreviewViewSingleProject() {Height = 500,Width = 500,AllowRotation = false};
            //  preview.Project = e.Data;
            // Mediator.OpenInRegion(projectPreviewRegion,preview);
        }

        private void button2_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            return;
            button2.Visibility = System.Windows.Visibility.Collapsed;
           // df1.Visibility = System.Windows.Visibility.Collapsed;
            projectsList.Visibility = System.Windows.Visibility.Collapsed;
            addNewButton.Visibility = System.Windows.Visibility.Collapsed;
          //  mainview2D2SV.Visibility = System.Windows.Visibility.Visible;

            //myStoryboard.Begin();

            //myStoryboard.Completed += (s, ev) =>
            //                              {

            projectpreview.Mediator.Controller3D.SetUpScene();
            //projectpreview.Margin = new Thickness() {Top = 0, Left = 100};
            //regionmain.Content = projectpreview as UserControl;
            regionmain.Visibility = System.Windows.Visibility.Visible;

            //projectpreview.View3d.Height = projectpreview.Height;
            //projectpreview.View3d.Width = projectpreview.Width;
            projectpreview.View3d.StopRotateScene();
            InitializeDesignMode();
            //};                    
        }



        #region DesignMode

        private readonly ActionManager ActionManager = new ActionManager();
        private ThermalBuildingModel Model { get { return ThermalBuildingModel.Instance; } }
        private readonly PropertyGrid properyGrid = new PropertyGrid();
        private MenuManager MenuManager;
        private KeyboardManager _keyboardManager;

        public void InitializeDesignMode()
        {
            _keyboardManager = new KeyboardManager(this);
            DesignViewModel = new DesignViewViewModel(this);
            Model.SelectedItem += Model_SelectedItem;
            Model.Initialize();

            var mediator = Mediator.Instance; //new Mediator(Model);
            mediator.DesignView = this;
            mediator.Initialize3DController();
            MenuManager = new Controllers.MenuManager(mediator.Controller3D);

            mediator.Add3DView(projectpreview.mainView3D);
            //if(this.mainview2D.DrawingControl!=null)
            //mediator.Add2DView(this.mainview2D);
        }

        public DesignViewViewModel DesignViewModel { get; set; }


        private void Model_SelectedItem(object sender, EventArgs e)
        {
            //properyGrid.Show(sender, ActionManager);
        }

        public void SetSectionView(UserControl thermalSectionEditor)
        {
            SectionRegion.Content = thermalSectionEditor;
        }

        public IView3D MainView3D { get { return projectpreview.View3d; } }
        public ContentControl DispalyRegion { get { return null; } }
        public ContentControl MainRegion { get { return null;/*this._MainRegion; */} }

        public Tree TreeView
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public ContentControl SecondaryRegion { get
        {
            return null;/* this.mainview2D2SV;*/ } }
        public ContentControl SectionRegion { get { return null;/* return this.sectionEditorRegion; */} }
        private void ContextMenu_Opened(object sender, RoutedEventArgs e)
        {
            if (DesignViewModel == null) return;
            DesignViewModel.UpdateDataContextItems();
            (sender as ContextMenu).ItemsSource = DesignViewModel.View3dMenuItems;
        }

        #endregion

        private void image1_ImageFailed(object sender, ExceptionRoutedEventArgs e)
        {

        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                //NErgyContext datacontext = new NErgyContext();

                //datacontext.LandingPageRegistrations.Add(new LandingPageRegistration() { Email = textBox1.Text });
                //datacontext.SubmitChanges();
                //this.thnxTetx.Visibility = System.Windows.Visibility.Visible;
            }
            catch (Exception exc)
            {
                
            }

        }

        private void rectangle2_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            HtmlPage.Window.Navigate(new Uri("http://www.wix.com/dimitrispapadim/greennergy"), "_blank");

        }

    }
}