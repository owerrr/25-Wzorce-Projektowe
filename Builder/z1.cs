namespace ConsoleApp1
{
    public interface IComputerBuilder
    {
        void BuildCPU();
        void BuildRAM();
        void BuildHD();
        void BuildMB();
        void BuildGPU();
        Computer GetResult();

    }

    public class Computer
    {
        private List<string> parts = new();
        private string[] partsName = ["CPU", "RAM", "HD", "MB", "GPU"];
        public void Add(string part) => parts.Add(part);
        public void Show()
        {
            Console.WriteLine("Your computer: ");
            for (int i = 0; i < parts.Count; i++) {
                Console.WriteLine($"{partsName[i]}: {parts[i]}");
            }
        }
    }
    public class ComputerBuilder : IComputerBuilder
    {
        private Computer computer = new();

        public void BuildCPU()
        {
            computer.Add("Intel i7");
        }
        public void BuildRAM()
        {
            computer.Add("16GB");
        }
        public void BuildHD()
        {
            computer.Add("HDD 1TB");
        }
        public void BuildMB()
        {
            computer.Add("ASUS TUF B650-E");
        }
        public void BuildGPU()
        {
            computer.Add("GTX 1060");
        }
        
        public Computer GetResult()
        {
            return computer;
        }

    }
    
    public class Director
    {
        private IComputerBuilder computerBuilder;
        public Director(IComputerBuilder computerBuilder)
        {
            this.computerBuilder = computerBuilder;
        }

        public void Construct()
        {
            computerBuilder.BuildCPU();
            computerBuilder.BuildRAM();
            computerBuilder.BuildHD();
            computerBuilder.BuildMB();
            computerBuilder.BuildGPU();
        }
    }

    public class Program
    {
        static void Main(string[] args)
        {
            IComputerBuilder computerBuilder = new ComputerBuilder();
            Director director = new(computerBuilder);
            director.Construct();

            Computer pc = computerBuilder.GetResult();
            pc.Show();
        }
    }
}
