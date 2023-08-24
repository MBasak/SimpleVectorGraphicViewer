using System;
using System.IO;
using Microsoft.Extensions.Logging;
using Microsoft.Win32;
using Newtonsoft.Json.Linq;
using VectorGraphicViewer.Contracts;
using VectorGraphicViewer.Model.Shapes;

namespace VectorGraphicViewer.FileProcessors
{
    /// <summary>
    /// Json file processor. We can add multiple processorsand select
    /// them according to the file extensions
    /// </summary>
    public class JsonFileProcessor : IJsonFileProcessor
    {
        private readonly IVectorGraphicViewerViewModel _vectorGraphicViewerViewModel;
        private readonly ILogger<JsonFileProcessor> _logger;
        ShapeFactory _shapeFactory; 
        public JsonFileProcessor(IJsonCircleProcessor jsonCircleProcessor,
            IJsonPolygonProcessor jsonPolygonProcessor,
            IVectorGraphicViewerViewModel vectorGraphicViewerViewModel,
            ILogger<JsonFileProcessor> logger)
        {
            
            _vectorGraphicViewerViewModel = vectorGraphicViewerViewModel;
            _logger = logger;
            _shapeFactory = new ShapeFactory(jsonCircleProcessor, jsonPolygonProcessor);
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

                        if (type != null)
                        {
                            string shapeType =  type.ToString().ToLower();
                            _vectorGraphicViewerViewModel.AddShape(_shapeFactory.CreateShape(shapeType, jObject));
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
