using System;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace MunicipalServicesApplication
{
    public partial class Register : Window
    {//SQL connection string to the databse 
        private string connectionString = "Data Source=labVMH8OX\\SQLEXPRESS;Initial Catalog=MunicipalServicesDB;MultipleActiveResultSets=True;Integrated Security=True;Encrypt=False";

        public Register()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string username = txtRegUsername.Text;
            string password = txtRegPassword.Password;
            string confirmPassword = txtRegConfirmPassword.Password;
            string address = txtRegAddress.Text;
            string email = txtRegEmail.Text;
            string mobile = txtRegMobile.Text;

            // Validate that all textboxes are filled
            if (string.IsNullOrWhiteSpace(username) ||
                string.IsNullOrWhiteSpace(password) ||
                string.IsNullOrWhiteSpace(confirmPassword) ||
                string.IsNullOrWhiteSpace(address) ||
                string.IsNullOrWhiteSpace(email) ||
                string.IsNullOrWhiteSpace(mobile))
            {
                MessageBox.Show("Please fill in all fields!");
                return;
            }

            if (password != confirmPassword)
            {//checks the password and confirm password is correct 
                MessageBox.Show("Passwords do not match!");
                return;
            }
            //hashes user password to ensure safety 
            string hashedPassword = HashPassword(password);

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {//opens connection string and saves all the users data to the database
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("INSERT INTO Users (Username, PasswordHash, Address, Email, MobileNumber) VALUES (@Username, @PasswordHash, @Address, @Email, @Mobile)", conn);
                    cmd.Parameters.AddWithValue("@Username", username);
                    cmd.Parameters.AddWithValue("@PasswordHash", hashedPassword);
                    cmd.Parameters.AddWithValue("@Address", address);
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@Mobile", mobile);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("User registered successfully!");

                    // Redirect to Profile page
                    Profile profileWindow = new Profile(username);//uses user name so profile can display correct details
                    profileWindow.Show();
                    this.Close();
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

        private void BackToHome_Click(object sender, MouseButtonEventArgs e)
        {
            // Navigate back to the Login page
            Login loginWindow = new Login();
            loginWindow.Show();
            this.Close();
        }
    }
}