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
using NErgy.Silverlight.Web.Models;

namespace NErgy.Silverlight.Views
{
    public partial class HelpUserControl : UserControl
    {
        protected IDataAccesLayerServiceAgent DataAccesLayerService = ThermalBuildingModel.Instance.DataAccesLayerService;

        public HelpUserControl()
        {
            InitializeComponent();

            if (Mediator.Instance.ActivePageType == (int)HelpPageTypeId.SystemsPage)
                label2.Content = "Σχετικά με τα Συστήματα";
            if (Mediator.Instance.ActivePageType == (int)HelpPageTypeId.ShellPage)
                label2.Content = "Σχετικά με τα Κέλυφη";
            if (Mediator.Instance.ActivePageType == (int)HelpPageTypeId.ThermalZonePage)
                label2.Content = "Σχετικά με την Θερμική Ζώνη";
            if (Mediator.Instance.ActivePageType == (int)HelpPageTypeId.ProjectsPage)
                label2.Content = "Σχετικά με τά Έργα σας";
            isBusyIndicator.IsBusy = true;
            DataAccesLayerService.GetCommentsForPageType(Mediator.Instance.ActivePageType, LoadHelpTopics);

        }

        private static int TopicAddedDateComparer(HelpTopic x, HelpTopic y)
        {
            if(x.DateAdded>y.DateAdded)return -1;
            if (x.DateAdded < y.DateAdded) return 1;
            return 0;
        }

        private void LoadHelpTopics(List<HelpTopic> helpTopics, Exception e)
        {
            if (e != null)
            {
                isBusyIndicator.IsBusy = false;
            }

            if (helpTopics != null && helpTopics.Count > 0)
            {
                isBusyIndicator.IsBusy = false;
                helpTopics.Sort(TopicAddedDateComparer);
                this.listBox1.ItemsSource = helpTopics;
                
               
            }
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            
            var comment = new HelpTopic()
                              {
                                  HelpTopicCommnet = textBox1.Text,
                                  HelpTopicTypeID = Mediator.Instance.ActivePageType,
                                  DateAdded = DateTime.Now
                                  ,
                                  UserName = WebContext.Current.User.DisplayName ?? WebContext.Current.User.Name
                              };
            DataAccesLayerService.AddHelpTopic(comment);

            DataAccesLayerService.SaveChanges(exception =>
            {
                DataAccesLayerService.GetCommentsForPageType(Mediator.Instance.ActivePageType, LoadHelpTopics);
            });

        }
    }
}
