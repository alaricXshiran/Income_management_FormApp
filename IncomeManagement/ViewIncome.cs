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
    public partial class ViewIncome : Form
    {
        public ViewIncome()
        {
            InitializeComponent();
            DisplayIncomes();
        }
        DataTable table = new DataTable(); //yt
        int indexRow;
       
        private void label2_Click(object sender, EventArgs e) //go to dashboard
        {
            Dashboard obj = new Dashboard();
            obj.Show();
            this.Hide();
        }

        private void label4_Click(object sender, EventArgs e) //go to income
        {
            Income obj = new Income();
            obj.Show();
            this.Hide();
        }

        private void label3_Click(object sender, EventArgs e) //go to expenses
        {
            Expenses obj = new Expenses();
            obj.Show();
            this.Hide();
        }

        private void label6_Click(object sender, EventArgs e) //go to view expenses
        {
            ViewExpenses obj = new ViewExpenses();
            obj.Show();
            this.Hide();
        }
        private void DisplayIncomes() //from this method, we display incomes in data grid view
        {
            try
            {
                //extract data from dataset to data grid view
                string query = "select * from IncomeTbl where IncUser= '" + LogIn.User + "'";
                SqlDataAdapter sda = new SqlDataAdapter(query, con);
                SqlCommandBuilder builder = new SqlCommandBuilder(sda);     //updating database table
                var ds = new DataSet();
                sda.Fill(ds);
                IncomeDGV.DataSource = ds.Tables[0];
            }
            catch
            {

            }
           

        }

        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\BLOOD\Documents\IncomeManagement.mdf;Integrated Security=True;Connect Timeout=30");
        private void label19_Click(object sender, EventArgs e) //close the application
        {
            Application.Exit();
        }

        private void IncomeDGV_CellContentClick(object sender, DataGridViewCellEventArgs e) //selecting a row
        {
            indexRow = e.RowIndex;
            DataGridViewRow row = IncomeDGV.Rows[indexRow];

            
        }

        private void button1_Click(object sender, EventArgs e) //go to login
        {
            LogIn obj = new LogIn();
            obj.Show();
            this.Hide();
        }

        private void dltbtn_Click(object sender, EventArgs e) //delete button
        {
            if (textexp.Text == "")
            {
                MessageBox.Show("Enter Valid ID to delete Record");
            }
            else
            {
                try
                {
                    con.Open();
                    string query = "delete from IncomeTbl where ID ='" + textexp.Text + "' AND IncUser = '" + LogIn.User + "'";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Item succussfully deleted!");
                    con.Close();

                }
                catch
                {

                    MessageBox.Show("Enter Valid ID to delete Record");
                }
                finally
                {
                    DisplayIncomes();
                }
            }
            
            
        }

        private void ViewIncome_Load(object sender, EventArgs e)
        {
            DisplayIncomes();
        }

        private void button6_Click(object sender, EventArgs e) //edit button
        {
            if (txtname.Text == "" || txtAmt.Text == "" || txtcat.Text == "" || txtdec.Text == "")
            {
                MessageBox.Show("Fill data to Edit");
            }
            else
            {
                try
                {
                    string query = "update  IncomeTbl set IncName='" + txtname.Text + "',IncAmt='" + txtAmt.Text + "',IncCat='" + txtcat.Text + "',IncDesc='" + txtdec.Text + "' where IncUser='" + LogIn.User + "' and ID ='" + textexp.Text + "'";
                    SqlCommand cmd = new SqlCommand(query, con);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Item successfully updates");
                    DisplayIncomes();
                    
                }
                catch (Exception)
                {
                    MessageBox.Show("something went wrong");
                }
                finally
                {
                    DisplayIncomes();
                    con.Close();
                }
               
            }
         
        }

        private void rfbtn_Click(object sender, EventArgs e)
        {
            
        }

        private void textexp_TextChanged(object sender, EventArgs e)// search and select bar
        {
            if (textexp.Text == "")// if this is empty
            {
                DisplayIncomes();
            }
            else// if not
            {
                try
                {
                    string expenseshowdata = "SELECT * from IncomeTbl where ID = '" + textexp.Text + "' and IncUser ='" + LogIn.User + "'";
                    SqlCommand cmds = new SqlCommand(expenseshowdata, con);//created sqlcommand object for msql variable and sqlconnection object
                    SqlDataAdapter dax = new SqlDataAdapter(cmds);//create dataadapter object from sqlcommand
                    DataTable expensedt = new DataTable();
                    dax.Fill(expensedt);
                    IncomeDGV.DataSource = expensedt;//adding object to datagrid source
                    
                }
                catch
                {
                    DisplayIncomes();
                }
                finally
                {
                    con.Close();
                }
            }
        }
    }
}
