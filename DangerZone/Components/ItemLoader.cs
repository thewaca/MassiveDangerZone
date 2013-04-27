using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Artemis;
using Artemis.Interface;
using Newtonsoft.Json;

namespace DangerZone.Components
{
    public class ItemLoader
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

        public readonly Dictionary<string, Entity> items = new Dictionary<string, Entity>();

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
                        reader.Read();
                        if (reader.TokenType == JsonToken.StartObject)
                        {
                            var item = entityWorld.CreateEntity();

                            Debug.WriteLine("creating item:" + itemName);

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

                                        if (type == typeof (Item))
                                        {
                                            ((Item) component).name = itemName;
                                        }

                                        item.AddComponent(component);

                                    }
                                }
                                else if (reader.TokenType == JsonToken.EndObject)
                                {
                                    break;
                                }
                            }

                            if(!item.HasComponent<Item>()) throw new Exception("Invalid item definition");
                            this.items[itemName] = item;

                            item.Refresh();
                        }
                    }
                }
            }
        }
    }
}
