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
using Microsoft.Maps.MapControl;
using NCad.Models;
using NErgy.Silverlight.GeocodeService;
using NErgy.Silverlight.SearchService;
using NCad.Application.Views.Controllers;
using NCad.Views.Views3D;
using NErgy.Silverlight.Models;

namespace NErgy.Silverlight.Views
{
    public partial class BuildingMapPage : UserControl
    {

        MainView mainView3D;
        public BuildingMapPage()
        {
            InitializeComponent();
            return;

            //TrermalBuildingBuilder builder = new TrermalBuildingBuilder();
            //builder.CreateDefaultBuilding();
            //mainView3D = new MainView() { Width = 20, Height = 20 };
            //BalderViewController _Controller3D = new BalderViewController() { Mediator = new Mediator (builder.Model) };
            //_Controller3D.Model =builder. Model;
            //_Controller3D.View = mainView3D.View3D;
            //_Controller3D.SetUpScene();

          
            //MapPolygon mp = new MapPolygon();
            //mp.Locations = new LocationCollection();
            //mp.Locations.Add(new Location(10, 0));
            //mp.Locations.Add(new Location(10, 10));
            //mp.Locations.Add(new Location(0, 10));
            //mp.Locations.Add(new Location(0, 0));

            //mp.Content = mainView3D;
            //mp.MouseRightButtonDown += new MouseButtonEventHandler(mp_MouseRightButtonDown);
  
            //this.mainMap.Children.Add(mp);
            //this.mainMap.MouseWheel += new MouseWheelEventHandler(mainMap_MouseWheel);
        }

        void mp_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
           
        }

        void mainMap_MouseWheel(object sender, MouseWheelEventArgs e)
        {
           this.mainView3D.Height =  this.mainMap.ZoomLevel *10;
           this.mainView3D.Width  =  this.mainMap.ZoomLevel *10;
        }

        private  void TestSearch()
        {

            GeocodeRequest geocodeRequest = new GeocodeRequest();

            // Set the credentials using a valid Bing Maps key
            //geocodeRequest.Credentials = new NErgy.Silverlight.GeocodeService.Credentials();
            geocodeRequest.Credentials.ApplicationId = "AliveLsVdOVnhqNkgV_FToj2KurkqQev_CRld2F-3TRGJMzq5q8ycnNR9REGotXB";

            // Set the full address query
            geocodeRequest.Query = address.Text ;

            ConfidenceFilter[] filters = new ConfidenceFilter[1];
            filters[0] = new ConfidenceFilter();
            filters[0].MinimumConfidence = GeocodeService.Confidence.High;

            GeocodeOptions geocodeOptions = new GeocodeOptions();
            geocodeOptions.Filters = new System.Collections.ObjectModel.ObservableCollection<FilterBase>(filters);

            geocodeRequest.Options = geocodeOptions;

            GeocodeServiceClient geocodeService = new GeocodeServiceClient("BasicHttpBinding_IGeocodeService");
            geocodeService.GeocodeCompleted += new EventHandler<GeocodeCompletedEventArgs>(geocodeService_GeocodeCompleted);
            geocodeService.GeocodeAsync(geocodeRequest);


        }
          Microsoft.Maps.MapControl.Location  resultLoaction { get; set; }
          void geocodeService_GeocodeCompleted(object sender, GeocodeCompletedEventArgs e)
        {
            // The result is a GeocodeResponse object
            GeocodeResponse geocodeResponse = e.Result;

            if (geocodeResponse.Results.Count  > 0)
            {
                resultLoaction = new Microsoft.Maps.MapControl.Location(geocodeResponse.Results[0].Locations[0].Latitude,
                    +geocodeResponse.Results[0].Locations[0].Longitude);

                Pushpin pin = new Pushpin() { Location = resultLoaction };
                mainMap.Children.Add(pin);
            }
     
        }

          private void Button_Click(object sender, RoutedEventArgs e)
          {
              TestSearch();
          }
    }
}
