using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Extensions.Logging;
using Microsoft.Win32;
using VectorGraphicViewer.Commands;
using VectorGraphicViewer.Contracts;
using VectorGraphicViewer.ViewHelpers;

namespace VectorGraphicViewer.ViewModels
{
    /// <summary>
    /// View Model for importing file(json or any other extensions as required)
    /// </summary>
    public class FileImporterViewModel : NotifyPropertyChanged, IFileImporterViewModel
    {
        private ICommand _openFileDialogCommand;
        private string _fileName = String.Empty;
        private readonly IJsonFileProcessor _jsonFileProcessor;
        private readonly ILogger<FileImporterViewModel> _logger;
        private readonly IFileDialog _fileDialog;

        public ICommand OpenFileDialogCommand
        {
            get
            {
                return _openFileDialogCommand;
            }
            set
            {
                _openFileDialogCommand = value;
            }
        }
        public string FileName
        {
            get
            {
                return _fileName;
            }

            set
            {
                _fileName = value;
                OnPropertyChanged(nameof(FileName));
            }
        }
        public FileImporterViewModel(IJsonFileProcessor jsonFileProcessor,
            ILogger<FileImporterViewModel> logger,
            IFileDialog fileDialog)
        {
            _jsonFileProcessor = jsonFileProcessor;
            _logger = logger;
            _fileDialog = fileDialog;

            _openFileDialogCommand = new RelayCommand(() => OpenFileDialogView());

        }

        private void OpenFileDialogView()
        {
            try
            {
                string filename = string.Empty;
                //Can include xml or other extensions
                string fileExtensionFilters = "Json documents (.json)|*.json";
                bool? result = _fileDialog.OpenFileDialog(fileExtensionFilters, out filename);

                if (result == true)
                {
                    FileName = filename;
                    SelectFileProcessor();
                }
                else
                {
                    _logger.LogInformation("No file selected to view shapes");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error detected while opening file {ex}");
            }

        }

        private void SelectFileProcessor()
        {
            if (FileName.Contains("json"))
            {
                _logger.LogInformation($"Json file processor select for file {FileName}");
                _jsonFileProcessor.ProcessJsonFile(FileName);
            }
        }
    }
}
