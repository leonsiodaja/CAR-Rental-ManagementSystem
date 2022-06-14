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
    public partial class Rental : Form
    {
        public Rental()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\user\OneDrive\Documents\CarRentaldb.mdf;Integrated Security=True;Connect Timeout=30");
        private void fillcombo()
        {
            con.Open();
            string query = "select  RegNum from CarTbl where Available = '" + "Yes" + "' ";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataReader rdr;
            rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("RegNum", typeof(string));
            dt.Load(rdr);
            CarRegCb.ValueMember = "RegNum";
            CarRegCb.DataSource = dt;
            con.Close();
        }

        private void fillCustomer()
        {
            con.Open();
            string query = "select Custld  from CustomerTbl ";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataReader rdr;
            rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("Custld", typeof(int));
            dt.Load(rdr);
            CustCb.ValueMember = "Custld";
            CustCb.DataSource = dt;

            con.Close();

        }
        private void fetchCustName()  
        {
            con.Open();
            string query = "select * from CustomerTbl where Custld =" + CustCb.SelectedValue.ToString() + "";
            SqlCommand cmd = new SqlCommand(query, con);
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            foreach(DataRow dr in dt.Rows)
            {
                CustNameTb.Text = dr["CustName"].ToString();
            }
            con.Close();
        }
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
        private void UpdateonRent()
        {
            con.Open();
            string query = "Update CarTbl set  Available='" + "No" + "'  Where RegNum='" + CarRegCb.SelectedValue.ToString()+ "';";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.ExecuteNonQuery();
         //   MessageBox.Show("Car Successfully Updated");
            con.Close();
        }
        private void UpdateonRentDelete()
        {
            con.Open();
            string query = "Update CarTbl set  Available='" + "Yes" + "'  Where RegNum='" + CarRegCb.SelectedValue.ToString()+ "';";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.ExecuteNonQuery();
            //   MessageBox.Show("Car Successfully Updated");
            con.Close();
        }
        private void Rental_Load(object sender, EventArgs e)
        {
              fillcombo();
            fillCustomer();
            populate();
        }

        private void CustCb_SelectionChangeCommitted(object sender, EventArgs e)
        {
            fetchCustName();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
                        if (IdTb.Text == "" || CustNameTb.Text == "" || FeesTb.Text == "" )
                        {
                            MessageBox.Show("Missing information");
                        }
                        else
                        {
                            try
                            {
                                con.Open();
                               string query = "insert into RentalTbl values(" + IdTb.Text + ", '" + CarRegCb.Text+ "', '"+CustNameTb.Text+"','"+dateTimePicker1.Value.ToString() +"','"+dateTimePicker2.Value.ToString() +"','"+FeesTb.Text+"')";
                                SqlCommand cmd = new SqlCommand(query, con);
                                cmd.ExecuteNonQuery();
                                MessageBox.Show("Car Successfully Rented");
                                con.Close();
                                UpdateonRent(); 
                                populate();

                            }
                            catch (Exception Myex)
                            {
                                MessageBox.Show(Myex.Message);
                            }
                        }
                    }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            Mainform main = new Mainform();
            main.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (IdTb.Text == "")
            {
                MessageBox.Show("Mising information");
            }
            else
            {
                try
                {
                    con.Open();
                    string query = "delete from RentalTbl where RentId=" + IdTb.Text + ";";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Rental Deleted Succesfully");
                    con.Close();
                    populate();
                    UpdateonRentDelete();
                }
                catch (Exception Myex)
                {
                    MessageBox.Show(Myex.Message);
                }
            }
        }

        private void RentDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            IdTb.Text = RentDGV.SelectedRows[0].Cells[0].Value.ToString();
            CarRegCb.Text = RentDGV.SelectedRows[0].Cells[1].Value.ToString();
         //  CustNameTb.Text = RentDGV.SelectedRows[0].Cells[3].Value.ToString();
            FeesTb.Text = RentDGV.SelectedRows[0].Cells[5].Value.ToString();

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void RentDate_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void ReturnDate_ValueChanged(object sender, EventArgs e)
        {

        }

        private void CarRegTb_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void FeeTb_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }
    }
    }

