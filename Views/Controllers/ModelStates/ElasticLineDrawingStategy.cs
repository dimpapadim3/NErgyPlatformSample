using System;
using System.Collections.Generic;
using System.Text;

namespace BuildingAplication
{
    public abstract class ElasticLineDrawingStategy
    {
      //  public System.Drawing.Color DrawingLinesColor = (System.Drawing.Color.Black );//BuildingAplication.Properties.Settings.Default["ElasticLinesColor"];

        public abstract void HandleDownPoint(BuildingBasicDomain.IPoint point);
        public abstract void HandleMovePoint(BuildingBasicDomain.IPoint point);
        public abstract void HandleUpPoint(BuildingBasicDomain.IPoint point);

        public abstract void  ClearLines();
        public abstract void  Finalize();
    }

    public class NullElasticLineDrawingStrategy : ElasticLineDrawingStategy
    {
        public override void HandleDownPoint(BuildingBasicDomain.IPoint point)
        {

        }

        public override void HandleMovePoint(BuildingBasicDomain.IPoint point)
        {

        }

        public override void HandleUpPoint(BuildingBasicDomain.IPoint point)
        {

        }

        public override void ClearLines()
        {

        }

        public override void Finalize()
        {

        }
    }

    public class SimpleSelectionElasticBox : ElasticLineDrawingStategy
    {
        protected  BuildingStructuralElements .ElasticLine  line1;
      //  protected  MVCFramework .Presenter .Presenter _presenter ;


        protected virtual void InitializeElasticLines()
        {
            line1 = new BuildingStructuralElements.ElasticLine();
          //  line1.Color = (System.Drawing.Color.Red);// BuildingAplication.Properties.Settings.Default["ElasticLinesColor"];
        }

        /// <summary>
        /// Templete method 
        /// </summary>
        /// <param name="point"></param>
        public override void HandleDownPoint(BuildingBasicDomain.IPoint point)
        {
            if (line1 == null)
            {
                InitializeLine(ref line1);

                line1.SNode = BuildingStructuralElements.Node.CreateNode(point);
                line1.ENode = BuildingStructuralElements.Node.CreateNode(point);

                DisplayLineToView(line1);
            }
            else
            {
                ClearLines();

                InitializeLine(ref line1);

                line1.SNode = BuildingStructuralElements.Node.CreateNode(point);
                line1.ENode = BuildingStructuralElements.Node.CreateNode(point);

                DisplayLineToView(line1);
            }
            

        }

        /// <summary>
        /// fuctory method for line 
        /// </summary>
        /// <param name="line1"></param>
        protected virtual void InitializeLine(ref BuildingStructuralElements .ElasticLine  line)
        {
            line = new BuildingStructuralElements.ElasticLine();
            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="line1"></param>
        protected virtual void DisplayLineToView(BuildingBasicDomain .ILine line)
        {
          //  _presenter.RequestAddition<MVCFramework.IModelData>(line1 as BuildingStructuralElements .ElasticLine);
          //  _presenter.AddLine(line);
            
        }

        public override void HandleMovePoint(BuildingBasicDomain.IPoint point)
        {
            if (line1 != null)
                line1.ENode = point;
        }

        public override void HandleUpPoint(BuildingBasicDomain.IPoint point)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override void ClearLines()
        {
            //_presenter.RemoveLine(line1);
            line1 = null;
        }

        public override void Finalize()
        {
            ClearLines();
        }
    }

    public class ComplexSelectionElasticBox : SimpleSelectionElasticBox
    {    
        protected  BuildingStructuralElements .ElasticLine  line1, line2;

     //   public ComplexSelectionElasticBox(MVCFramework.Presenter.Presenter presenter):base (presenter ){}
     //
        public override void HandleDownPoint(BuildingBasicDomain.IPoint point)
        {

            if (line2 == null)
            {
                InitializeLine(ref line2);
                line2.SNode = BuildingStructuralElements.Node.CreateNode(point);
                line2.ENode = BuildingStructuralElements.Node.CreateNode(point);
 
            }
            if (this.line2 != line1)
                DisplayLineToView(line2);
             
            base.HandleDownPoint(point);
        }

        public override void HandleMovePoint(BuildingBasicDomain.IPoint point)
        {
            if (line2 != null)
                line2.ENode = point;

            base.HandleMovePoint(point);
        }

        public override void ClearLines()
        {
            base.ClearLines();
        }

        public override void Finalize()
        {      
            //_presenter.RemoveLine(line2);
            line2 = null;
            base.Finalize();
        }
    }

    public class RectangularSelectionElasicBox : ElasticLineDrawingStategy
    {
        BuildingStructuralElements .ElasticLine  line1,line2,line3,line4;

        BuildingBasicDomain.IPoint point1, point2;

        /// <summary>
        /// fuctory method for line 
        /// </summary>
        /// <param name="line1"></param>
        protected virtual void InitializeLine(ref BuildingStructuralElements .ElasticLine line)
        {
            line = new BuildingStructuralElements.ElasticLine();
            //line .Color = DrawingLinesColor ;
        }


        public override void HandleDownPoint(BuildingBasicDomain.IPoint point)
        {
            if (point1 == null)
            {
                point1 = BuildingStructuralElements.Node.CreateNode(point);
                point2 = BuildingStructuralElements.Node.CreateNode(point);

                InitializeLine(ref line1);            
                InitializeLine(ref line2);
                InitializeLine(ref line3);
                InitializeLine(ref line4);

            }
        }

        public override void HandleMovePoint(BuildingBasicDomain.IPoint point)
        {

        }

        public override void HandleUpPoint(BuildingBasicDomain.IPoint point)
        {

        }

        public override void ClearLines()
        {

        }

        public override void Finalize()
        {

        }
    }

}
