using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System_Biblioteka.Class
{
    public abstract class User
    {
        private string _name;
        public int _id;

        public User(int id, string name)
        {
            _name = name;
            _id = id;
        }

        public bool Verify()
        {

            return true;
        }

        public bool CheckAccount()
        {

            return true;
        }

        public string GetBookInfo()
        {
            string res = "";


            return res;
        }
    }
}
