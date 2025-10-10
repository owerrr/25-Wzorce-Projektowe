using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System_Biblioteka.Class
{
    public class Account
    {
        private int _noBorrowedBooks;
        private int _noReservedBooks;
        private int _noReturnedBooks;
        private int _noLostBooks;
        private decimal _fineAmount;

        public decimal CalculateFine()
        {
            return 1m;
        }
    }
}
