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
using NErgy.Silverlight.Helpers;
using NErgy.Silverlight.Views.ViewModels;

namespace NErgy.Silverlight.Views
{
    public partial class SettingsPage : UserControl
    {
        public SettingsPage()
        {
            InitializeComponent();
            this.DataContext = new SettingsViewModel();
        }

        private void LoggButton_Click(object sender, RoutedEventArgs e)
        {
            Logger.Instance.LogMessages.ToList().ForEach(c=>
                                                             {
                                                                 Paragraph prgParagraph = new Paragraph();
                                                                 Run rnMyText = new Run();
                                                                 rnMyText.Text = c.LogTime+"  "+ c.Message;

                                                                 prgParagraph.Inlines.Add(rnMyText);
                                                                 prgParagraph.Inlines.Add(new LineBreak());
                                                                 richTextBox1.Blocks.Add(prgParagraph);
                                                             });
        }
    }
}
