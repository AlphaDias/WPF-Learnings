using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoApp_BusinessLogic.Repositories;
using ToDoApp_DataAccess;
using ToDoListApp.DataAccess;
using Unity;
using Unity.Injection;

namespace BusinessLogic.Services
{
    public class DatabaseService
    {
        public static ITaskRepository InitializeRepository()
        {
            IUnityContainer container = new UnityContainer();
            container.RegisterType<IDatabase, SQLiteDatabase>(
                new InjectionConstructor(Getconnectionstr()));

            container.RegisterType<ITaskRepository, TaskRepository>();

            var taskRepository = container.Resolve<ITaskRepository>();

            return taskRepository;
            
        }

        public static string Getconnectionstr()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            return connectionString;
        }

    }
}
