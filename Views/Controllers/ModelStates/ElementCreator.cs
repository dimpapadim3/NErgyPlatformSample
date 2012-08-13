using System;
using System.Collections.Generic;

using BuildingBasicDomain;
using BuildingStructuralElements;

namespace BuildingAplication
{
    public class ElementCreator
    {
   

        # region "elements Creation logic"
        /// <summary>
        /// Creates and Add to Models the Beam and Suports .NodeLine determines
        /// </summary>
        /// <param name="element"></param>
        public void CreateBeam(ILine element)
        {
            try
            {
                List<StructuralElement> ElementsToBeAdded = GetBeamAndSupports(element);
                CreateBeamAndSupport(ElementsToBeAdded);
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }

        public void CreateLoad(IPoint node)
        {
            //LoadingCase lc1 = new LoadingCase(2, "LiveLoads", LoadingCase.LCType.Q);
            //NodalLoad load = new NodalLoad(node);
            //load.LC = lc1;
            //load.Fz = 1000;
            ////  load.Fy = 1000;
            //_presenter.RequestAddition<Load>(load);
        }
        public void CreateLoad(ILine beam)
        {
          //  if (beam == null) return;
          //  LoadingCase lc1 = new LoadingCase(2, "LiveLoads", LoadingCase.LCType.Q);
          // NodalLoad load1 = new NodalLoad((beam).SNode);
          //  //load1.Fx = 100;
          //  load1.Fy = 2000;
          //NodalLoad load2 = new NodalLoad((beam).SNode);
          //  //load2.Fx = 100;
          //  load2.Fy = 2000;

          //  DistributedLoad destributed = new DistributedLoad(load1, load2, beam);
          //  destributed.LC = lc1;
          //  _presenter.RequestAddition<Load>(destributed);
        }

        public void CreateLoad(IArea area)
        {
            //SurfaceLoad serfuce = new SurfaceLoad(((Slab)area).Area);
            //serfuce.Pressure = new double[] { 0, 1000, 0, 0, 0, 0 };
            //_presenter.RequestAddition<Load>(serfuce);

        }
        /// <summary>
        /// Gets the Beams and Supports in a List of StructuralElements
        /// </summary>
        /// <param name="element"> Nodeline </param>
        /// <returns></returns>
        private List<StructuralElement> GetBeamAndSupports(ILine line)
        {
            List<StructuralElement> ElementsToBeAdded = new List<StructuralElement>();
            BeamElement createdBeam = BeamElement.CreateElement(line);

            ElementsToBeAdded.Add(createdBeam);
            if (createdBeam.isOnGround())
                foreach (Node groundnode in createdBeam.NodesOnGround())
                    if (!groundnode.IsSupported())
                    {
                        Support support = new Support(groundnode);
                        ElementsToBeAdded.Add(support);
                    }


            return ElementsToBeAdded;
        }

        /// <summary>
        /// Add the menulist of Elements to Models
        /// </summary>
        /// <param name="elementsToBeaAdded"></param>
        private void CreateBeamAndSupport(List<StructuralElement> elementsToBeaAdded)
        {
            //_presenter.RequestAddition<StructuralElement>(elementsToBeaAdded);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Lines"></param>
        public void CreateBeams(IList<ILine> Lines)
        {
            if (Lines == null) return;
            foreach (ILine line in Lines)
                CreateBeam(line);
        }

        /// <summary>
        /// Add an Area to modelcontroller 
        /// </summary>
        /// <param name="_area"></param>
        public void CreateArea(Area area)
        {
            //_presenter . RequestAddition<StructuralArea >(new StructuralArea (area));
        }

        /// <summary>
        /// Create Areas 
        /// </summary>
        /// <param name="areas"></param>
        public void CreateAreas(Area[] areas)
        {
            if (areas == null) return;
            foreach (Area area in areas)
                CreateArea(area);
        }

        /// <summary>
        /// gets the Slab form the _area and add it to  modelcontroller.
        /// </summary>
        /// <param name="_area"></param>
        public void CreateSlab(Area area)
        {
            Slab _slab = new Slab(area);
            //_presenter.RequestAddition<StructuralElement>(_slab);
            /*
          Command addSlab = new AddObjectCommand<StructuralElement>(_mainModel, _slab);

            if (addSlab.Execute())
                _mainPresenter.AddCommand(addSlab);
            else
                throw new Exception ("could not add Slab to Models ");
             * */
        }

        public void CreateSlabs(IList<Area> areas)
        {
            if (areas == null) return;
            foreach (Area area in areas)
                CreateSlab(area);
        }

        /// <summary>
        /// Create Seconday beams and add them to Models .
        /// </summary>
        /// <param name="area"></param>
        /// <param name="divisions"></param>
        public void CreateSecondaryBeams(Area area, int divisions)
        {

            List<StructuralElement> ElementsToBeAdded = new List<StructuralElement>();
            NodeLine[] lines = null;//= area.createSimpleLines(divisions);
            if (lines == null) return;

            Area[] areas = area.destributeOnBeams(lines);

            foreach (NodeLine line in lines)
            {
                ElementsToBeAdded.AddRange(this.GetBeamAndSupports(line));
            }
            this.CreateBeamAndSupport(ElementsToBeAdded);

        }    
#endregion
    }
}
