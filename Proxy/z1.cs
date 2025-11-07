namespace Proxy
{
    public interface IImage
    {
        void Display();
    }

    public class RealImage : IImage
    {
        private string _filename;
        public string FileName { get => _filename; private set => _filename = value; }

        public RealImage(string filename) {
            _filename = filename;
            LoadFromDisk();
        }

        private void LoadFromDisk() => Console.WriteLine($"Loading image {_filename}");

        public void Display()
        {
            if(_filename.Trim() == "")
            {
                Console.WriteLine($"Could not load image!");
                return;
            }

            Console.WriteLine($"Displaying image {_filename}");
        }
    }

    public class ImageProxy : IImage
    {
        private RealImage _realImage;
        private string _filename;

        public ImageProxy(string filename) => _filename = filename;

        public void Display()
        {
            if (_filename.Trim() == "")
            {
                Console.WriteLine($"Image not loaded!");
                return;
            }
            if(_realImage == null)
            {
                _realImage = new(_filename);
            }

            _realImage.Display();

        }


    }
    public class Program
    {
        static void Main(string[] args)
        {
            ImageProxy proxy = new("fajne.png");

            proxy.Display();
            proxy.Display();

        }
    }
}
