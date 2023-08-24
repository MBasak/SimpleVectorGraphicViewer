using Newtonsoft.Json.Linq;

namespace VectorGraphicViewer.Contracts
{
    public interface IJsonPolygonProcessor
    {
        IPolygon CreatePolygon(JToken jObject);
    }
}