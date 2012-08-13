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
using NErgy.Silverlight.Views.ViewModels;

namespace NErgy.Silverlight.Views
{
    public partial class LibraryEditorView : UserControl
    {
        public LibraryEditorView()
        {
            InitializeComponent();
        }


        public LibraryViewModel ViewModel
        {
            get { return this.DataContext as LibraryViewModel; }
            set { this.DataContext = value; }
   
        }
    }
}
