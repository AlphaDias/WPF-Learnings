using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using ToDoApp.Models;
using ToDoListApp;

namespace BusinessLogic.Services
{
    public class StatusFilteringService
    {
        private ObservableCollection<TaskModel> _allTasks;

        public StatusFilteringService(ObservableCollection<TaskModel> allTasks)
        {
            _allTasks = allTasks;
        }

        public ObservableCollection<TaskModel> ApplyStatusFilter(string selectedStatusFilter)
        {
            if (_allTasks == null)
                return new ObservableCollection<TaskModel>();

            if (selectedStatusFilter == "All")
            {
                return _allTasks;
            }
            else
            {
                var filteredTasks = _allTasks
                    .Where(task => task.Status == selectedStatusFilter)
                    .ToList();
                return new ObservableCollection<TaskModel>(filteredTasks);
            }
        }
    }
}
