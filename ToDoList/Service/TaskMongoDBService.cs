
using MongoDB.Bson;
using MongoDB.Driver;
using System.Linq.Expressions;
using System.Xml.Linq;
using ToDoList.Model;

namespace ToDoList.Service;

internal class TaskMongoDBService : ITaskService
{
    private string _connectionString;
    private string _databaseName = "Tasks";
    private string _collectionName = "Tasks";

    private MongoClient _client;
    private IMongoCollection<TaskModel> _taskCollection;


    public TaskMongoDBService()
    {
        Stream configurationStream = FileSystem.OpenAppPackageFileAsync("config.xml").Result ?? throw new FileNotFoundException();

        _connectionString = XDocument.Load(configurationStream).Descendants("Server").ElementAt(0).Attribute("connectionString")!.Value;

        _client = new MongoClient(_connectionString);

        var db = _client.GetDatabase(_collectionName);
        db.RunCommandAsync((MongoDB.Driver.Command<BsonDocument>)"{ping:1}").Wait();

        _taskCollection = db.GetCollection<TaskModel>(_collectionName);

    }

    public async Task<IEnumerable<TaskModel>> GetTasksCollectionAsync(Expression<Func<TaskModel, bool>> x) 
    {
        var tasksCursor = await _taskCollection.FindAsync(x);
        return tasksCursor.ToList();
    }

    public async Task InsertTaskAsync(TaskModel task)
    {
        await _taskCollection.InsertOneAsync(task);
    }

    //I really see no point in FactoryMethod for 
    /*public static async Task<TaskMongoDBService> CreateAsync()
    {
        var service = new TaskMongoDBService();
        Stream configurationStream = await FileSystem.OpenAppPackageFileAsync("config.xml") ?? throw new FileNotFoundException();

        service._connectionString = $"mongodb://{XDocument.Load(configurationStream).Descendants("Server").ElementAt(0).Attribute("ip")!.Value}";

        service._client = new MongoClient(service._connectionString);

        var db = service._client.GetDatabase(service._collectionName);
        service._tasks = db.GetCollection<TaskModel>(service._collectionName);
        return service;
    }*/


}

