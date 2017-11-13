using System;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace Roro.Workflow
{
    public sealed class DataContractSerializerHelper
    {
        private const string NamespacePrefix = "http://schemas.datacontract.org/2004/07/";

        private sealed class DataContractResolverHelper : DataContractResolver
        {
            public override Type ResolveName(string typeName, string typeNamespace, Type declaredType, DataContractResolver knownTypeResolver)
            {
                var name = typeName.Split('[').First();
                var args = typeName.Split('[').Last().Split(',', ']').Where(x => x.Length > 0).ToList();

                var typeNameFull = string.Format("{0}.{1}", typeNamespace.Replace(NamespacePrefix, string.Empty), name);
                foreach (var asm in AppDomain.CurrentDomain.GetAssemblies())
                {
                    if (asm.GetType(typeNameFull) is Type type)
                    {
                        if (type.IsGenericType)
                        {
                            // will not handle nested generic types.
                            var argTypes = args.ConvertAll(argType =>
                                this.ResolveName(argType, string.Empty, null, null)
                            ).ToArray();
                            type = type.MakeGenericType(argTypes);
                        }
                        return type;
                    }
                }
                return null;
            }

            public override bool TryResolveType(Type type, Type declaredType, DataContractResolver knownTypeResolver, out XmlDictionaryString typeName, out XmlDictionaryString typeNamespace)
            {
                typeName = new XmlDictionary().Add(type.ToString().Substring(type.Namespace.Length + 1));
                typeNamespace = new XmlDictionary().Add(NamespacePrefix + type.Namespace);
                return true;
            }
        }

        private sealed class DataContractSerializerSettingsHelper<T> : DataContractSerializerSettings
        {
            public DataContractSerializerSettingsHelper()
            {
                this.DataContractResolver = new DataContractResolverHelper();
            }
        }

        private static DataContractSerializer GetSerializer<T>()
        {
            return new DataContractSerializer(typeof(T), new DataContractSerializerSettingsHelper<T>());
        }

        public static string ToString<T>(T instance)
        {
            string result;
            using (var stream = new MemoryStream())
            {
                using (var reader = new StreamReader(stream))
                {
                    var serializer = GetSerializer<T>();
                    serializer.WriteObject(stream, instance);
                    stream.Seek(0, SeekOrigin.Begin);
                    result = reader.ReadToEnd();
                }
            }
            return OnSerialized(result);
        }

        private static string OnSerialized(string xml)
        {
            var xDoc = XDocument.Parse(xml);

            // Excluded all new members of the derived classes of Activity base class.
            xDoc.Descendants().Where(x => x.Name.LocalName == "Activity")
                .Elements().Where(x => x.Name.LocalName != "Inputs" && x.Name.LocalName != "Outputs")
                .Remove();

            return xDoc.ToString();
        }

        public static T ToObject<T>(string xml)
        {
            T result;
            using (var stream = new MemoryStream())
            {
                using (var writer = new StreamWriter(stream))
                {
                    Task.Run(() =>
                    {
                        writer.WriteAsync(xml);
                        writer.FlushAsync();
                    }).Wait();
                    var serializer = GetSerializer<T>();
                    stream.Seek(0, SeekOrigin.Begin);
                    result = (T)serializer.ReadObject(stream);
                }
            }
            return result;
        }
    }
}