using Artemis;
using Artemis.System;
using DangerZone.ScreenManagement;
using MassiveDangerZone.Items;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using DrawableGameComponent = DangerZone.Components.DrawableGameComponent;
using MassiveDangerZone.Templates;

namespace MassiveDangerZone.Components
{
    public class World: DrawableGameComponent
    {
        public World(GameScreen screen) : base(screen)
        {
        }

        private TileChunk _map;
        private EntityWorld entityWorld;

        public uint time { get; private set; }

        public override void LoadContent(ContentManager contentManager)
        {
            _map = new TileChunk(Screen, 32, 16);
            _map.LoadContent(contentManager);

            EntitySystem.BlackBoard.SetEntry("SpriteBatch", Screen.ScreenManager.SpriteBatch);
            EntitySystem.BlackBoard.SetEntry("ContentManager", contentManager);
            EntitySystem.BlackBoard.SetEntry("World", this);
            EntitySystem.BlackBoard.SetEntry("GraphicsDevice", Screen.ScreenManager.Game.GraphicsDevice);

            entityWorld = new EntityWorld();
            entityWorld.InitializeAll(true);
            Entity entity = entityWorld.CreateEntityFromTemplate(CharacterTemplate.Name);
            entity.Refresh();

            var converter = new ItemLoader(entityWorld);
            converter.loadFile("Content\\Items.json");
            var tiles = new Entity[16,16];
            for (int i = 0; i < 16; i++)
            {
                for (int j = 0; j < 16; j++)
                {
                    tiles[i, j] = TileTemplate.CreateEntity(entityWorld, DangerZone.Components.Tile.Type.Grass, 0,
                                                            new Vector2(i * MassiveDangerZone.tileSize, j * MassiveDangerZone.tileSize));
                }
            }

            ChunkTemplate.CreateEntity(entityWorld, tiles, new Vector2(256, 256));
        }

        public override void UnloadContent()
        {
            _map.UnloadContent();
            entityWorld.UnloadContent();

            base.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
            this.time += (uint)gameTime.ElapsedGameTime.TotalMilliseconds;
            entityWorld.Update();

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            entityWorld.Draw();
        }
    }
}
