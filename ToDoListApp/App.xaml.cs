using BusinessLogic.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
          //  System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("hi-IN");
          
          //  mainWindow.Show();

        }
        

    }
}
