using System;
using System.ComponentModel;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using NErgy.Silverlight.Views.Controllers;
using SimpleMvvmToolkit;
using MenuItem = SilverlightMenu.Library.MenuItem;
using RelayCommand = SilverlightCommands.RelayCommand;
namespace NErgy.Silverlight.Views.ViewModels
{
    public   class MenuItemExtension :MenuItem
    {
        public ICommand Command { get; set; }
    }
    public class MenuViewModel : INotifyPropertyChanged, IStatusBar
    {
        private string _statusBarDisplayMessage;
        public string StatusBarDisplayMessage
        {
            get { return _statusBarDisplayMessage; }
            set
            {
                _statusBarDisplayMessage = value;
                OnPropertyChanged("StatusBarDisplayMessage");
            }
        }


        MenuItem mvvmMenuItem;
        string imagesPath = "../../Assets/MenuIcons/";
        public MenuViewModel()
        {
            mvvmMenuItem = new MenuItem()
            {
                Name = "Root"
            };

            var mnuFile = new MenuItem() { Name = "mnuFile", Text = "Αρχεία" };
           // var mnuEdit = new MenuItem() { Name = "mnuEdit", Text = "Edit" };
           // var mnuWindow = new MenuItem() { Name = "mnuWindow", Text = "Window" };
            var mnuHelp = new MenuItem() { Name = "mnuHelp", Text = "Βοήθεια" };

            var mnuNew = new MenuItem() { Name = "mnuNew", Text = "Νέο" };
            var mnuSeparator1 = new MenuItem() { Name = "mnuSeparator1", Text = "-" };
            var mnuOpenFile = new MenuItemExtension() { Name = "mnuOpenFile", Text = "Ανοιγμα αρχείου", IsEnabled = true,Command = new DelegateCommand(()=>
                                                                                                                                                  {
                                                                                                                                                      Mediator.Instance.LoadProjectFromXml();
                                                                                                                                                  })};
            var mnuSaveFile = new MenuItemExtension()
            {
                Name = "mnuSave",
                Text = "Αποθήκευση αρχείου",
                IsEnabled = true,
                Command = new DelegateCommand(() =>
                                                                                                                                                  {
                                                                                                                                                      Mediator.Instance.SaveProjectAsXml();
                                                                                                                                                  })};
          //  var mnuCloseFile = new MenuItem() { Name = "mnuClose", Text = "Close File", IsEnabled = false };
            var mnuSeparator2 = new MenuItem() { Name = "mnuSeparator2", Text = "-" };
            var mnuExit = new MenuItem() { Name = "mnuExit", Text = "Exit" };

            var mnuNewFile = new MenuItemExtension() { Name = "mnuNewFile", Text = "Νέο Έργο(Μέλετη)"  ,Command = new DelegateCommand(() =>
                {
                    Mediator.Instance.CreateNewProjectStudy();
                }) };
            var mnuNewProject = new MenuItemExtension()
            {
                Name = "mnuNewProject",
                Text = "Νέο Έργο(Επιθεώρηση)",
                Command = new DelegateCommand(() =>
                {
                    Mediator.Instance.CreateNewProject();
                })
            };
          //  var mnuNewSolution = new MenuItem() { Name = "mnuNewSolution", Text = "New Solution" };

            var mnuCut = new MenuItem() { Name = "mnuCut", Text = "Cut" };
            var mnuCopy = new MenuItem() { Name = "mnuCopy", Text = "Copy" };
            var mnuPaste = new MenuItem() { Name = "mnuPaste", Text = "Paste" };
            var mnuDelete = new MenuItem() { Name = "mnuDelete", Text = "Delete" };

            var mnuWindow1 = new MenuItem() { Name = "mnuWindow1", Text = "Window 1", IsChecked = true, IsCheckable = true };
            var mnuWindow2 = new MenuItem() { Name = "mnuWindow2", Text = "Window 2", IsChecked = false, IsCheckable = true };
            var mnuWindow3 = new MenuItem() { Name = "mnuWindow3", Text = "Window 3", IsChecked = false, IsCheckable = true };

            var mnuAbout = new MenuItem() { Name = "mnuViewHelp", Text = "Σχετικά με Την Εφαρμογή" };

            var mnuRefreash = new MenuItem() { Name = "mnuRefreash", Text = " " };
            mnuNew.Add(mnuNewFile);
            mnuNew.Add(mnuNewProject);
       //     mnuNew.Add(mnuNewSolution);

            mnuFile.Add(mnuNew);
            mnuFile.Add(mnuSeparator1);
            mnuFile.Add(mnuOpenFile);
            mnuFile.Add(mnuSaveFile);
           // mnuFile.Add(mnuCloseFile);
            mnuFile.Add(mnuSeparator2);
            mnuFile.Add(mnuExit);

            //mnuEdit.Add(mnuCut);
            //mnuEdit.Add(mnuCopy);
            //mnuEdit.Add(mnuPaste);
            //mnuEdit.Add(mnuDelete);

            //mnuWindow.Add(mnuWindow1);
            //mnuWindow.Add(mnuWindow2);
            //mnuWindow.Add(mnuWindow3);

            mnuHelp.Add(mnuAbout);

            mvvmMenuItem.Add(mnuFile);
          //  mvvmMenuItem.Add(mnuEdit);
          //  mvvmMenuItem.Add(mnuWindow);
            mvvmMenuItem.Add(mnuHelp);
            mvvmMenuItem.Add(mnuRefreash);
        }

        public MenuItem MVVMMenuItem
        {
            get
            {
                return mvvmMenuItem;
            }
            set
            {
                mvvmMenuItem = value;
                OnPropertyChanged("MVVMMenuItem");
            }
        }

        public string ImagesPath
        {
            get
            {
                return imagesPath;
            }
            set
            {
                imagesPath = value;
                OnPropertyChanged("ImagesPath");
            }
        }

        public ICommand MenuCommand
        {
            get { return new RelayCommand(p => DoMenuCommand(p)); }
        }

        public void DoMenuCommand(object param)
        {
            var menuItem = (MenuItem)param;

            if (menuItem is MenuItemExtension)
            {
               ( menuItem as MenuItemExtension).Command.Execute(null);
            }
            //if (menuItem.IsCheckable)
            //    MessageBox.Show(string.Format("{0} is now {1}", menuItem.Name, menuItem.IsChecked ? "CHECKED" : "UNCHECKED"));
            //else
            //    MessageBox.Show(string.Format("You clicked: {0}", menuItem.Name));

        }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        #region Implementation of IStatusBar

        public void Display(string message)
        {
            this.StatusBarDisplayMessage = message;
        }

        #endregion
    }

}
