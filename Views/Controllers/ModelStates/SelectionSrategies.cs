using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;

namespace MVCFramework.Presenter.States
{
    public abstract class SelectionStrategy
    {
        public delegate bool SelectionHandlingDelegate(object _object);
        public delegate void CoordinatesHandlingDelegate(double[] coords);
        public SelectionHandlingDelegate selectionHandler;
        public CoordinatesHandlingDelegate coordinatesHandler;

        public abstract void ResolveSelection(object sender, EventArgs e);
    }

    public class SingleSelectionStrategy:SelectionStrategy
    {
        public override void ResolveSelection(object sender, EventArgs e)
        {
             
        }
    }

    public class MultiSelectionStrategy:SelectionStrategy
    {
        public override void ResolveSelection(object sender, EventArgs e)
        { 
        }
    }

    public class SortedSelectionStrategy:SelectionStrategy
    {
        public override void ResolveSelection(object sender, EventArgs e)
        {
          
            return ;
        }

        private bool Filter<T>(List<T> list)
        {
            bool val = false;
            foreach (object _object in list)
                if (_object is T)
                    val = val || selectionHandler(_object);
            return val;
        }
    }

}
