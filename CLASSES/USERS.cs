using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUITAR_SHOP
{
    public class USERS
    {
        private string USERNAME, PASSWORD;
        public USERS(string username,string pass)
        {
            USERNAME = username;
            PASSWORD = pass;
        }
        public string LOGIN_USERNAME { get { return USERNAME; } set { USERNAME = value; } }
        public string LOGIN_PASS { get { return PASSWORD; } set { PASSWORD = value; } }
    }
}
