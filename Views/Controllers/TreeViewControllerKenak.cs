using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using DynamicGeometry;
using NCad.Models;
using NErgy.Silverlight.Models;
using NErgy.Silverlight.Web.Models;

namespace NErgy.Silverlight.Views.Controllers
{
    public class TreeViewControllerKenak : NCad.Application.Views.Controllers.TreeViewController
    {
        protected override void SetUpScene(IModel model)
        {
            var thermalModel = model as ThermalBuildingModel;
            AddProject(thermalModel.Project);
        }

        private void AddProject(Project project)
        {
            var projectItem = new TreeViewItem() { };
            View1.Items.Add(projectItem);
            AddBuildings(projectItem, project.Building.ToList());
        }

        private void AddBuildings(TreeViewItem projectItem, IList<Web.Models.Building> buildings)
        {
            buildings.ForEach(b =>
                                  {
                                      var buildingItem = new TreeViewItem() { };
                                      View1.Items.Add(projectItem);
                                  });
        }


        protected override object CreateElementTreeViewItem(object element)
        {
            if (element is IList)
            {
                var tree = new TreeViewItem();
                foreach (var listElement in element as IList)
                {
                    tree.Items.Add(CreateElementTreeViewItem(listElement));
                }
            }
            if (element is Web.Models.Building)
            {
                return element;
            }
            return element;
        }
    }


}
