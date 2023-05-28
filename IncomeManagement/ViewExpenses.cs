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
    public partial class ViewExpenses : Form
    {
        public ViewExpenses()
        {
            InitializeComponent();
            DisplayExpenses();
        }
        private void ViewExpenses_Load(object sender, EventArgs e)
        {
            DisplayExpenses();
        }
        DataTable table = new DataTable();
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\BLOOD\Documents\IncomeManagement.mdf;Integrated Security=True;Connect Timeout=30");

        private void DisplayExpenses() //to display the expenses details
        {
            try
            {
                string query = "select * from ExpensesTbl where ExpUser= '" + LogIn.User + "'";
                SqlDataAdapter sda = new SqlDataAdapter(query, con);
                SqlCommandBuilder builder = new SqlCommandBuilder(sda);
                var ds = new DataSet();
                sda.Fill(ds);
                ExpensesDGV.DataSource = ds.Tables[0];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
            

            
        }

        private void dltbtn_Click(object sender, EventArgs e) //delete button
        {
            try
            {
                if (textexp.Text == "")
                {
                    MessageBox.Show("Select the item to be deleted");
                }
                else
                {
                    con.Open();
                    string query = "delete from ExpensesTbl where ExpUser='" + LogIn.User + "' and ID ='" + textexp.Text + "'";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Item succussfully deleted!");
                    con.Close();
                    DisplayExpenses();
                }
            }
            catch
            {
                con.Close();
            }
            finally
            {
                DisplayExpenses();
            }

        }

        

        private void button6_Click(object sender, EventArgs e) //edit button
        {
            // amount should be float         float amountx = float.Parse(txtAmt.Text);
            if(txtname.Text==""|| txtAmt.Text==""|| txtcat.Text==""|| txtdec.Text=="")
            {
                MessageBox.Show("Fill data to Edit");
            }
            else
            {
                try
                {
                    string query = "update  ExpensesTbl set ExpName='" + txtname.Text + "',ExpAmt='" + txtAmt.Text + "',ExpCat='" + txtcat.Text + "',ExpDesc='" + txtdec.Text + "' where ExpUser='" + LogIn.User + "' and ID ='" + textexp.Text + "'";
                    SqlCommand cmd = new SqlCommand(query, con);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Item successfully updates");
                    DisplayExpenses();
                }
                catch (Exception)
                {
                    MessageBox.Show("CHECK AMOUNT!!");
                }
                finally
                {
                    con.Close();
                    
                }
            }
            
        }

        private void textexp_TextChanged(object sender, EventArgs e)//search and select bar
        {
            if (textexp.Text == "")// if this is empty
            {
                DisplayExpenses();
            }
            else// if not
            {
                try
                {
                    string expenseshowdata = "SELECT * from ExpensesTbl where ID = '" + textexp.Text + "' and ExpUser ='" + LogIn.User + "'";
                    SqlCommand cmds = new SqlCommand(expenseshowdata, con);//created sqlcommand object for msql variable and sqlconnection object
                    SqlDataAdapter dax = new SqlDataAdapter(cmds);//create dataadapter object from sqlcommand
                    DataTable expensedt = new DataTable();
                    dax.Fill(expensedt);
                    ExpensesDGV.DataSource = expensedt;//adding object to datagrid source
                    con.Close();
                }
                catch
                {
                    DisplayExpenses();
                }
                finally
                {
                    con.Close();
                }
            }

        }

        private void label19_Click(object sender, EventArgs e) //exit button
        {
            Application.Exit();
        }

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

        private void label5_Click(object sender, EventArgs e) //go to view income
        {
            ViewIncome obj = new ViewIncome();
            obj.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e) //go to login
        {
            LogIn obj = new LogIn();
            obj.Show();
            this.Hide();
        }

       
    }
}
