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
    public partial class DashBoard : Form
    {
        public DashBoard()
        {
            InitializeComponent();
        }

      
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\user\OneDrive\Documents\CarRentaldb.mdf;Integrated Security=True;Connect Timeout=30");
        private void DashBoard_Load(object sender, EventArgs e)
        {
            string queryCar = "select Count(*) from CarTbl";
            SqlDataAdapter sda = new SqlDataAdapter(queryCar, con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            CarsLbl.Text= dt.Rows[0][0].ToString();


            string queryCust = "select Count(*) from CustomerTbl";
            SqlDataAdapter sda1 = new SqlDataAdapter(queryCust, con);
            DataTable dt1 = new DataTable();
            sda1.Fill(dt1);
            Custlbl.Text = dt1.Rows[0][0].ToString();


            string queryuser = "select Count(*) from UserTbl";
            SqlDataAdapter sda2 = new SqlDataAdapter(queryuser, con);
            DataTable dt2 = new DataTable();
            sda2.Fill(dt2);
            Userlbl.Text = dt2.Rows[0][0].ToString();
        }

      

        private void label2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            Mainform main = new Mainform();
            main.Show();
        }
    }
}
