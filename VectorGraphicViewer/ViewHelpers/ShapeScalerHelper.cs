using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using VectorGraphicViewer.Contracts;

namespace VectorGraphicViewer.ViewHelpers
{
    public class ShapeScalerHelper : IShapeScalerHelper
    {
        private readonly double ScreenWidth = (SystemParameters.PrimaryScreenWidth - 100) / 2;
        private readonly double ScreenHeight = (SystemParameters.PrimaryScreenHeight - 100) / 2;
        private double scaleFactorX;
        private double scaleFactorY;
        public double HighestX { get; set; }

        public double HighestY { get; set; }

        public ShapeScalerHelper()
        {
            ResetMax();
        }

        public List<Point> ScalePoints(List<Point> points)
        {
            List<Point> result = new List<Point>();
            scaleFactorX = ScreenWidth / HighestX;
            scaleFactorY = ScreenHeight / HighestY;
            foreach (Point point in points)
            {
                result.Add(new Point(point.X * scaleFactorX, point.Y * scaleFactorY));
            }
            return result;
        }

        public void ResetMax()
        {
            HighestX = ScreenWidth;
            HighestY = ScreenHeight;
        }

        public bool IsScalingNeeded
        {
            get
            {
                return HighestX > ScreenWidth || HighestY > ScreenHeight;
            }
        }
    }
}
