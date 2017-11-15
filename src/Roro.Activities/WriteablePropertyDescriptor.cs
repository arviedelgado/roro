
using System;
using System.ComponentModel;

namespace Roro.Activities
{
    public sealed class WriteablePropertyDescriptor : PropertyDescriptor
    {
        private readonly PropertyDescriptor propDesc;

        public string WriteableCategory { get; set; }

        public override string Category => this.WriteableCategory;

        public string WriteableDisplayName { get; set; }

        public override string DisplayName => this.WriteableDisplayName;

        public WriteablePropertyDescriptor(PropertyDescriptor propDesc) : base(propDesc)
        {
            this.propDesc = propDesc;
            this.WriteableCategory = propDesc.Category;
            this.WriteableDisplayName = propDesc.DisplayName;
        }

        public override Type ComponentType => this.propDesc.ComponentType;

        public override bool IsReadOnly => this.propDesc.IsReadOnly;

        public override Type PropertyType => this.propDesc.PropertyType;

        public override bool CanResetValue(object component) => this.propDesc.CanResetValue(component);

        public override object GetValue(object component) => this.propDesc.GetValue(component);

        public override void ResetValue(object component) => this.propDesc.ResetValue(component);

        public override void SetValue(object component, object value) => this.propDesc.SetValue(component, value);

        public override bool ShouldSerializeValue(object component) => this.propDesc.ShouldSerializeValue(component);
    }
}