using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUITAR_SHOP
{
    class DRUM:INSTRUMENTS
    {
        private int SHELL_THICKNESS, NBR_OF_PIECES;
        private string SIZE,EDGE;
        public int shell_thickness { get { return SHELL_THICKNESS; } set {SHELL_THICKNESS = value; } }
        public int nbr_of_pieces { get { return NBR_OF_PIECES; } set { NBR_OF_PIECES = value; } }
        public string size { get { return SIZE; } set { SIZE = value; } }
        public string edge { get { return EDGE; } set { EDGE = value; } }
        public DRUM() { }
        public DRUM(int a, string b, string c ,string d, int e,int f , int g, string  h , string i)
        {
            this.id = a;
            this.type = b;
            this.name = c;
            this.materials = d;
            this.shell_thickness = e;
            this.nbr_of_pieces = f;
            this.size = h;
            this.edge = i;
            this.price = g;

        }
        public override string get_info()
        {
            return this.id + "/" + this.name + "/" + this.type + "/" + this.materials + "/" + this.price + "$/" +
                  this.shell_thickness + "/" + this.nbr_of_pieces + "/" + this.size + "/" + this.edge + "/";
        }



    }
}
