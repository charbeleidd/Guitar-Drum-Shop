using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.IO;

namespace GUITAR_SHOP
{
    public partial class BUY_DRUM : Form
    {
        string path = "clients.json";
        string path1 = "DRUM.txt";
        string path2 = "buy_DRUM.txt";
     
        IDictionary<string, CLIENT> client_dict = new Dictionary<string, CLIENT>();
        IDictionary<int, DRUM> instrument_dict = new Dictionary<int, DRUM>();
     
        List<string> list_instruments = new List<string>();
       



        Comparison<int> c = delegate (int t1, int t2)
        {

            if (t1 > t2) return 1;
            else if (t1 < t2) return -1;
            else return 0;

        };


        public BUY_DRUM()
        {
            InitializeComponent();
        }



        private void BUY_RENT_Load(object sender, EventArgs e)
        {
            client.Enabled = false;
            drum.Enabled = false;
            textBox3.Enabled = false;
            button4.Enabled = false;
           
           


        }

        private void button2_Click(object sender, EventArgs e)
        {
            instruments__list_to_instrument_dict();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            List<CLIENT> list_client = new List<CLIENT>();
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
                try
                {
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
                catch (IOException)
                { MessageBox.Show("I/O ERROR"); }
                catch (Exception) { MessageBox.Show("UNKNOWN ERROR"); }
            }
        }
        public void instruments__list_to_instrument_dict()
        {
            try
            {
                using (StreamReader sr = new StreamReader(path1))
                {
                    string line = sr.ReadLine();
                    line = sr.ReadLine();
                    while (line != null)
                    {

                        list_instruments.Add(line);
                        line = sr.ReadLine();
                        line = sr.ReadLine();
                    }
                }

                foreach (var item in list_instruments)
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
                    a.size = broken_str[5];
                    a.edge = broken_str[5];
                    instrument_dict.Add(int.Parse(broken_str[0]), a);
                    Array.Clear(broken_str, 0, broken_str.Length);

                }
                DataTable table = new DataTable();
                table.Columns.Add("ID");
                table.Columns.Add("NAME");
                table.Columns.Add("TYPE");
                table.Columns.Add("MATERIALS");
                table.Columns.Add("PRICE");
                table.Columns.Add("SHELL THICKNESS");
                table.Columns.Add("PIECES");
                table.Columns.Add("SIZE");
                table.Columns.Add("EDGE");

                foreach (var item in instrument_dict)
                {
                    DataRow row = table.NewRow();
                    row["ID"] = item.Key;
                    row["NAME"] = item.Value.name;
                    row["TYPE"] = item.Value.type;
                    row["MATERIALS"] = item.Value.materials;
                    row["PRICE"] = item.Value.price;
                    row["SHELL THICKNESS"] = item.Value.shell_thickness;
                    row["PIECES"] = item.Value.nbr_of_pieces;
                    row["SIZE"] = item.Value.size;
                    row["EDGE"] = item.Value.edge;
                    table.Rows.Add(row);
                }
                dataGridView2.DataSource = table;
            }
            catch (FileNotFoundException)
            {
                MessageBox.Show("FILE NOT FOUND");
                var text = "DRUM ID|NAME|TYPE|MATERIAL|PRICE|SHELL|PIECES|SIZE|EDGE\n"; File.WriteAllText(path1, text);
            }

            catch (IOException) { MessageBox.Show("I|O ERROR"); }
            catch (Exception) { MessageBox.Show("ERROR LOADING DRUMS"); }



        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {


                bool exist = true;
               
                    if (dataGridView1.CurrentRow == null || dataGridView2.CurrentRow == null)
                    { MessageBox.Show("SELECT CLIENT/DRUM");  exist = false; }
                if (exist == true)
                { var selected_client = dataGridView1.CurrentRow.Cells[4].Value.ToString();
                    var selected_DRUM = dataGridView2.CurrentRow.Cells[4].Value.ToString();
                    button6.Enabled = true;
                    int x = int.Parse(selected_client);
                    int y = int.Parse(selected_DRUM);

                   
                    bool check = (x > y);
                    if (check == true)
                    {
                        client.Text = selected_client;
                        drum.Text = selected_DRUM;
                        MessageBox.Show("you have sufficient funds");
                        textBox3.Text = "DEAL"; button4.Enabled = true;
                    }
                    else
                    {
                        client.Text = selected_client;
                        drum.Text = selected_DRUM;
                        MessageBox.Show("you DON'T have sufficient funds");
                        textBox3.Text = "NO DEAL"; button4.Enabled = false;
                    }

                }
            }
            catch (Exception) { MessageBox.Show("select rows"); }
        }



        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                IList<string> list_buy = new List<string>();
                List<int> client_id = new List<int>();
                List<string> final_list = new List<string>();

                using (StreamReader sr = new StreamReader(path2))
                {
                    string line1 = sr.ReadLine();
                    line1 = sr.ReadLine();
                    while (line1 != null)
                    {
                        list_buy.Add(line1);
                        line1 = sr.ReadLine();
                    }
                }


                string selected_client = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                int client_money = Convert.ToInt32(dataGridView1.CurrentRow.Cells[4].Value);
                string selected_drum = dataGridView2.CurrentRow.Cells[0].Value.ToString();
                int drum_money = Convert.ToInt32(dataGridView2.CurrentRow.Cells[4].Value);

                string line = selected_client + "/" + selected_drum;


                list_buy.Add(line);

                List<string> nbr = new List<string>(list_buy);

                foreach (var line12 in nbr)
                {
                    string[] broken1 = line12.Split('/');
                    int y;
                    bool x = int.TryParse(broken1[0], out y);
                    if (x == true) client_id.Add(int.Parse(broken1[0]));

                    Array.Clear(broken1, 0, broken1.Length);
                }
              
                client_id.Sort(c);
                //grouping duplication
                var number = from x in client_id
                             group x by x into g
                             select g.Key;
             

                foreach (var item in number)
                {
                    foreach (var item1 in nbr)
                    {
                        string[] broken2 = item1.Split('/');
                        int x;
                        bool y = int.TryParse(broken2[0], out x);
                        if (y == true) { if (item == int.Parse(broken2[0])) 
                                
                            //adding to the final list
                            { final_list.Add(item1); } }
                        if (y == false) MessageBox.Show("can't add item to the list");
                        Array.Clear(broken2, 0, broken2.Length);
                    }
                }
                //writing to the file
                using (StreamWriter sw = new StreamWriter(path2))
                {
                    sw.WriteLine("CLIENT ID|DRUM ID");
                    foreach (var buy in final_list) sw.WriteLine(buy);
                }

                //updating client's money
                IDictionary<string, CLIENT> clients = new Dictionary<string, CLIENT>();

                var jsonStringg = File.ReadAllText(path);

                client_dict = JsonSerializer.Deserialize<IDictionary<string, CLIENT>>(jsonStringg);
                foreach (var item in client_dict)
                {
                    if ("ID:" + selected_client == item.Key)
                    {
                        item.Value.money = client_money - drum_money;
                    }
                }
                var jsonString = JsonSerializer.Serialize(client_dict);
                File.WriteAllText(path, jsonString);
                MessageBox.Show(" CLIENT UPDATED!");
                button4.Enabled = false;
            }
            catch (FileNotFoundException) { var text = ("client ID|DRUM ID"); File.WriteAllText(path2, text); }
            catch (Exception) { MessageBox.Show("ERROR"); }

        }

        private void button7_Click(object sender, EventArgs e)
        { 
            try
            {
                    var selected_client = dataGridView1.CurrentRow.Cells[4].Value.ToString();
                   var nut= "FALSE";
               //   selected_client ?? nut="TRUE";
                if (nut == "TRUE") { MessageBox.Show("select a value"); }
                else
                {
                    var selected_drum = dataGridView2.CurrentRow.Cells[4].Value.ToString();


                    client.Text = selected_client;
                    drum.Text = selected_drum;
                    button6.Enabled = true;
                }

            }
            catch (NullReferenceException) { MessageBox.Show("SELECT CLIENT/DRUM"); }

        }

        private void button8_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}

