using MongoDB.Bson.Serialization.Attributes;

namespace ToDoList.Model
{
    internal class TaskModel
    {
        private TaskStatus _status;
        private string _name;

        public TaskModel(string name)
        {
            _name = name;
            Id = "";
        }

        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }
        public string Name { get => _name; }
        public TaskStatus Status { get => _status; }
    }
}
