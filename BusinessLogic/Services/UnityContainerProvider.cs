using System.Configuration;
using ToDoApp_BusinessLogic.Repositories;
using ToDoApp_DataAccess;
using ToDoListApp.Business;
using ToDoListApp.DataAccess;
using Unity;
using Unity.Injection;

public static class UnityContainerProvider
{
    private static readonly IUnityContainer _container;

    static UnityContainerProvider()
    {
        _container = new UnityContainer();
      
    }

    public static IUnityContainer Container => _container;

    public static void RegisterTypes()
    {
        _container.RegisterType<IDatabase, SQLiteDatabase>(
            new InjectionConstructor(Getconnectionstr()));
        _container.RegisterType<ITaskRepository, TaskRepository>();
    }

    public static T Resolve<T>()
    {
        return _container.Resolve<T>();
    }

    public static void RegisterInstance<T>(T instance)
    {
        _container.RegisterInstance(instance);
    }
    public static string Getconnectionstr()
    {
        string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        return connectionString;
    }
}