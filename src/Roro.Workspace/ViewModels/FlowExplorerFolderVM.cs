using System.Collections.ObjectModel;

namespace Roro.Workspace.ViewModels
{
    public class FlowExplorerFolderVM : FlowExplorerItemVM
    {
        public ReadOnlyObservableCollection<FlowExplorerItemVM> Items { get; }

        // non-public

        internal FlowExplorerFolderVM(WorkspaceVM workspace, string path) : base(workspace, path)
        {
            _items = new ObservableCollection<FlowExplorerItemVM>();
            Items = new ReadOnlyObservableCollection<FlowExplorerItemVM>(_items);
        }

        #region Items

        private readonly ObservableCollection<FlowExplorerItemVM> _items;

        #endregion
    }
}
