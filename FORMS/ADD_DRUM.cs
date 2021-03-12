using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace GUITAR_SHOP
{
    public partial class ADD_DRUM : Form
    {
        string path = "DRUM.txt";
        Random rnd = new Random();
        public ADD_DRUM()
        {
            InitializeComponent();
        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                int x;
                bool success1 = int.TryParse(shell.Text, out x);
                bool success2 = int.TryParse(id.Text, out x);
                bool success3 = int.TryParse(pieces.Text, out x);
                bool success4 = int.TryParse(price.Text, out x);
                bool success5 = int.TryParse(drum_name.Text, out x);
                if (comboBox1.Text == "" || comboBox3.Text == "" || drum_name.Text == "" || pieces.Text == "" || shell.Text == ""
                    || comboBox3.Text == "" || price.Text == "" || id.Text == "") { MessageBox.Show("enter all fields"); }




                else if (success1 == false) { MessageBox.Show("enter a valid SHELL THICKNESS"); shell.Clear(); }
                else if (success2 == false) { MessageBox.Show("enter a valid ID"); id.Clear(); }
                else if (success3 == false) { MessageBox.Show("enter a valid NBR OF PIECES"); pieces.Clear(); }
                else if (success4 == false) { MessageBox.Show("enter a valid PRICE"); price.Clear(); }
                else if (success5 == true) { MessageBox.Show("enter a valid NAME"); drum_name.Clear(); }
                else if (ID_EXIST(id.Text) == true)
                { MessageBox.Show("duplicated id");id.Text = rnd.Next(0, 10000).ToString() ; }
                else
                {
                    IList<DRUM> new_inst = new List<DRUM> { };
                    new_inst.Add(save_inst());
             
                    StringBuilder sb = new StringBuilder();
                    string line = "";
                    foreach (DRUM inst in new_inst)
                    {
                        line = inst.get_info() + "\n" + "----" + "\n";

                        File.AppendAllText(path, sb.Append(line).ToString());


                    }
                    MessageBox.Show("item saved");
                    button3.Enabled = true;
                    choose_id();

                }
            }
            catch (FileNotFoundException) {
                MessageBox.Show("CREATING FILE...");
                var text = "DRUM ID|NAME|TYPE|MATERIAL|PRICE|SHELL|PIECES|SIZE|EDGE\n"; 
                File.WriteAllText(path, text);
                }
            catch (IOException) { MessageBox.Show("I/O ERROR"); }
            catch (Exception)
            {
                var E = MessageBox.Show("UNKNOWN ERROR! ERASE THE FILE AND REPLACE IT?", "ERROR", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Error);
                if (E == DialogResult.Yes)
                {
                    var f = MessageBox.Show("ARE YOU SURE?", "WARNING", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (f == DialogResult.Yes)
                    {
                        var text = "DRUM ID|NAME|TYPE|MATERIAL|PRICE|SHELL|PIECES|SIZE|EDGE\n";
                        File.WriteAllText(path, text);
                    }
                    else if (f == DialogResult.No)
                    {
                        MessageBox.Show("PLEASE REVIEW FILE SYNTAX!\nDRUM ID | NAME | TYPE | MATERIAL | PRICE | SHELL | PIECES | SIZE | EDGE", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else if (E == DialogResult.No)
                { MessageBox.Show("PLEASE REVIEW FILE SYNTAX!\n(USERNAME,PASSWORD)", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
            }
        }

        private void ADD_DRUM_Load(object sender, EventArgs e)
        {
            IList<DRUM> new_inst = new List<DRUM> { };

           
            
            drum_types();
             choose_id();
            add_materials();
            new_inst.Add(save_inst());
            add_edge();
            add_size();
        }
        public void drum_types()
        {
            comboBox1.Items.Add("BASS DRUM");
            comboBox1.Items.Add("CONGA DRUM");
            comboBox1.Items.Add("TABLA");
            comboBox1.Items.Add("GOBLET DRUM");

        }
        public void choose_id()
        {
            Random rnd = new Random();
            id.Text = rnd.Next(1, 1000).ToString();
        }
        public void add_materials()
        {
            comboBox3.Items.Add("WOOD");
            comboBox3.Items.Add("STEEL");
            comboBox3.Items.Add("PLASTIC");

        }
        private DRUM save_inst()
        {
            int x;
            DRUM drum = new DRUM();
            drum.materials =comboBox3.Text;
            drum.name = drum_name.Text;


            drum.size = comboBox2.Text;

           drum.type = comboBox1.Text;
           
            bool success1 = int.TryParse(price.Text, out x);
            if (success1 == true) drum.price = int.Parse(price.Text);

            bool success2 = int.TryParse(id.Text, out x);
            if (success2 == true) drum.id = int.Parse(id.Text);

      

            bool success3 = int.TryParse(shell.Text, out x);
            if (success3 == true) drum.shell_thickness = int.Parse(shell.Text);
            bool success4 = int.TryParse(pieces.Text, out x);
            if (success4 == true) drum.nbr_of_pieces = int.Parse(pieces.Text);
            drum.edge = comboBox4.Text;
           
           
            return drum;
        }
        public void add_size()
        {
            comboBox2.Items.Add("BIG");
            comboBox2.Items.Add("SMALL");
            
        }
        public void add_edge()
        {
            comboBox4.Items.Add("thick");
            comboBox4.Items.Add("thin");

        }


        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                IList<string> new_inst2 = new List<string> { };
                using (FileStream fs = new FileStream(path, FileMode.Open))
                {
                    using (StreamReader sr = new StreamReader(fs))
                    {
                        string line = sr.ReadLine();
                        line = sr.ReadLine();

                        while (line != null)
                        {
                            new_inst2.Add(line);
                            line = sr.ReadLine();
                            line = sr.ReadLine();
                        }
                    }

                }

                DataTable table = new DataTable();
                table.Columns.Add("ID");
                table.Columns.Add("NAME");
                table.Columns.Add("TYPE");
                table.Columns.Add("MATERIALS");
                table.Columns.Add("PRICE");
                table.Columns.Add("SHELL THICKNESS");
                table.Columns.Add("NBR OF PIECES");
                table.Columns.Add("SIZE");
                table.Columns.Add("EDGE");


                foreach (string item in new_inst2)
                {
                    string[] broken_arr = item.Split('/');
                    DataRow row = table.NewRow();
                    row["ID"] = broken_arr[0];
                    row["NAME"] = broken_arr[1];
                    row["TYPE"] = broken_arr[2];
                    row["MATERIALS"] = broken_arr[3];
                    row["PRICE"] = broken_arr[4];
                    row["SHELL THICKNESS"] = broken_arr[5];
                    row["NBR OF PIECES"] = broken_arr[6];
                    row["SIZE"] = broken_arr[7];
                    row["EDGE"] = broken_arr[7];
                    Array.Clear(broken_arr, 0, broken_arr.Length);

                    table.Rows.Add(row);
                }
                dataGridView1.DataSource = table;
            }
            catch (FileNotFoundException)
            {
                MessageBox.Show("CREATING FILE...");
                var text = "DRUM ID|NAME|TYPE|MATERIAL|PRICE|SHELL|PIECES|SIZE|EDGE\n"; 
                File.WriteAllText(path, text); }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                int y;
                shell.Enabled = true;
                if (id.Text == "") { MessageBox.Show("please select an ID"); }
                else if(int.TryParse(id.Text,out y) == false) { MessageBox.Show("enter a number");id.Clear(); }
                else
                {
                    IList<string> list_guitars = new List<string>();
                    IDictionary<int, DRUM> guitar_dict = new Dictionary<int, DRUM>();
                    using (StreamReader sr = new StreamReader(path))
                    {
                        string line = sr.ReadLine();
                        line = sr.ReadLine();
                        while (line != null)
                        {

                            list_guitars.Add(line);
                            line = sr.ReadLine();
                            line = sr.ReadLine();
                        }
                    }
                    //adding to dictionary
                    foreach (var item in list_guitars)
                    {

                        string[] broken_str = item.Split('/');

                        DRUM a = new DRUM();

                        a.id = int.Parse(broken_str[0].Trim());
                        a.name = broken_str[1].Trim();
                        a.type = broken_str[2];
                        a.materials = broken_str[3];
                        a.price = int.Parse(broken_str[4].TrimEnd('$'));
                        a.shell_thickness = int.Parse(broken_str[5]);
                        a.nbr_of_pieces = int.Parse(broken_str[6]);
                        a.size = broken_str[7];
                        a.edge = broken_str[8];

                        guitar_dict.Add(int.Parse(broken_str[0]), a);
                        Array.Clear(broken_str, 0, broken_str.Length);

                    }
                    //contains?
                    if (guitar_dict.Keys.Contains(int.Parse(id.Text)))
                    {
                        int x;
                        bool guitars = int.TryParse(id.Text, out x);
                        if (guitars == true)
                        { guitar_dict.Remove(int.Parse(id.Text)); }


                        using (StreamWriter sw = new StreamWriter(path))
                        { sw.WriteLine("DRUM ID|NAME|TYPE|MATERIAL|PRICE|SHELL|PIECES|SIZE|EDGE");
                            foreach (var item in guitar_dict)
                            {
                                sw.WriteLine(item.Value.get_info());
                                sw.WriteLine("----");
                            }
                            MessageBox.Show("DRUM DELETED");
                        }
                    }
                    else
                    {
                        MessageBox.Show("enter a valid ID");
                        id.Clear();
                    }

                }
            }
            catch (FileNotFoundException) {
                MessageBox.Show("CREATING FILE..."); 
                var text = "DRUM ID|NAME|TYPE|MATERIAL|PRICE|SHELL|PIECES|SIZE|EDGE\n";
                File.WriteAllText(path, text); }


                 
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        public bool ID_EXIST(string A)
        {
            IList<string> list_id = new List<string>();
            using (StreamReader sr = new StreamReader(path))
            {
                string[] broken_id = { };
                string line = sr.ReadLine();
                while (line != null)
                {
                    broken_id = line.Split('/');
                    list_id.Add(broken_id[0]);
                    line = sr.ReadLine();
                }
                if (list_id.Contains(A)) return true;
                else return false;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
