using MunicipalServicesApplication;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MunicipalServicesApplication
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnReportIssues_Click(object sender, RoutedEventArgs e)
        {
            // When the ReportIssues button is clicked, redirect to the ReportIssues form
            ReportIssues reportIssuesWindow = new ReportIssues();
            reportIssuesWindow.Show(); // Show the ReportIssues form
            this.Close(); // Close the current window (MainWindow)
        }

        private void TextBlock_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            // Check if the user is logged in
            if (App.IsLoggedIn)
            {
                // If logged in, navigate to the profile page and pass the logged-in username
                Profile profileWindow = new Profile(App.LoggedInUsername);
                profileWindow.Show();
                this.Close(); // Close the MainWindow
            }
            else
            {
                // If not logged in, redirect to the login page
                Login loginWindow = new Login();
                loginWindow.Show();
                this.Close(); // Close the MainWindow
            }
        }

        private void btnLocalEvents_Click(object sender, RoutedEventArgs e)
        {
            LocalEventsForm eventsForm = new LocalEventsForm();
            eventsForm.Show();
            this.Close();

        }

        private void btnServiceRequestStatus_Click(object sender, RoutedEventArgs e)
        {
            ServiceRequestStatus serviceForm = new ServiceRequestStatus();
            serviceForm.Show();
            this.Close();
        }
    }
}