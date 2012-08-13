using System;
using System.Collections.Generic;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using BuildingStructuralElements;
using NCad.Application.Views.Controllers;
using NErgy.Building;
using Geometry = Balder.Objects.Geometries.Geometry;
using Point = BuildingBasicDomain.Point;

namespace NErgy.Silverlight.Views.Controllers
{
    public class OpeningEditorController:Controller3DBalder
    {
        List<Balder.Objects.Geometries.Geometry> geometries = new List<Balder.Objects.Geometries.Geometry>();
       
        protected void CreateOpeningGeometries(Opening opening)
        {
            CreateBeamElementGeometry(new BuildingStructuralElements.BeamElement() { BeamType = new Beam(),SNode = new Point( 0,0,0),ENode = new Point( 10,0,0)});
            //CreateBeamElementGeometry(new BuildingStructuralElements.BeamElement() { BeamType = new Column(), SNode = new Point(0, 0, 0) });
            //opening.Edges.ForEach(e =>
            //{

            //    if (e.IsVertical())
            //        geometries.Add(
            //            CreateBeamElementGeometry(new NErgy.Silverlight.BeamElement(e) { BeamType = new Column() }));
            //    if (e.IsHorizontal())
            //        geometries.Add(
            //                CreateBeamElementGeometry(new NErgy.Silverlight.BeamElement(e) { BeamType = new Beam() }));
            //});


        }

        public void AddOpeningToView(Opening opening)
        {
            CreateOpeningGeometries(opening);
            geometries.ForEach(((ControllerBase<object,Geometry>) this).AddGeometryToView);
        }

        public void ClearOpeningFromView()
        {
            geometries.ForEach(((ControllerBase<object, Geometry>)this).RemoveGeometryFromView);
        }

        public static Opening TestOpening()
        {

            var kouf = new KoufomaMono()
            {
                Height = 1,
                Width = 0.5,
                Glass = new OpeningsGlass() { Height = 0.8, Width = 0.4 },
                Frame = new KoufomaFrame() { Af = 1, Uf = 2 }
            };


            return new Opening()
                       {
                           //ThermalOpening = kouf
                       };
        }
    }
}
