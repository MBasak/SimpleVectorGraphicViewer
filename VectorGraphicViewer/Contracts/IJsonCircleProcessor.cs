using Newtonsoft.Json.Linq;

namespace VectorGraphicViewer.Contracts
{
    public interface IJsonCircleProcessor: IShapeProcessor
    {
        ICircle CreateCircle(JToken jObject);
    }

    public interface IShapeProcessor
    {
    }
}