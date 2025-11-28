namespace Observer
{
    public interface ICurrencyObserver
    {
        public void Update(string currency, double value);
    }

    public interface ISubject
    {
        public void Attach(ICurrencyObserver obs);
        public void Detach(ICurrencyObserver observer);
        public void Notify();
    }

    public class CurrencyExchange : ISubject
    {
        private List<ICurrencyObserver> _observers = new();
        private Dictionary<string, double> _currencies = new();

        private string _currency;
        private double _value;
     
        public CurrencyExchange()
        {
            _currencies = new()
            {
                { "PLN", 1 },
                { "EUR", 0.24d },
                { "USD", 0.27d },
                { "GBP", 0.21d }
            };
        }

        public void Attach(ICurrencyObserver obs)
        {
            _observers.Add(obs);
            Console.WriteLine("Dodano nowy observer!");
        }

        public void Detach(ICurrencyObserver obs)
        {
            _observers.Remove(obs);
            Console.WriteLine("Pomyślnie usunięto observer!");
        }

        public void Notify()
        {
            foreach(var obs in _observers)
            {
                obs.Update(_currency, _value);
            }
        }

        public void UpdateRate(string currency, double rate)
        {
            if (!_currencies.ContainsKey(currency))
            {
                Console.WriteLine("\nNieprawidłowa waluta!");
                return;
            }

            double currencyValue = _currencies[currency];
            
            if(currencyValue + rate <= 0)
            {
                Console.WriteLine("\nwartość waluty musi być większa od 0!");
                return;
            }

            _currency = currency;
            _value = currencyValue + rate;

            _currencies[currency] = _value;
            
            Console.WriteLine($"\nPomyślnie zaktualizowano walute {currency}. Teraz jest warta: {_currencies[currency]} za 1PLN.");
            Notify();
        }
    }

    public class MobileAppNotifier : ICurrencyObserver
    {
        private string _currency;
        private double _value;

        public void Update(string currency, double value)
        {
            _currency = currency;
            _value = value;
            Console.WriteLine($"\nMOBILE\nUWAGA! PRZEWALUTOWANIE!\n{_currency} jest teraz warte {_value}{_currency} za 1PLN!");
        }
    }

    public class DesktopAppNotifier : ICurrencyObserver
    {
        private string _currency;
        private double _value;

        public void Update(string currency, double value)
        {
            _currency = currency;
            _value = value;
            Console.WriteLine($"\nDESKTOP\nUWAGA! PRZEWALUTOWANIE!\n{_currency} jest teraz warte {_value}{_currency} za 1PLN!");
        }
    }

    public class Program
    {
        static void Main(string[] args)
        {
            CurrencyExchange currencyExchange = new();

            currencyExchange.Attach(new MobileAppNotifier());
            currencyExchange.Attach(new DesktopAppNotifier());

            currencyExchange.UpdateRate("GBP", 0.1d);
            currencyExchange.UpdateRate("USD", -0.1d);
            currencyExchange.UpdateRate("EUR", -5d);

        }
    }
}
