using System.Text.RegularExpressions;

namespace Adapter
{

    public interface IBankPayment
    {
        int Amount();
        string BankAccount();
    }

    public interface IMobilePayment
    {
        int Amount();
        string PhoneNumber();
    }

    public class PaymentService
    {

        private bool ValidateBankAccount(string account)
        {
            if(Regex.IsMatch(account, @"^PL[0-9]{24}$"))
            {
                return true;
            }

            return false;
        }

        public void ProcessPayment(IBankPayment payment)
        {
            try
            {
                if (payment.Amount() <= 0)
                    throw new Exception("Invalid amount!");

                if (!ValidateBankAccount(payment.BankAccount()))
                    throw new Exception("Invalid bank account!");

                Console.WriteLine($"Successfully deposited {payment.Amount()}PLN to Bank account: {payment.BankAccount()}");

            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }

    public class SwiftPayment : IBankPayment
    {
        private int _amount;
        private string _account;
        public int Amount() => _amount;
        public string BankAccount() => _account;

        public SwiftPayment(int amount, string account)
        {
            _amount = amount;
            _account = account;
        }
    }

    public class BlikPayment : IMobilePayment
    {
        private int _amount;
        private string _phoneNumber;
        public int Amount() => _amount;
        public string PhoneNumber() => _phoneNumber;
        public BlikPayment(int amount, string phoneNumber)
        {
            _amount = amount;
            _phoneNumber = phoneNumber;
        }
    }

    public class MobileToBankPaymentAdapter : IBankPayment
    {
        private BlikPayment _blikPayment;
        public int Amount() => _blikPayment.Amount();
        public string BankAccount()
        {
            string res = "PL000000";

            for(int i = 0; i < _blikPayment.PhoneNumber().Length; i++)
            {
                switch (_blikPayment.PhoneNumber()[i])
                {
                    case '0':
                        res += "11";
                        break;
                    case '1':
                        res += "22";
                        break;
                    case '2':
                        res += "33";
                        break;
                    case '3':
                        res += "44";
                        break;
                    case '4':
                        res += "55";
                        break;
                    case '5':
                        res += "66";
                        break;
                    case '6':
                        res += "77";
                        break;
                    case '7':
                        res += "88";
                        break;
                    case '8':
                        res += "99";
                        break;
                    case '9':
                        res += "00";
                        break;
                    default:break;
                }
            }

            return res;
        }

        public MobileToBankPaymentAdapter(BlikPayment blikPayment)
        {
            _blikPayment = blikPayment;
        }
    }

    public class Program
    {
        static void Main(string[] args)
        {
            PaymentService service = new PaymentService();
            service.ProcessPayment(new SwiftPayment(14, "PL123412341234123412341234"));
            service.ProcessPayment(new SwiftPayment(-5, "PL123412341234123412341234"));
            service.ProcessPayment(new SwiftPayment(14, "PL12341234123412341234123"));
            MobileToBankPaymentAdapter adapter = new MobileToBankPaymentAdapter(new BlikPayment(15, "123123456"));
            service.ProcessPayment(adapter);
            
        }
    }
}
