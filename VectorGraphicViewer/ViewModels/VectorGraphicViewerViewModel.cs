

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using VectorGraphicViewer.Contracts;
using VectorGraphicViewer.Model.Shapes;
using VectorGraphicViewer.ViewHelpers;
using VectorGraphicViewer.ExtensionMethods;

namespace VectorGraphicViewer.ViewModels
{
    /// <summary>
    /// View model for Vector graphic Viewer main screen
    /// </summary>
    public class VectorGraphicViewerViewModel : IVectorGraphicViewerViewModel
    {
        private ObservableCollection<ICircle> _circles;

        private ObservableCollection<IShape> _shapes;
        private ObservableCollection<IPolygon> _polygons;
        private IShapeScalerHelper _shapeScalerHelper;

        public ObservableCollection<ICircle> Circles
        {
            get { return _circles; }
            private set
            {
                _circles = value;
            }
        }
        public ObservableCollection<IPolygon> Polygons
        {
            get { return _polygons; }
            private set
            {
                _polygons = value;
            }

        }
        public ObservableCollection<IShape> Shapes
        {
            get { return _shapes; }
            private set
            {
                _shapes = value;
            }

        }


        public VectorGraphicViewerViewModel(IShapeScalerHelper shapeScalerHelper)
        {
            _circles = new ObservableCollection<ICircle>();
            _polygons = new ObservableCollection<IPolygon>();
            _shapes = new ObservableCollection<IShape>();
            _shapeScalerHelper = shapeScalerHelper;
        }

        public void AddShape(IShape shape)
        {
            _shapes.Add(shape);
        }

        public void AddPolygon(IPolygon polygon)
        {
            Polygons.Add(polygon);
            
        }

        public void AddCircle(ICircle circle)
        {
            Circles.Add(circle);
        }

        public void ClearAllShapes()
        {
            Polygons.Clear();
            Circles.Clear();
            Shapes.Clear();
        }

        public void ScaleShapes()
        {
            if (_shapeScalerHelper.IsScalingNeeded)
            {
                foreach (var shape in Shapes)
                {
                    ((IPolygon)shape).Points= _shapeScalerHelper.ScalePoints(((IPolygon)shape).Points);
                }
            }
            _shapeScalerHelper.ResetMax();
        }
    }
}
