using System.Collections.Generic;
using System.Windows;

namespace VectorGraphicViewer.Contracts
{
    public interface IShapeScalerHelper
    {
        double HighestX { get; set; }

        public double HighestY { get; set; }

        List<Point> ScalePoints(List<Point> points);

        void ResetMax();
        bool IsScalingNeeded { get; }

    }
}