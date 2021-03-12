using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUITAR_SHOP
{
    class INSTRUMENTS
    {
        private int ID,PRICE;
        private string TYPE,NAME,MATERIALS;
        public int id { get { return ID; } set { ID = value; } }
        public string type { get { return TYPE; } set {TYPE = value; } }
        public string name { get { return NAME; } set { NAME = value; } }
        public int price { get { return PRICE; } set { PRICE = value; } }
        public string materials { get { return MATERIALS; } set { MATERIALS = value; } }
        public INSTRUMENTS() { }
        public  virtual string get_info()
        {
            return this.id + "/" + this.name + "/" + this.type + "/" + this.materials + "/" + this.price + "$/";

        }
    }
}
