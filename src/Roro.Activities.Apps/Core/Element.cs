using System;
using System.Linq;

namespace Roro.Activities.Apps
{
    public abstract class Element
    {
        [Property]
        public abstract string Id { get; }

        [Property]
        public abstract string Name { get; }

        [Property]
        public abstract string ClassName { get; }

        [Property(Required: true)]
        public abstract string Type { get; }

        [Property(Required: true)]
        public abstract string Path { get; }

        [Property(Enabled: true)]
        public abstract string Value { get; set; }

        public abstract Rect Bounds { get; }

        [Property(Required: true)]
        public abstract string MainWindowName { get; }

        [Property(Required: true)]
        public abstract string WindowName { get; }

        public abstract void Focus();

        public abstract void Click();

        public ElementQuery GetQuery()
        {
            var query = new ElementQuery();
            var props = this.GetType().GetProperties().Where(attr => Attribute.IsDefined(attr, typeof(Property)));
            foreach (var prop in props)
            {
                var attr = Attribute.GetCustomAttribute(prop, typeof(Property), true) as Property;
                var condition = new Condition(prop.Name, prop.GetValue(this), attr.Enabled, attr.Required);
                query.Add(condition);
            }
            return query;
        }

        public bool TryQuery(ElementQuery query)
        {
            foreach (var condition in query)
            {
                if (condition.Required || condition.Enabled)
                {
                    var prop = this.GetType().GetProperty(condition.Name);
                    if (condition.Compare(prop.GetValue(this), prop.PropertyType))
                    {
                        continue;
                    }
                    return false;
                }
            }
            return true;
        }
    }
}
