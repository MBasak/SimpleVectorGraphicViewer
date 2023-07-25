using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using VectorGraphicViewer.Contracts;

namespace VectorGraphicViewer.Views
{
    /// <summary>
    /// Interaction logic for importing new files
    /// </summary>
    public partial class FileImporterView : UserControl
    {
        private readonly ILogger<FileImporterView> _logger;
        public FileImporterView()
        {
            try
            {
                InitializeComponent();

                //As it is an user control, cannot inject dependencies in constructor.
                _logger = App.AppHost?.Services.GetRequiredService<ILogger<FileImporterView>>();

                DataContext = App.AppHost?.Services.GetRequiredService<IFileImporterViewModel>();
            }
            catch (Exception ex)
            {
                if (_logger != null)
                {
                    _logger.LogError(ex.StackTrace);
                }
                else
                {
                    throw new ApplicationException($"Application encountered unexpected error." +
                    $"Please contact tech team for support {ex.StackTrace}");
                }
            }

        }
    }
}
