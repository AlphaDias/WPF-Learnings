using BusinessLogic.Services;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using ToDoApp.Models;
using ToDoApp_BusinessLogic.Repositories;

namespace ToDoListApp.ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private readonly ITaskRepository _taskRepository;
        private ObservableCollection<TaskModel> _tasks;
        private ObservableCollection<TaskModel> _allTasks;
        private TaskModel _selectedTask;
        private string _newTaskContent;
        private string _searchKeyword;
        private string _selectedStatusFilter;
        private DateTime _dueDate = DateTime.Today;


        public MainViewModel(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
            // Initialize commands
            AddTaskCommand = new RelayCommand(AddTask);
            UpdateTaskCommand = new RelayCommand(UpdateTask, CanUpdateOrDeleteTask);
            DeleteTaskCommand = new RelayCommand(DeleteTask, CanUpdateOrDeleteTask);
            ClearAllTasksCommand = new RelayCommand(ClearAllTasks);
            SearchCommand = new RelayCommand(Search);

            // Status filter options
            StatusOptions = new ObservableCollection<string>
            {
                "All",
                "New",
                "Active",
                "Completed"
            };
            SelectedStatusFilter = "All";
            LoadTasks();
            CheckDueDatesAndSendReminder();
        }

   
        public ObservableCollection<TaskModel> Tasks
        {
            get => _tasks;
            set
            {
                _tasks = value;
                OnPropertyChanged(nameof(Tasks));
            }
        }

        public TaskModel SelectedTask
        {
            get => _selectedTask;
            set
            {
                _selectedTask = value;
                OnPropertyChanged(nameof(SelectedTask));
                if (SelectedTask != null)
                {
                    NewTaskContent = SelectedTask.Content;
                    DueDate = SelectedTask.DueDate; 
                }
            }
        }

        public string NewTaskContent
        {
            get => _newTaskContent;
            set
            {
                _newTaskContent = value;
                OnPropertyChanged(nameof(NewTaskContent));
            }
        }

        public DateTime DueDate
        {
            get => _dueDate;
            set
            {
                _dueDate = value;
                OnPropertyChanged(nameof(DueDate));
            }
        }

        public string SearchKeyword
        {
            get => _searchKeyword;
            set
            {
                _searchKeyword = value;
                OnPropertyChanged(nameof(SearchKeyword));
                Search(null);
            }
        }
      

        public ObservableCollection<string> StatusOptions { get; }

        public string SelectedStatusFilter
        {
            get => _selectedStatusFilter;
            set
            {
                _selectedStatusFilter = value;
                OnPropertyChanged(nameof(SelectedStatusFilter));
                ApplyStatusFilter();
            }
        }

        public ICommand AddTaskCommand { get; }
        public ICommand UpdateTaskCommand { get; }
        public ICommand DeleteTaskCommand { get; }
        public ICommand ClearAllTasksCommand { get; }
        public ICommand SearchCommand { get; }

        public void LoadTasks()
        {
            _allTasks = _taskRepository.GetAllTasks();
            Tasks = new ObservableCollection<TaskModel>(_allTasks);
            foreach (var task in _allTasks)
            {
                task.PropertyChanged += Task_PropertyChanged;
            }
            ApplyStatusFilter();
        }

        private void AddTask(object parameter)
        {
            var newTask = new TaskModel
            {
                Content = NewTaskContent,
                Date = DateTime.Now,
                DueDate = DueDate,  
                Status = "New"
            };

            _taskRepository.AddTask(newTask);
            LoadTasks();
            ClearTask(null);
        }

        public void UpdateTask(object parameter)
        {
            try
            {
                if (SelectedTask != null)
                {
                    SelectedTask.Content = NewTaskContent;
                    SelectedTask.DueDate = DueDate;  
                    _taskRepository.UpdateTask(SelectedTask);
                    LoadTasks();
                }
            }
            finally
            {
            }
        }

        private void DeleteTask(object parameter)
        {
            if (SelectedTask != null)
            {
                _taskRepository.DeleteTask(SelectedTask.Id);
                LoadTasks();
                ClearTask(null);
            }
        }

        private void ClearAllTasks(object parameter)
        {
            _taskRepository.DeleteAllTasks();
            LoadTasks();
            SelectedTask = null;
            NewTaskContent = string.Empty;
            DueDate = DateTime.Today; 
        }

        private bool CanUpdateOrDeleteTask(object parameter)
        {
            return SelectedTask != null;
        }

        private void ClearTask(object parameter)
        {
            SelectedTask = null;
            NewTaskContent = string.Empty;
            DueDate = DateTime.Today; 
        }

        private void ApplyStatusFilter()
        {
            if (_allTasks == null)
                return;

            if (SelectedStatusFilter == "All")
            {
                Tasks = new ObservableCollection<TaskModel>(_allTasks);
            }
            else
            {
                var filteredTasks = _allTasks
                    .Where(task => task.Status == SelectedStatusFilter)
                    .ToList();
                Tasks = new ObservableCollection<TaskModel>(filteredTasks);
            }
        }

        private void Search(object parameter)
        {
            if (string.IsNullOrEmpty(SearchKeyword))
            {
                Tasks = new ObservableCollection<TaskModel>(_allTasks);
                ApplyStatusFilter();
            }
            else
            {
                var filteredTasks = _allTasks
                    .Where(task => !string.IsNullOrEmpty(task.Content) &&
                                   task.Content.IndexOf(SearchKeyword, StringComparison.OrdinalIgnoreCase) >= 0)
                    .ToList();

                Tasks = new ObservableCollection<TaskModel>(filteredTasks);
                ApplyStatusFilter();
            }
        }

        private async void CheckDueDatesAndSendReminder()
        {
            await Task.Delay(TimeSpan.FromMinutes(1));

            while (true)
            {
                foreach (var task in _allTasks)
                {
                    if (task.DueDate.Date == DateTime.Today.AddDays(1))
                    {
                        MessageBox.Show($"Reminder: Task '{task.Content}' is due tomorrow!", "Reminder", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
                await Task.Delay(TimeSpan.FromMinutes(1));
            }
        }

        private void Task_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(TaskModel.Status))
            {
                var task = sender as TaskModel;
                if (task != null && task.Status == "Completed")
                {
                    ShowCompletionPopup(task);
                }
            }
        }
        private void ShowCompletionPopup(TaskModel task)
        {
            MessageBox.Show($"Task '{task.Content}' is completed!", "Task Completed", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
