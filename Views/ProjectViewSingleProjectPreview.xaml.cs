using System;
using System.ServiceModel.DomainServices.Client.ApplicationServices;
using System.Windows;
using System.Windows.Controls;
using Liquid;
using NCad.Models;
using NCad.Utilities;
using NCad.Views;
using NCad.Views.ViewModels3D;
using NErgy.Silverlight.Helpers;
using NErgy.Silverlight.Models;
using NErgy.Silverlight.Views.Controllers;
using NErgy.Silverlight.Views.ViewModels;
using NErgy.Silverlight.Web.Models;
using SimpleMvvmToolkit;

namespace NErgy.Silverlight.Views
{
    public partial class ProjectViewSingleProjectPreview : UserControl, IDesignView
    {
        public ProjectViewSingleProjectPreview()
        {
            InitializeComponent();
           
            LoadContentByAuthentication();
 
            WebContext.Current.Authentication.LoggedIn+=new EventHandler<System.ServiceModel.DomainServices.Client.ApplicationServices.AuthenticationEventArgs>(Authentication_LoggedIn);
            this.nonAnthenticatedUser.OnLoginOperationCompleted += new EventHandler(LoginOperation_Completed); ;
        }

        private void LoginOperation_Completed(object sender, EventArgs e)
        {
            LoadContentByAuthentication();
        }

        private void LoadContentByAuthentication()
        {
            Logger.Instance.Log("User Name" + WebContext.Current.User.Name);
            if (!WebContext.Current.User.IsAuthenticated)
            {
                Logger.Instance.Log("User is not Authenitcated");
                this.nonAnthenticatedUser.Visibility = System.Windows.Visibility.Visible;
                projectListTabcontrol.Visibility = System.Windows.Visibility.Collapsed;
            }
            else
            {
                Logger.Instance.Log("User is Authenitcated");
                this.nonAnthenticatedUser.Visibility = System.Windows.Visibility.Collapsed;
                projectListTabcontrol.Visibility = System.Windows.Visibility.Visible;

                ViewModel = new ProjectListViewModel();
                ProjectListViewModel.SelectedProjectChanged += ProjectListViewModel_SelectedProjectChanged;
                //Mediator.Instance.ProjectView = this; 
                //this.ViewModel.BuildingInProjectLoaded += projectPreview.BuildingLoaded;
                //this.ViewModel = new ProjectViewModel(ThermalBuildingModel.Instance.Project);
                //   mainview2D2SV.Visibility = System.Windows.Visibility.Collapsed;
            }
        }

        private void Authentication_LoggedIn(object sender, AuthenticationEventArgs e)
        {
            LoadContentByAuthentication();
        }

        public ProjectListViewModel ViewModel
        {
            get { return DataContext as ProjectListViewModel; }
            set { DataContext = value; }
        }

        private void ProjectListViewModel_SelectedProjectChanged(object sender, NotificationEventArgs<Project> e)
        {
            var preview = e.Data;
            this.projectView.ViewModel = ViewModel.ProjectViewModel;
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
            //    mainview2D2SV.Visibility = System.Windows.Visibility.Visible;
            //
            //myStoryboard.Begin();

            //myStoryboard.Completed += (s, ev) =>
            //                              {

            //  projectpreview.Mediator.Controller3D.SetUpScene();
            //projectpreview.Margin = new Thickness() {Top = 0, Left = 100};
            //regionmain.Content = projectpreview as UserControl;
            regionmain.Visibility = System.Windows.Visibility.Visible;

            //projectpreview.View3d.Height = projectpreview.Height;
            //projectpreview.View3d.Width = projectpreview.Width;
            //   projectpreview.View3d.StopRotateScene();
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
            // mediator.Initialize3DController();
            //   MenuManager = new Controllers.MenuManager(mediator.Controller3D);

            // mediator.Add3DView(projectpreview.mainView3D);
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

        public IView3D MainView3D { get { return null;/*projectpreview.View3d; */} }
        public ContentControl DispalyRegion { get { return null; } }
        public ContentControl MainRegion { get { return null;/*this._MainRegion; */} }

        public Tree TreeView
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public ContentControl SecondaryRegion { get { return null; } }
        public ContentControl SectionRegion { get { return null; } }
        private void ContextMenu_Opened(object sender, RoutedEventArgs e)
        {
            if (DesignViewModel == null) return;
            DesignViewModel.UpdateDataContextItems();
            (sender as ContextMenu).ItemsSource = DesignViewModel.View3dMenuItems;
        }

        #endregion

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {

        }
    }
}