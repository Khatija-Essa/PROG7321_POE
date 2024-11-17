using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Linq;

namespace MunicipalServicesApplication
{
    // Represents a service request with properties such as ID, description, status, priority, and progress.
    public class ServiceRequest : IComparable<ServiceRequest>
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public int Priority { get; set; }
        public int Progress { get; set; }

        // CompareTo is used to compare service requests based on priority.
        public int CompareTo(ServiceRequest other)
        {
            if (other == null) return 1;
            return this.Priority.CompareTo(other.Priority);
        }

        // Override ToString to return a readable representation of the request.
        public override string ToString()
        {
            return $"{Description} ({Status})";
        }
    }

    // Graph class for managing dependencies
    public class Graph<T>
    {
        private Dictionary<T, List<T>> _adjacencyList;

        public Graph()
        {
            _adjacencyList = new Dictionary<T, List<T>>();
        }

        // Adds a node to the graph.
        public void AddNode(T node)
        {
            if (!_adjacencyList.ContainsKey(node))
            {
                _adjacencyList[node] = new List<T>();
            }
        }
        // Adds an edge between two nodes (indicating a dependency).
        public void AddEdge(T node1, T node2)
        {
            if (!_adjacencyList.ContainsKey(node1))
            {
                AddNode(node1);
            }
            if (!_adjacencyList.ContainsKey(node2))
            {
                AddNode(node2);
            }

            _adjacencyList[node1].Add(node2);
            _adjacencyList[node2].Add(node1);
        }
        // Retrieves the neighbors (dependencies) of a given node.
        public List<T> GetNeighbors(T node)
        {
            if (_adjacencyList.ContainsKey(node))
            {
                return _adjacencyList[node];
            }
            return new List<T>();
        }
        // Checks if a node exists in the graph.
        public bool ContainsNode(T node)
        {
            return _adjacencyList.ContainsKey(node);
        }
        // Retrieves all nodes in the graph
        public IEnumerable<T> GetAllNodes()
        {
            return _adjacencyList.Keys;
        }
    }

    // Binary Search Tree Node
    public class BSTNode
    {
        public ServiceRequest Request { get; set; }
        public BSTNode Left { get; set; }
        public BSTNode Right { get; set; }

        public BSTNode(ServiceRequest request)
        {
            Request = request;
        }
    }

    // Binary Search Tree (BST) for managing service requests by priority.
    public class BinarySearchTree
    {
        public BSTNode Root { get; set; }
        // Inserts a new service request into the BST.
        public void Insert(ServiceRequest request)
        {
            if (Root == null)
            {
                Root = new BSTNode(request);
            }
            else
            {
                InsertRec(Root, request);
            }
        }
        // Recursive helper for inserting nodes into the BST.
        private void InsertRec(BSTNode node, ServiceRequest request)
        {
            if (request.Priority < node.Request.Priority)
            {
                if (node.Left == null)
                    node.Left = new BSTNode(request);
                else
                    InsertRec(node.Left, request);
            }
            else
            {
                if (node.Right == null)
                    node.Right = new BSTNode(request);
                else
                    InsertRec(node.Right, request);
            }
        }
        // Performs an in-order traversal of the BST, returning sorted requests.
        public List<ServiceRequest> InOrderTraversal()
        {
            List<ServiceRequest> result = new List<ServiceRequest>();
            InOrderTraversalRec(Root, result);
            return result;
        }
        // Recursive helper for in-order traversal.
        private void InOrderTraversalRec(BSTNode node, List<ServiceRequest> result)
        {
            if (node == null) return;

            InOrderTraversalRec(node.Left, result);
            result.Add(node.Request);
            InOrderTraversalRec(node.Right, result);
        }
    }

    // MinHeap implementation
    public class MinHeap
    {
        private List<ServiceRequest> heap = new List<ServiceRequest>();
        // Inserts a new request into the heap.
        public void Insert(ServiceRequest request)
        {
            heap.Add(request);
            HeapifyUp(heap.Count - 1);
        }
        // Extracts the request with the smallest progress value.
        public ServiceRequest ExtractMin()
        {
            if (heap.Count == 0) return null;

            ServiceRequest min = heap[0];
            heap[0] = heap[heap.Count - 1];
            heap.RemoveAt(heap.Count - 1);
            HeapifyDown(0);
            return min;
        }
        // Moves an element up in the heap to maintain heap property.
        private void HeapifyUp(int index)
        {
            while (index > 0)
            {
                int parentIndex = (index - 1) / 2;
                if (heap[index].Progress >= heap[parentIndex].Progress) break;
                Swap(index, parentIndex);
                index = parentIndex;
            }
        }
        // Moves an element down in the heap to maintain heap property.
        private void HeapifyDown(int index)
        {
            int leftChildIndex = 2 * index + 1;
            int rightChildIndex = 2 * index + 2;
            int smallest = index;

            if (leftChildIndex < heap.Count && heap[leftChildIndex].Progress < heap[smallest].Progress)
                smallest = leftChildIndex;
            if (rightChildIndex < heap.Count && heap[rightChildIndex].Progress < heap[smallest].Progress)
                smallest = rightChildIndex;

            if (smallest != index)
            {
                Swap(index, smallest);
                HeapifyDown(smallest);
            }
        }
        // Swaps two elements in the heap.
        private void Swap(int i, int j)
        {
            ServiceRequest temp = heap[i];
            heap[i] = heap[j];
            heap[j] = temp;
        }
        // Retrieves all requests in the heap.
        public List<ServiceRequest> GetAllRequests()
        {
            return new List<ServiceRequest>(heap);
        }
    }

    // Main Window Implementation
    public partial class ServiceRequestStatus : Window
    {
        private List<ServiceRequest> serviceRequests;
        private Graph<ServiceRequest> requestGraph = new Graph<ServiceRequest>();
        private BinarySearchTree bst = new BinarySearchTree();
        private MinHeap heap = new MinHeap();
        private Dictionary<ServiceRequest, UIElement> requestToUIElementMap = new Dictionary<ServiceRequest, UIElement>();

        // Defines the different view states of the application.
        private enum ViewState
        {
            List,
            Graph
        }
        private ViewState currentView = ViewState.List;

        public ServiceRequestStatus()
        {
            InitializeComponent();
            LoadServiceRequests();
            InitializeViewControls();

            // Add SizeChanged event handler for the GraphCanvas
            GraphCanvas.SizeChanged += (s, e) =>
            {
                if (currentView == ViewState.Graph && GraphCanvas.ActualWidth > 0 && GraphCanvas.ActualHeight > 0)
                {
                    UpdateGraph();
                }
            };
        }

        private void InitializeViewControls()
        {
            SetViewState(ViewState.List);
        }

        private void LoadServiceRequests()
        {
            // Loads a set of predefined service requests.
            serviceRequests = new List<ServiceRequest>
            {
                new ServiceRequest { Id = 1, Description = "Burst water pipe in Umlazi", Status = "Pending", Priority = 1, Progress = 20 },
                new ServiceRequest { Id = 2, Description = "Non-functional streetlights in Chatsworth", Status = "In Progress", Priority = 2, Progress = 50 },
                new ServiceRequest { Id = 3, Description = "Pothole on M4 Highway near Durban North", Status = "Completed", Priority = 1, Progress = 100 },
                new ServiceRequest { Id = 4, Description = "Uncollected waste in Phoenix", Status = "Pending", Priority = 3, Progress = 10 },
                new ServiceRequest { Id = 5, Description = "Traffic light malfunction at Warwick Junction", Status = "Pending", Priority = 2, Progress = 40 },
                new ServiceRequest { Id = 6, Description = "Illegal dumping in Sydenham", Status = "In Progress", Priority = 2, Progress = 60 },
                new ServiceRequest { Id = 7, Description = "Blocked stormwater drain in Westville", Status = "Pending", Priority = 1, Progress = 15 },
                new ServiceRequest { Id = 8, Description = "Vandalized park equipment in Musgrave", Status = "Pending", Priority = 3, Progress = 5 }
            };


            foreach (var request in serviceRequests)
            {
                requestGraph.AddNode(request);// Add the request to the dependency graph.
                bst.Insert(request);// Insert the request into the BST.
                heap.Insert(request);// Add the request to the heap.
            }

            requestGraph.AddEdge(serviceRequests[0], serviceRequests[2]);
            requestGraph.AddEdge(serviceRequests[1], serviceRequests[4]);

            ServiceRequestDataGrid.ItemsSource = serviceRequests;
        }
        // Switches between list and graph views.
        private void SetViewState(ViewState newState)
        {
            currentView = newState;

            switch (newState)
            {//list of what is must not be shown when the grapah is shown
                case ViewState.List:
                    ServiceRequestDataGrid.Visibility = Visibility.Visible;
                    StatusChangeSection.Visibility = Visibility.Visible;
                    DependenciesSection.Visibility = Visibility.Visible;
                    ShowGraphButton.Visibility = Visibility.Visible;
                    GraphCanvas.Visibility = Visibility.Collapsed;
                    BackButton.Visibility = Visibility.Collapsed;
                    break;

                case ViewState.Graph:
                    GraphCanvas.Visibility = Visibility.Visible;
                    BackButton.Visibility = Visibility.Visible;
                    ServiceRequestDataGrid.Visibility = Visibility.Collapsed;
                    StatusChangeSection.Visibility = Visibility.Collapsed;
                    DependenciesSection.Visibility = Visibility.Collapsed;
                    ShowGraphButton.Visibility = Visibility.Collapsed;

                    Dispatcher.BeginInvoke(new Action(() =>
                    {
                        UpdateGraph();
                    }), DispatcherPriority.Render);
                    break;
            }
        }
        // Updates the graph view to reflect the latest progress.
        private void UpdateGraph()
        {
            GraphCanvas.Children.Clear();
            requestToUIElementMap.Clear();

            double canvasHeight = GraphCanvas.ActualHeight;
            double canvasWidth = GraphCanvas.ActualWidth;
            int barWidth = 60;
            int barSpacing = 40;
            int topMargin = 60;
            int bottomMargin = 60;
            int sideMargin = 80;

            // Title
            TextBlock title = new TextBlock
            {
                Text = "Service Request Progress",
                Foreground = Brushes.White,
                FontSize = 20,
                FontWeight = FontWeights.Bold
            };
            Canvas.SetLeft(title, sideMargin);
            Canvas.SetTop(title, 10);
            GraphCanvas.Children.Add(title);

            // Calculate available height for bars
            double availableHeight = canvasHeight - topMargin - bottomMargin;

            // Y-axis
            Line yAxis = new Line
            {
                X1 = sideMargin,
                Y1 = topMargin,
                X2 = sideMargin,
                Y2 = canvasHeight - bottomMargin,
                Stroke = Brushes.White,
                StrokeThickness = 1
            };
            GraphCanvas.Children.Add(yAxis);

            // X-axis
            Line xAxis = new Line
            {
                X1 = sideMargin,
                Y1 = canvasHeight - bottomMargin,
                X2 = canvasWidth - sideMargin,
                Y2 = canvasHeight - bottomMargin,
                Stroke = Brushes.White,
                StrokeThickness = 1
            };
            GraphCanvas.Children.Add(xAxis);

            // Y-axis labels and grid lines
            for (int i = 0; i <= 100; i += 20)
            {
                double yPos = canvasHeight - bottomMargin - (i * availableHeight / 100);

                TextBlock label = new TextBlock
                {
                    Text = $"{i}%",
                    Foreground = Brushes.White,
                    FontSize = 12
                };
                Canvas.SetLeft(label, sideMargin - 30);
                Canvas.SetTop(label, yPos - 10);
                GraphCanvas.Children.Add(label);

                Line gridLine = new Line
                {
                    X1 = sideMargin,
                    Y1 = yPos,
                    X2 = canvasWidth - sideMargin,
                    Y2 = yPos,
                    Stroke = new SolidColorBrush(Color.FromRgb(70, 70, 70)),
                    StrokeThickness = 0.5
                };
                GraphCanvas.Children.Add(gridLine);
            }

            // Draw bars
            for (int i = 0; i < serviceRequests.Count; i++)
            {
                var request = serviceRequests[i];
                double barHeight = (request.Progress * availableHeight / 100);
                double xPos = sideMargin + i * (barWidth + barSpacing) + barSpacing;
                double yPos = canvasHeight - bottomMargin - barHeight;

                Rectangle bar = new Rectangle
                {
                    Width = barWidth,
                    Height = Math.Max(barHeight, 1), 
                    Fill = GetBarColor(request.Status),
                    Stroke = Brushes.White,
                    StrokeThickness = 1,
                    RadiusX = 4,
                    RadiusY = 4
                };
                Canvas.SetLeft(bar, xPos);
                Canvas.SetTop(bar, yPos);
                GraphCanvas.Children.Add(bar);

                TextBlock percentLabel = new TextBlock
                {
                    Text = $"{request.Progress}%",
                    Foreground = Brushes.White,
                    FontSize = 12
                };
                Canvas.SetLeft(percentLabel, xPos + (barWidth - 20) / 2);
                Canvas.SetTop(percentLabel, yPos - 20);
                GraphCanvas.Children.Add(percentLabel);

                TextBlock descLabel = new TextBlock
                {
                    Text = request.Description,
                    Foreground = Brushes.White,
                    FontSize = 12,
                    TextWrapping = TextWrapping.Wrap,
                    Width = barWidth + barSpacing,
                    TextAlignment = TextAlignment.Center
                };
                Canvas.SetLeft(descLabel, xPos - barSpacing / 2);
                Canvas.SetTop(descLabel, canvasHeight - bottomMargin + 10);
                GraphCanvas.Children.Add(descLabel);

                requestToUIElementMap[request] = bar;
            }

            AddLegend();
        }

        private void AddLegend()
        {
            Border legendBorder = new Border
            {
                Background = new SolidColorBrush(Color.FromRgb(45, 45, 45)),
                BorderBrush = new SolidColorBrush(Color.FromRgb(70, 70, 70)),
                BorderThickness = new Thickness(1),
                CornerRadius = new CornerRadius(4),
                Padding = new Thickness(10)
            };

            StackPanel legend = new StackPanel
            {
                Orientation = Orientation.Vertical
            };

            TextBlock legendTitle = new TextBlock
            {
                Text = "Status Legend",
                Foreground = Brushes.White,
                FontWeight = FontWeights.Bold,
                Margin = new Thickness(0, 0, 0, 5)
            };
            legend.Children.Add(legendTitle);

            var statuses = new Dictionary<string, Brush>
            {
                { "Pending", Brushes.Orange },
                { "In Progress", Brushes.DeepSkyBlue },
                { "Completed", Brushes.LimeGreen }
            };

            foreach (var status in statuses)
            {
                StackPanel item = new StackPanel
                {
                    Orientation = Orientation.Horizontal,
                    Margin = new Thickness(0, 2, 0, 2)
                };

                Rectangle colorBox = new Rectangle
                {
                    Width = 16,
                    Height = 16,
                    Fill = status.Value,
                    Stroke = Brushes.White,
                    StrokeThickness = 1,
                    RadiusX = 2,
                    RadiusY = 2,
                    Margin = new Thickness(0, 0, 5, 0)
                };

                TextBlock label = new TextBlock
                {
                    Text = status.Key,
                    Foreground = Brushes.White,
                    VerticalAlignment = VerticalAlignment.Center
                };

                item.Children.Add(colorBox);
                item.Children.Add(label);
                legend.Children.Add(item);
            }

            legendBorder.Child = legend;
            Canvas.SetTop(legendBorder, 10);
            Canvas.SetRight(legendBorder, 10);
            GraphCanvas.Children.Add(legendBorder);
        }
        //key colours
        private Brush GetBarColor(string status)
        {
            switch (status)
            {
                case "Pending":
                    return Brushes.Orange;
                case "In Progress":
                    return Brushes.DeepSkyBlue;
                case "Completed":
                    return Brushes.LimeGreen;
                default:
                    return Brushes.Gray;
            }
        }
        //changes from list to grpah when button is clicked 
        private void ShowGraphButton_Click(object sender, RoutedEventArgs e)
        {
            SetViewState(ViewState.Graph);
        }
        // to go back to the list
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            SetViewState(ViewState.List);
        }
        //code to view what task is dependent on what 
        private void ViewDependenciesButton_Click_1(object sender, RoutedEventArgs e)
        {
            DependencyTreeView.Items.Clear();

            foreach (var request in requestGraph.GetAllNodes())
            {
                var dependencies = requestGraph.GetNeighbors(request);
                if (dependencies.Count > 0)
                {
                    var rootNode = new TreeViewItem
                    {
                        Header = new TextBlock
                        {
                            Text = $"{request.Description} depends on:",
                            FontWeight = FontWeights.Bold,
                            Foreground = Brushes.White
                        }
                    };

                    foreach (var dependentRequest in dependencies)
                    {
                        var childNode = new TreeViewItem
                        {
                            Header = new TextBlock
                            {
                                Text = $"• {dependentRequest.Description}",
                                Margin = new Thickness(5, 0, 0, 0),
                                Foreground = Brushes.White
                            }
                        };
                        rootNode.Items.Add(childNode);
                    }

                    DependencyTreeView.Items.Add(rootNode);
                }
            }
        }
        // to change a task status
        private void ChangeStatusButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedRequest = ServiceRequestDataGrid.SelectedItem as ServiceRequest;
            if (selectedRequest == null)
            {
                MessageBox.Show("Please select a service request to update.", "No Selection", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var selectedStatus = (StatusComboBox.SelectedItem as ComboBoxItem)?.Content as string;
            if (string.IsNullOrEmpty(selectedStatus))
            {
                MessageBox.Show("Please select a new status.", "No Status Selected", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            selectedRequest.Status = selectedStatus;

            switch (selectedStatus)
            {
                case "Pending":
                    selectedRequest.Progress = 0;
                    break;
                case "In Progress":
                    selectedRequest.Progress = 50;
                    break;
                case "Completed":
                    selectedRequest.Progress = 100;
                    break;
            }

            ServiceRequestDataGrid.Items.Refresh();

            if (currentView == ViewState.Graph)
            {
                UpdateGraph();//changes on the graph as well
            }

            MessageBox.Show($"Status updated to {selectedStatus} successfully!", "Status Updated", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void MainMenuButton_Click(object sender, RoutedEventArgs e)
        {// to go back to the main window
            MessageBoxResult result = MessageBox.Show(
                "Are you sure you want to return to the main menu?",
                "Confirm Navigation",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                var mainWindow = new MainWindow();
                mainWindow.Show();
                this.Hide();
            }
        }
    }
}