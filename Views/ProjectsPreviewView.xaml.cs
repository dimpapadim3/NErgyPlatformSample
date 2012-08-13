using System.Linq;
using System.Windows;
using System.Windows.Controls;
 
using NCad.Models;
using NErgy.Silverlight.Models;
using NErgy.Silverlight.Views.Controllers;
using NErgy.Silverlight.Views.ViewModels;
using NErgy.Silverlight.Web.Models;
using SimpleMvvmToolkit;

namespace NErgy.Silverlight.Views
{
    public partial class ProjectsPreviewView : UserControl
    {
        public static readonly DependencyProperty ProjectProperty =
            DependencyProperty.Register("Project", typeof(Project),
                                        typeof(ProjectsPreviewView),
                                        new PropertyMetadata(null, ProjectPropertyChangedCallBack));

        public Mediator Mediator;

        public ProjectsPreviewView()
        {
            InitializeComponent();//this.mainView3D.RotateScene();
             ProjectListViewModel.BuildingInProjectLoaded += BuildingLoaded;
        }

        public Project Project
        {
            get { return (Project)GetValue(ProjectProperty); }
            set { SetValue(ProjectProperty, value); }
        }

        public void BuildingLoaded(object sender, NotificationEventArgs<Project> e)
        {

            if (Project==null || e.Data.ProjectID != Project.ProjectID) return;

            var model = (ThermalBuildingModel)Mediator.Controller3D.Model;
            //model.Initialize();
            if (e.Data.Building != null)
            {
                model.LoadBuildingData(e.Data.Building.ToList().FirstOrDefault());
                Mediator.Controller3D.SetUpScene();
            }
        }

        private static void ProjectPropertyChangedCallBack(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue == null) return;
            var view = d as ProjectsPreviewView;
            var project = e.NewValue as Project;
            view.DataContext = project;

            var buildingModel = new ThermalBuildingModel(project);
            if (view != null)
            {
                view.Mediator = new Mediator(buildingModel);
                view.Mediator.Add3DView(view.mainView3D);
            }
        }
    }
}