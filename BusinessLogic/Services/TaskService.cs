using System.Collections.ObjectModel;
using ToDoApp.Models;
using ToDoApp_BusinessLogic.Repositories;
using ToDoListApp.DataAccess;

namespace ToDoListApp.Business
{
    public class TaskService
    {
        private readonly ITaskRepository _taskRepository;

        public TaskService(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public ObservableCollection<TaskModel> GetAllTasks()
        {
            return _taskRepository.GetAllTasks();
        }

        public void AddTask(TaskModel task)
        {
            _taskRepository.AddTask(task);
        }

        public void UpdateTask(TaskModel task)
        {
            _taskRepository.UpdateTask(task);
        }

        public void DeleteTask(int taskId)
        {
            _taskRepository.DeleteTask(taskId);
        }
    }
}
