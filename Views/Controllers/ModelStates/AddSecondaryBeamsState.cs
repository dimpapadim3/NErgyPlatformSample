using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;

using BuildingBasicDomain;
using BuildingStructuralElements;
using MVCFramework.Presenter;
using CommonTypes.Events;
using CommonTypes.Math;
using MVCFramework.Presenter.States;

namespace BuildingAplication.States
{
    public class AddSecondaryBeamsState: ControllState
    {

        # region "Singleteon Implementation"


        private static ControllState instance;

        public static ControllState Instance
        {
            get
            {
                if (instance == null)
                    instance = new AddSecondaryBeamsState();
                return instance;
            }
        }
        #endregion

        # region " IViewActionListener Implementation "
        /// <summary>
        /// Method MouseDownHandler
        /// </summary>
        /// <returns>A bool</returns>
        /// <param name="sender">An object</param>
        /// <param name="e">An ActionEventArgs</param>
        public override bool MouseDownHandler(Object sender, ActionEventArgs e)
        {
            if (e.Selected != null && (e.Selected is Area) )
            {
               Handle(e.Selected as Area);
            }
            return false;
        }

        /// <summary>
        /// Method MouseMoveHandler
        /// </summary>
        /// <returns>A bool</returns>
        /// <param name="sender">An object</param>
        /// <param name="e">An ActionEventArgs</param>
        public override bool MouseMoveHandler(Object sender, ActionEventArgs e)
        {
            // TODO: Implement this method
            return false;
        }

        /// <summary>
        /// Method MouseUpHandler
        /// </summary>
        /// <returns>A bool</returns>
        /// <param name="sender">An object</param>
        /// <param name="e">An ActionEventArgs</param>
        public override bool MouseUpHandler(Object sender, ActionEventArgs e)
        {
            // TODO: Implement this method
            return false;
        }
        #endregion 

        public AddSecondaryBeamsState()
        {
         
        }
 
        private void Handle(Area area)
        {
            _presenter._creator.CreateSecondaryBeams(area, 6);
        }
    }

    public class AddSecondaryTest : ControllState
    {
        private double[,] boundingRect;

        # region "Singleton Implementation"

        private static ControllState instance;
        private Slab  slab;

        public AddSecondaryTest()
        {
 
        }

        public static ControllState Instance
        {
            get
            {
                if (instance == null)
                    instance = new AddSecondaryTest();
                return instance;
            }
        }

        #endregion

        public Slab Slab
        {
            get
            {
                return slab;
            }
            set
            {
                slab = value;
            }
        }

        public override bool MouseDownHandler(object sender, ActionEventArgs e)
        {
            foreach (object _object in e.List)
            {
                if (_object is Slab)
                    SetUp(_object as Slab);
                    
            }
            drawLines();
            return false;
        }

        private List < ILine > drawLines()
        {
            if (Slab == null) return null;
           
            PolyLine<Line> poly = (PolyLine<Line>)Slab.Area;
            ILine line = poly[0];
            Vector3D vector = line.Direction;
            int spaceN = 5;
            int spaceS = 1;
      
            IPoint p1 = new Point(Slab.Area.Polygon[0].X, Slab.Area.Polygon[0].Y, Slab.Area.Polygon[0].Z);

            List<ILine> lines = new List<ILine> ();// GeometryWorkBench.UniformLines(p1, vector, spaceN, spaceS, 2, 15);

            Intersctions(lines, poly);
            return lines;

        }

        private void Intersctions(IList<ILine> lines, PolyLine<Line > poly )
        {
            IList<IPoint> points;Node node2 ; Node node1; NodeLine nodeline ;
            foreach (ILine line in lines)
            {
                points  = poly.PointsIntersects(line);
                if (points.Count == 2)
                {
                    node1 = new Node(points[0].X, points[0].Y, points[0].Z);
                    node2 = new Node(points[1].X, points[1].Y, points[1].Z);

                   nodeline = new NodeLine(node1, node2);
                   _presenter._creator.CreateBeam(nodeline);
                }
                
            }
        }

        private void SetUp(Slab slab)
        {
            this.Slab = slab;
            boundingRect = Slab.Area.Polygon.BoundingCube(1.0f);

            IPoint p1 = new Point(boundingRect[0 ,0], Slab.Area.Polygon[0].Y, boundingRect[0, 2]);
            IPoint p2 = new Point(boundingRect[0, 0], Slab.Area.Polygon[0].Y, boundingRect[1, 2]); 
            IPoint p3 = new Point(boundingRect[1, 0], Slab.Area.Polygon[0].Y, boundingRect[1, 2]);
            IPoint p4 = new Point(boundingRect[1, 0], Slab.Area.Polygon[0].Y, boundingRect[0, 2]);
            
            /*
            ConstructionLine line1 = new ConstructionLine();
            line1.SNode = p1;
            line1.ENode = p2;
            ConstructionLine line2 = new ConstructionLine();
            line2.SNode = p2;
            line2.ENode = p3; 
            ConstructionLine line3 = new ConstructionLine();
            line3.SNode = p3;
            line3.ENode = p4;
            ConstructionLine line4 = new ConstructionLine();
            line4.SNode = p4;
            line4.ENode = p1;
            */
          //  _creator.CreateLine(line1);
           // _creator.CreateLine(line2);
          //  _creator.CreateLine(line3);
         //   _creator.CreateLine(line4);


            
        }

        public override bool MouseMoveHandler(object sender, ActionEventArgs e)
        {
            return false;
        }

        public override bool MouseUpHandler(object sender, ActionEventArgs e)
        {
            return false;
        }
    }
}
