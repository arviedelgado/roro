using System.Collections.ObjectModel;
using System.Linq;

namespace Roro.Workspace.ViewModels
{
    public sealed class FlowRunnerVM : WorkspaceItemVM
    {
        public ReadOnlyObservableCollection<FlowRunnerFileVM> Items { get; }

        public FlowRunnerFileVM SelectedItem { get => Get_SelectedItem(); set => Set_SelectedItem(value); }

        // non-public

        internal FlowRunnerVM(WorkspaceVM workspace) : base(workspace)
        {
            _items = new ObservableCollection<FlowRunnerFileVM>();
            Items = new ReadOnlyObservableCollection<FlowRunnerFileVM>(_items);
        }

        #region Items

        private readonly ObservableCollection<FlowRunnerFileVM> _items;

        internal FlowRunnerFileVM Items_AddItem(FlowExplorerFileVM explorerFile)
        {
            if (Items.FirstOrDefault(x => x.Path.Equals(explorerFile.Path, System.StringComparison.OrdinalIgnoreCase))
                is FlowRunnerFileVM runnerFile)
            { }
            else
            {
                runnerFile = new FlowRunnerFileVM(_workspace, explorerFile);
                _items.Add(runnerFile);
            }
            return runnerFile;
        }

        internal void Items_RemoveItem(FlowRunnerFileVM editorFile)
        {
            _items.Remove(editorFile);
        }

        #endregion

        #region SelectedItem

        private FlowRunnerFileVM _selectedItem;

        private FlowRunnerFileVM Get_SelectedItem()
        {
            return _selectedItem;
        }

        private void Set_SelectedItem(FlowRunnerFileVM value)
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
