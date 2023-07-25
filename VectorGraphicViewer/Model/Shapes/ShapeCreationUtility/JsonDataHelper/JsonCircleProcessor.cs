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
    public class JsonCircleProcessor : JsonShapeProcessor,
        IJsonCircleProcessor
    {
        public JsonCircleProcessor(ILogger<JsonShapeProcessor> logger)
            : base(logger)
        {
        }

        public ICircle CreateCircle(JToken jToken)
        {
            ICircle circle = new Circle();

            if (CheckIfPropertyExists(JsonFileConstants.RADIUS, jToken))
            {
                circle.Radius = jToken[JsonFileConstants.RADIUS].ToString();
            }
            else
            {
                Logger.LogError($"Circle radius is missing in json object {jToken}. " +
                    $"Circle Cannot be formed");
                return null;
            }

            //As assumption is data is valid, checks are not performed further,
            //but in production real time code, we should perform checks if it is
            //not guaranteed that data is valid

            var vertices = jToken[JsonFileConstants.CENTER].ToString().Split(';');
            circle.Center = new Point(double.Parse(vertices[0]), double.Parse(vertices[1]));

            circle.Filled = bool.Parse(jToken[JsonFileConstants.FILLED].ToString());

            circle.BorderColor = jToken[JsonFileConstants.COLOR].ToString();
            return circle;

        }



    }
}
