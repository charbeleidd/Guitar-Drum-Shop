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
    public partial class BUY_GUITAR : Form
    {
        string path = "clients.json";
        string path1 = "GUITAR.txt";
        string path2 = "BUY_GUITAR.txt";
        //string path2 = "buy.txt";
        IDictionary<string, CLIENT> client_dict = new Dictionary<string, CLIENT>();
        IDictionary<int, GUITAR> GUITAR_dict = new Dictionary<int, GUITAR>();
        //dont forget to sort the dictionaries
        List<string> list_instruments = new List<string>();
       

        Comparison<int> c = delegate (int t1, int t2)
        {

            if (t1 > t2) return 1;
            else if (t1 < t2) return -1;
            else return 0;

        };

        public BUY_GUITAR()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
            List<CLIENT> list_client = new List<CLIENT>();
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
            
            }
            catch (IOException)
            { MessageBox.Show("I/O ERROR"); }
            catch (Exception) { MessageBox.Show("UNKNOWN ERROR"); }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            GUITAR__list_to_GUITAR_dict();
        }
        public void GUITAR__list_to_GUITAR_dict()
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
                //converting to a dict
                foreach (var item in list_instruments)
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
                    GUITAR_dict.Add(int.Parse(broken_str[0]), a);
                    Array.Clear(broken_str, 0, broken_str.Length);

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


                foreach (var item in GUITAR_dict)
                {
                    DataRow row = table.NewRow();
                    row["ID"] = item.Key;
                    row["NAME"] = item.Value.name;
                    row["TYPE"] = item.Value.type;
                    row["MATERIALS"] = item.Value.materials;
                    row["PRICE"] = item.Value.price;
                    row["COLOR"] = item.Value.color;
                    row["FRETS"] = item.Value.nbr_of_frets;
                    row["STRINGS"] = item.Value.nbr_of_strings;

                    table.Rows.Add(row);
                }
                dataGridView2.DataSource = table;
            }
            catch (FileNotFoundException) { MessageBox.Show("FILE NOT FOUND");
                var text = "ID|NAME|TYPE|MATERIALS|PRICE|COLOR|FRETS|STRINGS \n"; File.WriteAllText(path1, text);
            }
            
            catch (IOException) { MessageBox.Show("I|O ERROR"); }
            catch (Exception) { MessageBox.Show("ERROR LOADING GUITARS"); }



        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            IList<string> list_buy = new List<string>();
            List<int> client_id = new List<int>();
            List<string> final_list = new List<string>();
            try
            {
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
                string selected_GUITAR = dataGridView2.CurrentRow.Cells[0].Value.ToString();
                int GUITAR_COST = Convert.ToInt32(dataGridView2.CurrentRow.Cells[4].Value);

                string line = selected_client + "/" + selected_GUITAR;


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
                //grouping duplications
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
                        if (y == true) { if (item == int.Parse(broken2[0])) { final_list.Add(item1); } }
                        if (y == false) MessageBox.Show("can't add item to the list");
                        Array.Clear(broken2, 0, broken2.Length);
                    }
                }

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
                        item.Value.money = client_money - GUITAR_COST;
                    }
                }
                var jsonString = JsonSerializer.Serialize(client_dict);
                File.WriteAllText(path, jsonString);
                MessageBox.Show(" CLIENT UPDATED!");
                button4.Enabled = false;
            }
            catch (FileNotFoundException) {
                MessageBox.Show("CREATING BUY GUITAR FILE"); var text= ("client ID|GUITAR ID");File.WriteAllText(path2, text); }
            catch (Exception) { MessageBox.Show("ERROR"); }
        }

        private void BUY_GUITAR_Load(object sender, EventArgs e)
        {
            client_moeny.Enabled = false;
            guitar_price.Enabled = false;
            textBox3.Enabled = false;
            button4.Enabled = false;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {


                bool exist = true;

                if (dataGridView1.CurrentRow == null || dataGridView2.CurrentRow == null)
                { MessageBox.Show("SELECT CLIENT/DRUM"); exist = false; }
                if (exist == true)
                {
                    var selected_client = dataGridView1.CurrentRow.Cells[4].Value.ToString();
                    var selected_DRUM = dataGridView2.CurrentRow.Cells[4].Value.ToString();
                    button6.Enabled = true;
                    int x = int.Parse(selected_client);
                    int y = int.Parse(selected_DRUM);

        
                    bool check = (x > y);
                    if (check == true)
                    {
                        client_moeny.Text = selected_client;
                        guitar_price.Text = selected_DRUM;
                        MessageBox.Show("you have sufficient funds");
                        textBox3.Text = "DEAL"; button4.Enabled = true;
                    }
                    else
                    {
                        client_moeny.Text = selected_client;
                        guitar_price.Text = selected_DRUM;
                        MessageBox.Show("you DON'T have sufficient funds");
                        textBox3.Text = "NO DEAL"; button4.Enabled = false;
                    }

                }
            }
            catch (Exception) { MessageBox.Show("select rows"); }
        }
    }
}
