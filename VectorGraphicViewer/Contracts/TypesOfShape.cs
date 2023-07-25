using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VectorGraphicViewer.Contracts
{
    /// <summary>
    /// We can have only two shapes for 
    /// this project - circle and polygon. A line can be considered as a polygon
    /// with no filled property.
    /// Using this model we can extend our code to any shape with minimal
    /// to changes.
    /// </summary>
    public enum TypesOfShape
    {
        circle,
        polygon
    }
}
