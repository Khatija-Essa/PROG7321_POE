using System;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace MunicipalServicesApplication
{
    public partial class Login : Window
    {
        // SQL connection string 
        private string connectionString = "Data Source=labVMH8OX\\SQLEXPRESS;Initial Catalog=MunicipalServicesDB;MultipleActiveResultSets=True;Integrated Security=True;Encrypt=False";

        public Login()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Password;

            // Hash the password to protect it
            string hashedPassword = HashPassword(password);

            // Using try-catch for error handling
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    // Check the database to see if the username and hashed password match
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("SELECT COUNT(1) FROM Users WHERE Username=@Username AND PasswordHash=@PasswordHash", conn);
                    cmd.Parameters.AddWithValue("@Username", username);
                    cmd.Parameters.AddWithValue("@PasswordHash", hashedPassword);

                    int count = (int)cmd.ExecuteScalar();
                    if (count == 1)
                    {
                        // Set the global logged-in flag and store the username
                        App.IsLoggedIn = true;
                        App.LoggedInUsername = username;

                        // Navigate to the profile page and pass the username
                        Profile profileWindow = new Profile(username);
                        profileWindow.Show();
                        this.Close(); // Close the login window
                    }
                    else
                    {
                        MessageBox.Show("Invalid username or password.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        // Hashing Password
        private string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();
                foreach (byte b in bytes)
                {
                    builder.Append(b.ToString("x2"));
                }
                return builder.ToString();
            }
        }

        private void TextBlock_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // Redirect user back to registration form if they click on the text
            Register registerWindow = new Register();
            registerWindow.Show();
            this.Close();
        }


        // Event handler for guest login
        private void GuestLogin_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // Set the logged-in flag to false for guest
            App.IsLoggedIn = false;
            App.LoggedInUsername = "Guest";

            // Navigate to MainWindow as a guest
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
    }
}