using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using Artemis;
using Artemis.Interface;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;

namespace MassiveDangerZone.Items
{
    class ItemLoader
    {
        private static readonly Dictionary<string, Type> classNames = new Dictionary<string, Type>();

        static ItemLoader()
        {
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                foreach (var type in assembly.GetTypes())
                {
                    foreach (SerializableComponent attribute in type.GetCustomAttributes(typeof(SerializableComponent), true))
                    {
                        classNames[attribute.name] = type;
                    }
                }
            }
        }

        public readonly EntityWorld entityWorld;

        public ItemLoader(EntityWorld entityWorld)
        {
            this.entityWorld = entityWorld;
        }

        public void loadFile(string fileName)
        {
            var serializer = new JsonSerializer();
            var re = new StreamReader(fileName);
            using (var reader = new JsonTextReader(re))
            {
                while (reader.Read())
                {
                    if (reader.TokenType == JsonToken.PropertyName)
                    {
                        var itemName = (string)reader.Value;
                        var entity = entityWorld.CreateEntity();

                        Debug.WriteLine("creating item:" + itemName);

                        reader.Read();
                        if (reader.TokenType == JsonToken.StartObject)
                        {
                            while (reader.Read())
                            {
                                if (reader.TokenType == JsonToken.PropertyName)
                                {
                                    var componentName = (string)reader.Value;
                                    Debug.WriteLine("adding component: " + componentName);

                                    reader.Read();
                                    if (reader.TokenType == JsonToken.StartObject)
                                    {
                                        var type = ItemLoader.classNames[componentName];
                                        var component = (IComponent) serializer.Deserialize(reader, type);
                                        entity.AddComponent(component);
                                    }
                                }
                                else if (reader.TokenType == JsonToken.EndObject)
                                {
                                    break;
                                }
                            }
                        }

                        entity.Refresh();
                    } 
                }
            }
        }
    }
}
