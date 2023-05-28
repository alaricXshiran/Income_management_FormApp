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
    public partial class Income : Form
    {
        public Income()
        {
            InitializeComponent();
            GetToInc();
        }
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\BLOOD\Documents\IncomeManagement.mdf;Integrated Security=True;Connect Timeout=30");
        private void Clear() //clear method
        {
            IncName.Text = "";
            IncAmt.Text = "";
            IncDesc.Text = "";
            IncCat.SelectedIndex = 0;
        }
        private void label19_Click(object sender, EventArgs e) //to close the application
        {
            Application.Exit();
        }

        private void label2_Click(object sender, EventArgs e) //go to the dashboard
        {
            Dashboard obj = new Dashboard();
            obj.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e) //save the incomes
        {
            if (IncName.Text == "" ||IncAmt.Text == "" || IncCat.SelectedIndex == -1 || IncDesc.Text == "") //if these details are missing show the below messege
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try               //from this query we insert data into the IncomeTbl. 
                {
                    con.Open();
                    //SqlCommand is another class in System.Data.SqlClient library file
                    SqlCommand cmd = new SqlCommand("insert into IncomeTbl(IncName,IncAmt,IncCat,IncDate,IncDesc,IncUser)values(@IN,@IA,@IC,@ID,@IDe,@IU)", con);
                    cmd.Parameters.AddWithValue("@IN", IncName.Text);
                    cmd.Parameters.AddWithValue("@IA", IncAmt.Text);
                    cmd.Parameters.AddWithValue("@IC", IncCat.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@ID", IncDate.Value.Date);
                    cmd.Parameters.AddWithValue("@IDe", IncDesc.Text);
                    cmd.Parameters.AddWithValue("@IU", LogIn.User);
                    cmd.ExecuteNonQuery();  //executing the command. 
                    MessageBox.Show("Income added!!");
                    con.Close();
                    GetToInc();
                    Clear();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void label5_Click(object sender, EventArgs e) //go to view income
        {
            ViewIncome obj = new ViewIncome();
            obj.Show();
            this.Hide();
        }

        private void label6_Click(object sender, EventArgs e) //go to view expenses
        {
            ViewExpenses obj = new ViewExpenses();
            obj.Show();
            this.Hide();
        }

        private void label3_Click(object sender, EventArgs e) //go to expenses
        {
            Expenses obj = new Expenses();
            obj.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e) //go to login page
        {
            LogIn obj = new LogIn();
            obj.Show();
            this.Hide();
        }
        private void GetToInc() //getting the total income. 
        {
            try
            {
                con.Open();
                SqlDataAdapter sda = new SqlDataAdapter("select Sum(IncAmt) from IncomeTbl where IncUser= '" + LogIn.User + "'", con);
                DataTable dt = new DataTable();
                sda.Fill(dt);
               // Inc = Convert.ToInt32(dt.Rows[0][0].ToString());
                totincome.Text = "Rs " + dt.Rows[0][0].ToString();
                con.Close();
            }
            catch (Exception ex)
            {
                con.Close();
            }

        }
    }
}
