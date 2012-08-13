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

namespace NErgy.Silverlight.Views.Controllers
{
    public class TreeViewControllerDefault : NCad.Application.Views.Controllers.TreeViewController
    {
        protected override object CreateElementTreeViewItem(object element)
        {
            if (element is Web.Models.Building)
            {
                return element;
            }
            return element;
        }
    }
}
