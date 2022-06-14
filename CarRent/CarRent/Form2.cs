using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
namespace CarRent
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\user\OneDrive\Documents\CarRentaldb.mdf;Integrated Security=True;Connect Timeout=30");
        private void Log_in_Click(object sender, EventArgs e)
        {
            string query = "select Count(*) from UserTbl where UserName = '" + Uname.Text + "' and Upass = '" + PassTb.Text + "'";
            con.Open();
            SqlDataAdapter sda = new SqlDataAdapter(query, con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows[0][0].ToString() == "1")
            {
                Mainform mainform = new Mainform();
                mainform.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Wrong User or Password");
            }
            con.Close();
        }

        private void label8_Click_1(object sender, EventArgs e)
        {
            Uname.Clear();
            PassTb.Clear(); 
        }

    
        private void label8_Click(object sender, EventArgs e)
        {
            Uname.Clear();
            PassTb.Clear();
        }
    }
}
