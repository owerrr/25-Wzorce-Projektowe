using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System_Biblioteka.Class
{
    public static class LibraryManagementSystem
    {
        private static List<User> _userList = new List<User>();

        private static User _userType;
        private static string _userName;
        private static string _password;

        public static bool _logged = false;

        public static void Login()
        {
            Console.WriteLine("\tLogowanie\n=============");
            Console.Write("Login: ");
            string login = Console.ReadLine();
            Console.Write("Haslo: ");
            string pass = Console.ReadLine();

            if (_userType.Verify())
            {
                Console.WriteLine("Udalo sie zalogowac!");
            }
            else
            {
                Console.WriteLine("Nie ma takiego konta!\nChcesz sie zarejestrowac? (T/n)");
                string ans = Console.ReadLine();

            }
        }

        public static void Register()
        {
            Console.WriteLine("\tRejestracja\n=============");
            Console.Write("Typ konta\n1. Staff\n2. Student\n > ");
            int accType = Convert.ToInt32(Console.ReadLine());
            Console.Write("Login: ");
            string login = Console.ReadLine();
            Console.Write("Haslo: ");
            string pass = Console.ReadLine();

            if (accType == 1) {
                Console.Write("Departament: ");
                string dept = Console.ReadLine();
                Console.Write("Fullname: ");
                string fullName = Console.ReadLine();

                int id = _userList.Select(u => u._id).FirstOrDefault();

                Staff newAcc = new(id, fullName, dept);
                _userType = newAcc;
                _userName = login;
                _password = pass;
            }
            else{
            
            }

            Console.WriteLine("Udalo sie zarejestrowac!");
        }

        public static void Logout()
        {
            Console.WriteLine("Wylogowywanie...");
        }
    }
}
