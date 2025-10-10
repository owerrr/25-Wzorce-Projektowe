using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System_Biblioteka.Class
{
    public class Student : User
    {
        private string _class;

        public Student(int id, string name, string _class) : base(id, name) {
            this._class = _class;
        }
    }
}
