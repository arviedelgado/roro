using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;

namespace Roro.Activities
{
    public class ActivityTypeConverter : ExpandableObjectConverter
    {
        public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
        {
            var oldPropDescs = base.GetProperties(context, value, attributes);
            var newPropDescs = new List<WriteablePropertyDescriptor>();
            foreach (PropertyDescriptor propDesc in oldPropDescs)
            {
                var editPropDesc = new WriteablePropertyDescriptor(propDesc);
                // Display Name
                editPropDesc.WriteableDisplayName = new string(
                    editPropDesc.WriteableDisplayName.ToCharArray().
                    SelectMany(c => (Char.IsUpper(c) ? " " : "") + c).ToArray()).Trim();
                // Category
                if ((typeof(InArgument)).IsAssignableFrom(editPropDesc.PropertyType))
                {
                    editPropDesc.WriteableCategory = "Inputs";
                }
                if ((typeof(OutArgument)).IsAssignableFrom(editPropDesc.PropertyType))
                {
                    editPropDesc.WriteableCategory = "Outputs";
                }
                if (editPropDesc.WriteableCategory != "Misc")
                {
                    newPropDescs.Add(editPropDesc);
                }
            }
            return new PropertyDescriptorCollection(newPropDescs.ToArray(), true);
        }
    }
}
