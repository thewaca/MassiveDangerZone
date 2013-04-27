using Artemis;
using Artemis.Attributes;
using Artemis.Interface;
using DangerZone.Components;
using Microsoft.Xna.Framework;

namespace MassiveDangerZone.Templates
{
    [ArtemisEntityTemplate(Name)]
    public class ChunkTemplate : IEntityTemplate
    {
        public const string Name = "ChunkTemplate";

        public Entity BuildEntity(Entity entity, EntityWorld entityWorld, params object[] args)
        {
            entity.AddComponent(new Chunk
            {
                tiles = (Entity[,])args[0],
                clean = false,
                size = new Point(((Entity[,])args[0]).GetLength(0), ((Entity[,])args[0]).GetLength(1)),
            });
            entity.AddComponent(new WorldPosition
            {
                position = (Vector2)args[1]
            });
            entity.AddComponent(new Drawable());

            return entity;
        }

        public static Entity CreateEntity(EntityWorld entityWorld, Entity[,] tiles, Vector2 position)
        {
            Entity entity = entityWorld.CreateEntityFromTemplate(Name, tiles, position);
            entity.Refresh();
            return entity;
        }
    }
}