using MongoDB.Bson.Serialization.Attributes;

namespace ToDoList.Model
{
    public class TaskModel
    {
        public TaskModel(string name)
        {
            Name = name;
            Id = "";
        }

        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("name")]
        public string Name { get; private set; }
        [BsonElement("status")]
        public TaskStatus Status { get; set; }
    }
}
