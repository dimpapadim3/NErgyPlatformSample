using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.ServiceModel.DomainServices.Client.ApplicationServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using NErgy.Silverlight.LoginUI;

namespace NErgy.Silverlight.Views
{
    public partial class NonAuthenticatedUserPage : UserControl
    {
        public LoginOperation LoginOperation { get; set; }
        public event EventHandler OnLoginOperationCompleted;

        public NonAuthenticatedUserPage()
        {
            InitializeComponent();
        }

        private void label1_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void HyperlinkButton_Click(object sender, RoutedEventArgs e)
        {
            if (!WebContext.Current.Authentication.IsLoggingIn)
            {
                LoginOperation = WebContext.Current.Authentication.Login("demo", "demodemo@");
                LoginOperation.Completed += (s, ev) =>
                {
                    if (OnLoginOperationCompleted != null)
                        OnLoginOperationCompleted(this, null);
                };
            }
            else

            {
                MessageBox.Show("Δοκημάστε Σε λίγο να συνδεθήτε σαν Demo");
            }
        }

        private void HyperlinkButton_Click_User(object sender, RoutedEventArgs e)
        {
            LoginRegistrationWindow loginWindow = new LoginRegistrationWindow();
            loginWindow.Show();
        }
    }
}
