using BusinessLogic.Services;
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
using ToDoApp_BusinessLogic.Repositories;
using ToDoListApp.ViewModel;
using Unity;

namespace ToDoListApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
            UnityContainerProvider.RegisterTypes();
          
           // var mainWindow = UnityContainerProvider.Resolve<ITaskRepository>();
             DataContext = new MainViewModel(UnityContainerProvider.Resolve<ITaskRepository>());

        }
        private void TriggerUIThreadException_Click(object sender, RoutedEventArgs e)
        {
            throw new InvalidOperationException("This is a test exception on the UI thread.");
        }

        private void TriggerNonUIThreadException_Click(object sender, RoutedEventArgs e)
        {
            Task.Run(() => throw new InvalidOperationException("This is a test exception on a non-UI thread."));
        }

    }
}
