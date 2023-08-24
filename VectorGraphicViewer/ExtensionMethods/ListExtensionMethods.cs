using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Shapes;
using VectorGraphicViewer.Contracts;

namespace VectorGraphicViewer.ExtensionMethods
{
    public static class ListExtensionMethods
    {
        public static T OfType<T>(this IShape shape)
        {
          
                return (T)shape;
            

         
           
        }
    }
}
