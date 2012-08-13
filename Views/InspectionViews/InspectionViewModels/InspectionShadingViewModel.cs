using System;
using System.Collections.Generic;
using System.Linq;
using NErgy.Building;
using NErgy.Building.BuildingShell.ConductivityStandards;
using NErgy.Building.BuildingShell.ShadingStandards;
using NErgy.Silverlight.Web.Models;
using FrameType = NErgy.Building.BuildingShell.ConductivityStandards.FrameType;

namespace NErgy.Silverlight.Views.ViewModels
{
    public class InspectionShadingViewModel : ViewModelBase<InspectionShadingViewModel>
    {
        public InspectionShadingViewModel(IShadedElement shadedElement)
        {
            AreaOrientation = new List<EnumObject<AreaOrientation>>(GetEnumList<AreaOrientation>());
            Angle = new List<EnumObject<Angle>>(GetEnumList<Angle>());
        }

        private List<EnumObject<Angle>> _angle;
        protected List<EnumObject<Angle>> Angle
        {
            get { return _angle; }
            set { _angle = value;
            NotifyPropertyChanged(m => m.Angle);
            }
        }

        private EnumObject<Angle> _selectedAngle;
        public EnumObject<Angle> SelectedAngle
        {
            get { return _selectedAngle; }
            set
            {
                _selectedAngle = value;
                NotifyPropertyChanged(m => m.SelectedAngle);
            }
        }

        private List<EnumObject<AreaOrientation>> _areaOrientation;
        protected List<EnumObject<AreaOrientation>> AreaOrientation
        {
            get { return _areaOrientation; }
            set { _areaOrientation = value; NotifyPropertyChanged(m => m.AreaOrientation); }
        }

       
        private EnumObject<DefaultUgConductivityStandard.OpeningsGlassType> _selectedAreaOrientation;
        public EnumObject<DefaultUgConductivityStandard.OpeningsGlassType> SelectedAreaOrientation
        {
            get { return _selectedAreaOrientation; }
            set
            {
                _selectedAreaOrientation = value;
                NotifyPropertyChanged(m => m.SelectedAreaOrientation);
            }
        }
 

        private Shader Shader;

        private void DefaultUgconductivityStandartLoaded(List<Shader> entities, Exception error)
        {
            // If no error is returned, set the model to entities
            if (error == null)
            {
                Shader = entities.FirstOrDefault();
            }
     
            else
            {
            
            }

        
        }
         
    }
}