using Artemis;
using Artemis.Attributes;
using Artemis.Interface;
using DangerZone.Components;
using Microsoft.Xna.Framework;

namespace MassiveDangerZone.Templates
{
    [ArtemisEntityTemplate(Name)]
    public class TileTemplate : IEntityTemplate
    {
        public const string Name = "TileTemplate";

        public Entity BuildEntity(Entity entity, EntityWorld entityWorld, params object[] args)
        {
            entity.AddComponent(new Character());
            entity.AddComponent(new WorldPosition
            {
                position = new Vector2(100, 100)
            });
            entity.AddComponent(new Drawable());

            return entity;
        }
    }
}
