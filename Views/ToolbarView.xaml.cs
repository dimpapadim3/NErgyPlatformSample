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
using NErgy.Silverlight.Views;
using NErgy.Silverlight.Views.Controllers.Commands;

namespace NErgy.Silverlight.Views
{
    public partial class ToolbarView : UserControl
    {
        public event EventHandler CommandSelected;

        #region Commands

        public ICommand UndoCommand { get { return new UndoCommand(Mediator.Instance); } }
        public ICommand RedoCommand { get { return new RedoCommand(Mediator.Instance); } }

        #endregion
          
        public ToolbarView()
        {
            InitializeComponent();
            Mediator.Instance.ToolbarView = this;
            DataContext = this;//the self is ViewModel 
        }

        private void ToolbarButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Image_ImageFailed(object sender, ExceptionRoutedEventArgs e)
        {

        }

        private void ToolbarButton_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private void AddBeam_Click(object sender, RoutedEventArgs e)
        {
            //if(CommandSelected!=null)
            //CommandSelected(new AddBeamFree(), null);
            Mediator.Instance.SaveBuildingData();
        }

        private void AddSlab_Click(object sender, RoutedEventArgs e)
        {
            if (CommandSelected != null)
                CommandSelected(new AddSlabRectangular(), null);
        }

        private void Selection_Click(object sender, RoutedEventArgs e)
        {
            if (CommandSelected != null)
                CommandSelected(new SelectionState(), null);
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {

        }
        private void New_click(object sender, RoutedEventArgs e)
        {
            Mediator.Instance.CreateNewDesignModel();
        }
        
 
         

        
    }
}
