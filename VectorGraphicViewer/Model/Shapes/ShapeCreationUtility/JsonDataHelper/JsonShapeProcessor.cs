using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using VectorGraphicViewer.Constants;
using VectorGraphicViewer.Contracts;

namespace VectorGraphicViewer.Model.Shapes.ShapeCreationUtility.JsonDataHelper
{
    public abstract class JsonShapeProcessor
    {
        protected ILogger<JsonShapeProcessor> Logger;
      
        protected JsonShapeProcessor(ILogger<JsonShapeProcessor> logger)
        {
            Logger = logger;
            
        }

        protected bool CheckIfPropertyExists(string property, JToken jToken)
        {
            return jToken[property] != null;
        }

      
    }
}
