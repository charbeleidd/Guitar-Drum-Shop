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
using System.Security.Cryptography;

namespace GUITAR_SHOP
{
    public partial class LOGIN : Form
    {
        static List<USERS> list_users = new List<USERS> { };
        string PATH = "USERS.txt";

        public LOGIN()
        {
            InitializeComponent();

        }

        private void Form1_Load(object sender, EventArgs e)
        {
           // btn_register.Enabled = false;
            button1.Enabled = false;
            button2.Enabled = false;
        }
        
        
        private void btn_login_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(usernme.Text) || string.IsNullOrEmpty(pass.Text))
                MessageBox.Show("please enter all fields");
           else login();
           
        }

        private void btn_register_Click(object sender, EventArgs e)
        { 
            if (string.IsNullOrEmpty(usernme.Text) || string.IsNullOrEmpty(pass.Text))
                MessageBox.Show("please enter all fields");
            else
            {
                registering(usernme.Text, pass.Text);
            }
        }
        public void registering(string a,string b)
        {
             try
             {
               
                if (File.Exists(PATH) == false)
                {
                    MessageBox.Show("File not found:+" + PATH + "\n CREATING FILE...");
                    var text = "USERNAME,PASSWORD\nNA/NA\n";
                    File.WriteAllText(PATH, text);
                }
                else
                {

                    list_users.Add(new USERS(a, b));
                    StringBuilder sb = new StringBuilder();
                    string line = "";

                    foreach (USERS c in list_users)
                    {
                       
                        line = c.LOGIN_USERNAME + "," + ComputeSha256Hash(c.LOGIN_PASS) + "\n" + "---" + DateTime.Now + "---" + "\n";
                        sb.Append(line).ToString();
                    }
                    File.AppendAllText(PATH, line);
                    MessageBox.Show("registration completed at" + DateTime.Now, "great!");

                }
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
                        var text = "USERNAME/PASSWORD\nNA/NA\n";
                        File.WriteAllText(PATH, text);
                    }
                    else if (f == DialogResult.No)
                    {
                        MessageBox.Show("PLEASE REVIEW FILE SYNTAX!\n(USERNAME,PASSWORD)", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else if (E == DialogResult.No)
                { MessageBox.Show("PLEASE REVIEW FILE SYNTAX!\n(USERNAME,PASSWORD)", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
            }
        }
        public void login()
        {
           
            try
            { 
                USERS new_user = new USERS(usernme.Text, pass.Text);

                    using (StreamReader sr = new StreamReader(PATH))
                    {
                        string line = usernme.Text + "," + ComputeSha256Hash(pass.Text);
                        bool existss = false;
                        string line1 = sr.ReadLine();
                        line1 = sr.ReadLine();
                        line1 = sr.ReadLine();
                        while (line1 != null)
                        {

                            if (line1 == line)
                            {
                                existss = true;
                                MessageBox.Show("WELCOME :" + new_user.LOGIN_USERNAME);
                                btn_register.Enabled = true;
                                button1.Enabled = true;
                                button2.Enabled = true;
                                usernme.Clear();
                                pass.Clear();

                            }
                            line1 = sr.ReadLine();
                        }
                        if (existss == false) { MessageBox.Show("user not found!"); };

                    }
                
            }
                catch (FileNotFoundException)
                {
                MessageBox.Show("File not found:+"+PATH+"\n CREATING FILE...");
                var text = "USERNAME,PASSWORD\nNA/NA\n";
                File.WriteAllText(PATH, text);
            }
                catch (IOException){ MessageBox.Show("I/O ERROR"); }
            catch (Exception)
            {
                var E = MessageBox.Show("UNKNOWN ERROR! ERASE THE FILE AND REPLACE IT?", "ERROR", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Error);
                if (E == DialogResult.Yes) 
                { var f=MessageBox.Show("ARE YOU SURE?","WARNING",MessageBoxButtons.YesNo,MessageBoxIcon.Warning);
                    if (f == DialogResult.Yes)
                    {
                        var text = "USERNAME/PASSWORD\nNA/NA\n";
                        File.WriteAllText(PATH, text); }
                    else if(f == DialogResult.No)
                    { MessageBox.Show("PLEASE REVIEW FILE SYNTAX!\n(USERNAME,PASSWORD)","WARNING", MessageBoxButtons.OK,MessageBoxIcon.Warning); 
                    }
                }
                else if (E == DialogResult.No)
                { MessageBox.Show("PLEASE REVIEW FILE SYNTAX!\n(USERNAME,PASSWORD)", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form ADD = new HOME();
             ADD.Show();
     
        }
        public string ComputeSha256Hash(string Data)
        {
            // Create a SHA256   
            using (SHA256 sha256Hash = SHA256.Create())
            {
                 
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(Data));

                // Convert byte array to a string 
                //X2 is for using hexadecimal
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
        public void delete_user(USERS a)
        {

            try
            {
               
                    List<string> string_lines = new List<string>();

                    string line2 = a.LOGIN_USERNAME + "," + ComputeSha256Hash(a.LOGIN_PASS);

                    
                    using (StreamReader sr = new StreamReader(PATH))
                    {
                        string line1 = sr.ReadLine();
                        while (line1 != null)
                        {

                            string_lines.Add(line1);

                            line1 = sr.ReadLine();

                        }
                    }
                    bool count = false;
                    //removing the date line
                    for (int i = 0; i < string_lines.Count(); i++)
                    {
                        if (i == string_lines.IndexOf(line2) + 1)
                            string_lines.Remove((string_lines.ElementAt(i)));

                    }
                    //removing user
                    if (string_lines.Any(e => e.StartsWith(a.LOGIN_USERNAME) &&
                            string_lines.Any(es => es.EndsWith(ComputeSha256Hash(a.LOGIN_PASS)))))

                    {
                        string_lines.Remove(line2); count = true;

                        MessageBox.Show("USER DELETED!");
                    }


                    if (count == false) MessageBox.Show("user not found!");
                    if (count == true)
                    {
                        StringBuilder sb = new StringBuilder();
                        string new_line = "";
                        foreach (string c in string_lines)
                        {
                            new_line = sb.Append(c).ToString();
                            new_line = sb.Append("\n").ToString();
                        }
                        File.WriteAllText(PATH, new_line);
                    }
                
            }
            catch (FileNotFoundException)
            {
                MessageBox.Show("File not found:+" + PATH + "\n CREATING FILE...");
                var text = "USERNAME,PASSWORD\nNA/NA\n";
                File.WriteAllText(PATH, text);
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
                        var text = "USERNAME/PASSWORD\nNA/NA\n";
                        File.WriteAllText(PATH, text);
                    }
                    else if (f == DialogResult.No)
                    {
                        MessageBox.Show("PLEASE REVIEW FILE SYNTAX!\n(USERNAME,PASSWORD)", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else if (E == DialogResult.No)
                { MessageBox.Show("PLEASE REVIEW FILE SYNTAX!\n(USERNAME,PASSWORD)", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            USERS old_user = new USERS(usernme.Text, pass.Text);
            delete_user(old_user);
        }
    }
   
}

