using System.Configuration;
using System.Data;
using System.Windows;

namespace MunicipalServicesApplication
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {        // This flag tracks whether the user is logged in
        public static bool IsLoggedIn { get; set; } = false;

        // Optionally, you can store the logged-in user's username
        public static string LoggedInUsername { get; set; }
    }
}