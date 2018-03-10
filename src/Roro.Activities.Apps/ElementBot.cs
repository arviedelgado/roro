using System;
using System.Collections.Generic;
using System.Linq;

namespace Roro.Activities.Apps
{
    public static class ElementBot
    {
        public static Element GetElement(this ActivityContext context, Input<ElementQuery> input)
        {
            var query = new ElementQuery(XmlSerializerHelper.ToObject<List<Condition>>(input.Value));
            if (query == null)
                throw new Exception("Input 'Element' is required.");

            var elements = WinContext.Shared.GetElementsFromQuery(query);

            if (elements.Count() == 0)
                throw new Exception("No element found.");
            if (elements.Count() > 1)
                throw new Exception("Too many elements found.");

            return elements.First();
        }

        public static int CountElements(this ActivityContext context, Input<ElementQuery> input)
        {
            var query = new ElementQuery(XmlSerializerHelper.ToObject<List<Condition>>(input.Value));
            if (query == null)
                throw new Exception("Input 'Element' is required.");

            var elements = WinContext.Shared.GetElementsFromQuery(query);

            return elements.Count();
        }

    }
}
