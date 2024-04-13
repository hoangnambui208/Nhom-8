using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;

namespace WindowsFormsApp6
{
    public partial class Order : Form
    {
        private List<Button> selectedButtons = new List<Button>();
        string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=KaraokeFoodDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;";
        private decimal totalAmount = 0;
        public Order()
        {
            InitializeComponent();
            LoadMenu();
            
            foreach (Control control in Controls)
            {
                if (control is Button button && button != addButton && button != removeButton && button != button1 && button != buttonSave)
                {
                    button.Click += Button_Click;
                }
            }

            // Dictionary to map each button to its corresponding text box and room code
            Dictionary<Button, Tuple<TextBox, string>> roomButtonMap = new Dictionary<Button, Tuple<TextBox, string>>();

            // Associate each button with its corresponding text box and room code
            roomButtonMap.Add(buttonRoom1, Tuple.Create(textBoxRoom, "Room1"));
            roomButtonMap.Add(buttonRoom2, Tuple.Create(textBoxRoom, "Room2"));
            roomButtonMap.Add(buttonRoom3, Tuple.Create(textBoxRoom, "Room2"));
            roomButtonMap.Add(buttonRoom4, Tuple.Create(textBoxRoom, "Room2"));
            roomButtonMap.Add(buttonRoom5, Tuple.Create(textBoxRoom, "Room2"));
            roomButtonMap.Add(buttonRoom6, Tuple.Create(textBoxRoom, "Room2"));
            roomButtonMap.Add(buttonRoom7, Tuple.Create(textBoxRoom, "Room2"));
            roomButtonMap.Add(buttonRoom8, Tuple.Create(textBoxRoom, "Room2"));
            roomButtonMap.Add(buttonRoom9, Tuple.Create(textBoxRoom, "Room2"));
            roomButtonMap.Add(buttonRoom10, Tuple.Create(textBoxRoom, "Room2"));
            roomButtonMap.Add(buttonRoom11, Tuple.Create(textBoxRoom, "Room2"));
            roomButtonMap.Add(buttonRoom12, Tuple.Create(textBoxRoom, "Room2"));
            roomButtonMap.Add(buttonRoom13, Tuple.Create(textBoxRoom, "Room2"));
            // Add more buttons as needed

            // Associate click event handlers with each button
            foreach (var kvp in roomButtonMap)
            {
                Button button = kvp.Key;
                Tuple<TextBox, string> roomInfo = kvp.Value;

                button.Click += (sender, e) =>
                {
                    TextBox textBox = roomInfo.Item1;
                    string roomCode = roomInfo.Item2;

                    // Update button text and list box
                    textBox.Text = button.Text;
                    listBoxSelectedItems.Items.Clear();
                    //DisplayInvoiceForRoom(roomCode);
                };
            }

        }

        private void Button_Click(object sender, EventArgs e)
        {
            Button clickedButton = sender as Button;
            if (clickedButton != null)
            {
                // Change color to blue when a selected button is clicked
                clickedButton.BackColor = Color.Blue;
                // Add the clicked button to the list of selected buttons
                selectedButtons.Add(clickedButton);
            }
        }


        private void addButton_Click(object sender, EventArgs e)
        {
            foreach (Button button in selectedButtons)
            {
                button.BackColor = Color.Yellow;
            }
            selectedButtons.Clear();
        }

        private void removeButton_Click(object sender, EventArgs e)
        {
            foreach (Button button in selectedButtons)
            {
                button.BackColor = Color.White;
            }
            // Clear the list of selected buttons
            selectedButtons.Clear();
        }

        private void pictureBox10_Click(object sender, EventArgs e)
        {
            Main form = new Main();
            form.Show();
            this.Hide();
        }
        private void LoadMenu()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT Name, Price, Describe, Picture FROM Food2";
                    SqlCommand command = new SqlCommand(query, connection);
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        string name = reader["Name"].ToString();
                        decimal price = Convert.ToDecimal(reader["Price"]);
                        string description = reader["Describe"].ToString();
                        byte[] imageData = (byte[])reader["Picture"];

                        // Convert byte array to image
                        Image image;
                        using (MemoryStream ms = new MemoryStream(imageData))
                        {
                            image = Image.FromStream(ms);
                        }

                        // Create a panel for each food item
                        CreateFoodPanel(name, price, description, image);
                    }

                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred while loading the menu: " + ex.Message);
                }
            }
        }
        private void CreateFoodPanel(string name, decimal price, string description, Image image)
        {
            // Create a panel for the food item
            Panel panel = new Panel();
            panel.BorderStyle = BorderStyle.FixedSingle;
            panel.Size = new Size(200, 200);

            // Create controls for displaying food information
            PictureBox pictureBox = new PictureBox();
            pictureBox.Image = image;
            pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox.Size = new Size(150, 100);
            pictureBox.Location = new Point(25, 10);

            Label nameLabel = new Label();
            nameLabel.Text = name;
            nameLabel.Location = new Point(10, 120);
            nameLabel.Size = new Size(80, 50);
            nameLabel.ForeColor = Color.Black;

            Label priceLabel = new Label();
            priceLabel.Text =  price.ToString("0.000");
            priceLabel.Location = new Point(10, 170);
            priceLabel.ForeColor = Color.Red;

            panel.Click += (sender, e) =>
            {
                if (!panel.BackColor.Equals(Color.LightGray))
                {
                    panel.BackColor = Color.LightGray;
                    AddToSelectedItems(name, price);
                }
                else
                {
                    panel.BackColor = SystemColors.Control;
                    RemoveFromSelectedItems(name, price);
                }
            };

            panel.Controls.Add(pictureBox);
            panel.Controls.Add(nameLabel);
            panel.Controls.Add(priceLabel);
            //panel.Controls.Add(quantityTextBox);

            flowLayoutPanel1.Controls.Add(panel);
        }
        private void AddToSelectedItems(string name, decimal price)
        {
            totalAmount += price;
            listBoxSelectedItems.Items.Add(name + " - " + price.ToString("0.000"));
            UpdateTotalAmount();
        }

        private void RemoveFromSelectedItems(string name, decimal price)
        {
            totalAmount -= price;
            listBoxSelectedItems.Items.Remove(name + " - " + price.ToString("0.000"));
            UpdateTotalAmount();
        }
        private void UpdateTotalAmount()
        {
            labelTotalAmount.Text = totalAmount.ToString("0.000");
        }

        private void UpdateTotal()
        {
            totalAmount = 0;

            foreach (Panel panel in flowLayoutPanel1.Controls)
            {
                TextBox quantityTextBox = (TextBox)panel.Controls[3];
                int quantity = 0;

                if (int.TryParse(quantityTextBox.Text, out quantity))
                {
                    Label priceLabel = (Label)panel.Controls[2];
                    decimal price = decimal.Parse(priceLabel.Text.Substring(8)); // Extract price from label text
                    totalAmount += price * quantity;
                }
            }

            labelTotalAmount.Text = " VNĐ" + totalAmount.ToString("0.000");
        }
        private void buttonSaveInvoice_Click(object sender, EventArgs e)
        {
         
        }
        
        private void buttonSaveInvoice_Click_1(object sender, EventArgs e)
        {
            string roomCode = textBoxRoom.Text;
            decimal totalAmount = decimal.Parse(labelTotalAmount.Text); // Assuming you have a method to calculate the total amount
            SaveDataToDatabase(roomCode, listBoxSelectedItems, totalAmount);
        }
        private void SaveDataToDatabase(string roomCode, ListBox listBox, decimal totalAmount)
{
    // Assuming connectionString is defined somewhere in your code
    using (SqlConnection connection = new SqlConnection(connectionString))
    {
        try
        {
            connection.Open();
            List<string> selectedItems = new List<string>();

            foreach (var item in listBox.Items)
            {
                selectedItems.Add(item.ToString());
            }

            string selectedItemsString = string.Join(",", selectedItems);

            string query = "INSERT INTO [Order] (RoomCode, SelectedDishes, TotalAmount) VALUES (@RoomCode, @SelectedDishes, @TotalAmount)";

                    SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@RoomCode", roomCode);
            command.Parameters.AddWithValue("@SelectedDishes", selectedItemsString);
            command.Parameters.AddWithValue("@TotalAmount", totalAmount);
            int rowsAffected = command.ExecuteNonQuery();

            if (rowsAffected > 0)
            {
                MessageBox.Show("Data saved successfully.");
            }
            else
            {
                MessageBox.Show("Failed to save data.");
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show("An error occurred while saving data: " + ex.Message);
        }
    }
}



        private void button13_Click(object sender, EventArgs e)
        {

        }

        private void button11_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            listBoxSelectedItems.Items.Clear();

            // Reset the total price label
            labelTotalAmount.Text = "0.000";
        }
    }
}
