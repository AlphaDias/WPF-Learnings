using System;
using System.Collections.ObjectModel;
using NUnit.Framework;
using ToDoApp.Models;
using ToDoApp_BusinessLogic.Repositories;
using ToDoListApp.ViewModel;
using NSubstitute;

namespace ToDoListApp.Tests
{
    [TestFixture]
    public class MainViewModelTests
    {
        private ITaskRepository _faketaskRepository;
        private MainViewModel _viewModel;

        [SetUp]
        public void SetUp()
        {
            _faketaskRepository = Substitute.For<ITaskRepository>();
            _viewModel = new MainViewModel(_faketaskRepository);
        }

        [Test]
        public void When_AddTaskCommandIsCalled_Then_AddTaskOnRepository()
        {
            var newTaskContent = "Sample task content";
            _viewModel.NewTaskContent = newTaskContent;
            _viewModel.AddTaskCommand.Execute(null);
            _faketaskRepository.Received(1).AddTask(Arg.Is<TaskModel>(t => t.Content == newTaskContent));
        }

        [Test]
        public void When_DeleteTaskCalled_then_DeleteTask_On_Repository()
        {
            var existingTask = new TaskModel { Id = 1, Content = "Existing task" };
            _viewModel.Tasks = new ObservableCollection<TaskModel> { existingTask };
            _viewModel.SelectedTask = existingTask;
            _viewModel.DeleteTaskCommand.Execute(null);
            _faketaskRepository.Received(1).DeleteTask(existingTask.Id);
        }

        [Test]
        public void When_LoadTasksCalled_then_LoadTaskfromRepository()
        {
            var tasks = new ObservableCollection<TaskModel>
            {
                new TaskModel { Content = "Task 1" },
                new TaskModel { Content = "Task 2" }
            };
            _faketaskRepository.GetAllTasks().Returns(tasks);
            _viewModel.LoadTasks();
            Assert.AreEqual(2, _viewModel.Tasks.Count);
            Assert.AreEqual("Task 1", _viewModel.Tasks[0].Content);
            Assert.AreEqual("Task 2", _viewModel.Tasks[1].Content);
        }
    }
}




