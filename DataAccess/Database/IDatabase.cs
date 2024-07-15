using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoApp.Models;
using ToDoListApp;
namespace ToDoApp_DataAccess
{
    public interface IDatabase
    {
        ObservableCollection<TaskModel> GetAllTasks();
        void AddTask(TaskModel task);
        void UpdateTask(TaskModel task);
        void DeleteTask(int taskId);
        void DeleteAllTasks();
    }
}
