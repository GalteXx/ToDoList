
using MongoDB.Bson;
using MongoDB.Driver;
using System.Linq.Expressions;
using ToDoList.Model;

namespace ToDoList.Service;

internal class TaskMongoDBService : ITaskService
{
    private string _connectionString;
    private string _databaseName = "Tasks";
    private string _collectionName = "Tasks";

    private MongoClient _client;
    private IMongoCollection<TaskModel> _taskCollection;


    public TaskMongoDBService(string connectionString) => Initialization = InitializeAsync(connectionString);

    private async Task InitializeAsync(string connectionString)
    {
        _connectionString = connectionString;
        _client = new MongoClient(_connectionString);

        var db = _client.GetDatabase(_collectionName);
        await db.RunCommandAsync((MongoDB.Driver.Command<BsonDocument>)"{ping:1}");

        _taskCollection = db.GetCollection<TaskModel>(_collectionName);
    }

    public Task Initialization { get; private set; }

    public async Task<IEnumerable<TaskModel>> GetTasksCollectionAsync(Expression<Func<TaskModel, bool>> x) 
    {
        var tasksCursor = await _taskCollection.FindAsync(x);
        return tasksCursor.ToList();
    }

    public async Task InsertTaskAsync(TaskModel task)
    {
        await _taskCollection.InsertOneAsync(task);
    }
}

