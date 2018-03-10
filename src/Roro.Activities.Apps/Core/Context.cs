using System.Collections.Generic;
using System.Drawing;

namespace Roro.Activities.Apps
{
    public abstract class Context
    {
        public virtual Rectangle Viewport { get; protected set; }

        public virtual int ProcessId { get; protected set; }

        public abstract Element GetElementFromFocus();

        public abstract Element GetElementFromPoint(int screenX, int screenY);

        public abstract IEnumerable<Element> GetElementsFromQuery(ElementQuery query);
    }
}
