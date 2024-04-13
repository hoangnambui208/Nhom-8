using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Runtime.CompilerServices.RuntimeHelpers;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;

namespace WindowsFormsApp6
{
    public partial class FoodForm : Form
    {
        string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=KaraokeFoodDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;";
        public FoodForm()
        {
            InitializeComponent();
            this.Load += MainForm_Load; // Attach the load event handler
        }
        private void MainForm_Load(object sender, EventArgs e)
        {
            // Do nothing when the form loads
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Main form = new Main();
            form.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            byte[] imageData;
            using (MemoryStream ms = new MemoryStream())
            {
                pictureBox1.Image.Save(ms, pictureBox1.Image.RawFormat);
                imageData = ms.ToArray();
            }
            // Save other data to the database (name, price, etc.) along with the image data
            string Name = txtNameFood.Text;
            string Price = txtPrice.Text;
            string Describe = txtDescribe.Text;

            if (string.IsNullOrEmpty(Name) || string.IsNullOrEmpty(Price) || string.IsNullOrEmpty(Describe))
            {
                MessageBox.Show("Please fill in all the fields.");
                return;
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "INSERT INTO Food2 (Name, Price, Describe, Picture) VALUES (@Name, @Price, @Describe, @Picture)";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Name", Name);
                    command.Parameters.AddWithValue("@Price", Price);
                    command.Parameters.AddWithValue("@Describe", Describe);
                    command.Parameters.AddWithValue("@Picture", imageData);
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Food information saved successfully.");
                        ClearTextBoxes();
                        pictureBox1.Image = null; // Clear the PictureBox after saving
                    }
                    else
                    {
                        MessageBox.Show("Failed to save food information.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred: " + ex.Message);
                }
            }
        //string Name = txtNameFood.Text;
        //string Price = txtPrice.Text;
        //string Describe = txtDescribe.Text;

        //if (string.IsNullOrEmpty(Name) || string.IsNullOrEmpty(Price) || string.IsNullOrEmpty(Describe) || pictureBox1.Image == null )
        //{
        //    {
        //        MessageBox.Show("Please fill in all the fields.");
        //        return;
        //    }
        //}
        //byte[] pictureData = null;
        //using (MemoryStream ms = new MemoryStream())
        //{
        //    pictureBox1.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg); // Change the format if necessary
        //    pictureData = ms.ToArray();
        //}
        //if (pictureData != null && pictureData.Length > 0)
        //{
        //    using (MemoryStream ms = new MemoryStream(pictureData))
        //    {
        //        pictureBox1.Image = Image.FromStream(ms);
        //    }
        //}
        //using (SqlConnection connection = new SqlConnection(connectionString))
        //{
        //    try
        //    {
        //        connection.Open();
        //        string query = "INSERT INTO Food2 (Name, Price, Describe, Picture) VALUES (@Name, @Price, @Describe, @Picture)";
        //        SqlCommand command = new SqlCommand(query, connection);
        //        command.Parameters.AddWithValue("@Name", Name);
        //        command.Parameters.AddWithValue("@Price", Price);
        //        command.Parameters.AddWithValue("@Describe", Describe);
        //        command.Parameters.AddWithValue("@Picture", pictureData);
        //        int rowsAffected = command.ExecuteNonQuery();

        //        if (rowsAffected > 0)
        //        {
        //            MessageBox.Show("Food information saved successfully.");
        //            ClearTextBoxes();
        //            pictureBox1.Image = null;
        //            //LoadClients();
        //        }
        //        else
        //        {
        //            MessageBox.Show("Failed to save food information.");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("An error occurred: " + ex.Message);
        //    }

    }
        private void ClearTextBoxes()
        {
            txtNameFood.Clear();
            txtPrice.Clear();
            txtDescribe.Clear();
            //AnhFood.Clear();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Main form = new Main();
            form.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string name = textSearchFood.Text.Trim();

            if (string.IsNullOrEmpty(name))
            {
                MessageBox.Show("Please enter a name to search.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Food2 WHERE Name LIKE @Name";
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
            RefreshDataGridView();
        }
        private void RefreshDataGridView()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Food2";
                SqlDataAdapter dataAdapter = new SqlDataAdapter(query, connection);
                DataTable dataTable = new DataTable();
                dataAdapter.Fill(dataTable);
                dataGridView1.DataSource = dataTable;
            }
        }

        //private void FoodForm_Load(object sender, EventArgs e)
        //{
        //    // TODO: This line of code loads data into the 'karaokeFoodDBDataSet.Food' table. You can move, or remove it, as needed.
        //    this.foodTableAdapter.Fill(this.karaokeFoodDBDataSet.Food);

        //}

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            Main form = new Main();
            form.Show();
            this.Hide();
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
                string query = "SELECT Name, Price, Describe, Picture FROM Food2 WHERE ID = @ID";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ID", id);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        NameFood.Text = reader["Name"].ToString();
                        PriceFood.Text = reader["Price"].ToString();
                        DescribeFood.Text = reader["Describe"].ToString();
                        PictureFood.Text = reader["Picture"].ToString();
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

        private void button6_Click(object sender, EventArgs e)
        {
            int id;
            if (!int.TryParse(txtID.Text, out id))
            {
                MessageBox.Show("Please enter a valid ID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string name = NameFood.Text.Trim();
            //string price = PriceFood.Text.Trim();
            string describe = DescribeFood.Text.Trim();
            //string picturefood = PictureFood.Text.Trim();
            byte[] imageId;
            using (MemoryStream ms = new MemoryStream())
            {
                PictureFood.Image.Save(ms, PictureFood.Image.RawFormat);
                imageId = ms.ToArray();
            }
            decimal price;
            if (!decimal.TryParse(PriceFood.Text, out price))
            {
                MessageBox.Show("Please enter a valid price.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "UPDATE Food2 SET Name = @Name, Price = @Price, Describe = @Describe, Picture =@Picture   WHERE id = @ID";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Name", name);
                command.Parameters.AddWithValue("@Price", price);
                command.Parameters.AddWithValue("@Describe", describe);
                command.Parameters.AddWithValue("@Picture", imageId);
                command.Parameters.AddWithValue("@ID", id);


                try
                {
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Food item updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        RefreshDataGridView(); // Refresh DataGridView after update
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
                    string query = "DELETE FROM Food2 WHERE ID = @ID";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@ID", id);

                    try
                    {
                        connection.Open();
                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Food item deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            RefreshDataGridView(); // Refresh DataGridView after delete
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

        private void button3_Click(object sender, EventArgs e)
        {
            // Open a file dialog to allow the user to choose an image file
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files (*.jpg;*.jpeg;*.png;*.gif)|*.jpg;*.jpeg;*.png;*.gif|All files (*.*)|*.*";
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                // Load the selected image into the PictureBox
                pictureBox1.Image = Image.FromFile(openFileDialog.FileName);
                LoadFoodImage(openFileDialog.FileName);
            }
        }
        private void LoadFoodImage(string imageFilePath)
        {
            try
            {
                // Load the image file from disk
                Image image = Image.FromFile(imageFilePath);

                // Set the loaded image to the PictureBox
                pictureBox1.Image = image;
                pictureBox1.SizeMode = PictureBoxSizeMode.Zoom; // Adjust PictureBox size mode as needed
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading food image: " + ex.Message);
            }
        }

        private void FoodForm_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'karaokeFoodDBDataSet1.Food2' table. You can move, or remove it, as needed.
            this.food2TableAdapter.Fill(this.karaokeFoodDBDataSet1.Food2);

        }

        private void button8_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files (*.jpg;*.jpeg;*.png;*.gif)|*.jpg;*.jpeg;*.png;*.gif|All files (*.*)|*.*";
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                // Load the selected image into the PictureBox
                PictureFood.Image = Image.FromFile(openFileDialog.FileName);
                LoadFoodImage1(openFileDialog.FileName);
            }
        }
        private void LoadFoodImage1(string imageFilePath)
        {
            try
            {
                // Load the image file from disk
                Image image = Image.FromFile(imageFilePath);

                // Set the loaded image to the PictureBox
                PictureFood.Image = image;
                PictureFood.SizeMode = PictureBoxSizeMode.Zoom; // Adjust PictureBox size mode as needed
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading food image: " + ex.Message);
            }
        }
    }
}
