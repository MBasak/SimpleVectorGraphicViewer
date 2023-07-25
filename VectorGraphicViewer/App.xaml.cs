using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using VectorGraphicViewer.Contracts;
using VectorGraphicViewer.FileProcessors;
using VectorGraphicViewer.Model.Shapes.ShapeCreationUtility.JsonDataHelper;
using VectorGraphicViewer.ViewHelpers;
using VectorGraphicViewer.ViewModels;
using VectorGraphicViewer.Views;

namespace VectorGraphicViewer
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private ILogger<VectorGraphicViewWindow> _logger;
        public static IHost? AppHost { get; private set; }

        public App()
        {
            try
            {
                //Create Generic host and register dependencies
                AppHost = Host.CreateDefaultBuilder()
                        .ConfigureLogging((context, loggingBuilder) =>
                        {
                            loggingBuilder.AddConsole();
                        })

                        .ConfigureServices((context, services) =>
                        {
                            services.AddScoped<IFileDialog, FileDialog>();
                            services.AddScoped<IShapeScalerHelper, ShapeScalerHelper>();
                            services.AddScoped<IJsonFileProcessor, JsonFileProcessor>();
                            services.AddScoped<IJsonCircleProcessor, JsonCircleProcessor>();
                            services.AddScoped<IJsonPolygonProcessor, JsonPolygonProcessor>();
                            services.AddScoped<IFileImporterViewModel, FileImporterViewModel>();
                            services.AddScoped<IVectorGraphicViewerViewModel, VectorGraphicViewerViewModel>();

                            services.AddSingleton<FileImporterView>();
                            services.AddSingleton<VectorGraphicViewWindow>();
                        })
                        .ConfigureLogging(logging =>
                        {
                            logging.AddConsole();
                        })
                        .Build();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Fatal error encountered while starting application. " +
                    "Please contact tech team for support" +
                    $"{ex.StackTrace}");

            }
        }
        private async void Application_Startup(object sender, StartupEventArgs e)
        {
            try
            {
                await AppHost.StartAsync();

                var mainWindow = AppHost.Services.GetService<VectorGraphicViewWindow>();
                mainWindow.Show();
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Fatal error encountered while starting application. " +
                    "Please contact tech team for support" +
                    $"{ex.StackTrace}");
            }
        }

        private async void Application_Exit(object sender, ExitEventArgs e)
        {
            try
            {
                using (AppHost)
                {
                    await AppHost.StopAsync(TimeSpan.FromSeconds(5));
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Fatal error encountered while starting application. " +
                   "Please contact tech team for support" +
                   $"{ex.StackTrace}");
            }


        }

    }
}
