using System.Collections.Generic;
using System.Linq;

namespace Roro.Activities.Apps
{
    public sealed class WinContext : Context
    {
        public static readonly WinContext Shared = new WinContext();

        public WinElement Target { get; private set; }

        private WinContext()
        {
            this.ProcessId = WinElement.GetRoot().ProcessId;
        }

        public override Element GetElementFromFocus()
        {
            return this.Target = WinElement.GetFromFocus();
        }

        public override Element GetElementFromPoint(int screenX, int screenY)
        {
            return this.Target = WinElement.GetFromPoint(screenX, screenY);
        }

        public override IEnumerable<Element> GetElementsFromQuery(ElementQuery query)
        {
            var result = new List<WinElement>();
            var candidates = new Queue<WinElement>();
            var targetPath = query.FirstOrDefault(x => x.Name == "Path")?.Value.ToString();
            if (targetPath == null) return result;

            var targetMainWindowName = query.FirstOrDefault(x => x.Name == "MainWindowName").Value?.ToString() ?? string.Empty;
            var targetWindowName = query.FirstOrDefault(x => x.Name == "WindowName").Value?.ToString() ?? string.Empty;
            var targetWindowCount = targetPath.Split('/').Count();
            
            candidates.Enqueue(WinElement.GetRoot());
            while (candidates.Count > 0)
            {
                var candidate = candidates.Dequeue();
                var candidatePath = candidate.Path;
                var candidateWindowCount = candidatePath.Split('/').Count(x => x == "window");
                if (targetPath.StartsWith(candidatePath))
                {
                    if (candidateWindowCount > 0)
                    {
                        if (candidate.MainWindowName != targetMainWindowName)
                        {
                            continue;
                        }
                    }
                    if (candidateWindowCount == targetWindowCount)
                    {
                        if (candidate.WindowName != targetWindowName)
                        {
                            continue;
                        }
                    }
                    if (targetPath.Equals(candidatePath))
                    {
                        if (candidate.TryQuery(query))
                        {
                            result.Add(candidate);
                        }
                    }
                    else
                    {
                        foreach (var child in candidate.Children)
                        {
                            candidates.Enqueue(child);
                        }
                    }
                }
            }

            return result;
        }
    }
}