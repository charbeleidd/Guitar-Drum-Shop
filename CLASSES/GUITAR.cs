using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUITAR_SHOP
{
    class GUITAR:INSTRUMENTS
    {
        private string COLOR;
        private int NBR_OF_STRINGS,NBR_OF_FRETS;
     
        public string color { get { return COLOR; } set { COLOR = value; } }
      
        public int nbr_of_strings { get { return NBR_OF_STRINGS; } set { NBR_OF_STRINGS = value; } }
        public int nbr_of_frets { get { return NBR_OF_FRETS; } set { NBR_OF_FRETS = value; } }
        public GUITAR(int a,string h,string b, string c ,int d, int e ,string f,int g)
        {
            this.id = a;
            this.type = h;
            this.name = b;
            this.color = c;
            this.nbr_of_frets = d;
            this.nbr_of_strings = e;
            this.materials = f;
            this.price = g;
        }
        public GUITAR() { }

        public override string get_info()
        {
            return this.id + "/" + this.name + "/" + this.type + "/" + this.materials + "/" + this.price + "$/" +
                this.color + "/" + this.nbr_of_frets + "/" + this.nbr_of_strings +"/";
        }
    }
}
