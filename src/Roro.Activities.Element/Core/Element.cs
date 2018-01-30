
using System;
using System.Drawing;
using System.Linq;

namespace Roro
{
    public abstract class Element
    {
        public abstract string Path { get; }

        public abstract Rectangle Bounds { get; }

        public Query GetQuery()
        {
            var query = new Query();
            var props = this.GetType().GetProperties().Where(attr => Attribute.IsDefined(attr, typeof(Property)));
            foreach (var prop in props)
            {
                query.Append(prop.Name, prop.GetValue(this));
            }
            return query;
        }

        public bool TryQuery(Query query)
        {
            foreach (var condition in query)
            {
                var prop = this.GetType().GetProperty(condition.Name);
                if (condition.Compare(prop.GetValue(this), prop.PropertyType))
                {
                    continue;
                }
                return false;
            }
            return true;
        }
    }
}
