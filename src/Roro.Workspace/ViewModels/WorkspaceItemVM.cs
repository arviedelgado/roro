using Roro.Workspace.Framework;
using System;

namespace Roro.Workspace.ViewModels
{
    public abstract class WorkspaceItemVM : ViewModel
    {
        // non-public

        protected readonly WorkspaceVM _workspace;

        protected WorkspaceItemVM(WorkspaceVM workspace)
        {
            _workspace = workspace ?? throw new ArgumentNullException(nameof(workspace));
        }
    }
}
