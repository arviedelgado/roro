namespace Roro.Workspace.ViewModels
{
    public sealed class FlowRunnerFileVM : WorkspaceItemVM
    {
        public string Name { get; }

        public string Path { get; }

        public bool IsSelected { get => Get_IsSelected(); set => Set_IsSelected(value); }

        // non-public

        internal FlowRunnerFileVM(WorkspaceVM workspace, FlowExplorerFileVM explorerFile) : base(workspace)
        {
            Name = explorerFile.Name;
            Path = explorerFile.Path;
        }

        #region IsSelected

        private bool Get_IsSelected()
        {
            return _workspace.FlowRunner.SelectedItem == this;
        }

        private void Set_IsSelected(bool value)
        {
            if (IsSelected is false && value is true)
            {
                _workspace.FlowRunner.SelectedItem = this;
            }
        }

        #endregion
    }
}
