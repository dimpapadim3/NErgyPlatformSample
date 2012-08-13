using System;
using System.Collections;
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
//using NErgy.Silverlight.Helpers;
using NErgy.Silverlight.Views.Controllers;
//using PdfSharp.Pdf;

namespace NErgy.Silverlight.Views
{
    public partial class ReportsView : UserControl
    {
        public ReportsView()
        {
            InitializeComponent();
        }

        public void button1_Click(object sender, RoutedEventArgs e)
        {
            //ReportsManager reportsManager = new ReportsManager();
 
            // SaveFileDialog d = new SaveFileDialog();
            //d.Filter = "PDF file format|*.pdf";

            //// Save the document...
            //if (d.ShowDialog() == true)
            //{
 
            //    reportsManager.GenerateSectionCatalogReport(TestCompositeThermalSection());
            //    reportsManager.DrawOpening(OpeningEditorController. TestOpening());
            //    reportsManager.Document.Save(d.OpenFile());
            //}


        }

        public static CompositeThermalSection TestCompositeThermalSection()
        {

            return new CompositeThermalSection()
            {
                Sections = new List<IThermalSection>()
                                          {
                                              new NErgy.Silverlight.Web.Models.Section() ,
                                              new NErgy.Silverlight.Web.Models.Section (),
                                              new NErgy.Silverlight.Web.Models.Section() ,
                                          }
            };
        }
     



       
    }
}
