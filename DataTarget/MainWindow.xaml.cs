using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;

namespace TreeViewCollection
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private ObservableCollection<Node> m_nodes = new ObservableCollection<Node>();
        private string m_searchText;

        public MainWindow()
        {
            InitializeComponent();

            var branch1 = new Node() { Name = "Group 1" };
            branch1.Items.Add(new Node() { Name = "test 1" });
            branch1.Items.Add(new Node() { Name = "test 2" });
            branch1.Items.Add(new Node() { Name = "test 3" });
            var branch2 = new Node() { Name = "Group 2" };
            branch2.Items.Add(new Node() { Name = "test 1" });
            var node = new Node() { Name = "test 5" };
            node.Items.Add(new Node() { Name = "lower" });
            branch2.Items.Add(node);
            branch2.Items.Add(new Node() { Name = "nom" });
            var branch3 = new Node() { Name = "Group 3" };
            branch3.Items.Add(new Node() { Name = "test 1" });
            branch3.Items.Add(new Node() { Name = "test 7" });
            branch3.Items.Add(new Node() { Name = "test 8" });

            m_nodes.Add(branch1);
            m_nodes.Add(branch2);
            m_nodes.Add(branch3);

            DataContext = this;
        }

        public ObservableCollection<Node> Items => m_nodes;

        public string SearchText
        {
            get => m_searchText;
            set
            {
                m_searchText = value;
                NotifyPropertyChanged(nameof(SearchText));
                CheckVisibility(m_searchText);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void CheckVisibility(string searchText)
        {
            foreach(var node in m_nodes)
            {
                node.CheckVisibility(searchText);
            }
        }

        private void CheckHighlight(string searchText)
        {
            foreach (var node in m_nodes)
            {
                node.CheckHighlight(searchText);
            }
        }
    }
}
