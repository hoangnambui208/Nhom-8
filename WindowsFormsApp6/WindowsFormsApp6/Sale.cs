using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp6
{
    public partial class Sale : Form
    {
        string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=KaraokeClientDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;";

        public Sale()
        {
            InitializeComponent();
        }

        private void Sale_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'karaokeClientDBDataSet2.Bills' table. You can move, or remove it, as needed.
            this.billsTableAdapter.Fill(this.karaokeClientDBDataSet2.Bills);
            decimal totalRevenue = 0;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT SUM(SaleAmount) AS TotalRevenue FROM Bills"; // Replace Sales with your table name
                SqlCommand command = new SqlCommand(query, connection);

                try
                {
                    connection.Open();
                    object result = command.ExecuteScalar();

                    if (result != DBNull.Value)
                    {
                        totalRevenue = Convert.ToDecimal(result);
                    }
                    else
                    {
                        Console.WriteLine("No sales data found.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("An error occurred: " + ex.Message);
                }
            }

            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            RefreshDataGridView1();
        }
        private void RefreshDataGridView1()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Bills";
                SqlDataAdapter dataAdapter = new SqlDataAdapter(query, connection);
                DataTable dataTable = new DataTable();
                dataAdapter.Fill(dataTable);
                dataGridView1.DataSource = dataTable;
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Main form = new Main();
            form.Show();
            this.Hide();
        }

        //private void button2_Click(object sender, EventArgs e)
        //{
        //    decimal totalRevenue = CalculateTotalRevenue(connectionString);
        //    lblResult.Text = $"Total Revenue: {totalRevenue.ToString("0.000")}";
        //}
        //static decimal CalculateTotalRevenue(string connectionString)
        //{
        //    decimal totalRevenue = 0;

        //    using (SqlConnection connection = new SqlConnection(connectionString))
        //    {
        //        string query = "SELECT SUM(TotalAmount) AS TotalAmount FROM Bills"; // Replace Sales with your table name
        //        SqlCommand command = new SqlCommand(query, connection);

        //        try
        //        {
        //            connection.Open();
        //            object result = command.ExecuteScalar();

        //            if (result != DBNull.Value)
        //            {
        //                totalRevenue = Convert.ToDecimal(result);
        //            }
        //            else
        //            {
        //                MessageBox.Show("No sales data found.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        }
        //    }

        //    return totalRevenue;
        //}



    }
}
