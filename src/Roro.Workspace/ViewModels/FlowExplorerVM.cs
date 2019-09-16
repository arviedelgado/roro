using Roro.Workspace.Framework;
using System;

namespace Roro.Workspace.ViewModels
{
    public sealed class FlowExplorerVM : WorkspaceItemVM
    {
        public FlowExplorerRootFolderVM RootFolder { get; }

        public FlowExplorerItemVM SelectedItem { get => Get_SelectedItem(); set => Set_SelectedItem(value); }

        public Command RefreshCommand { get; }

        // non-public

        internal FlowExplorerVM(WorkspaceVM workspace) : base(workspace)
        {
            RootFolder = new FlowExplorerRootFolderVM(_workspace);
            RefreshCommand = new Command(CanExecuteRefresh, ExecuteRefresh);
        }

        #region SelectedItem

        private FlowExplorerItemVM _selectedItem;

        private FlowExplorerItemVM Get_SelectedItem()
        {
            return _selectedItem ?? RootFolder;
        }

        private void Set_SelectedItem(FlowExplorerItemVM value)
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

        #region RefreshCommand

        private bool CanExecuteRefresh()
        {
            return true;
        }

        private void ExecuteRefresh()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
