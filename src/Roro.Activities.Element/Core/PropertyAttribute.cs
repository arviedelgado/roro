
using System;

namespace Roro
{
    [AttributeUsage(AttributeTargets.Property)]
    public class Property : Attribute
    {
        private bool Enabled { get; set; }

        public Property() : this(false)
        {

        }

        public Property(bool Enabled)
        {
            this.Enabled = Enabled;
        }
    }
}
