using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient; //connect with SQL server
using System.Drawing;
using System.Linq;           
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IncomeManagement
{
    public partial class LogIn : Form
    {
        public LogIn()
        {
            InitializeComponent();
            PW.PasswordChar = '*';
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Register obj = new Register(); //open the registration page
            obj.Show();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            Application.Exit(); //closing the application (X button)
        }
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\BLOOD\Documents\IncomeManagement.mdf;Integrated Security=True;Connect Timeout=30");
        public static string User;
        private void button1_Click(object sender, EventArgs e)
        {
            if(UName.Text == "" || PW.Text == "") //check whether the username and password are empty or not
            {
                MessageBox.Show("Enter both username and password");
            }
            else //if it's not empty select the name and password from the UserTbl
            {
                con.Open();
                SqlDataAdapter sda = new SqlDataAdapter("select count(*) from UserTbl where Uname= '" + UName.Text + "' and UPass = '" + PW.Text + "'", con);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                if (dt.Rows[0][0].ToString() == "1") //if it matches with the previous one, login to the system
                {
                    User = UName.Text;
                    Dashboard obj = new Dashboard();
                    obj.Show();
                    this.Hide();
                    con.Close();
                }
                else //if not display this message
                {
                    MessageBox.Show("Wrong username or password!");
                    UName.Text = "";
                    PW.Text = "";
                }
                con.Close();
            }
            
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)//show password check box
        {
            if (checkBox1.Checked)
            {
                PW.PasswordChar = '\0';
               
            }
            else
            {
                PW.PasswordChar = '*';
               
            }
        }
    }
}
