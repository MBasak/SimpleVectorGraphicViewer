using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using VectorGraphicViewer.Constants;
using VectorGraphicViewer.Contracts;
using VectorGraphicViewer.ViewHelpers;

namespace VectorGraphicViewer.Model.Shapes.ShapeCreationUtility.JsonDataHelper
{
    public class JsonPolygonProcessor : JsonShapeProcessor, IJsonPolygonProcessor
    {
        IShapeScalerHelper _shapeScalerHelper;
        public JsonPolygonProcessor(IShapeScalerHelper scaler,
            ILogger<JsonShapeProcessor> logger)
            : base(logger)
        {
            _shapeScalerHelper = scaler;
        }

        public IPolygon CreatePolygon(JToken jObject)
        {
            //As assumption is data is valid, checks are not performed further,
            //but in production real time code, we should perform checks
            //like performed for circle processor class, if it is
            //not guaranteed that data is valid

            IPolygon polygon = new Polygon();
            var filledProperty = jObject[JsonFileConstants.FILLED];
            if (filledProperty != null)
            {
                polygon.Filled = bool.Parse(filledProperty.ToString());
            }

            polygon.BorderColor = jObject[JsonFileConstants.COLOR].ToString();


            polygon.Points = new List<Point>();

            char vertexNotation = 'a';
            while (jObject[vertexNotation.ToString()] != null)
            {
                var vertex = jObject[vertexNotation.ToString()].ToString().Split(';');
                polygon.Points.Add(new Point(double.Parse(vertex[0]), double.Parse(vertex[1])));

                vertexNotation++;
            }
            foreach (var point in polygon.Points)
            {
                _shapeScalerHelper.HighestX = Math.Max(Math.Abs(point.X), _shapeScalerHelper.HighestX);
                _shapeScalerHelper.HighestY = Math.Max(Math.Abs(point.Y), _shapeScalerHelper.HighestY);
            }
            return polygon;

        }
    }
}
