using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.FileProviders;
using Microsoft.Win32;
using VectorGraphicViewer.Contracts;

namespace VectorGraphicViewer.Views
{
    public class FileDialog : IFileDialog
    {
        public  bool? OpenFileDialog(string fileExtensions, out string filename)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = fileExtensions;
            bool? result = openFileDialog.ShowDialog();
            filename = openFileDialog.FileName;

            return result;
        }
    }
}
