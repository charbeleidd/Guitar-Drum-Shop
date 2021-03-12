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
    public partial class ADD_GUITAR : Form
    {
        string path = "GUITAR.txt";
        Random rnd = new Random();
        public ADD_GUITAR()
        {
            InitializeComponent();
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                int x;
                bool success1 = int.TryParse(strings.Text, out x);
                bool success2 = int.TryParse(id.Text, out x);
                bool success3 = int.TryParse(frets.Text, out x);
                bool success4 = int.TryParse(price.Text, out x);
                bool success5 = int.TryParse(name.Text, out x);
                if (comboBox1.Text == "" || comboBox2.Text == "" || name.Text == "" || frets.Text == "" || strings.Text == ""
                    || comboBox3.Text == "" || price.Text == "" || id.Text == "") { MessageBox.Show("enter all fields"); }




                else if (success1 == false) { MessageBox.Show("enter a valid nbr of strings"); strings.Clear(); }
                else if (success2 == false) { MessageBox.Show("enter a valid ID"); id.Clear(); }
                else if (success3 == false) { MessageBox.Show("enter a valid NBR OF FRETS"); frets.Clear(); }
                else if (success4 == false) { MessageBox.Show("enter a valid PRICE"); price.Clear(); }
                else if (success5 == true) { MessageBox.Show("enter a valid NAME"); name.Clear(); }
                //id exist? 
                else if (ID_EXIST(id.Text)) { MessageBox.Show("duplicated id"); id.Text = rnd.Next(0, 10000).ToString(); }

                else
                {
                    IList<GUITAR> new_inst = new List<GUITAR> { };
                    new_inst.Add(save_inst());
                
                    StringBuilder sb = new StringBuilder();
                    string line = "";
                    foreach (GUITAR inst in new_inst)
                    {
                        line = inst.get_info() + "\n" + "----" + "\n";

                        File.AppendAllText(path, sb.Append(line).ToString());


                    }
                    MessageBox.Show("item saved");
                    button3.Enabled = true;
                    choose_id();

                }
            }
            catch (FileNotFoundException)
            {
                MessageBox.Show("File not found:+" +path + "\n CREATING FILE...");
                var text = "ID|NAME|TYPE|MATERIALS|PRICE|COLOR|FRETS|STRINGS \n";
                File.WriteAllText(path, text); }
        }

        private void ADD_GUITAR_Load(object sender, EventArgs e)
        {
            IList<GUITAR> new_inst = new List<GUITAR> { };

            Random rnd = new Random();
            //textBox5.Enabled = false;
            add_colors();
            add_types();
            choose_id();
            add_materials();
            new_inst.Add(save_inst());
        }
        private GUITAR save_inst()
        {
            int x;
            GUITAR guitar = new GUITAR();
            guitar.color = comboBox2.Text;
            guitar.name = name.Text;




            guitar.type = comboBox1.Text;
            guitar.materials = comboBox3.Text;
            bool success1 = int.TryParse(frets.Text, out x);
            if (success1 == true) guitar.price = int.Parse(price.Text);

            bool success2 = int.TryParse(id.Text, out x);
            if (success2 == true) guitar.id = int.Parse(id.Text);

            guitar.type = comboBox1.Text;

            bool success3 = int.TryParse(strings.Text, out x);
            if (success3 == true) guitar.nbr_of_strings = int.Parse(strings.Text);
            bool success4 = int.TryParse(frets.Text, out x);
            if (success4 == true) guitar.nbr_of_frets = int.Parse(frets.Text);


            return guitar;
        }
        public void add_colors()
        {
            string path = "colors.txt";


            string[] lines = File.ReadAllLines(path);
            foreach (string line in lines) comboBox2.Items.Add(line);

        }
        public void add_types()
        {
            comboBox1.Items.Add("The ELECTRICAL family");

            comboBox1.Items.Add("The string family");

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

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
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
                table.Columns.Add("COLOR");
                table.Columns.Add("FRETS");
                table.Columns.Add("STRINGS");


                foreach (string item in new_inst2)
                {
                    string[] broken_arr = item.Split('/');
                    DataRow row = table.NewRow();
                    row["ID"] = broken_arr[0];
                    row["NAME"] = broken_arr[1];
                    row["TYPE"] = broken_arr[2];
                    row["MATERIALS"] = broken_arr[3];
                    row["PRICE"] = broken_arr[4];
                    row["COLOR"] = broken_arr[5];
                    row["FRETS"] = broken_arr[6];
                    row["STRINGS"] = broken_arr[7];
                    Array.Clear(broken_arr, 0, broken_arr.Length);

                    table.Rows.Add(row);
                }
                dataGridView1.DataSource = table;
            }
            catch (FileNotFoundException)
            {
                MessageBox.Show("File not found:+" + path + "\n CREATING FILE...");
                var text = "ID|NAME|TYPE|MATERIALS|PRICE|COLOR|FRETS|STRINGS \n";
                File.WriteAllText(path, text); }


        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                int y;
                id.Enabled = true;
                if (id.Text == "") { MessageBox.Show("please select an ID"); }
                else if(int.TryParse(id.Text,out y) == false) { MessageBox.Show("ENTER A NUMBER"); id.Clear(); }
                else
                {
                    IList<string> list_guitars = new List<string>();
                    IDictionary<int, GUITAR> guitar_dict = new Dictionary<int, GUITAR>();
                    using (StreamReader sr = new StreamReader(path))
                    {
                        string line = sr.ReadLine();
                        line = sr.ReadLine();
                        //line = sr.ReadLine();
                        while (line != null)
                        {

                            list_guitars.Add(line);
                            line = sr.ReadLine();
                            line = sr.ReadLine();
                        }
                    }

                    foreach (var item in list_guitars)
                    {

                        string[] broken_str = item.Split('/');

                        GUITAR a = new GUITAR();

                        a.id = int.Parse(broken_str[0].Trim());
                        a.name = broken_str[1].Trim();
                        a.type = broken_str[2];
                        a.materials = broken_str[3];
                        a.price = int.Parse(broken_str[4].TrimEnd('$'));
                        a.color = broken_str[5];
                        a.nbr_of_frets = int.Parse(broken_str[6]);
                        a.nbr_of_strings = int.Parse(broken_str[7]);

                        guitar_dict.Add(int.Parse(broken_str[0]), a);
                        Array.Clear(broken_str, 0, broken_str.Length);

                    }
                    if (guitar_dict.Keys.Contains(int.Parse(id.Text)))
                    {
                        int x;
                        bool guitars = int.TryParse(id.Text, out x);
                        if (guitars == true) 
                        { guitar_dict.Remove(int.Parse(id.Text)); }

                        // write to the file
                        using (StreamWriter sw = new StreamWriter(path))
                        {
                            sw.WriteLine("ID|NAME|TYPE|MATERIALS|PRICE|COLOR|FRETS|STRINGS");
                            foreach (var item in guitar_dict)
                            {
                                sw.WriteLine(item.Value.get_info());
                                sw.WriteLine("----");
                            }
                            MessageBox.Show("GUITAR DELETED");
                        }
                    }
                    else
                    {
                        MessageBox.Show("enter a valid ID");
                        id.Clear();
                    }

                }
            }
            catch (FileNotFoundException)
            {
                MessageBox.Show("File not found:+" + path + "\n CREATING FILE...");
                var text = "ID|NAME|TYPE|MATERIALS|PRICE|COLOR|FRETS|STRINGS \n";
                File.WriteAllText("GUITAR.TXT", text); }
            catch (IOException) { MessageBox.Show("I/O ERROR \nREDELETE THE ITEM"); }
           


        }
        public bool ID_EXIST(string A)
        {
            IList<string> list_id = new List<string>();
            using(StreamReader sr=new StreamReader("GUITAR.txt"))
            {
                string[] broken_id = { };
                string line = sr.ReadLine();
                while (line != null)
                {
                    broken_id = line.Split('/');
                    list_id.Add(broken_id[0]);
                    line = sr.ReadLine();
                }
                if (list_id.Contains(A))  return true; 
                else return false;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}