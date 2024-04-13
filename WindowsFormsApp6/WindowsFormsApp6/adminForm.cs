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
//sing static System.Windows.Forms.VisualStyles.VisualStyleElement;
//using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;

namespace WindowsFormsApp6
{
    public partial class adminForm : Form
    {
        string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=KaraokeClientDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;";
        private List<Button> SelectedButtons = new List<Button>();

        public adminForm()
        {
            InitializeComponent();
            
        }
        //private void Button_Click(object sender, EventArgs e)
        //{
        //    Button clickedButton = sender as Button;
        //    if (clickedButton != null)
        //    {
        //        // Change color to blue when a selected button is clicked
        //        clickedButton.BackColor = Color.Blue;
        //        // Add the clicked button to the list of selected buttons
        //        SelectedButtons.Add(clickedButton);
        //    }
        //}


        //private void addButton_Click(object sender, EventArgs e)
        //{
           
        //}

        //private void removeButton_Click(object sender, EventArgs e)
        //{
        //    foreach (Button button in selectedButtons)
        //    {
        //        button.BackColor = Color.White;
        //    }
        //    // Clear the list of selected buttons
        //    selectedButtons.Clear();
        //}

        private void adminForm_Load(object sender, EventArgs e)
        {
            txtType.Items.Add("Normal");
            txtType.Items.Add("Premium");
            txtType.Items.Add("Luxury");
        }
        private void txtType_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtCode.Items.Clear();
            if (txtType.SelectedItem == "Normal")
            {
                txtCode.Items.Add("N-1");
                txtCode.Items.Add("N-2");
                txtCode.Items.Add("N-3");
                txtCode.Items.Add("N-4");
                txtCode.Items.Add("N-5");
                txtCode.Items.Add("N-6");
            }
            else if (txtType.SelectedItem == "Premium")
            {
                txtCode.Items.Add("P-1");
                txtCode.Items.Add("P-2");
                txtCode.Items.Add("P-3");
                txtCode.Items.Add("P-4");
            }
            else if (txtType.SelectedItem == "Luxury")
            {
                txtCode.Items.Add("L-1");
                txtCode.Items.Add("L-2");
                txtCode.Items.Add("L-3");
            }

        }
        private void button4_Click(object sender, EventArgs e)
        {
            string Name = txtName.Text;
            string Phone = txtPhone.Text;
            string Type = txtType.Text;
            string Code = txtCode.Text;
            string Time = txtTime.Text;
            string TimeUse = txtTimeUse.Text;

            if (string.IsNullOrEmpty(Name) || string.IsNullOrEmpty(Phone) || string.IsNullOrEmpty(Type) || string.IsNullOrEmpty(Code) || string.IsNullOrEmpty(Time) || string.IsNullOrEmpty(TimeUse))

            {
                {
                    MessageBox.Show("Please fill in all the fields.");
                    return;
                }
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "INSERT INTO Karaoke (Name, Phone, TypeRoom, CodeRoom, Time, TimeUse) VALUES (@Name, @Phone, @TypeRoom, @CodeRoom, @Time, @TimeUse)";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Name", Name);
                    command.Parameters.AddWithValue("@Phone", Phone);
                    command.Parameters.AddWithValue("@TypeRoom", Type);
                    command.Parameters.AddWithValue("@CodeRoom", Code);
                    command.Parameters.AddWithValue("@Time", Time);
                    command.Parameters.AddWithValue("@TimeUse", TimeUse);
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Client information saved successfully.");
                        ClearTextBoxes();
                        clearComBoxes();
                        //LoadClients();
                    }
                    else
                    {
                        MessageBox.Show("Failed to save client information.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred: " + ex.Message);
                }
            }
        }

        private void ClearTextBoxes()
        {
            txtName.Clear();
            txtPhone.Clear();
            txtTime.Clear();
            txtTimeUse.Clear();
        }
        private void clearComBoxes()
        {
            txtType.Text = "";
            txtCode.Text = "";
        }

        private void txtCode_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
        public class Type
        {
            public string type { get; set; }
            public List<string> ListCode { get; set; }

        }

        private void txtType_SelectedValueChanged(object sender, EventArgs e)
        {
            //ComboBox cb = sender as ComboBox;
            //if (cb.SelectedValue != null)
            //{
            //    Type cl = cb.SelectedValue as Type;
            //    txtType.DataSource = cl.ListCode;
            //}
        }

        private void adminForm_Load_1(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'karaokeClientDBDataSet1.Karaoke' table. You can move, or remove it, as needed.
            this.karaokeTableAdapter1.Fill(this.karaokeClientDBDataSet1.Karaoke);
            // TODO: This line of code loads data into the 'karaokeClientDBDataSet.Karaoke' table. You can move, or remove it, as needed.
            this.karaokeTableAdapter.Fill(this.karaokeClientDBDataSet.Karaoke);

        }

        private void button20_Click(object sender, EventArgs e)
        {

        }

        private void button16_Click(object sender, EventArgs e)
        {

        }

        private void button17_Click(object sender, EventArgs e)
        {

        }

        private void button19_Click(object sender, EventArgs e)
        {

        }

        private void button14_Click(object sender, EventArgs e)
        {

        }

        private void button18_Click(object sender, EventArgs e)
        {

        }

        private void button11_Click(object sender, EventArgs e)
        {

        }

        private void button15_Click(object sender, EventArgs e)
        {

        }

        private void button10_Click(object sender, EventArgs e)
        {

        }

        private void button9_Click(object sender, EventArgs e)
        {

        }

        private void button13_Click(object sender, EventArgs e)
        {

        }

        private void button12_Click(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {

        }

        private void button17_Click_1(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void txtTimeUse_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string name = textSearch.Text.Trim();

            if (string.IsNullOrEmpty(name))
            {
                MessageBox.Show("Please enter a name to search.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Karaoke WHERE Name LIKE @Name";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Name", "%" + name + "%");

                try
                {
                    connection.Open();
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                    DataTable dataTable = new DataTable();
                    dataAdapter.Fill(dataTable);
                    dataGridView1.DataSource = dataTable;

                    if (dataTable.Rows.Count == 0)
                    {
                        MessageBox.Show("No matching records found.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                string query = "SELECT * FROM Karaoke";
                SqlDataAdapter dataAdapter = new SqlDataAdapter(query, connection);
                DataTable dataTable = new DataTable();
                dataAdapter.Fill(dataTable);
                dataGridView1.DataSource = dataTable;
            }
        }
        private void button22_Click(object sender, EventArgs e)
        {
            
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Main form = new Main();
            form.Show();
            this.Hide();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Main form = new Main();
            form.Show();
            this.Hide();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Main form = new Main();
            form.Show();
            this.Hide();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            Main form = new Main();
            form.Show();
            this.Hide();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            int id;
            if (!int.TryParse(txtID.Text, out id))
            {
                MessageBox.Show("Please enter a valid ID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string name = txtNameSearch.Text.Trim();
            string phone = txtPhoneSearch.Text.Trim();
            string type = txtLoaiPhong.Text.Trim();
            string room = txtMaPhong.Text.Trim();
            string clock = time.Text.Trim();
            decimal usetime;
            if (!decimal.TryParse(timeuse.Text, out usetime))
            {
                MessageBox.Show("Please enter a valid usetime.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "UPDATE Karaoke SET Name = @Name, Phone = @Phone, TypeRoom = @TypeRoom, CodeRoom = @CodeRoom, Time = @Time, TimeUse = @TimeUse WHERE id = @ID";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Name", name);
                command.Parameters.AddWithValue("@Phone", phone);
                command.Parameters.AddWithValue("@TypeRoom", type);
                command.Parameters.AddWithValue("@CodeRoom", room);
                command.Parameters.AddWithValue("@Time", clock);
                command.Parameters.AddWithValue("@TimeUse", usetime);
                command.Parameters.AddWithValue("@ID", id);

                try
                {
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Karaoke item updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        RefreshDataGridView1(); // Refresh DataGridView after update
                    }
                    else
                    {
                        MessageBox.Show("No record found with the specified ID.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            int id;
            if (!int.TryParse(txtID.Text, out id))
            {
                MessageBox.Show("Please enter a valid ID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (MessageBox.Show("Are you sure you want to delete this record?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "DELETE FROM Karaoke WHERE ID = @ID";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@ID", id);

                    try
                    {
                        connection.Open();
                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Karaoke item deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            RefreshDataGridView1(); // Refresh DataGridView after delete
                        }
                        else
                        {
                            MessageBox.Show("No record found with the specified ID.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
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
                        timeuse.Text = reader["TimeUse"].ToString();
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

        private void txtLoaiPhong_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtMaPhong.Items.Clear();
            if (txtLoaiPhong.SelectedItem == "Normal")
            {
                txtMaPhong.Items.Add("N-1");
                txtMaPhong.Items.Add("N-2");
                txtMaPhong.Items.Add("N-3");
                txtMaPhong.Items.Add("N-4");
                txtMaPhong.Items.Add("N-5");
                txtMaPhong.Items.Add("N-6");
            }
            else if (txtLoaiPhong.SelectedItem == "Premium")
            {
                txtMaPhong.Items.Add("P-1");
                txtMaPhong.Items.Add("P-2");
                txtMaPhong.Items.Add("P-3");
                txtMaPhong.Items.Add("P-4");
            }
            else if (txtLoaiPhong.SelectedItem == "Luxury")
            {
                txtMaPhong.Items.Add("L-1");
                txtMaPhong.Items.Add("L-2");
                txtMaPhong.Items.Add("L-3");
            }

        }

        private void button22_Click_1(object sender, EventArgs e)
        {

        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            foreach (Button Button in SelectedButtons)
            {
                Button.BackColor = Color.Yellow;
            }

        }

        private void RemoveButton_Click(object sender, EventArgs e)
        {
            foreach (Button Button in SelectedButtons)
            {
                Button.BackColor = Color.White;
            }
            // Clear the list of selected buttons
            SelectedButtons.Clear();
        }

        private void AddButton_Click_1(object sender, EventArgs e)
        {

        }
    }
}
