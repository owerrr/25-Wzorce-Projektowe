namespace State
{

    public class StateContext
    {
        private State _currentState;
        public int Balance { get; set; }

        public StateContext(int balance)
        {
            Balance = balance;

            if(Balance > 0)
            {
                _currentState = new NoCardState(this);
            }
            else
            {
                _currentState = new NoCashState(this);
            }
        }

        public void ChangeState(State state)
        {
            _currentState = state;
        }

        public void InsertCard()
        {
            _currentState.InsertCard();
        }

        public void EjectCard()
        {
            _currentState.EjectCard();
        }

        public void InsertPin(int pin)
        {
            _currentState.InsertPin(pin);
        }

        public void WithdrawCash(int amount)
        {
            _currentState.WithdrawCash(amount);
        }
    }

    public abstract class State
    {
        protected StateContext _context;

        protected State(StateContext context)
        {
            _context = context;
        }

        public abstract void InsertCard();
        public abstract void EjectCard();
        public abstract void InsertPin(int pin);
        public abstract void WithdrawCash(int amount);
    }

    public class NoCardState : State
    {
        public NoCardState(StateContext context) : base(context) { }

        public override void InsertCard()
        {
            Console.WriteLine("Włożono kartę.");
            _context.ChangeState(new CardInsertedState(_context));
        }

        public override void EjectCard()
        {
            Console.WriteLine("Brak karty do wyjęcia.");
        }

        public override void InsertPin(int pin)
        {
            Console.WriteLine("Brak karty!");
        }

        public override void WithdrawCash(int amount)
        {
            Console.WriteLine("Nie można wypłacić pieniędzy! Najpierw włóż kartę!");
        }
    }

    public class CardInsertedState : State
    {
        public CardInsertedState(StateContext context) : base(context) { }

        public override void InsertCard()
        {
            Console.WriteLine("Karta została już włożona!");
        }

        public override void EjectCard()
        {
            Console.WriteLine("Karta została wyjęta.");
            _context.ChangeState(new NoCardState(_context));
        }

        public override void InsertPin(int pin)
        {
            if(pin == 1234)
            {
                Console.WriteLine("PIN poprawny.");
                _context.ChangeState(new PinInsertedState(_context));
            }
            else
            {
                Console.WriteLine("Błędny kod PIN!");
            }
        }

        public override void WithdrawCash(int amount)
        {
            Console.WriteLine("Najpierw wprowadź PIN!");
        }
    }

    public class PinInsertedState : State
    {
        public PinInsertedState(StateContext context) : base(context) { }

        public override void InsertCard()
        {
            Console.WriteLine("Karta została już włożona!");
        }

        public override void EjectCard()
        {
            Console.WriteLine("Karta została wyjęta.");
            _context.ChangeState(new NoCardState(_context));
        }

        public override void InsertPin(int pin)
        {
            Console.WriteLine("PIN został już wprowadzony!");
        }

        public override void WithdrawCash(int amount)
        {
            if(amount <= 0)
            {
                Console.WriteLine("Kwota musi być większa niz 0!");
                return;
            }

            if(amount > _context.Balance)
            {
                Console.WriteLine("Bankomat nie posiada tyle gotówki!");
                return;
            }

            _context.Balance -= amount;
            Console.WriteLine($"Wypłacono {amount}PLN. Pozostało {_context.Balance}PLN.\nDziękujemy za skorzystanie z usług bankomatu.");
            
            if(_context.Balance == 0)
            {
                _context.ChangeState(new NoCashState(_context));
            }
            else
            {
                _context.ChangeState(new NoCardState(_context));
            }
        }
    }

    public class NoCashState : State
    {
        public NoCashState(StateContext context) : base(context) { }

        public override void InsertCard()
        {
            Console.WriteLine("Bankomat nie ma gotówki. Operacja niedostepna.");
        }

        public override void EjectCard()
        {
            Console.WriteLine("Brak karty do wyjęcia.");
        }

        public override void InsertPin(int pin)
        {
            Console.WriteLine("Banomat nie ma gotówki.");
        }

        public override void WithdrawCash(int amount)
        {
            Console.WriteLine("Brak dostępnych środków w bankomacie.");
        }
    }

    public class Program
    {
        static void Main(string[] args)
        {
            StateContext atmContext = new(500);

            atmContext.InsertCard();
            atmContext.InsertPin(1234);
            atmContext.WithdrawCash(200);

            atmContext.InsertCard();
            atmContext.InsertPin(4321);
            atmContext.InsertPin(1234);
            atmContext.WithdrawCash(400);
            atmContext.WithdrawCash(300);

            atmContext.InsertCard();
        }
    }
}
