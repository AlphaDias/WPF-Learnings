using NUnit.Framework;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System;
using System.Linq;
using NSubstitute;
using ToDoApp.Models;
using ToDoApp_BusinessLogic.Repositories;
using ToDoListApp.ViewModel;

namespace ToDoList_UnitTests
{
    [TestFixture]
    public class MainViewModelTests
    {
        private ITaskRepository _taskRepository;
        private MainViewModel _viewModel;

        [SetUp]
        public void Setup()
        {

            _taskRepository = Substitute.For<ITaskRepository>();
            _viewModel = new MainViewModel(_taskRepository);
        }

        [Test]
        public void AddTaskCommand_AddsNewTask()
        {

            var initialTasks = new List<TaskModel>(_viewModel.Tasks);
            var newTaskContent = "Test Task";
            var dueDate = DateTime.Today;
            _viewModel.NewTaskContent = newTaskContent;
            _viewModel.DueDate = dueDate;
            _viewModel.AddTaskCommand.Execute(null);
            _taskRepository.Received().AddTask(Arg.Is<TaskModel>(t =>
                t.Content == newTaskContent &&
                t.DueDate == dueDate &&
                t.Status == "New"));

            Assert.AreNotEqual(initialTasks, _viewModel.Tasks);
            Assert.IsTrue(_viewModel.Tasks.Any(t => t.Content == newTaskContent));
        }

    }
}