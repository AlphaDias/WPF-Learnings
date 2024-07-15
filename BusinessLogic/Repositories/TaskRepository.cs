using System.Collections.ObjectModel;
using ToDoApp.Models;
using ToDoApp_DataAccess;

namespace ToDoApp_BusinessLogic.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly IDatabase _database;

        public TaskRepository(IDatabase database)
        {
            _database = database;
        }

        public ObservableCollection<TaskModel> GetAllTasks()
        {
            return _database.GetAllTasks();
        }

        public void AddTask(TaskModel task)
        {
            _database.AddTask(task);
        }

        public void UpdateTask(TaskModel task)
        {
            _database.UpdateTask(task);
        }

        public void DeleteTask(int taskId)
        {
            _database.DeleteTask(taskId);
        }

        public void DeleteAllTasks()
        {
            _database.DeleteAllTasks();
        }
    }
}
