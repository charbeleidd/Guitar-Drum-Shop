using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUITAR_SHOP
{
    class CLIENT
    {
        private int ID,AGE,MONEY;
      private string F_NAME,L_NAME;
        public int id { set { ID = value; } get { return ID; } }
        public int age { set { AGE = value; } get { return AGE; } }
        public string f_name { set { F_NAME = value; } get { return F_NAME; } }
        public string l_name { set { L_NAME = value; } get { return L_NAME; } }
        public int money { set {MONEY = value; } get { return MONEY; } }

        public CLIENT(int a, int b, string c, string d,int e) {
            id = a;
            age = b;
            f_name = c;
            l_name = d;
            money = e;
        }
        public CLIENT() { }
        
    }
}
