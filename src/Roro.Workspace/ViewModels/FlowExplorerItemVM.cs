using Roro.Workspace.Framework;
using Roro.Workspace.Services;
using System.Linq;

namespace Roro.Workspace.ViewModels
{
    public abstract class FlowExplorerItemVM : WorkspaceItemVM
    {
        public virtual string Name { get; }

        public virtual string ParentFolderPath { get; }

        public virtual string Path => ParentFolderPath + IFlowExplorerService.PATH_SEPARATOR_CHAR + Name;

        public bool IsSelected { get => Get_IsSelected(); set => Set_IsSelected(value); }

        public Command AddFileCommand { get; }

        public Command AddFolderCommand { get; }

        public Command DeleteCommand { get; }

        // non-public

        protected FlowExplorerItemVM(WorkspaceVM workspace, string path) : base(workspace)
        {
            Name = path.Split(IFlowExplorerService.PATH_SEPARATOR_CHAR).Last();
            ParentFolderPath = string.Join(IFlowExplorerService.PATH_SEPARATOR_CHAR,
                                path.Split(IFlowExplorerService.PATH_SEPARATOR_CHAR).SkipLast(1));
        }

        #region IsSelected

        private bool Get_IsSelected()
        {
            return _workspace.FlowExplorer.SelectedItem == this;
        }

        private void Set_IsSelected(bool value)
        {
            if (IsSelected is false && value is true)
            {
                _workspace.FlowExplorer.SelectedItem = this;
            }
        }

        #endregion
    }
}
