using Newtonsoft.Json.Linq;

namespace VectorGraphicViewer.Contracts
{
    public interface IJsonCircleProcessor
    {
        ICircle CreateCircle(JToken jObject);
    }
}