using System.Collections.Generic;
using System.Windows;

namespace VectorGraphicViewer.Contracts
{
    public interface IPolygon: IShape
    {
        List<Point> Points { get; set; }
    }
}