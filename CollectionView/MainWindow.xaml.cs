using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Data;

namespace TreeViewCollection
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private ObservableCollection<Node> m_nodes = new ObservableCollection<Node>();
        private string m_searchText;
        private ICollectionView m_collectionView;

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

            m_collectionView = CollectionViewSource.GetDefaultView(m_nodes);
            m_collectionView.Filter = Filter;

            DataContext = this;
        }

        public ObservableCollection<Node> Items => m_nodes;

        public ICollectionView FilteredItems => m_collectionView;

        public string SearchText
        {
            get => m_searchText;
            set
            {
                m_searchText = value;
                NotifyPropertyChanged(nameof(SearchText));
                m_collectionView.Refresh();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private bool Filter(object o)
        {
            var node = o as Node;
            if (node == null)
                return false;

            if (string.IsNullOrEmpty(m_searchText))
                return true;

            return node.CheckSearch(m_searchText);
        }
    }
}
