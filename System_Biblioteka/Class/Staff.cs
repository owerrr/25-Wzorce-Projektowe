using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System_Biblioteka.Class
{
    public class Staff : User
    {
        private string _dept;

        public Staff(int id, string name, string dept) : base(id, name)
        {
            _dept = dept;
        }
    }
}
