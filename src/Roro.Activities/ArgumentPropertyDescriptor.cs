using System;
using System.ComponentModel;
using System.Linq;

namespace Roro.Activities
{
    public class ArgumentPropertyDescriptor : PropertyDescriptor
    {
        private Activity act;

        private Argument arg;

        public string WriteableCategory { get; set; }

        public override string Category => this.WriteableCategory;

        public string WriteableDisplayName { get; set; }

        public override string DisplayName => this.WriteableDisplayName;

        public string WriteableDescription { get; set; }

        public override string Description => this.WriteableDescription;

        public ArgumentPropertyDescriptor(Activity act, Argument arg) : base(arg.Name, null)
        {
            this.act = act;
            this.arg = arg;
            this.WriteableCategory = base.Category;
            this.WriteableDescription = base.Description;
            this.WriteableDisplayName = new string(base.DisplayName.ToCharArray().SelectMany(c => (Char.IsUpper(c) ? " " : "") + c).ToArray());
        }

        public override string Name => this.arg.Name;

        public override bool ShouldSerializeValue(object component) => true;

        public override void ResetValue(object component) => this.arg.Value = string.Empty;

        public override bool IsReadOnly => false;

        public override Type PropertyType => this.arg.GetType();

        public override bool CanResetValue(object component) => true;

        public override Type ComponentType => this.act.GetType();

        public override void SetValue(object component, object value) => this.arg = value as Argument;

        public override object GetValue(object component) => this.arg;
    }
}
