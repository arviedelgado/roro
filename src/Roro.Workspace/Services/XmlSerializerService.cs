using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace Roro.Workspace.Services
{
    public abstract class XmlSerializerService
    {
        public static T ToObject<T>(string xml)
        {
            using var reader = new StringReader(xml);
            return (T)GetOrCreateSerializer<T>().Deserialize(reader);
        }

        public static string ToString<T>(T obj)
        {
            using var writer = new StringWriter();
            GetOrCreateSerializer<T>().Serialize(writer, obj);
            return writer.ToString();
        }

        public static void ClearCache()
        {
            _cachedSerializers.Clear();
        }

        // non-public

        private static Dictionary<Type, XmlSerializer> _cachedSerializers;

        private static XmlSerializer GetOrCreateSerializer<T>()
        {
            if (_cachedSerializers is null)
            {
                _cachedSerializers = new Dictionary<Type, XmlSerializer>();
            }
            if (!_cachedSerializers.TryGetValue(typeof(T), out XmlSerializer serializer))
            {
                serializer = new XmlSerializer(typeof(T));
                _cachedSerializers.Add(typeof(T), serializer);
            }
            return serializer;
        }
    }
}
