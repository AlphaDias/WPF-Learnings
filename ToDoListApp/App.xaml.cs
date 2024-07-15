using BusinessLogic.Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using ToDoApp.Models;
using ToDoApp_BusinessLogic.Services;
using ToDoListApp.ViewModel;
using Unity;

namespace ToDoListApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Subscribe to the global exception events
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            DispatcherUnhandledException += App_DispatcherUnhandledException;
        }

        private void App_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            // Handle UI thread exceptions
            HandleException(e.Exception);
            e.Handled = true;
        }

        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            // Handle non-UI thread exceptions
            if (e.ExceptionObject is Exception exception)
            {
                HandleException(exception);
            }
        }

        private void HandleException(Exception exception)
        {
            // Log the exception (optional)
            // Show a custom error dialog
            ShowErrorDialog(exception.Message);
        }

        private void ShowErrorDialog(string errorMessage)
        {
            // Ensure this runs on the UI thread
            Current.Dispatcher.Invoke(() =>
            {
                var errorDialog = new ErrorDialog(errorMessage);
                errorDialog.ShowDialog();
            });
        }

    }
}
