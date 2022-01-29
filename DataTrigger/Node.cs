using System.Collections.ObjectModel;
using System.ComponentModel;

namespace TreeViewCollection
{
    public class Node : INotifyPropertyChanged
    {
        protected string m_name;
        protected bool m_visibleSelf;
        protected bool m_highlightSelf;
        protected bool m_visibleChildOrSelf;

        public Node()
        {
            m_name = "unknown";
            m_visibleSelf = true;
            m_highlightSelf = false;
            m_visibleChildOrSelf = true;
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

        public bool VisibleSelf
        {
            get => m_visibleSelf;
            set
            {
                m_visibleSelf = value;
                NotifyPropertyChanged(nameof(VisibleSelf));
            }
        }

        public bool HighlightSelf
        {
            get => m_highlightSelf;
            set
            {
                m_highlightSelf = value;
                NotifyPropertyChanged(nameof(HighlightSelf));
            }
        }

        public ObservableCollection<Node> Items { get; } = new ObservableCollection<Node>();

        public bool VisibleChildOrSelf
        {
            get => m_visibleChildOrSelf;
            set
            {
                m_visibleChildOrSelf = value;
                NotifyPropertyChanged(nameof(VisibleChildOrSelf));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public virtual void CheckVisibility(string searchText)
        {
            VisibleSelf = m_name.ToLower().Contains(searchText.ToLower());
            HighlightSelf = false;

            m_visibleChildOrSelf = m_visibleSelf;

            foreach (var node in Items)
            {
                node.CheckVisibility(searchText);
                m_visibleChildOrSelf |= node.m_visibleChildOrSelf;
            }

            NotifyPropertyChanged(nameof(VisibleChildOrSelf));
        }

        public virtual void CheckHighlight(string searchText)
        {
            HighlightSelf = m_name.ToLower().Contains(searchText.ToLower());
            VisibleSelf = true;

            m_visibleChildOrSelf = true;

            foreach (var node in Items)
            {
                node.CheckHighlight(searchText);
                m_visibleChildOrSelf |= node.m_visibleChildOrSelf;
            }

            NotifyPropertyChanged(nameof(VisibleChildOrSelf));
        }
    }
}
