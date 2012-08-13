using System;

namespace NErgy.Silverlight.Views.Controllers
{
    /// <summary>
    /// controll State resolves  the actions That must be made in the specified mouse Event .
    /// (only supports mouseEvents )
    /// </summary>
    public abstract class ControllState  
    {
 

        /// <summary>
        /// deterimes witch object from the list will be selected  . 
        /// or how and when  the coordinates will be Used 
        /// this should propably moved and unified with view selection 
        /// </summary>
        //protected SelectionStrategy selectionStrategy;

        public  void initModel( )
        {
             
            InitializeSelectionStrategy();
        }

        protected virtual void InitializeSelectionStrategy()
        {
           // InitializeSelectionStrategy(typeof(SingleSelectionStrategy));
        }
        public virtual void InitializeSelectionStrategy(Type strategy)
        {
        //  selectionStrategy = Activator.CreateInstance(strategy)as SelectionStrategy;
          //selectionStrategy.selectionHandler = this.DoObjectSelection;
          // selectionStrategy.coordinatesHandler = this.DoHandleCoordinates;
        }

        public void initPresenter( )
        {
            
            //_presenter.OnStateChange += new EventHandler(StateChangeHandler);
        }
        public virtual void StateChangeHandler(object sender, EventArgs e) { } //make abstract 

        #region IViewActionListener Members

        //public abstract bool MouseDownHandler(object sender, EventArgs e);
        //public abstract bool MouseMoveHandler(object sender, EventArgs e);
        //public abstract bool MouseUpHandler(object sender, EventArgs e);

        #endregion

        # region " Selection Resolve "
        /// <summary>
        /// examines the list of objects selected in view 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void ResolveSelection(object sender, EventArgs e)
        {
            //selectionStrategy.ResolveSelection(sender, e);
        }
        //Handles the Coordinates event occured
        protected virtual void DoHandleCoordinates(double [] coords)
        {
            
        }
        //Makes the Check for an object that is Selected
        protected virtual bool DoObjectSelection(object _object)
        {
            return false;
        }

        #endregion

        #region "MenuHandling Block"
 

        #endregion

        public virtual bool IsNull { get { return false; } }

    }

    public class NullState : ControllState
    {
        public NullState()
        {
           
        }

        public override bool IsNull
        {
            get
            {
                return true;
            }
        }

        public void Handle(object SelectedObject)
        {
            return;
        }

        //public override bool MouseDownHandler(object sender, EventArgs e)
        //{
        //    return false;
        //}

        //public override bool MouseMoveHandler(object sender, EventArgs e)
        //{
        //   // throw new ControllStateExeption("The method or operation is not implemented.");
        //    return false;
        //}

        //public override bool MouseUpHandler(object sender, EventArgs e)
        //{
        //   // throw new ControllStateExeption("The method or operation is not implemented.");
        //    return false;
        //}
    }


    public class SelectionState:ControllState
    { 
        
    }
 
 
   
}
