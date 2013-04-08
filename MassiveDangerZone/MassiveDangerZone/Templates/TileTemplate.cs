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
            entity.AddComponent(new Tile()
            {
                type = (Tile.Type)args[0],
                zLevel = (int)args[1],
            });
            entity.AddComponent(new ChunkPosition()
            {
                position = (Vector2)args[2]
            });

            return entity;
        }

        public static void CreateEntity(EntityWorld entityWorld, Tile.Type type, int zLevel, Vector2 position)
        {
            Entity entity = entityWorld.CreateEntityFromTemplate(TileTemplate.Name, type, zLevel, position);
            entity.Refresh();
        }
    }
}
