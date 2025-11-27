namespace State
{
    public interface IVendingMachineState
    {
        void InsertMoney(int amount);
        void SelectProduct(string productName);
        void WithdrawMoney();
    }

    public class VendingMachine
    {
        private IVendingMachineState _currentState { get; set; }
        public IVendingMachineState CurrentState { get => _currentState; set => _currentState = value; }

        public Dictionary<string, (int Price, int Quantity)> Products { get; private set; }
        public int CurrentBalance { get; set; }
        public string SelectedProduct { get; set; }

        public VendingMachine()
        {
            Products = new() {
                { "Cola", (5, 5) },
                { "Woda", (2, 2) },
                { "Sok", (4, 6) },
                { "Nestea", (5, 0) }
            };

            _currentState = new WaitingForMoneyState(this);
            CurrentBalance = 0;
            SelectedProduct = null;
        }

        public void InsertMoney(int amount)
        {
            _currentState.InsertMoney(amount);
        }

        public void SelectProduct(string productName)
        {
            _currentState.SelectProduct(productName);
        }

        public void WithdrawMoney()
        {
            if(CurrentBalance <= 0)
            {
                Console.WriteLine("Brak środków do wpyłaty!");
                return;
            }

            Console.WriteLine($"Wydaje {CurrentBalance}PLN...");
            CurrentBalance = 0;
            _currentState = new WaitingForMoneyState(this);
        }

        public void DispenseProduct()
        {
            if(SelectedProduct == null)
            {
                Console.WriteLine("Wybierz produkt!");
                return;
            }

            if (!Products.ContainsKey(SelectedProduct))
            {
                Console.WriteLine("Nie ma takiego produktu!");
                return;
            }

            var product = Products[SelectedProduct];

            if(product.Quantity == 0)
            {
                Console.WriteLine($"Brak na stanie produktu {SelectedProduct}");
                _currentState = new WaitingForMoneyState(this);
                return;
            }

            if(product.Price > CurrentBalance)
            {
                Console.WriteLine($"Niewystarczająca ilość pieniędzy! Ten produkt kosztuje {product.Price}PLN.");
                _currentState = new WaitingForMoneyState(this);
                return;
            }

            Products[SelectedProduct] = (product.Price, product.Quantity - 1);
            CurrentBalance -= product.Price;
            Console.WriteLine($"Wydano {SelectedProduct}. Reszta: {CurrentBalance}PLN.");
            CurrentBalance = 0;
            SelectedProduct = null;
            _currentState = new WaitingForMoneyState(this);
        }
    }

    public class WaitingForMoneyState : IVendingMachineState
    {
        private readonly VendingMachine _vMachine;

        public WaitingForMoneyState(VendingMachine vMachine)
        {
            _vMachine = vMachine;
        }

        public void InsertMoney(int amount)
        {
            if(amount <= 0)
            {
                Console.WriteLine("Kwota musi być dodatnia!");
                return;
            }

            _vMachine.CurrentBalance += amount;
            Console.WriteLine($"Pomyślnie włożono {amount}PLN. Aktualny stan: {_vMachine.CurrentBalance}PLN.");
            _vMachine.CurrentState = new ProductSelectionState(_vMachine);
        }

        public void SelectProduct(string productName)
        {
            Console.WriteLine("Najpierw włóż pieniądze!");
        }

        public void WithdrawMoney()
        {
            if (_vMachine.CurrentBalance <= 0)
            {
                Console.WriteLine("Brak środków do wpyłaty!");
                return;
            }

            Console.WriteLine($"Wydaje {_vMachine.CurrentBalance}PLN...");
            _vMachine.CurrentBalance = 0;
            _vMachine.CurrentState = new WaitingForMoneyState(_vMachine);
        }
    }

    public class ProductSelectionState : IVendingMachineState
    {
        private readonly VendingMachine _vMachine;

        public ProductSelectionState(VendingMachine vMachine)
        {
            _vMachine = vMachine;
        }

        public void InsertMoney(int amount)
        {
            if (amount <= 0)
            {
                Console.WriteLine("Kwota musi być dodatnia!");
                return;
            }

            _vMachine.CurrentBalance += amount;
            Console.WriteLine($"Pomyślnie włożono {amount}PLN. Aktualny stan: {_vMachine.CurrentBalance}PLN.");
            _vMachine.CurrentState = new ProductSelectionState(_vMachine);
        }

        public void SelectProduct(string productName)
        {
            if (!_vMachine.Products.ContainsKey(productName)){
                Console.WriteLine("Nie ma takiego produktu!");
                return;
            }

            _vMachine.SelectedProduct = productName;
            _vMachine.CurrentState = new FinalizeTransactionState(_vMachine);
            _vMachine.CurrentState.SelectProduct(productName);
        }
        public void WithdrawMoney()
        {
            if (_vMachine.CurrentBalance <= 0)
            {
                Console.WriteLine("Brak środków do wpyłaty!");
                return;
            }

            Console.WriteLine($"Wydaje {_vMachine.CurrentBalance}PLN...");
            _vMachine.CurrentBalance = 0;
            _vMachine.CurrentState = new WaitingForMoneyState(_vMachine);
        }
    }

    public class FinalizeTransactionState : IVendingMachineState
    {
        private readonly VendingMachine _vMachine;

        public FinalizeTransactionState(VendingMachine vMachine)
        {
            _vMachine = vMachine;
        }

        public void InsertMoney(int amount)
        {
            Console.WriteLine("Transakcja w toku. Nie można dodawać pieniędzy!");
        }

        public void SelectProduct(string productName)
        {
            _vMachine.DispenseProduct();
        }
        public void WithdrawMoney()
        {
            Console.WriteLine("Transakcja w toku. Nie można wypłacić środków!");
        }
    }

    public class Program
    {
        static void Main(string[] args)
        {
            VendingMachine vendingMachine = new();

            vendingMachine.InsertMoney(2);
            vendingMachine.InsertMoney(5);
            vendingMachine.SelectProduct("Cola");
            vendingMachine.InsertMoney(2);
            vendingMachine.SelectProduct("Woda");
            vendingMachine.SelectProduct("Sok");
            vendingMachine.InsertMoney(1);
            vendingMachine.SelectProduct("Sok");
            vendingMachine.InsertMoney(3);
            vendingMachine.SelectProduct("Sok");
            vendingMachine.SelectProduct("Nestea");
            vendingMachine.InsertMoney(2);
            vendingMachine.SelectProduct("Nestea");
            vendingMachine.InsertMoney(3);
            vendingMachine.WithdrawMoney();
            vendingMachine.SelectProduct("Nestea");
            vendingMachine.InsertMoney(5);
            vendingMachine.SelectProduct("Nestea");
        }
    }
}
