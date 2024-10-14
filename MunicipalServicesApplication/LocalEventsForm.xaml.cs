using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Globalization;

namespace MunicipalServicesApplication
{
    public partial class LocalEventsForm : Window
    {
        // Data structures to store and organize events the user of Dictionary, Queue and hashset 
        private Dictionary<DateTime, List<Event>> eventsByDate;
        private Dictionary<string, HashSet<Event>> eventsByCategory;
        private Queue<Event> upcomingEvents;
        private Queue<string> recentSearches;
        private Queue<string> searchHistory;
        private HashSet<string> uniqueCategories;
        private HashSet<DateTime> uniqueDates;

        public LocalEventsForm()
        {
            InitializeComponent();
            InitializeDataStructures();
            PopulateSampleData();
            DisplayEvents();
        }

        // Initialize all data structures used in the application
        private void InitializeDataStructures()
        {
            eventsByDate = new Dictionary<DateTime, List<Event>>();
            eventsByCategory = new Dictionary<string, HashSet<Event>>();
            upcomingEvents = new Queue<Event>();
            recentSearches = new Queue<string>();
            searchHistory = new Queue<string>();
            uniqueCategories = new HashSet<string>();
            uniqueDates = new HashSet<DateTime>();
        }

        // Populate the data structures with sample event data
        private void PopulateSampleData()
        {
            // Populate eventsByDate with sample events
            eventsByDate = new Dictionary<DateTime, List<Event>>
            {
                { new DateTime(2024, 10, 24), new List<Event> { new Event("Beach Cleanup", new DateTime(2024, 10, 24), "Environment") } },
                { new DateTime(2024, 10, 30), new List<Event> { new Event("Umhlanga Ladies Taleem", new DateTime(2024, 10, 30), "Culture") } },
                { new DateTime(2024, 11, 7), new List<Event> { new Event("Voting Day", new DateTime(2024, 11, 7), "Governance") } },
                { new DateTime(2024, 11, 13), new List<Event> { new Event("Varsity Collage Rugby Game ", new DateTime(2024, 11, 13), "Sports") } },
                { new DateTime(2024, 11, 16), new List<Event> { new Event("Shongweni Farmers Market", new DateTime(2024, 11, 16), "Community") } },
                { new DateTime(2024, 11, 28), new List<Event> { new Event("Music Festival At Chris Saunders Park", new DateTime(2024, 11, 28), "Entertainment") } },
                { new DateTime(2024, 12, 14), new List<Event> { new Event("Park Run", new DateTime(2024, 12, 14), "Sports") } },
                { new DateTime(2024, 12, 24), new List<Event> { new Event("Chrimas Party At Grace Family Church", new DateTime(2024, 12, 24), "Community") } }
            };

            // Populate eventsByCategory with sample events
            eventsByCategory = new Dictionary<string, HashSet<Event>>
            {
                { "environment", new HashSet<Event> { new Event("Beach Cleanup", new DateTime(2024, 10, 24), "Environment") } },
                { "culture", new HashSet<Event> { new Event("Umhlanga Ladies Taleem", new DateTime(2024, 10, 30), "Culture") } },
                { "governance", new HashSet<Event> { new Event("Voting Day", new DateTime(2024, 11, 7), "Governance") } },
                { "sports", new HashSet<Event> {
                    new Event("Varsity Collage Rugby Game", new DateTime(2024, 11, 13), "Sports"),
                    new Event("Park Run", new DateTime(2024, 12, 14), "Sports")
                } },
                { "community", new HashSet<Event> {
                    new Event("Shongweni Farmers Market", new DateTime(2024, 11, 16), "Community"),
                    new Event("Chrimas Party At Grace Family Church", new DateTime(2024, 12, 24), "Community")
                } },
                { "entertainment", new HashSet<Event> { new Event("Music Festival At Chris Saunders Park", new DateTime(2024, 11, 28), "Entertainment") } }
            };

            // Populate upcomingEvents queue with events ordered by date
            upcomingEvents = new Queue<Event>(eventsByDate.Values.SelectMany(e => e).OrderBy(e => e.Date));

            // Populate uniqueCategories with all distinct event categories
            uniqueCategories = new HashSet<string>(eventsByCategory.Keys);

            // Populate uniqueDates with all distinct event dates
            uniqueDates = new HashSet<DateTime>(eventsByDate.Keys);

            // Initialize recentSearches and searchHistory as empty queues
            recentSearches = new Queue<string>();
            searchHistory = new Queue<string>();
        }

        // Display all events in the ListView
        private void DisplayEvents()
        {
            var allEvents = eventsByDate.SelectMany(kvp => kvp.Value).OrderBy(e => e.Date).ToList();
            listViewEvents.ItemsSource = allEvents;
        }

        // Event handler for the Search button click
        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            PerformSearch();
        }

        // Perform the search based on the user's input
        private void PerformSearch()
        {
            string searchTerm = txtSearch.Text.Trim().ToLower();
            if (string.IsNullOrEmpty(searchTerm))
            {
                DisplayEvents();
                ClearRecommendations();
                return;
            }

            UpdateSearchHistory(searchTerm);

            var searchResults = new HashSet<Event>();

            // Search by category (case-insensitive)
            foreach (var category in eventsByCategory.Keys)
            {
                if (category.Contains(searchTerm))
                {
                    searchResults.UnionWith(eventsByCategory[category]);
                }
            }

            // Search by date 
            DateTime searchDate;
            string[] dateFormats = { "M/d/yy", "MM/dd/yyyy", "yyyy-MM-dd", "MMMM d, yyyy", "MMM d, yyyy" };
            if (DateTime.TryParseExact(searchTerm, dateFormats, CultureInfo.InvariantCulture, DateTimeStyles.None, out searchDate))
            {
                if (eventsByDate.ContainsKey(searchDate))
                {
                    searchResults.UnionWith(eventsByDate[searchDate]);
                }
            }
            else
            {
                // If the search term is not a valid date, try matching month names, days, or years
                foreach (var date in eventsByDate.Keys)
                {
                    if (date.ToString("MMMM", CultureInfo.InvariantCulture).ToLower().Contains(searchTerm) ||
                        date.ToString("MMM", CultureInfo.InvariantCulture).ToLower().Contains(searchTerm) ||
                        date.ToString("dd", CultureInfo.InvariantCulture).Contains(searchTerm) ||
                        date.ToString("yyyy", CultureInfo.InvariantCulture).Contains(searchTerm))
                    {
                        searchResults.UnionWith(eventsByDate[date]);
                    }
                }
            }

            // Search by name (case-insensitive)
            foreach (var eventList in eventsByDate.Values)
            {
                searchResults.UnionWith(eventList.Where(e => e.Name.ToLower().Contains(searchTerm)));
            }

            // Display search results and show recommendations
            listViewEvents.ItemsSource = searchResults.OrderBy(e => e.Date);
            ShowRecommendations(searchTerm, searchResults);
        }

        // Update the search history and recent searches
        private void UpdateSearchHistory(string searchTerm)
        {
            // Maintain a history of the last 10 searches
            if (searchHistory.Count >= 10)
            {
                searchHistory.Dequeue();
            }
            searchHistory.Enqueue(searchTerm);

            // Maintain a list of the 5 most recent searches
            if (recentSearches.Count >= 5)
            {
                recentSearches.Dequeue();
            }
            recentSearches.Enqueue(searchTerm);
        }

        // Show event recommendations based on search term and results
        private void ShowRecommendations(string searchTerm, HashSet<Event> searchResults)
        {
            var recommendations = new HashSet<Event>();

            // Add events from recent searches
            foreach (var recentSearch in recentSearches)
            {
                foreach (var category in eventsByCategory.Keys)
                {
                    if (category.Contains(recentSearch))
                    {
                        recommendations.UnionWith(eventsByCategory[category]);
                    }
                }
            }

            // Add events from the current search term's category
            foreach (var category in eventsByCategory.Keys)
            {
                if (category.Contains(searchTerm))
                {
                    recommendations.UnionWith(eventsByCategory[category]);
                }
            }

            // Add upcoming events if needed to fill recommendations
            var tempUpcomingEvents = new Queue<Event>(upcomingEvents);
            while (recommendations.Count < 5 && tempUpcomingEvents.Count > 0)
            {
                recommendations.Add(tempUpcomingEvents.Dequeue());
            }

            // Remove events that are already in the search results
            recommendations.ExceptWith(searchResults);

            // Display up to 5 recommendations
            listViewRecommendations.ItemsSource = recommendations.OrderBy(e => e.Date).Take(5);
        }

        // Event handler to return back to MainWindow
        private void TextBlock_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var mainWin = new MainWindow();
            mainWin.Show();
            Close(); // Close current window
        }

        // Event handler to clear user search 
        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            txtSearch.Clear();
            DisplayEvents();
            ClearRecommendations();
        }

        // Clear the recommendations ListView
        private void ClearRecommendations()
        {
            listViewRecommendations.ItemsSource = null;
        }
    }

    // Event class to represent individual events
    public class Event
    {
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public string Category { get; set; }

        public Event(string name, DateTime date, string category)
        {
            Name = name;
            Date = date;
            Category = category;
        }
    }
}