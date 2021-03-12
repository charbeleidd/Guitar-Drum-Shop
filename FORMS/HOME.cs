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
    public partial class HOME : Form
    {
        public delegate void LoginEventHandler(string loginName, Boolean status);
        public HOME()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        

        private void button1_Click(object sender, EventArgs e)
        {
            Form add = new BUY_DRUM();
            add.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
          
            Form add = new ADD_GUITAR();
            add.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form add = new ADD_DRUM();
            add.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            
            Form add = new BUY_GUITAR();
            add.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Form add = new ADD_CLIENT();
            add.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked == false && radioButton2.Checked == false) MessageBox.Show("SELECT A CATEGORY");
            
            else if (client_id.Text.Length == 0) { MessageBox.Show("enter an ID"); }
            else if (radioButton1.Checked == true) { display_drum(); }
            else if (radioButton2.Checked == true) { display_guitar(); }
           
        }
        public void display_drum()
        {
            string path = "buy_DRUM.txt";
            try
            {
                
                var Id_list = File.ReadLines(path).
                Select(x => new
                {
                    ClientId = x.Split('/').First(),
                    DrumId = x.Split('/').Last()
                }).ToList();

                var input_id = client_id.Text;
                var foundIds = Id_list.Where(x => x.ClientId == input_id);
                if (!foundIds.Any()) { MessageBox.Show("CLIENT NOT FOUND"); }
                else
                {
                    DataTable table = new DataTable();

                    table.Columns.Add("CLIENT ID");
                    table.Columns.Add("DRUM ID");
                    foreach (var item in foundIds)
                    {
                        DataRow row = table.NewRow();
                        row["CLIENT ID"] = item.ClientId;
                        row["DRUM ID"] = item.DrumId;

                        table.Rows.Add(row);
                    }
                    dataGridView1.DataSource = table;
                }
            }
            catch (FileNotFoundException) { MessageBox.Show("FILE NOT FOUND! creating buy drum file");
                var text = ("client ID|DRUM ID"); File.WriteAllText(path, text); }
            catch (Exception) { MessageBox.Show("ERROR"); }

        }
        public void display_guitar()
        {
            string path = "BUY_GUITAR.txt";
            try
            {
                var Id_list = File.ReadLines(path).
                Select(x => new
                {
                    ClientId = x.Split('/').First(),
                    GUITARID = x.Split('/').Last()
                }).ToList();

                var input_id = client_id.Text;
                var foundIds = Id_list.Where(x => x.ClientId == input_id);
                if (!foundIds.Any()) { MessageBox.Show("CLIENT NOT FOUND"); }
                else
                {
                    DataTable table = new DataTable();

                    table.Columns.Add("CLIENT ID");
                    table.Columns.Add("GUITAR ID");
                    foreach (var item in foundIds)
                    {
                        DataRow row = table.NewRow();
                        row["CLIENT ID"] = item.ClientId;
                        row["GUITAR ID"] = item.GUITARID;

                        table.Rows.Add(row);
                    }
                    dataGridView1.DataSource = table;
                }
            }
            catch (FileNotFoundException)
            {
                MessageBox.Show("FILE NOT FOUND!CREATING BUY GUITAR FILE"); var text = ("client ID|GUITAR ID"); File.WriteAllText(path, text);
            }
            catch (Exception) { MessageBox.Show("ERROR"); }

        }
      
        
       
    }
}
