using System;
using System.IO;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using VectorGraphicViewer.Contracts;

namespace VectorGraphicViewer.FileProcessors
{
    /// <summary>
    /// Json file processor. We can add multiple processorsand select
    /// them according to the file extensions
    /// </summary>
    public class JsonFileProcessor : IJsonFileProcessor
    {
        private readonly IJsonCircleProcessor _jsonCircleProcessor;
        private readonly IJsonPolygonProcessor _jsonPolygonProcessor;
        private readonly IVectorGraphicViewerViewModel _vectorGraphicViewerViewModel;
        private readonly ILogger<JsonFileProcessor> _logger;
        public JsonFileProcessor(IJsonCircleProcessor jsonCircleProcessor,
            IJsonPolygonProcessor jsonPolygonProcessor,
            IVectorGraphicViewerViewModel vectorGraphicViewerViewModel,
            ILogger<JsonFileProcessor> logger)
        {
            _jsonCircleProcessor = jsonCircleProcessor;
            _jsonPolygonProcessor = jsonPolygonProcessor;
            _vectorGraphicViewerViewModel = vectorGraphicViewerViewModel;
            _logger = logger;
        }


        public void ProcessJsonFile(string path)
        {
            try
            {
                _vectorGraphicViewerViewModel.ClearAllShapes();
                using (StreamReader r = new StreamReader(path))
                {
                    string jsonData = r.ReadToEnd();
                    var jArrayOfObjects = JArray.Parse(jsonData);
                    foreach (var jObject in jArrayOfObjects)
                    {
                        _logger.LogInformation($"Json object {jObject.ToString()}");
                        var type = jObject["type"];
                        object typeOfShape = null;

                        if (type != null)
                        {
                            Enum.TryParse(typeof(TypesOfShape), type.ToString().ToLower(), out typeOfShape);
                        }

                        switch (typeOfShape)
                        {
                            case TypesOfShape.circle:
                                _vectorGraphicViewerViewModel
                                    .AddCircle(_jsonCircleProcessor.CreateCircle(jObject));
                                break;
                            default:
                                _vectorGraphicViewerViewModel
                                    .AddPolygon(_jsonPolygonProcessor.CreatePolygon(jObject, typeOfShape));
                                break;
                        }
                    }

                    _vectorGraphicViewerViewModel.ScaleShapes();
                }

            }
            catch(Exception ex)
            {
                _logger.LogError($"Error occured while processing json file {ex.StackTrace}");
            }
        }
    }
}
