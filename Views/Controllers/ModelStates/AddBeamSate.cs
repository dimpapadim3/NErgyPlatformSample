using System;
using System.Collections.Generic;

using BuildingBasicDomain;

using BuildingStructuralElements;
using BuildingGrid;
using NCad.Models;

namespace NErgy.Silverlight.Views.Controllers
{
    /// <summary>
    /// Abstract state For BeamElement Addidtion
    /// </summary>
    public abstract class AddBeamState : ControllState
    {
       
    }

    /// <summary>
    /// Adds a beamElement by Selecting Only GridLines 
    /// </summary>
    public class AddBeamByClick : AddBeamState
    {
      
    }

    /// <summary>
    /// Adds a BeamElement by Selecting two Points 
    /// </summary>
    public class AddBeamFree : AddBeamState
    {
        
    }
}
