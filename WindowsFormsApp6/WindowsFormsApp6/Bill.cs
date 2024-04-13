using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;

namespace WindowsFormsApp6
{
    public partial class Bill : Form
    {
        string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=KaraokeClientDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;";
        string ConnectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=KaraokeFoodDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;";
        private PrintDocument printDocument = new PrintDocument();
        private PrintPreviewDialog printPreviewDialog = new PrintPreviewDialog();
        public Bill()
        {
            InitializeComponent();
            printDocument.PrintPage += PrintDocument_PrintPage;
        }

        private void label15_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            int id;
            if (!int.TryParse(txtID.Text, out id))
            {
                MessageBox.Show("Please enter a valid ID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT Name, Phone, TypeRoom, CodeRoom, Time, TimeUse FROM Karaoke WHERE ID = @ID";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ID", id);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        txtNameSearch.Text = reader["Name"].ToString();
                        txtPhoneSearch.Text = reader["Phone"].ToString();
                        txtLoaiPhong.Text = reader["TypeRoom"].ToString();
                        txtMaPhong.Text = reader["CodeRoom"].ToString();
                        time.Text = reader["Time"].ToString();
                    }
                    else
                    {
                        MessageBox.Show("No record found with the specified ID.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            Main form = new Main();
            form.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string roomcode = txtcoderoom.Text.Trim();


            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                string query = "SELECT RoomCode, SelectedDishes, TotalAmount, Id FROM [Order] WHERE RoomCode = @RoomCode";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@RoomCode", roomcode);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        txtTotal.Text = reader["TotalAmount"].ToString();
                        txtMaHoaDon.Text = reader["Id"].ToString();
                        string productName = reader["SelectedDishes"].ToString();
                        string[] productParts = productName.Split(',');

                        foreach (string part in productParts)
                        {
                            txtItems.Items.Add(part.Trim()); // Add each part to a new line in the ListBox
                        }
                    }
                    else
                    {
                        MessageBox.Show("No record found with the specified roomcode.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private decimal totalBill; // Declare TotalBill as a decimal variable
        private void button2_Click(object sender, EventArgs e)
        {
            decimal tonghatValue;
            if (!decimal.TryParse(tonghat.Text, out tonghatValue))
            {
                MessageBox.Show("Please enter a valid number in tonghat.");
                return;
            }

            decimal txtTotalValue;
            if (!decimal.TryParse(txtTotal.Text, out txtTotalValue))
            {
                MessageBox.Show("Please enter a valid number in txtTotal.");
                return;
            }
            decimal discountValue;
            if (!decimal.TryParse(discount.Text, out discountValue))
            {
                MessageBox.Show("Please enter a valid number in discount.");
                return;
            }

            // Calculate the total bill by adding tonghatValue and txtTotalValue
            totalBill = (tonghatValue + txtTotalValue) * (100 - discountValue)/100 ;

            // Display or use the total bill as needed
            labelTotalBill.Text = totalBill.ToString("0.000");


        }
        private void Thoigianhat_TextChanged(object sender, EventArgs e)
        {
            int num = 0;

            if (Int32.TryParse(Thoigianhat.Text, out num))
                tonghat.Text = (70.000 * num).ToString();
            else
                tonghat.Text = "Vui lòng nhập số!";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            printPreviewDialog.Document = printDocument;
            printPreviewDialog.ShowDialog();
        }
        private void PrintDocument_BeginPrint(object sender, PrintEventArgs e)
        {
            // Reset the page counter for the print document
            printDocument.PrinterSettings.PrintRange = PrintRange.AllPages;
        }
        private void PrintDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            // Define the content to print
            string billContent = $"Bill:\n\n{labelTotalBill.Text}\n\nItems:\n";

            foreach (string item in txtItems.Items)
            {
                billContent += item + "\n";
            }

            // Define font and brush
            Font printFont = new Font("Arial", 12);
            SolidBrush brush = new SolidBrush(Color.Black);

            // Draw the content on the page
            e.Graphics.DrawString(billContent, printFont, brush, new PointF(100, 100));

            SaveBillToDatabase(billContent);
        }
        // SAVE BILL TO DB
        private void SaveBillToDatabase(string billContent)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();

                // Insert command to save the bill into the database
                string insertQuery = "INSERT INTO Bills (Date, TotalAmount, Items) VALUES (@Date, @TotalAmount, @Items)";
                SqlCommand command = new SqlCommand(insertQuery, connection);
                command.Parameters.AddWithValue("@Date", DateTime.Now);
                command.Parameters.AddWithValue("@TotalAmount", labelTotalBill.Text);
                command.Parameters.AddWithValue("@Items", billContent);
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving bill to database: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }
    }
}
