using System.Windows;

namespace VectorGraphicViewer.Contracts
{
    public interface ICircle: IShape
    {
        string Radius { get; set; }

        Point Center { get; set; }
    }
}