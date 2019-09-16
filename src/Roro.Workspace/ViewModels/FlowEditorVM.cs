using System.Collections.ObjectModel;
using System.Linq;

namespace Roro.Workspace.ViewModels
{
    public sealed class FlowEditorVM : WorkspaceItemVM
    {
        public ReadOnlyObservableCollection<FlowEditorFileVM> Items { get; }

        public FlowEditorFileVM SelectedItem { get => Get_SelectedItem(); set => Set_SelectedItem(value); }

        // non-public

        internal FlowEditorVM(WorkspaceVM workspace) : base(workspace)
        {
            _items = new ObservableCollection<FlowEditorFileVM>();
            Items = new ReadOnlyObservableCollection<FlowEditorFileVM>(_items);
        }

        #region Items

        private readonly ObservableCollection<FlowEditorFileVM> _items;

        internal FlowEditorFileVM Items_AddItem(FlowExplorerFileVM explorerFile)
        {
            if (Items.FirstOrDefault(x => x.Path.Equals(explorerFile.Path, System.StringComparison.OrdinalIgnoreCase))
                is FlowEditorFileVM editorFile)
            { }
            else
            {
                editorFile = new FlowEditorFileVM(_workspace, explorerFile);
                _items.Add(editorFile);
            }
            return editorFile;
        }

        internal void Items_RemoveItem(FlowEditorFileVM editorFile)
        {
            _items.Remove(editorFile);            
        }

        #endregion

        #region SelectedItem

        private FlowEditorFileVM _selectedItem;

        private FlowEditorFileVM Get_SelectedItem()
        {
            return _selectedItem;
        }

        private void Set_SelectedItem(FlowEditorFileVM value)
        {
            var oldValue = SelectedItem;
            OnPropertyChanged(ref _selectedItem, value, nameof(SelectedItem));
            if (oldValue != null)
            {
                oldValue.OnPropertyChanged(nameof(oldValue.IsSelected));
            }
            if (value != null)
            {
                value.OnPropertyChanged(nameof(value.IsSelected));
            }
        }

        #endregion
    }
}
