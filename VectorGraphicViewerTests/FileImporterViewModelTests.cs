using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.Core.Logging;
using Microsoft.Extensions.Logging;
using NSubstitute;
using VectorGraphicViewer.Contracts;
using VectorGraphicViewer.ViewModels;

namespace VectorGraphicViewerTests
{
    [TestFixture]
    public class FileImporterViewModelTests
    {
        FileImporterViewModel fileImporterViewModel;
        ILogger<FileImporterViewModel> logger;
        [SetUp]
        public void Setup()
        {
            var jsonFileProcessor = Substitute.For<IJsonFileProcessor>();
            var fileDialog = Substitute.For<IFileDialog>();
            logger = Substitute.For<ILogger<FileImporterViewModel>>();

            fileImporterViewModel = new FileImporterViewModel(jsonFileProcessor,
                logger, fileDialog);
        }

        [Test]
        public void OpenDialog()
        {
            fileImporterViewModel.OpenFileDialogCommand.Execute(null);
            logger.Received().LogInformation("No file selected to view shapes");
        }
    }
}
