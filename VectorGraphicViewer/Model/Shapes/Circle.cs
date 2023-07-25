using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using VectorGraphicViewer.Contracts;

namespace VectorGraphicViewer.Model.Shapes
{
    public class Circle : Shape, ICircle
    {
        public string Radius { get; set; }

        public Point Center { get; set; }
        

        public Circle()
        {
            Type = TypesOfShape.circle.ToString();
        }
    }
}
