using System.Xml.Linq;

namespace ToDoList.Service
{
    //It is not a service locator
    //I think...
    //I hope
    public class ServiceFactory
    {
        private ITaskService? _service;

        public async Task<ITaskService> GetOrCreateService()
        {
           if(_service != null) 
                return _service;

            Stream configurationStream = FileSystem.OpenAppPackageFileAsync("config.xml").Result ?? throw new FileNotFoundException();
            string _connectionString = XDocument.Load(configurationStream).Descendants("Server").ElementAt(0).Attribute("connectionString")!.Value;
            
            //if xml - use xml
            //WIP

            _service = new TaskMongoDBService(_connectionString);
            await _service.Initialization;
            return _service;
        }

    }
}
