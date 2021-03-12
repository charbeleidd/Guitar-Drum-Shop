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
using System.Text.Json;
using System.Text.Json.Serialization;


namespace GUITAR_SHOP
{
    public partial class ADD_CLIENT : Form
    {
        public delegate int clienteventhandler(int id);

        string path = "clients.json";


        IDictionary<string, CLIENT> client_dict = new Dictionary<string, CLIENT>();
       

        public ADD_CLIENT()
        {
            InitializeComponent();
        }

        private void ADD_CLIEINT_Load(object sender, EventArgs e)
        {
            Random rnd = new Random();
            id.Text = rnd.Next(0, 10000).ToString();
           // button3.Enabled = false;
           

        }

        private void button2_Click(object sender, EventArgs e)
        {
            int x;
            if (fname.Text == "" || lname.Text == "" || age.Text == "" || money.Text == "" || id.Text == "")
            { MessageBox.Show("enter all fields"); }

            else if (int.TryParse(age.Text, out x) == false)
            { MessageBox.Show("enter the right format"); age.Clear(); }
            else if (int.TryParse(money.Text, out x) == false)
            { MessageBox.Show("enter the right format"); money.Clear(); }
            else if (int.TryParse(id.Text, out x) == false)
            { MessageBox.Show("enter the right format"); id.Clear(); }
            else if (int.TryParse(fname.Text, out x) == true)
            { MessageBox.Show("enter the right format"); fname.Clear(); }

            else if (int.TryParse(lname.Text, out x) == true)
            { MessageBox.Show("enter the right format"); lname.Clear(); }


            else save_client(path);

        }
        public void save_client(string path)
        {
            try
            {
                
                if (File.Exists(path) == false)
                {
                    MessageBox.Show("FILE IS EMPTY" +
                            " ADDING VALUES");
                    CLIENT B = new CLIENT(0, 0, "NA", "NA", 0);
                    client_dict.Add("NA", B);
                    var jsonString1 = JsonSerializer.Serialize(client_dict);

                    File.WriteAllText(path, jsonString1);

                }
                    else
                {
                   
                    Random rnd = new Random();
                        var jsonStringg = File.ReadAllText(path);

                        client_dict = JsonSerializer.Deserialize<IDictionary<string, CLIENT>>(jsonStringg);


                        CLIENT a = new CLIENT
                            (int.Parse(id.Text), int.Parse(age.Text), fname.Text, lname.Text, int.Parse(money.Text));
                        
                        string y = "ID:" + a.id.ToString();
                       
                        //changing duplicated id
                        foreach (var item in client_dict)
                        {
                            if (a.id == item.Value.id)

                            {
                                a.id = rnd.Next(0, 10000);
                                y = "ID:" + a.id.ToString();
                                MessageBox.Show("your new id is " + a.id);
                            }
                        }
                        client_dict.Add(y, a);


                        var jsonString = JsonSerializer.Serialize(client_dict);
                        File.WriteAllText(path, jsonString);
                        MessageBox.Show("CLIENT saved!");
                        id.Clear();
                    button3.Enabled = true;
                        id.Text = rnd.Next(0, 10000).ToString();
                    }
                
            }
           
            catch (OutOfMemoryException) { MessageBox.Show(" program out of memory"); }
            catch (FileLoadException) { MessageBox.Show("file could not be loaded"); }
            catch (Exception)
            {
                var E = MessageBox.Show("UNKNOWN ERROR! ERASE THE FILE AND REPLACE IT?", "ERROR", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Error);
                if (E == DialogResult.Yes)
                {
                    var f = MessageBox.Show("ARE YOU SURE?", "WARNING", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (f == DialogResult.Yes)
                    {
                        CLIENT B = new CLIENT(0, 0, "NA", "NA", 0);
                        client_dict.Add("NA", B);
                        var jsonString1 = JsonSerializer.Serialize(client_dict);

                        File.WriteAllText(path, jsonString1);
                    }
                    else if (f == DialogResult.No)
                    {
                        MessageBox.Show("PLEASE REVIEW FILE SYNTAX!\n", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else if (E == DialogResult.No)
                { MessageBox.Show("PLEASE REVIEW FILE SYNTAX!\n", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            order_by_id();
        }
        public void order_by_id()
        {
            try
            {
                if (File.Exists(path) == false)
                {
                    MessageBox.Show("FILE IS EMPTY" +
                            " ADDING VALUES");
                    CLIENT B = new CLIENT(0, 0, "NA", "NA", 0);
                    client_dict.Add("NA", B);
                    var jsonString1 = JsonSerializer.Serialize(client_dict);

                    File.WriteAllText(path, jsonString1);

                }
                else
                {
                    var jsonStringg = File.ReadAllText(path);
                    client_dict = JsonSerializer.Deserialize<IDictionary<string, CLIENT>>(jsonStringg);


                    var sortedDict = from entry in client_dict
                                     orderby entry.Value.id ascending
                                     select entry;
                    //sorteddict was displaying key : ../ value:...
                    IDictionary<string, CLIENT> new_dic = new Dictionary<string, CLIENT>();

                    foreach (var d in sortedDict)
                    {
                        new_dic.Add(d.Key, d.Value);
                    }

                    var jsonString = JsonSerializer.Serialize(new_dic);

                    File.WriteAllText(path, jsonString);
                    MessageBox.Show("sorted by id!");
                }
            }
            catch (OutOfMemoryException) { MessageBox.Show(" program out of memory"); }
            catch (FileLoadException) { MessageBox.Show("file could not be loaded"); }
            catch (Exception)
            {
                var E = MessageBox.Show("UNKNOWN ERROR! ERASE THE FILE AND REPLACE IT?", "ERROR", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Error);
                if (E == DialogResult.Yes)
                {
                    var f = MessageBox.Show("ARE YOU SURE?", "WARNING", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (f == DialogResult.Yes)
                    {
                        CLIENT B = new CLIENT(0, 0, "NA", "NA", 0);
                        client_dict.Add("NA", B);
                        var jsonString1 = JsonSerializer.Serialize(client_dict);

                        File.WriteAllText(path, jsonString1);
                    }
                    else if (f == DialogResult.No)
                    {
                        MessageBox.Show("PLEASE REVIEW FILE SYNTAX!\n", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else if (E == DialogResult.No)
                { MessageBox.Show("PLEASE REVIEW FILE SYNTAX!\n", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                

                List<CLIENT> list_client = new List<CLIENT>();


                var jsonStringg = File.ReadAllText(path);
                client_dict = JsonSerializer.Deserialize<IDictionary<string, CLIENT>>(jsonStringg);


                foreach (KeyValuePair<string, CLIENT> client in client_dict)
                {
                    list_client.Add(client.Value);

                }
                DataTable table = new DataTable();

                table.Columns.Add("ID");
                table.Columns.Add("F_NAME");
                table.Columns.Add("L_NAME");
                table.Columns.Add("AGE");
                table.Columns.Add("MONEY");


                foreach (CLIENT item in list_client)
                {
                    DataRow row = table.NewRow();
                    row["ID"] = item.id;
                    row["F_NAME"] = item.f_name;
                    row["L_NAME"] = item.l_name;
                    row["AGE"] = item.age;
                    row["MONEY"] = item.money;

                    table.Rows.Add(row);
                }
                dataGridView1.DataSource = table;
            }
            catch (OutOfMemoryException) { MessageBox.Show(" program out of memory"); }
            catch (FileLoadException) { MessageBox.Show("file could not be loaded"); }
            catch (FileNotFoundException)
            {
                IDictionary<string, CLIENT> client_dictt = new Dictionary<string, CLIENT>();
                MessageBox.Show("FILE IS EMPTY" +
                                    " ADDING VALUES");
                CLIENT B = new CLIENT(0, 0, "NA", "NA", 0);
                client_dictt.Add("NA", B);
                var jsonString1 = JsonSerializer.Serialize(client_dictt);

                File.WriteAllText(path, jsonString1);
            }

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            try
            {
                var jsonStringg = File.ReadAllText(path);
                client_dict = JsonSerializer.Deserialize<IDictionary<string, CLIENT>>(jsonStringg);

                bool exist = false;
                foreach (var item in client_dict)
                {
                    if (id.Text == item.Value.id.ToString()) { exist = true; }
                }
                if (exist == true) delete_client(path);
                else { MessageBox.Show("ID NOT FOUND!");id.Clear(); }
            }
            catch (OutOfMemoryException) { MessageBox.Show(" program out of memory"); }
            catch (FileLoadException) { MessageBox.Show("file could not be loaded"); }
            catch (Exception)
            {
                var E = MessageBox.Show("UNKNOWN ERROR! ERASE THE FILE AND REPLACE IT?", "ERROR", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Error);
                if (E == DialogResult.Yes)
                {
                    var f = MessageBox.Show("ARE YOU SURE?", "WARNING", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (f == DialogResult.Yes)
                    {
                        CLIENT B = new CLIENT(0, 0, "NA", "NA", 0);
                        client_dict.Add("NA", B);
                        var jsonString1 = JsonSerializer.Serialize(client_dict);

                        File.WriteAllText(path, jsonString1);
                    }
                    else if (f == DialogResult.No)
                    {
                        MessageBox.Show("PLEASE REVIEW FILE SYNTAX!\n", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else if (E == DialogResult.No)
                { MessageBox.Show("PLEASE REVIEW FILE SYNTAX!\n", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
            }
        }
        public void delete_client(string a)
        {
           
                var jsonStringg = File.ReadAllText(a);
                client_dict = JsonSerializer.Deserialize<IDictionary<string, CLIENT>>(jsonStringg);
                int x;
                //checking if the client exists

                if (int.TryParse(id.Text, out x) == true)
                {
                    bool found = false;

                    foreach (KeyValuePair<string, CLIENT> kvPair in client_dict)
                    {
                        if (int.Parse(id.Text) == kvPair.Value.id)
                    { client_dict.Remove(kvPair); found = true; }
                        if (found == true) { MessageBox.Show("item found"); break; }

                    }
                }
              
                var new_e = JsonSerializer.Serialize(client_dict);
                File.WriteAllText(a, new_e);
                MessageBox.Show("CLIENT DELETED!");
        }
    }
}
