using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using NErgy.Silverlight.Models;
using NErgy.Silverlight.Views.Controllers;
using NErgy.Silverlight.Views.ViewModels;

namespace NErgy.Silverlight.Views
{
    public partial class ProjectView : UserControl
    {
        public ProjectView()
        {
            InitializeComponent();
          //  ViewModel = new ProjectListViewModel();
           //  ProjectListViewModel.SelectedProjectChanged +=  ProjectListViewModel_SelectedProjectChanged;
            Mediator.Instance.ProjectView = this; 
            //this.ViewModel.BuildingInProjectLoaded += projectPreview.BuildingLoaded;
            //this.ViewModel = new ProjectViewModel(ThermalBuildingModel.Instance.Project);
        }

        void ProjectListViewModel_SelectedProjectChanged(object sender, SimpleMvvmToolkit.NotificationEventArgs<Web.Models.Project> e)
        {
         // var preview = new ProjectsPreviewView() {Height = 500,Width = 500,AllowRotation = false};
         // preview.Project = e.Data;
         // Mediator.OpenInRegion(projectPreviewRegion,preview);
        }
 
        public ProjectViewModel ViewModel
        {
            get
            {
                return DataContext as ProjectViewModel;
            }
            set
            {
                DataContext  = value;
            }
        }
    }
}
