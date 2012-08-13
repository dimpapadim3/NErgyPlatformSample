using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using NErgy.Silverlight.Helpers;

namespace NErgy.Silverlight.Views.ViewModels
{
    public class SettingsViewModel : ViewModelBase<SettingsViewModel>
    {
        //public ApplicationSettings ApplicationSettings { get; set; }

        public bool IsLogEnabled
        {
            get { return ApplicationSettings.IsLoggingEnabled; }
            set
            {
                ApplicationSettings.IsLoggingEnabled = value;
                NotifyPropertyChanged(vm => vm.IsLogEnabled);
            }
        }

    }
}
