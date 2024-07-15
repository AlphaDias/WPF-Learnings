using System;
using System.Collections.ObjectModel;
using System.Data.SQLite;
using ToDoApp.Models;
using ToDoApp_DataAccess;

namespace ToDoListApp.DataAccess
{
    public class SQLiteDatabase : IDatabase
    {
        private readonly string _connectionString;

        public SQLiteDatabase(string connectionString)
        {
            _connectionString = connectionString;
        }

        public ObservableCollection<TaskModel> GetAllTasks()
        {
            try
            {
                var tasks = new ObservableCollection<TaskModel>();

                using (var connection = new SQLiteConnection(_connectionString))
                {
                    connection.Open();
                    var command = connection.CreateCommand();
                    command.CommandText = "SELECT Id, Content, Date, Status, DueDate FROM Tasks";
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            tasks.Add(new TaskModel
                            {
                                Id = reader.GetInt32(0),
                                Content = reader.GetString(1),
                                Date = reader.GetDateTime(2),
                                Status = reader.GetString(3),
                                DueDate = reader.GetDateTime(4)  
                            });
                        }
                    }
                }

                return tasks;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving tasks: {ex.Message}");
                throw;
            }
        }


        public void AddTask(TaskModel task)
        {
            try
            {
                using (var connection = new SQLiteConnection(_connectionString))
                {
                    connection.Open();
                    var command = connection.CreateCommand();
                    command.CommandText = "INSERT INTO Tasks (Content, Date, Status, DueDate) VALUES (@content, @date, @status, @dueDate)";
                    command.Parameters.AddWithValue("@content", task.Content);
                    command.Parameters.AddWithValue("@date", task.Date);
                    command.Parameters.AddWithValue("@status", task.Status);
                    command.Parameters.AddWithValue("@dueDate", task.DueDate); 
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding task: {ex.Message}");
                throw;
            }
        }

        public void UpdateTask(TaskModel task)
        {
            try
            {
                using (var connection = new SQLiteConnection(_connectionString))
                {
                    connection.Open();
                    var command = connection.CreateCommand();
                    command.CommandText = "UPDATE Tasks SET Content = @content, Status = @status, DueDate = @dueDate WHERE Id = @id";
                    command.Parameters.AddWithValue("@content", task.Content);
                    command.Parameters.AddWithValue("@status", task.Status); // Update due date
                    command.Parameters.AddWithValue("@dueDate", task.DueDate); 
                    command.Parameters.AddWithValue("@id", task.Id);
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating task: {ex.Message}");
                throw;
            }
        }
        public void DeleteAllTasks()
        {
            try {  using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = "DELETE FROM Tasks";
                command.ExecuteNonQuery();
            } } catch(Exception ex) { }
          
        }

        public void DeleteTask(int taskId)
        {
            try {
                using (var connection = new SQLiteConnection(_connectionString))
                {
                    connection.Open();
                    var command = connection.CreateCommand();
                    command.CommandText = "DELETE FROM Tasks WHERE Id = $id";
                    command.Parameters.AddWithValue("$id", taskId);
                    command.ExecuteNonQuery();
                }
            } catch(Exception ex) { }
           
        }
    }
}
