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

namespace VectorGraphicViewer.Model.Shapes.ShapeCreationUtility.JsonDataHelper
{
    public class JsonPolygonProcessor : JsonShapeProcessor, IJsonPolygonProcessor
    {
        public JsonPolygonProcessor(ILogger<JsonShapeProcessor> logger)
            : base(logger)
        {
        }

        public IPolygon CreatePolygon(JToken jObject, object typeOfShape)
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

            return polygon;

        }
    }
}
