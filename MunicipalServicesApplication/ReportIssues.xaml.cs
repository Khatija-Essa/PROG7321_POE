using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace MunicipalServicesApplication
{
    public partial class ReportIssues : Window
    {
        // Variable to store the file path of the attached media
        private string _selectedMediaPath;

        // Create a list to store the issues 
        private List<Issue> reportedIssues = new List<Issue>();

        public ReportIssues()
        {
            InitializeComponent();
        }

        private void btnAttachMedia_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog
            {
                Filter = "Image Files (*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg;*.jpeg;*.gif;*.bmp"
            };

            if (open.ShowDialog() == true) // Use 'true' for DialogResult in WPF
            {
                txtMediaFileName.Text = open.FileName;

                // Create a new BitmapImage to load the image
                BitmapImage bitmapImage = new BitmapImage();
                using (FileStream fileStream = new FileStream(open.FileName, FileMode.Open, FileAccess.Read))
                {
                    bitmapImage.BeginInit();
                    bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                    bitmapImage.StreamSource = fileStream;
                    bitmapImage.EndInit();
                }

                imgAttachedMedia.Source = bitmapImage;
            }
        }


        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            var issueLocation = txtLocation.Text.Trim(); // Get and trim the location text
            var selectedCategoryItem = cbCategory.SelectedItem as ComboBoxItem; // Get the selected item from the ComboBox
            var issueCategory = selectedCategoryItem?.Content.ToString(); // Get the category text from the selected item
            var issueDescription = new TextRange(rtbDescription.Document.ContentStart, rtbDescription.Document.ContentEnd).Text.Trim(); // Get and trim the description text

            /*Code Attribute for saving images to the images folder
             *Source: https://youtu.be/TqWP1rpkx9k?si=_fQ6i5hKPooydAKG 
             *Creater : Haritha Computer & Technology
             */

            File.Copy(txtMediaFileName.Text, Path.Combine(@"C:\Users\lab_services_student\source\repos\MunicipalServicesApplication\MunicipalServicesApplication\Images\", Path.GetFileName(txtMediaFileName.Text)), true);

            // Check if a category is selected
            if (string.IsNullOrWhiteSpace(issueCategory))
            {
                MessageBox.Show("You must select a category.");
                return;
            }

            // Store the information into the list instead of the database
            InsertIssueToList(issueLocation, issueCategory, issueDescription, _selectedMediaPath);

            // Display message to show the issue was successfully reported 
            MessageBox.Show("Your issue has been reported successfully!");
            ResetForm();
        }

        // Define the Issue class to hold the issue details
        public class Issue
        {
            //get and set for the data being saved in a list 
            public string Location { get; set; }
            public string Category { get; set; }
            public string Description { get; set; }
            public string MediaPath { get; set; }
        }

        // Method to add issues to the list
        private void InsertIssueToList(string location, string category, string description, string mediaPath)
        {
            // Create a new Issue object and add it to the list
            var issue = new Issue
            {
                Location = location,
                Category = category,
                Description = description,
                MediaPath = mediaPath
            };

            reportedIssues.Add(issue);
        }

        private void ResetForm()
        {
            // Clear the textboxes, combo box, and RichTextBox once the report is saved 
            txtLocation.Clear();
            cbCategory.SelectedIndex = -1;
            rtbDescription.Document.Blocks.Clear();
            txtMediaFileName.Clear();
            imgAttachedMedia.Source = null; // Clear the image display
            _selectedMediaPath = null;
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            // Redirect back to the MainWindow page 
            var mainWin = new MainWindow(); // Create a new instance of MainWindow
            mainWin.Show(); // Show the MainWindow
            Close(); // Close the current window (ReportIssues)
        }

        private void txtLocation_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txtLocation.Text == "Enter the Location") // The text placeholder for location
            {
                txtLocation.Text = string.Empty; // Clear the text box
                txtLocation.Foreground = new SolidColorBrush(Colors.White); // Set text color to white
            }
        }

        private void txtLocation_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtLocation.Text)) // Check if the text box is empty
            {
                txtLocation.Text = "Please make sure you enter the Location"; // Set placeholder text
                txtLocation.Foreground = new SolidColorBrush(Colors.White); // Set text color to white
            }
        }
    }
}