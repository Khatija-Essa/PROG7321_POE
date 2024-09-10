using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;
using Microsoft.Win32;

namespace MunicipalServicesApplication
{
    public partial class Profile : Window
    {
        //SQL connection string to the database 
        private string connectionString = "Data Source=labVMH8OX\\SQLEXPRESS;Initial Catalog=MunicipalServicesDB;MultipleActiveResultSets=True;Integrated Security=True;Encrypt=False";
        private string username;

        //to get the profile picture from database
        public byte[] ProfileImageData { get; set; }

        public Profile(string username)
        {
            InitializeComponent();
            this.username = username;//user the username that logged in to display that user information from the database 
            LoadUserProfile();//loads user profile details
        }

        private void LoadUserProfile()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();//connection is open
                    //selecting all of the data saved to be displayed on the page
                    string query = "SELECT * FROM Users WHERE Username = @Username";
                    SqlDataAdapter sda = new SqlDataAdapter(query, conn);
                    sda.SelectCommand.Parameters.AddWithValue("@Username", username);
                    //hold in-memory data in a tabular format
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    // Check if there are any rows in the DataTable
                    if (dt.Rows.Count > 0)
                    {// Retrieve the first row from the DataTable
                        DataRow row = dt.Rows[0];
                        // Populate the text boxes with values from the DataRow
                        txtUsername.Text = row["Username"].ToString();
                        txtAddress.Text = row["Address"].ToString();
                        txtEmail.Text = row["Email"].ToString();
                        txtMobileNumber.Text = row["MobileNumber"].ToString();
                        // Retrieve the profile image data as a byte array
                        ProfileImageData = row["ProfileImage"] as byte[];
                        //checks if there is any image
                        if (ProfileImageData != null)
                        {// Create a BitmapImage to hold the profile image
                            BitmapImage bitmap = new BitmapImage();
                            // Use a MemoryStream to read the byte array data
                            using (MemoryStream ms = new MemoryStream(ProfileImageData))
                            {// Initialize the BitmapImage
                                bitmap.BeginInit();
                                bitmap.StreamSource = ms;
                                bitmap.CacheOption = BitmapCacheOption.OnLoad;
                                bitmap.EndInit();
                            }
                            // Set the image source for the UI element that displays the profile image
                            ProfileImageBrush.ImageSource = bitmap;
                        }
                        else
                        {
                            ProfileImageBrush.ImageSource = null;
                        }
                    }
                    else
                    {
                        MessageBox.Show("User not found.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading profile: {ex.Message}");
            }
        }

        private void UploadImage_Click(object sender, RoutedEventArgs e)
        {//code to ensure the image uploaded can only be a certain type
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Image files (*.png;*.jpeg)|*.png;*.jpeg"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                ProfileImageData = File.ReadAllBytes(openFileDialog.FileName);
                BitmapImage bitmap = new BitmapImage();
                using (MemoryStream ms = new MemoryStream(ProfileImageData))
                {
                    bitmap.BeginInit();
                    bitmap.StreamSource = ms;
                    bitmap.CacheOption = BitmapCacheOption.OnLoad;
                    bitmap.EndInit();
                }
                ProfileImageBrush.ImageSource = bitmap;
            }
        }

        private void SaveProfile_Click(object sender, RoutedEventArgs e)
        {//saves the user upadted data to the databse 
            string address = txtAddress.Text;
            string email = txtEmail.Text;
            string mobileNumber = txtMobileNumber.Text;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "UPDATE Users SET Address=@Address, Email=@Email, MobileNumber=@MobileNumber, ProfileImage=@ProfileImage WHERE Username=@Username";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Username", username);
                    cmd.Parameters.AddWithValue("@Address", address);
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@MobileNumber", mobileNumber);
                    cmd.Parameters.AddWithValue("@ProfileImage", (object)ProfileImageData ?? DBNull.Value);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Profile updated successfully!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving profile: {ex.Message}");
            }
        }

        private void TextBlock_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            // Redirect back to the MainWindow page 
            var mainWin = new MainWindow(); // Create a new instance of MainWindow
            mainWin.Show(); // Show the MainWindow
            Close(); // Close the current window 
        }
    }
}