using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Reflection;

namespace Roro.Activities
{
    public sealed class ActivityComponentSite : IMenuCommandService, ISite
    {
        private readonly IComponent component;

        public ActivityComponentSite(IComponent component) => this.component = component;

        public void AddCommand(MenuCommand command) => throw new NotImplementedException();

        public void AddVerb(DesignerVerb verb) => throw new NotImplementedException();

        public MenuCommand FindCommand(CommandID commandID) => throw new NotImplementedException();

        public bool GlobalInvoke(CommandID commandID) => throw new NotImplementedException();

        public void RemoveCommand(MenuCommand command) => throw new NotImplementedException();

        public void RemoveVerb(DesignerVerb verb) => throw new NotImplementedException();

        public void ShowContextMenu(CommandID menuID, int x, int y) => throw new NotImplementedException();

        public DesignerVerbCollection Verbs
        {
            get
            {
                var verbs = new DesignerVerbCollection();
                var mInfos = this.component.GetType().GetMethods(BindingFlags.Public | BindingFlags.Instance);
                foreach (var mInfo in mInfos)
                {
                    var attrs = mInfo.GetCustomAttributes(typeof(BrowsableAttribute), true);
                    if (attrs == null || attrs.Length == 0)
                        continue;

                    if (!((BrowsableAttribute)attrs[0]).Browsable)
                        continue;

                    verbs.Add(new DesignerVerb(mInfo.Name, new EventHandler(VerbEventHandler)));
                }
                return verbs;
            }
        }

        private void VerbEventHandler(object sender, EventArgs e)
        {
            var verb = sender as DesignerVerb;
            var mInfos = this.component.GetType().GetMethods(BindingFlags.Public | BindingFlags.Instance);
            foreach (var mInfo in mInfos)
            {
                var attrs = mInfo.GetCustomAttributes(typeof(BrowsableAttribute), true);
                if (attrs == null || attrs.Length == 0)
                    continue;

                if (!((BrowsableAttribute)attrs[0]).Browsable)
                    continue;

                if (verb.Text == mInfo.Name)
                {
                    mInfo.Invoke(this.component, null);
                    return;
                }
            }
        }

        public IComponent Component => throw new NotImplementedException();

        public IContainer Container => null; // if use throw, instead of null, the Properties won't show in PropertyGrid.

        public bool DesignMode => throw new NotImplementedException();

        public string Name { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public object GetService(Type serviceType) => serviceType == typeof(IMenuCommandService) ? this : null;
    }
}