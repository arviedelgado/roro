namespace Roro.Workspace.ViewModels
{
    public sealed class FlowExplorerRootFolderVM : FlowExplorerFolderVM
    {
        public override string Name => string.Empty;

        public override string ParentFolderPath => null;

        public override string Path => string.Empty;

        // non-public

        internal FlowExplorerRootFolderVM(WorkspaceVM workspace) : base(workspace, string.Empty)
        {

        }
    }
}
