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
using System.Windows.Navigation;
using NErgy.Silverlight.Models;
using NErgy.Silverlight.Web.Models;
using NErgy.Silverlight.Views.Controllers;

namespace NErgy.Silverlight.Views
{
    public partial class SecurityBlocked3DPage : Page
    {


        protected Mediator Mediator;

        public SecurityBlocked3DPage()
        {
            InitializeComponent();

            var buildingModel =new ThermalBuildingModel(new Project()
                                                            {
                                                                //Building = new List<Web.Models.Building>() {new Web.Models.Building()}
                                                            });
            Mediator = new Mediator(buildingModel);
            Mediator.Controller3D = new Controller3DBalder();
            buildingModel.Initialize();  
            buildingModel.BuildingBuilder.CreateDefaultBuilding(null);   
            Mediator.Add3DView(balderView1);
            Mediator.Controller3D.SetUpScene();
            balderView1.RotateScene();

            
        }

        // Executes when the user navigates to this page.
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

    }
}
