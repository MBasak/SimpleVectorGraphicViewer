using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VectorGraphicViewer.Contracts;
using VectorGraphicViewer.ViewHelpers;

namespace VectorGraphicViewer.Model.Shapes
{
    public abstract class Shape : NotifyPropertyChanged, IShape
    {
        public string Type { get; set; }

        public bool Filled { get; set; }
        public string BorderColor { get; set; }
        public string FillColor
        {
            get => Filled ? BorderColor : string.Empty;
        }
    }
}
