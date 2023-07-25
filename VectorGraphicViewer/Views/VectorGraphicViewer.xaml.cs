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
using Microsoft.Extensions.Logging;
using VectorGraphicViewer.Contracts;
using VectorGraphicViewer.Views;

namespace VectorGraphicViewer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class VectorGraphicViewWindow : Window
    {
        private readonly ILogger<VectorGraphicViewWindow> _logger;
        public VectorGraphicViewWindow(ILogger<VectorGraphicViewWindow> logger,
            IVectorGraphicViewerViewModel vectorGraphicViewerViewModel)
        {
            try
            {
                InitializeComponent();
                _logger = logger;
                DataContext = vectorGraphicViewerViewModel;
            }
            catch (Exception ex)
            {
                if (_logger != null)
                {
                    _logger.LogError(ex.StackTrace);
                }
                throw new ApplicationException($"Application encountered unexpected error." +
                    $"Please contact tech team for support {ex.StackTrace}"
                    );
            }
        }

    }
}
