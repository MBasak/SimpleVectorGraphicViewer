using System;
using System.Collections.Generic;
using System.Windows;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VectorGraphicViewer.Contracts;
using System.Windows.Media;
using VectorGraphicViewer.ViewHelpers;

namespace VectorGraphicViewer.Model.Shapes
{
    public class Polygon : Shape, IPolygon
    {
        private List<Point> _points;
        public List<Point> Points

        {
            get
            {
                return _points;
            }
            set
            {
                _points = value;
                OnPropertyChanged(nameof(Points));
                OnPropertyChanged(nameof(PointCollection));
            }
        }
        public Polygon()
        {
            _points = new List<Point>();
        }

        public PointCollection PointCollection
        {
            get
            {
                return new PointCollection(Points);
            }
        }
    }
}
