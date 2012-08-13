using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Browser;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Navigation;
using Microsoft.Maps.MapControl;
namespace NErgy.Silverlight.Views.Engineers
{
    public partial class EngineerProfileView : Page
    {
        public EngineerProfileView()
        {
            InitializeComponent();
        }

        // Executes when the user navigates to this page.
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }


        public void OpenImageToUpload()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "*.*";

            if (openFileDialog.ShowDialog() == true)
            {
                Stream stream = (Stream)openFileDialog.File.OpenRead();
                byte[] bytes = new byte[stream.Length];
                stream.Read(bytes, 0, (int)stream.Length);
                var bi = new BitmapImage();
                bi.SetSource(stream);
                UserImage.Source = bi;
                string fileName = openFileDialog.File.Name; 
            }
        }

        private Image _userImage;
        public Image UserImage
        {
            get { return _userImage; }
            set { _userImage = value; }
        }

        //private int geocodesInProgress;
        //private Microsoft.Maps.MapControl.PlatformServices.GeocodeServiceClient geocodeClient;
        //private Microsoft.Maps.MapControl.PlatformServices.GeocodeServiceClient GeocodeClient
        //{
        //    get
        //    {
        //        if (null == geocodeClient)
        //        {
        //            //Handle http/https; OutOfBrowser is currently supported on the MapControl only for http pages
        //            bool httpsUriScheme = !Application.Current.IsRunningOutOfBrowser && HtmlPage.Document.DocumentUri.Scheme.Equals(Uri.UriSchemeHttps);
        //            BasicHttpBinding binding = httpsUriScheme ? new BasicHttpBinding(BasicHttpSecurityMode.Transport) : new BasicHttpBinding(BasicHttpSecurityMode.None);
        //            UriBuilder serviceUri = new UriBuilder("http://dev.virtualearth.net/webservices/v1/GeocodeService/GeocodeService.svc");
        //            if (httpsUriScheme)
        //            {
        //                //For https, change the UriSceheme to https and change it to use the default https port.
        //                serviceUri.Scheme = Uri.UriSchemeHttps;
        //                serviceUri.Port = -1;
        //            }

        //            //Create the Service Client
        //            geocodeClient = new PlatformServices.GeocodeServiceClient(binding, new EndpointAddress(serviceUri.Uri));
        //            geocodeClient.GeocodeCompleted += new EventHandler<PlatformServices.GeocodeCompletedEventArgs>(client_GeocodeCompleted);
        //        }
        //        return geocodeClient;
        //    }
        //}

        //private GeocodeLayer geocodeLayer;
        //private GeocodeLayer GeocodeLayer
        //{
        //    get
        //    {
        //        if (null == geocodeLayer)
        //        {
        //            geocodeLayer = new GeocodeLayer(MyMap);
        //        }
        //        return geocodeLayer;
        //    }
        //}

        //private void GeocodeAddress(string address)
        //{
        //    PlatformServices.GeocodeRequest request = new PlatformServices.GeocodeRequest();
        //    request.Culture = MyMap.Culture;
        //    request.Query = address;
        //    // Don't raise exceptions.
        //    request.ExecutionOptions = new PlatformServices.ExecutionOptions();
        //    request.ExecutionOptions.SuppressFaults = true;

        //    // Only accept results with high confidence.
        //    request.Options = new PlatformServices.GeocodeOptions();
        //    // Using ObservableCollection since this is the default for Silverlight proxy generation.
        //    request.Options.Filters = new ObservableCollection<PlatformServices.FilterBase>();
        //    PlatformServices.ConfidenceFilter filter = new PlatformServices.ConfidenceFilter();
        //    filter.MinimumConfidence = PlatformServices.Confidence.High;
        //    request.Options.Filters.Add(filter);

        //    Output.Text = "< geocoding " + address + " >";
        //    geocodesInProgress++;

        //    MyMap.CredentialsProvider.GetCredentials(
        //        (Credentials credentials) =>
        //        {
        //            //Pass in credentials for web services call.
        //            //Replace with your own Credentials.
        //            request.Credentials = credentials;

        //            // Make asynchronous call to fetch the data ... pass state object.
        //            GeocodeClient.GeocodeAsync(request, address);
        //        });
        //}

        //private void client_GeocodeCompleted(object sender, PlatformServices.GeocodeCompletedEventArgs e)
        //{
        //    // Callback when the geocode is finished.
        //    string outString;
        //    geocodesInProgress--;
        //    try
        //    {
        //        if (e.Result.ResponseSummary.StatusCode != PlatformServices.ResponseStatusCode.Success)
        //        {
        //            outString = "error geocoding ... status <" + e.Result.ResponseSummary.StatusCode.ToString() + ">";
        //        }
        //        else if (0 == e.Result.Results.Count)
        //        {
        //            outString = "No results";
        //        }
        //        else
        //        {
        //            // Only report on first result.
        //            outString = e.Result.Results[0].DisplayName;
        //            Location loc = GeocodeLayer.AddResult(e.Result.Results[0]);
        //            // Zoom the map to the location of the item.
        //            MyMap.SetView(loc, 18);
        //        }
        //    }
        //    catch
        //    {
        //        outString = "Exception raised";
        //    }

        //    Output.Text = outString;
        //}


        //private void GeocodeInput()
        //{
        //    // Geocode whatever is in the textbox.
        //    string address = Input.Text;
        //    if (address.Length > 0)
        //    {
        //        GeocodeAddress(address);
        //    }
        //}

        //private void Button_Click(object sender, RoutedEventArgs e)
        //{
        //    GeocodeInput();
        //}

        //private void Input_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.Key == Key.Enter)
        //    {
        //        GeocodeInput();
        //    }
        //}

        private void Input_GotFocus(object sender, RoutedEventArgs e)
        {
            Input.SelectAll();
        }

        private void Input_TextChanged(object sender, TextChangedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void Input_KeyDown(object sender, KeyEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
