using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Collections.Generic;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using NErgy.Silverlight.Web.Models;

namespace NErgy.Silverlight.Views.ViewModels
{
    public class ProjectsItemsControlViewModel : ViewModelBase<ProjectsItemsControlViewModel>
    {
       

        private ObservableCollection<ProjectViewModel> _displayedProjects;
        public ObservableCollection<ProjectViewModel> DisplayedProjects
        {
            get { return _displayedProjects; }
            set
            {
                _displayedProjects = value;
                //_displayedProjects.ToList().ForEach(p=>p.)
                NotifyPropertyChanged(vm => vm.DisplayedProjects);
            }
        }


        public ProjectsItemsControlViewModel()
        {
            
        }




    }
}
