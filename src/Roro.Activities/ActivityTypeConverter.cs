using System;
using System.ComponentModel;
using System.Collections.Generic;

namespace Roro.Activities
{
    public class ActivityTypeConverter : ExpandableObjectConverter
    {
        public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
        {
            var act = value as Activity;
            var actProps = new List<PropertyDescriptor>();
            foreach (var inArg in act.Inputs)
            {
                var inArgPropDesc = new ArgumentPropertyDescriptor(act, inArg);
                inArgPropDesc.WriteableCategory = "Inputs";
                actProps.Add(inArgPropDesc);
            }
            foreach (var outArg in act.Outputs)
            {
                var outArgPropDesc = new ArgumentPropertyDescriptor(act, outArg);
                outArgPropDesc.WriteableCategory = "Outputs";
                actProps.Add(outArgPropDesc);
            }
            return new PropertyDescriptorCollection(actProps.ToArray(), true);
        }
    }
}
