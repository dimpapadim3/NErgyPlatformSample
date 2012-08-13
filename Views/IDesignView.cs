using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using Liquid;
using NCad.Views;

namespace NErgy.Silverlight.Views
{
    public interface  IDesignView
    {
        void SetSectionView(UserControl thermalSectionEditor);
        IView3D MainView3D { get;  }
        ContentControl SecondaryRegion { get;  }
        ContentControl MainRegion { get;  }
        Tree TreeView { get; set; }
        void UpdateLayout();
    }
}
