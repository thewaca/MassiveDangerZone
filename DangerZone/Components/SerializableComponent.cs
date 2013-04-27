using System;

namespace DangerZone.Components
{
    [AttributeUsage(AttributeTargets.Class)]
    class SerializableComponent:Attribute
    {
        public SerializableComponent(string name)
        {
            this.name = name;
        }

        public readonly string name;
    }
}
