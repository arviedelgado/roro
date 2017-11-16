using System;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace Roro.Activities
{
    [DataContract]
    public abstract class ActivityComponent : IComponent
    {
        [Browsable(false)]
        public ISite Site
        {
            get { return new ActivityComponentSite(this); }
            set { throw new NotImplementedException(); }
        }

        [Browsable(false)]
        public event EventHandler Disposed;

        public void Dispose()
        {
            if (this.Disposed != null)
            {
                this.Disposed.Invoke(null, EventArgs.Empty);
            }
        }
    }
}