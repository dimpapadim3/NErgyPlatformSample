using System;
using System.Collections.ObjectModel;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using NErgy.Building;

namespace NErgy.Silverlight.Views.ViewModels
{
    public class OpeningViewModel : ViewModelBase<OpeningViewModel>
    {
   
        public OpeningViewModel(ITransparentElement opening)
        {
            transparentElement = opening;
        }
 
        #region Properties

        ITransparentElement transparentElement;
        public ITransparentElement TransparentElement
        {
            get { return transparentElement; }
            set
            {
                transparentElement = value;
                NotifyPropertyChanged(vm => vm.TransparentElement);
            }
        }

        public IKoufomaFrame Frame
        {
            get { return transparentElement.Frame; }
            set
            {
                transparentElement.Frame = value;
                NotifyPropertyChanged(vm => vm.Frame);
            }
        }

        public IOpeningsGlass Glass
        {
            get { return transparentElement.Glass; }
            set
            {
                transparentElement.Glass = value;
                NotifyPropertyChanged(vm => vm.Glass);
            }
        }

        public Double U
        {
            get { return transparentElement.U; }
        }


        #endregion //Properties

        private ObservableCollection<object> glassTypes;
        public ObservableCollection<object> GlassTypes
        {
            get { return glassTypes; }
            set
            {
                glassTypes = value;
                NotifyPropertyChanged(m => m.GlassTypes);
            }
        }
         
        private ObservableCollection<object> frameTypes;
        public ObservableCollection<object> FrameTypes
        {
            get { return frameTypes; }
            set
            {
                frameTypes = value;
                NotifyPropertyChanged(m => m.FrameTypes);
            }
        }

    }
}
