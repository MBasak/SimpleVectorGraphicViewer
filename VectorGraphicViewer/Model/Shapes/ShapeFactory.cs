using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using VectorGraphicViewer.Contracts;

namespace VectorGraphicViewer.Model.Shapes
{
    public class ShapeFactory
    {
        private readonly Dictionary<string, Func<JToken, IShape>> shapes;

        public ShapeFactory(IJsonCircleProcessor jsonCircleProcessor,
            IJsonPolygonProcessor jsonPolygonProcessor)
        {
            shapes = new Dictionary<string, Func<JToken, IShape>>();
            shapes.Add("circle", (jToken) => jsonCircleProcessor.CreateCircle(jToken));
            shapes.Add("line", (jToken) => jsonPolygonProcessor.CreatePolygon(jToken));
            shapes.Add("triangle", (jToken) => jsonPolygonProcessor.CreatePolygon(jToken));
            shapes.Add("polygon", (jToken) => jsonPolygonProcessor.CreatePolygon(jToken));
        }



        public IShape CreateShape(string shapeType, JToken jToken) => shapes[shapeType](jToken);

        public string[] RegisteredShapes => shapes.Keys.ToArray();

        public void RegisterShape(string shape, Func<JToken, IShape> factoryMethod)
        {
            shapes[shape] = factoryMethod;
        }


    }
}
