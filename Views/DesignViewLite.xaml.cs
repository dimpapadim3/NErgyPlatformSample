using System;
using System.Linq;
using System.ServiceModel.DomainServices.Client.ApplicationServices;
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
using NErgy.Silverlight.Web.Models;
using Mediator = NErgy.Silverlight.Views.Controllers.Mediator;
//using MenuManager = NCad.Application.Views.Controllers.SLPopupMenuManager;
using NCad.Views;
using Controller3DBabylon = NErgy.Silverlight.Views.Controllers.Controller3DBabylon;

namespace NErgy.Silverlight.Views
{
    public partial class DesignViewLite : UserControl, IDesignView
    {


        private readonly ActionManager ActionManager = new ActionManager();

        private ThermalBuildingModel Model { get { return ThermalBuildingModel.Instance; } }

        private readonly PropertyGrid properyGrid = new PropertyGrid();
        private MenuManager MenuManager;
        private KeyboardManager _keyboardManager;

        public LoginOperation loginOpertion; 
        /// <summary>
        /// 
        /// </summary>
        public DesignViewLite()
        {
            InitializeComponent();
            // ServiceReference1.Service1 service1=new 
           // properyGridScrollViewer.Content = properyGrid;
            _keyboardManager = new KeyboardManager(this);
            this.nonAnthenticatedUser.OnLoginOperationCompleted += new EventHandler(LoginOperation_Completed); ;
            LoadContentByAuthentication();
            WebContext.Current.Authentication.LoggedIn += new EventHandler<System.ServiceModel.DomainServices.Client.ApplicationServices.AuthenticationEventArgs>(Authentication_LoggedIn);

        }

        private void LoginOperation_Completed(object sender, EventArgs e)
        {
            LoadContentByAuthentication();
        }

        private void LoadContentByAuthentication()
        {
            if (!WebContext.Current.User.IsAuthenticated)
            {
                this.nonAnthenticatedUser.Visibility = System.Windows.Visibility.Visible;
                _MainRegion.Visibility = System.Windows.Visibility.Collapsed;
                MyTreeView.Visibility = System.Windows.Visibility.Collapsed;
            }
            else
            {
                this.nonAnthenticatedUser.Visibility = System.Windows.Visibility.Collapsed;
                _MainRegion.Visibility = System.Windows.Visibility.Visible;
                MyTreeView.Visibility = System.Windows.Visibility.Visible;

                UserControl_Loaded(null, null);
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

            var mediator = Mediator.Instance; 
            mediator.DesignView = this;
            ViewModel.TreeViewd = MyTreeView;
            //MyTreeView.ItemsSource = ViewModel.CreateRootTreeViewItem();
           UpdateTreeView();
            // MyTreeView.ExpandAll();
            // mediator.AddTreeView(this.buildingTreeView1);


        }

        public void UpdateTreeView()
        {
            ViewModel.UpdateTreeView(); 
        }

        private void InitialBuildingConfiguration()
        {

        }

        private void Model_SelectedItem(object sender, EventArgs e)
        {
            properyGrid.Show(sender, ActionManager);
        }

       
 
        public ContentControl MainRegion { get { return this._MainRegion; } }

        public Tree TreeView
        {
            get { return MyTreeView; }
            set { MyTreeView = value; }
        }

        private void ContextMenu_Opened(object sender, RoutedEventArgs e)
        {
            ViewModel.UpdateDataContextItems();
            this.ContextMenu.Items.Clear();
            ViewModel.TreeViewdMenuItems.ToList().ForEach(ContextMenu.Items.Add);

        }

        public void SetSectionView(UserControl thermalSectionEditor)
        {
         
        }

        public IView3D MainView3D
        {
            get { throw new NotImplementedException(); }
        }

        public ContentControl SecondaryRegion
        {
            get { return this.SecondaryRegion; }
        }

        private void MyTreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            ViewModel.SelectedTreeViewItem = e.NewValue;

            ViewModel.DisplaySlectedElement(e.NewValue);
        }


        private void MyTreeView_SelectedItemChangedLiquid(object sender, TreeEventArgs e)
        {
            ViewModel.SelectedTreeViewItem = e.Source;

            ViewModel.DisplaySlectedElement(e.Source);
        }

        private void Tree_NodeDoubleClick(object sender, TreeEventArgs e)
        {
        
        }

        private void Tree_Drop(object sender, TreeEventArgs e)
        {
    
        }

        private void TestTree_NodeEdited(object sender, TreeEventArgs e)
        {
         
        }

        private void MyTreeView_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            ViewModel.KeyDownPressed(sender,e.Key);
        }
    }
}