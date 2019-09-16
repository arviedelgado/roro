using Roro.Workspace.Framework;

namespace Roro.Workspace.ViewModels
{
    public sealed class WorkspaceVM : ViewModel
    {
        public FlowExplorerVM FlowExplorer { get; }
       
        public FlowEditorVM FlowEditor { get; }

        public FlowRunnerVM FlowRunner { get; }

        public WorkspaceVM()
        {
            FlowExplorer = new FlowExplorerVM(this);
            FlowEditor = new FlowEditorVM(this);
            FlowRunner = new FlowRunnerVM(this);
        }
    }
}
