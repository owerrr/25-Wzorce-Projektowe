namespace Adapter
{
    public interface IImage
    {
        void Load(string fileName);
        void Display();
    }

    public class JpegImage : IImage
    {
        public void Load(string fileName) => Console.WriteLine($"Loading: {fileName}.jpeg");
        public void Display() => Console.WriteLine($"Displaying JPEG file");
    }

    public class PngImage : IImage
    {
        public void Load(string fileName) => Console.WriteLine($"Loading: {fileName}.png");
        public void Display() => Console.WriteLine($"Displaying PNG file");
    }

    public class GifHandler
    {
        public void OpenFile(string filename) => Console.WriteLine($"Opening: {filename}.gif");
        public void RenderGif() => Console.WriteLine("Rendering GIF...");
    }

    public class GifImageAdapter : IImage
    {
        private GifHandler _gifHandler = new GifHandler();
        public void Load(string fileName) => _gifHandler.OpenFile(fileName);
        public void Display() => _gifHandler.RenderGif();
    }

    public class Program
    {
        static void Main(string[] args)
        {
            JpegImage jpeg = new();
            jpeg.Load("testowyJPEG");
            jpeg.Display();

            PngImage png = new();
            png.Load("DrugiToPNG");
            png.Display();

            GifImageAdapter gif = new();
            gif.Load("tenToGif");
            gif.Display();
        }
    }
}
