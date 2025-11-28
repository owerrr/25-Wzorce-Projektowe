namespace Observer
{
    public interface IObserver
    {
        public void Update(float temp, float humi, float press);
    }
    public interface ISubject
    {
        public void RegisterObserver(IObserver obs);
        public void UnregisterObserver(IObserver obs);
        public void NotifyObservers();
    }

    public class WeatherStation : ISubject
    {
        private List<IObserver> _observers = new();
        private float _temp;
        private float _humi;
        private float _press;

        public void NotifyObservers()
        {
            foreach (var obs in _observers)
            {
                obs.Update(_temp, _humi, _press);
            }
        }

        public void RegisterObserver(IObserver obs)
        {
            _observers.Add(obs);
            Console.WriteLine("Dodano nowy observer!");
        }

        public void UnregisterObserver(IObserver obs)
        {
            _observers.Remove(obs);
            Console.WriteLine("Usunięto observer!");
        }

        public void UpdateMeasurements(float temp, float humi, float press)
        {
            _temp = temp;
            _humi = humi;
            _press = press;
            NotifyObservers();
        }
    }

    public class MobileDisplay : IObserver
    {
        private WeatherStation _weatherStation;
        private float _temp;
        private float _humi;
        private float _press;

        public MobileDisplay(WeatherStation weatherStation)
        {
            _weatherStation = weatherStation;
            _weatherStation.RegisterObserver(this);
        }

        public void Update(float temp, float humi, float press)
        {
            _temp = temp;
            _humi = humi;
            _press = press;
            Display();
        }

        public void Display()
        {
            Console.WriteLine($"MOBILE\nTemperatura: {_temp}\nWilgotność: {_humi}\nCiśnienie: {_press}\n");
        }
    }

    public class DesktopDisplay : IObserver
    {
        private WeatherStation _weatherStation;
        private float _temp;
        private float _humi;
        private float _press;

        public DesktopDisplay(WeatherStation weatherStation)
        {
            _weatherStation = weatherStation;
            _weatherStation.RegisterObserver(this);
        }

        public void Update(float temp, float humi, float press)
        {
            _temp = temp;
            _humi = humi;
            _press = press;
            Display();
        }

        public void Display()
        {
            Console.WriteLine($"DESKTOP\nTemperatura: {_temp}\nWilgotność: {_humi}\nCiśnienie: {_press}\n");
        }
    }

    public class Program
    {
        static void Main(string[] args)
        {
            WeatherStation weatherStation = new();

            MobileDisplay mobileDisplay = new(weatherStation);
            DesktopDisplay desktopDisplay = new(weatherStation);

            weatherStation.UpdateMeasurements(15f, 1f, 25f);

            weatherStation.UnregisterObserver(mobileDisplay);

            weatherStation.UpdateMeasurements(10f, 5f, 10f);
        }
    }
}