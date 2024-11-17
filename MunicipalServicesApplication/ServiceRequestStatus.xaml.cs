using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace MunicipalServicesApplication
{
    // Represents a service request with properties for ID, description, status, priority, and progress
    public class ServiceRequest : IComparable<ServiceRequest>
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public int Priority { get; set; }
        public int Progress { get; set; }

        // Used for comparing service requests based on their priority
        public int CompareTo(ServiceRequest other)
        {
            if (other == null) return 1;
            return this.Priority.CompareTo(other.Priority);
        }

        // Returns a string representation of the service request
        public override string ToString()
        {
            return $"{Description} ({Status})";
        }
    }

    // Represents a graph with generic nodes and edges
    public class Graph<T>
    {
        private Dictionary<T, List<T>> _adjacencyList;

        public Graph()
        {
            _adjacencyList = new Dictionary<T, List<T>>();
        }

        // Adds a node to the graph
        public void AddNode(T node)
        {
            if (!_adjacencyList.ContainsKey(node))
            {
                _adjacencyList[node] = new List<T>();
            }
        }

        // Adds an edge between two nodes
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

        // Gets the neighbors of a given node
        public List<T> GetNeighbors(T node)
        {
            if (_adjacencyList.ContainsKey(node))
            {
                return _adjacencyList[node];
            }
            return new List<T>();
        }

        // Checks if the graph contains a specific node
        public bool ContainsNode(T node)
        {
            return _adjacencyList.ContainsKey(node);
        }

        // Gets all nodes in the graph
        public IEnumerable<T> GetAllNodes()
        {
            return _adjacencyList.Keys;
        }
    }

    // Represents a node in a binary search tree (BST) for service requests
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

    // Implements a binary search tree (BST) for managing service requests
    public class BinarySearchTree
    {
        public BSTNode Root { get; set; }

        // Inserts a service request into the BST
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

        // Recursive helper for inserting a node into the BST
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

        // Performs an in-order traversal of the BST and returns a sorted list of service requests
        public List<ServiceRequest> InOrderTraversal()
        {
            List<ServiceRequest> result = new List<ServiceRequest>();
            InOrderTraversalRec(Root, result);
            return result;
        }

        // Recursive helper for in-order traversal
        private void InOrderTraversalRec(BSTNode node, List<ServiceRequest> result)
        {
            if (node == null) return;

            InOrderTraversalRec(node.Left, result);
            result.Add(node.Request);
            InOrderTraversalRec(node.Right, result);
        }
    }

    // Implements a min-heap for managing service requests based on their progress
    public class MinHeap
    {
        private List<ServiceRequest> heap = new List<ServiceRequest>();

        // Inserts a service request into the heap
        public void Insert(ServiceRequest request)
        {
            heap.Add(request);
            HeapifyUp(heap.Count - 1);
        }

        // Extracts the service request with the smallest progress value
        public ServiceRequest ExtractMin()
        {
            if (heap.Count == 0) return null;

            ServiceRequest min = heap[0];
            heap[0] = heap[heap.Count - 1];
            heap.RemoveAt(heap.Count - 1);
            HeapifyDown(0);
            return min;
        }

        // Ensures heap properties are maintained after insertion
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

        // Ensures heap properties are maintained after extraction
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

        // Swaps two elements in the heap
        private void Swap(int i, int j)
        {
            ServiceRequest temp = heap[i];
            heap[i] = heap[j];
            heap[j] = temp;
        }

        // Returns all service requests in the heap
        public List<ServiceRequest> GetAllRequests()
        {
            return new List<ServiceRequest>(heap);
        }
    }

    // Represents the main window of the application
    public partial class ServiceRequestStatus : Window
    {
        private List<ServiceRequest> serviceRequests;
        private Graph<ServiceRequest> requestGraph = new Graph<ServiceRequest>();
        private BinarySearchTree bst = new BinarySearchTree();
        private MinHeap heap = new MinHeap();
        private Dictionary<ServiceRequest, UIElement> requestToUIElementMap = new Dictionary<ServiceRequest, UIElement>();

        // Enum to track the current view state
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
        }

        // Initializes the view controls, defaulting to the list view
        private void InitializeViewControls()
        {
            SetViewState(ViewState.List);
        }

        // Sets the current view state (List or Graph)
        private void SetViewState(ViewState newState)
        {
            currentView = newState;

            switch (newState)
            {
                case ViewState.List:
                    // Show list view elements and hide graph view elements
                    ServiceRequestDataGrid.Visibility = Visibility.Visible;
                    StatusChangeSection.Visibility = Visibility.Visible;
                    DependenciesSection.Visibility = Visibility.Visible;
                    ShowGraphButton.Visibility = Visibility.Visible;
                    GraphCanvas.Visibility = Visibility.Collapsed;
                    BackButton.Visibility = Visibility.Collapsed;
                    break;

                case ViewState.Graph:
                    // Show graph view elements and hide list view elements
                    GraphCanvas.Visibility = Visibility.Visible;
                    BackButton.Visibility = Visibility.Visible;
                    ServiceRequestDataGrid.Visibility = Visibility.Collapsed;
                    StatusChangeSection.Visibility = Visibility.Collapsed;
                    DependenciesSection.Visibility = Visibility.Collapsed;
                    ShowGraphButton.Visibility = Visibility.Collapsed;
                    UpdateGraph();
                    break;
            }
        }

        // Loads initial service requests and sets up the graph, BST, and heap
        private void LoadServiceRequests()
        {
            serviceRequests = new List<ServiceRequest>
            {
                new ServiceRequest { Id = 1, Description = "Water leak", Status = "Pending", Priority = 1, Progress = 30 },
                new ServiceRequest { Id = 2, Description = "Street light not working", Status = "In Progress", Priority = 2, Progress = 60 },
                new ServiceRequest { Id = 3, Description = "Pothole repair", Status = "Completed", Priority = 1, Progress = 100 },
                new ServiceRequest { Id = 4, Description = "Garbage pickup", Status = "Pending", Priority = 3, Progress = 10 },
                new ServiceRequest { Id = 5, Description = "Traffic light issue", Status = "Pending", Priority = 2, Progress = 50 }
            };

            foreach (var request in serviceRequests)
            {
                requestGraph.AddNode(request);
                bst.Insert(request);
                heap.Insert(request);
            }

            // Add example dependencies between requests
            requestGraph.AddEdge(serviceRequests[0], serviceRequests[2]);
            requestGraph.AddEdge(serviceRequests[1], serviceRequests[4]);

            ServiceRequestDataGrid.ItemsSource = serviceRequests;
        }

        // Event handler for the Show Graph button
        private void ShowGraphButton_Click(object sender, RoutedEventArgs e)
        {
            SetViewState(ViewState.Graph);
        }

        // Event handler for the Back button
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            SetViewState(ViewState.List);
        }

        // Updates the graph view with dynamic bar charts based on service request progress
        private void UpdateGraph()
        {
            // Clear any existing UI elements in the graph canvas
            GraphCanvas.Children.Clear();
            requestToUIElementMap.Clear();

            int barWidth = 50; // Width of each progress bar
            int barSpacing = 20; // Spacing between progress bars
            int legendSpacing = 100; // Space reserved for the legend

            // Loop through all service requests to create and display progress bars
            for (int i = 0; i < serviceRequests.Count; i++)
            {
                var request = serviceRequests[i];
                // Calculate the height of the bar based on the progress percentage
                double barHeight = GraphCanvas.ActualHeight * request.Progress / 100.0;

                // Create a rectangle to represent the progress bar
                Rectangle bar = new Rectangle
                {
                    Width = barWidth,
                    Height = barHeight,
                    Fill = GetBarColor(request.Status), // Set the fill color based on status
                    Stroke = Brushes.Black // Add a black border for better visibility
                };

                // Calculate the position of the bar in the canvas
                double xPosition = i * (barWidth + barSpacing) + legendSpacing;
                double yPosition = GraphCanvas.ActualHeight - barHeight;

                // Place the bar in the canvas
                Canvas.SetLeft(bar, xPosition);
                Canvas.SetTop(bar, yPosition);
                GraphCanvas.Children.Add(bar);

                // Store the mapping between the service request and its UI element
                requestToUIElementMap[request] = bar;
            }

            // Add a legend to explain the bar colors
            AddLegend();
        }

        // Adds a legend to explain the meaning of bar colors in the graph view
        private void AddLegend()
        {
            StackPanel legend = new StackPanel { Orientation = Orientation.Vertical };

            // Define the mapping of statuses to their corresponding colors
            Dictionary<string, Brush> statusColors = new Dictionary<string, Brush>
    {
        { "Pending", Brushes.Orange },
        { "In Progress", Brushes.Blue },
        { "Completed", Brushes.Green }
    };

            // Create a legend item for each status-color pair
            foreach (var status in statusColors)
            {
                StackPanel legendItem = new StackPanel { Orientation = Orientation.Horizontal };

                // Add a colored rectangle to represent the status
                Rectangle colorBox = new Rectangle
                {
                    Width = 20,
                    Height = 20,
                    Fill = status.Value,
                    Margin = new Thickness(5)
                };

                // Add a label with the name of the status
                TextBlock label = new TextBlock
                {
                    Text = status.Key,
                    VerticalAlignment = VerticalAlignment.Center,
                    Margin = new Thickness(5)
                };

                // Combine the rectangle and label into the legend item
                legendItem.Children.Add(colorBox);
                legendItem.Children.Add(label);
                legend.Children.Add(legendItem);
            }

            // Position the legend in the top-left corner of the canvas
            Canvas.SetTop(legend, 10);
            Canvas.SetLeft(legend, 10);
            GraphCanvas.Children.Add(legend);
        }

        // Returns a color for the bar based on the status of the request
        private Brush GetBarColor(string status)
        {
            // Map each status to a specific color
            switch (status)
            {
                case "Pending":
                    return Brushes.Orange;
                case "In Progress":
                    return Brushes.Blue;
                case "Completed":
                    return Brushes.Green;
                default:
                    return Brushes.Gray; // Default color for unknown statuses
            }
        }
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
                            FontWeight = FontWeights.Bold
                        }
                    };

                    foreach (var dependentRequest in dependencies)
                    {
                        var childNode = new TreeViewItem
                        {
                            Header = new TextBlock
                            {
                                Text = $"• {dependentRequest.Description}",
                                Margin = new Thickness(5, 0, 0, 0)
                            }
                        };
                        rootNode.Items.Add(childNode);
                    }

                    DependencyTreeView.Items.Add(rootNode);
                }
            }
        }
    }
}