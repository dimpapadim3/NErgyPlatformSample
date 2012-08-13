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
using NCad.Application.Views.Controllers;

namespace NErgy.Silverlight.Views.Controllers.Commands
{


    public class UndoCommand : ICommand
    {
        private Mediator Mediator { get; set; }
        public UndoCommand(Mediator mediator)
        {
            this.Mediator = mediator;
        }

        #region ICommand Members

        public bool CanExecute(object parameter)
        {
            //get this from settings 
            return true;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
              Mediator.ActionManager.Redo();
        }

        #endregion
    }
}
