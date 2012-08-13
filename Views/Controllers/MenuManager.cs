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
//using SL4PopupMenu;
using NErgy.Silverlight.Models;
using NCad.Views.Controllers;
using BuildingStructuralElements;

namespace NErgy.Silverlight.Views.Controllers
{
    public class MenuManager : NCad.Application.Views.Controllers.SLToolkitMenuManager
    {
        public MenuManager()
        {
            
        }
        public MenuManager(IController Controller)
            : base(Controller)
        {
            Controller.ViewChanged += new EventHandler(Controller_ViewChanged);
        }

        void Controller_ViewChanged(object sender, EventArgs e)
        {
            AttachMenuToView(sender as FrameworkElement);
        }

        //private void AddGeneralMenuItems(PopupMenu PopupMenu)
        //{
        //    PopupMenu.AddItem("Copy Level", delegate { (Controller.Model as ThermalBuildingModel).CopyStory(); });
        //    PopupMenu.AddItem("Create New Level", delegate { (Controller.Model as ThermalBuildingModel).CreateNewStory(); });
        //}

        
        //private void AddShearWallMenuItems(PopupMenu PopupMenu)
        //{
        //    PopupMenu.AddItem("Create Door", delegate { Controller.Model.CreateDefaultDoorOpeningOnWall(); });
        //    PopupMenu.AddItem("Create Window", delegate { Controller.Model.CreateWindowOpeningOnWall(); });
        //    PopupMenu.AddItem("Edit Surface", delegate { Controller.Mediator.EditSurface(Controller.Model.CurrentSelectedItem as StructuralArea); });
        //}


        //private void AddSlabMenuItems(PopupMenu PopupMenu)
        //{
        //    PopupMenu.AddItem("Create Walls On Slab Edges", delegate { (Controller.Model as ThermalBuildingModel).CreateWallsOnSlabEdges(); });
        //}


        //public void GenerateMenuItems(SL4PopupMenu.PopupMenu PopupMenu)
        //{
        //    AddGeneralMenuItems(PopupMenu);

        //    if (Controller.Model.CurrentSelectedItem is Slab)
        //        AddSlabMenuItems(PopupMenu);
        //    if (Controller.Model.CurrentSelectedItem is ShearWall)
        //        AddShearWallMenuItems(PopupMenu);
        //}

    }
}
