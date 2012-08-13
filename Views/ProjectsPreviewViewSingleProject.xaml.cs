using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
 
using NCad.Models;
using NErgy.Silverlight.Models;
using NErgy.Silverlight.Views.Controllers;
using NErgy.Silverlight.Views.ViewModels;
using NErgy.Silverlight.Web.Models;
using SimpleMvvmToolkit;
using NCad.Views.Views3D;

namespace NErgy.Silverlight.Views
{
    public partial class ProjectsPreviewViewSingleProject : UserControl
    {
        public static readonly DependencyProperty ProjectProperty =
            DependencyProperty.Register("Project", typeof(Project),
                                        typeof(ProjectsPreviewViewSingleProject),
                                        new PropertyMetadata(null, ProjectPropertyChangedCallBack));

        public Mediator Mediator;

        public ProjectsPreviewViewSingleProject()
        {
            InitializeComponent();      
            Mediator.Instance.MainView3D = this.mainView3D;
            AllowRotation = true;
            //this.mainView3D.RotateScene(); 
     
            ProjectListViewModel.BuildingInProjectLoaded += BuildingLoaded;
        
        }



        public BalderView View3d { get { return this.mainView3D; } }
        public Project Project
        {
            get { return (Project)GetValue(ProjectProperty); }
            set { SetValue(ProjectProperty, value); }
        }

        private bool allowRotation;
        public bool AllowRotation
        {
            get
            {
                return allowRotation;
              }
            set { allowRotation = value;
            if (allowRotation)
               Mediator.Instance.MainView3D.RotateScene();
            else 
                Mediator.Instance.MainView3D.StopRotateScene();

            }
        }

        public void BuildingLoaded(object sender, NotificationEventArgs<Project> e)
        {

           // if (Project==null || e.Data.ProjectID != Project.ProjectID) return;

        
            //model.Initialize();
 
            var view = this;
            var project = e.Data;
            view.DataContext = project;

            var buildingModel = new ThermalBuildingModel(project);
            if (view != null)
            {
                view.Mediator = new Mediator(buildingModel);
                //view.Mediator.Add3DView(view.mainView3D);
                view.Mediator.Add3DView(Mediator.Instance.MainView3D);
            }
            var model = (ThermalBuildingModel)Mediator.Controller3D.Model;
            if (e.Data.Building != null)
            {
                model.LoadBuildingData(e.Data.Building.ToList().FirstOrDefault());
                Mediator.Controller3D.View.Clear();
                Mediator.Controller3D.SetUpScene();
                //this.Region3D.Content = Mediator.Instance.MainView3D;
            }
        }

        private static void ProjectPropertyChangedCallBack(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue == null) return;
            var view = d as ProjectsPreviewViewSingleProject;
            var project = e.NewValue as Project;
            view.DataContext = project;

            var buildingModel = new ThermalBuildingModel(project);
            if (view != null)
            {
                view.Mediator = new Mediator(buildingModel);
                //view.Mediator.Add3DView(view.mainView3D);
                view.Mediator.Add3DView(Mediator.Instance.MainView3D);
            }
        }

        private void UserControl_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (e.NewSize.IsEmpty) return;
            
  
        }
    }
}