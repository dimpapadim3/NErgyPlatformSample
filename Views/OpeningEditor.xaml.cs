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
using NErgy.Building;
using NErgy.Silverlight.Views.ViewModels;

namespace NErgy.Silverlight.Views
{
    public partial class OpeningEditor : UserControl
    {
        public OpeningEditor()
        {
            InitializeComponent();
        }
 
        public static readonly DependencyProperty TransparentElementProperty =
        DependencyProperty.Register("TransparentElement", typeof(ITransparentElement),
        typeof(OpeningEditor), new PropertyMetadata(null, TransparentElementPropertyChangedCallBack));

        private static void TransparentElementPropertyChangedCallBack(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var editor = d as OpeningEditor;
            if (e.NewValue == null) return;
            editor.DataContext = new OpeningViewModel(e.NewValue as ITransparentElement); ;
        }
 
        public ITransparentElement TransparentElement
        {
            get { return (ITransparentElement)GetValue(TransparentElementProperty); }
            set { SetValue(TransparentElementProperty, value);                      }
        }

    }
}
