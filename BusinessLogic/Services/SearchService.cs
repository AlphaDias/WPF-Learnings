using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using ToDoApp.Models;
using ToDoListApp;
namespace ToDoApp_BusinessLogic.Services
{
    public class SearchService
    {
        private readonly List<TaskModel> _allTasks;
        private ObservableCollection<TaskModel> allTasks;

        public SearchService(List<TaskModel> allTasks)
            {
                _allTasks = allTasks;
            }

        public SearchService(ObservableCollection<TaskModel> allTasks)
        {
            this.allTasks = allTasks;
        }

        public List<TaskModel> SearchTasks(string searchKeyword)
            {
                if (string.IsNullOrEmpty(searchKeyword))
                    return _allTasks;

                return _allTasks
                    .Where(task => !string.IsNullOrEmpty(task.Content) &&
                                   task.Content.IndexOf(searchKeyword, StringComparison.OrdinalIgnoreCase) >= 0)
                    .ToList();
            }
        
    }

}
