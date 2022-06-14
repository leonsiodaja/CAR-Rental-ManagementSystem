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
    public partial class Return : Form
    {
        public Return()
        {
            InitializeComponent();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\user\OneDrive\Documents\CarRentaldb.mdf;Integrated Security=True;Connect Timeout=30");
        private void populate()
        {
            con.Open();
            string query = "select * from RentalTbl";
            SqlDataAdapter da = new SqlDataAdapter(query, con);
            SqlCommandBuilder builder = new SqlCommandBuilder(da);
            var ds = new DataSet();
            da.Fill(ds);
            RentDGV.DataSource = ds.Tables[0];
            con.Close();

        }

        private void populateRet()
        {
            con.Open();
            string query = "select * from ReturnTbl";
            SqlDataAdapter da = new SqlDataAdapter(query, con);
            SqlCommandBuilder builder = new SqlCommandBuilder(da);
            var ds = new DataSet();
            da.Fill(ds);
            ReturnDGV.DataSource = ds.Tables[0];
            con.Close();

        }

        private void Deleteonreturn()
        {
            int RentId;
            RentId = Convert.ToInt32(RentDGV.SelectedRows[0].Cells[0].Value.ToString());
            con.Open();
            string query = "delete from RentalTbl where RentId=" + rentId + ";";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.ExecuteNonQuery();
          //  MessageBox.Show("Rental Deleted Succesfully");
            con.Close();
            populate();
          
            // UpdateonRentDelete();
        }
        private void Return_Load(object sender, EventArgs e)
        {
            populate();
            populateRet();
            Deleteonreturn();
        }

        private void RentDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            CarIdTb.Text = RentDGV.SelectedRows[0].Cells[1].Value.ToString();
            CustNameTb.Text = RentDGV.SelectedRows[0].Cells[2].Value.ToString();
             DateTimePicker1.Text = RentDGV.SelectedRows[0].Cells[4].Value.ToString();
             DateTime d1 = DateTimePicker1.Value.Date;
             DateTime d2 = DateTime.Now;
             TimeSpan t = d2 - d1;
             int NrOfDays = Convert.ToInt32(t.TotalDays);
             if(NrOfDays<=0)
             {
                DelayTb.Text = "No Delay";
                FineTb.Text = "0";
             }
             else
             {
                DelayTb.Text = "" + NrOfDays;
                FineTb.Text = "" + (NrOfDays * 250); 
             }
         
            
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            Mainform main = new Mainform();
            main.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {


            if (IdTb.Text == "" || CustNameTb.Text == "" || FineTb.Text=="" || DelayTb.Text=="" || DelayTb.Text=="")
            {
                MessageBox.Show("Missing information");
            }
            else
            {
                try
                {
                    con.Open();
                    string query = "insert into REturnTbl values(" + IdTb.Text + ",' " + CarIdTb.Text + "', '" + CustNameTb.Text + "','" + DateTimePicker1.Value.ToString() + "','" +DelayTb.Text + "','" + FineTb.Text + "')";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Car Successfully Returned");
                    con.Close();
                   // UpdateonRent();
                    populateRet();
                    Deleteonreturn();

                }
                catch (Exception Myex)
                {
                    MessageBox.Show(Myex.Message);
                }
            }
        }
    }

}
