# TreeView
TreeView filtering using two different techniques

## CollectionView

Filtering using ICollectionView - doesn't work that well with TreeView - seems to show parent and siblings, instead of just relevant leaf.

## DataTrigger

Filtering using Visible - only shows parent and relevant leaf.

Code for DataTrigger version based on reply by https://stackoverflow.com/users/526957/agroskin in https://stackoverflow.com/questions/1313325/how-to-filter-a-wpf-treeview-hierarchy-using-an-icollectionview
