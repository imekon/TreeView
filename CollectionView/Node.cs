using System.Collections.ObjectModel;
using System.ComponentModel;

namespace TreeViewCollection
{
    public class Node : INotifyPropertyChanged
    {
        protected string m_name;

        public Node()
        {
            m_name = "unknown";
        }

        public string Name
        {
            get => m_name;
            set
            {
                m_name = value;
                NotifyPropertyChanged(nameof(Name));
            }
        }

        public ObservableCollection<Node> Items { get; } = new ObservableCollection<Node>();

        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public bool CheckSearch(string searchText)
        {
            if (string.IsNullOrEmpty(searchText))
                return true;

            if (m_name.ToLower().Contains(searchText.ToLower()))
                return true;

            foreach(var node in Items)
            {
                if (node.CheckSearch(searchText))
                    return true;
            }

            return false;
        }
    }
}
