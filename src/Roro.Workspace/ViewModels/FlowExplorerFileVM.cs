using Roro.Workspace.Framework;
using System;

namespace Roro.Workspace.ViewModels
{
    public sealed class FlowExplorerFileVM : FlowExplorerItemVM
    {
        public Command OpenCommand { get; }

        // non-public

        internal FlowExplorerFileVM(WorkspaceVM workspace, string path) : base(workspace, path)
        {
            OpenCommand = new Command(CanExecuteOpen, ExecuteOpen);
        }

        #region OpenCommand

        private bool CanExecuteOpen()
        {
            return true;
        }

        private void ExecuteOpen()
        {
            _workspace.FlowEditor.Items_AddItem(this);
        }

        #endregion
    }
}
