using System;
using System.Collections.Generic;
using System.Text;
using MVCFramework;

namespace MVCFramework.Presenter.States
{
    public class MoveCompositeState : MoveElementState
    {

        protected override bool DoObjectSelection(object _object)
        {
            _modiafiable = new CompositeModifiable();

            foreach (object selected in _presenter.SelectedObjects )
                if (selected is IModifiable)
                    ((CompositeModifiable)_modiafiable).Add(selected as IModifiable);

            return true;
        }

        public override bool MouseUpHandler(object sender, ActionEventArgs e)
        {
            base.MouseUpHandler(sender, e);
            _presenter.ClearSelection();
            return true;
        }
    }
}
