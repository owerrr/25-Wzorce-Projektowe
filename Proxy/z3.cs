namespace Proxy
{
    public class User
    {
        private bool _hasAccess;
        public bool HasAccess { get => _hasAccess; private set => _hasAccess = value; }

        public User(bool hasAccess) => HasAccess = hasAccess;
    }

    public interface IDocument
    {
        public void ReadContent();
    }

    public class SecureDocument : IDocument
    {
        private string _data;
        public SecureDocument(string data) => _data = data;
        public void ReadContent()
        {
            Console.WriteLine("Reading secure document...");
            Console.WriteLine(_data);
        }
    }

    public class AccessProxy : IDocument
    {
        private SecureDocument _secureDocument;
        private User _user;
        public AccessProxy(SecureDocument secureDocument, User user)
        {
            _secureDocument = secureDocument;
            _user = user;
        }

        public void ReadContent()
        {
            if (!_user.HasAccess)
            {
                Console.WriteLine("You don't have access to this document!");
                return;
            }

            _secureDocument.ReadContent();
        }
    }

    public class Program
    {
        static void Main(string[] args)
        {
            User admin = new(true);
            User user = new(false);
            SecureDocument document = new("Tajna wiadomosc");
            AccessProxy proxy = new(document, admin);
            proxy.ReadContent();
            proxy = new(document, user);
            proxy.ReadContent();
        }
    }
}
