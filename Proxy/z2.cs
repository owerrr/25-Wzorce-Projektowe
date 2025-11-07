namespace Proxy
{
    
    public interface IRemoteService
    {
        public string GetData(int id);
    }

    public class RealRemoteService : IRemoteService
    {
        private string _data;
        public RealRemoteService(string data) => _data = data;
        public string GetData(int id)
        {
            Console.WriteLine("Connecting to remote service...");
            return _data;
        }
    }

    public class RemoteServiceProxy : IRemoteService
    {
        private RealRemoteService _realRemoteService;
        private Dictionary<int, string> _cache = new Dictionary<int, string>();

        public RemoteServiceProxy(RealRemoteService realRemoteService)
        {
            _realRemoteService = realRemoteService;
        }

        public string GetData(int id)
        {
            string data;
            if (_cache.TryGetValue(id, out data))
            {
                Console.WriteLine("Getting data from cache...");
                return data;
            }

            data = _realRemoteService.GetData(id);
            _cache.Add(id, data);
            return data;
        }
    }

    public class Program
    {
        static void Main(string[] args)
        {
            RealRemoteService service = new("halo");
            RemoteServiceProxy proxy = new(service);

            proxy.GetData(1);
            proxy.GetData(1);

        }
    }
}
