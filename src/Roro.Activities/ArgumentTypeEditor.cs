using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms.Design;
using System.Windows.Forms;

namespace Roro.Activities
{
    public class ArgumentTypeEditor : UITypeEditor
    {
        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.Modal;
        }

        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            var svc = provider.GetService(typeof(IWindowsFormsEditorService)) as IWindowsFormsEditorService;
            var arg = value as Argument;
            using (ArgumentTypeEditorUI form = new ArgumentTypeEditorUI())
            {
                form.Value = arg.Value;
                if (svc.ShowDialog(form) == DialogResult.OK)
                {
                    arg.Value = form.Value;
                }
            }
            return arg.Clone();
        }
    }
}
