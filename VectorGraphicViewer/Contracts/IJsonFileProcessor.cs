using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VectorGraphicViewer.Contracts
{
    public interface IJsonFileProcessor
    {
        void ProcessJsonFile(string fileName);
    }
}
