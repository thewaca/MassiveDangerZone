using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Artemis;
using Artemis.Attributes;
using Artemis.Manager;
using Artemis.System;
using DangerZone.Components;
using DangerZone.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;


namespace MassiveDangerZone.Systems
{
    [ArtemisEntitySystem(GameLoopType = GameLoopType.Draw, Layer = 0)]
    public class ChunkSpriteSystem : EntityProcessingSystem
    {
        private readonly Dictionary<Tile.Type, Texture2D> groundTextures;

        public ChunkSpriteSystem()
            : base(Aspect.All(typeof(Chunk), typeof(Drawable)))
        {
            groundTextures = new Dictionary<Tile.Type, Texture2D>();
        }

        private ComponentMapper<Chunk> chunkMapper;
        private ComponentMapper<Drawable> drawableMapper;
        private ComponentMapper<Tile> tileMapper;
        private ComponentMapper<ChunkPosition> chunkPositionMapper;

        private ContentManager contentManager;
        private GraphicsDevice graphics;
        private SpriteBatch spriteBatch;

        public override void LoadContent()
        {
            this.chunkMapper = new ComponentMapper<Chunk>(EntityWorld);
            this.drawableMapper = new ComponentMapper<Drawable>(EntityWorld);
            this.tileMapper = new ComponentMapper<Tile>(EntityWorld);
            this.chunkPositionMapper = new ComponentMapper<ChunkPosition>(EntityWorld);
            contentManager = BlackBoard.GetEntry<ContentManager>("ContentManager");
            graphics = BlackBoard.GetEntry<GraphicsDevice>("GraphicsDevice");
            spriteBatch = BlackBoard.GetEntry<SpriteBatch>("SpriteBatch");

            groundTextures.Add(Tile.Type.Grass, contentManager.Load<Texture2D>(@"Tiles\grass"));
            groundTextures.Add(Tile.Type.Dirt, contentManager.Load<Texture2D>(@"Tiles\dirt"));
        }

        public override void Process(Entity e)
        {
            var chunk = this.chunkMapper.Get(e);
            var drawable = this.drawableMapper.Get(e);

            if (drawable.sprite == null)
            {
                drawable.sprite = new Sprite();
            }

            if (!chunk.clean)
            {
                var sprite = drawable.sprite;
                int chunkPixelSize = chunk.size * MassiveDangerZone.tileSize;
                var chunkRenderer = new RenderTarget2D(graphics, chunkPixelSize, chunkPixelSize, false, SurfaceFormat.Color, DepthFormat.None);
                graphics.SetRenderTarget(chunkRenderer);
                graphics.Clear(Color.Transparent);

                spriteBatch.Begin();
                for (int x = 0; x < chunk.size; x++)
                {
                    for (int y = 0; y < chunk.size; y++)
                    {
                        Tile topLeft = tileMapper.Get(chunk.tiles[x, y]);
                        /*Tile topRight = tileMapper.Get(chunk.tiles[x + 1, y]);
                        Tile bottomLeft = tileMapper.Get(chunk.tiles[x, y + 1]);
                        Tile bottomRight = tileMapper.Get(chunk.tiles[x + 1, y + 1]);*/

                        ChunkPosition pos = chunkPositionMapper.Get(chunk.tiles[x, y]);

                        Rectangle source = Sprite.GetSheetRectangle(new Vector2(MassiveDangerZone.tileSize, MassiveDangerZone.tileSize), 1, 3);
                        spriteBatch.Draw(groundTextures[Tile.Type.Grass], pos.position, source, Color.White);
                    }
                }
                spriteBatch.End();
                graphics.SetRenderTarget(null);
                graphics.Clear(Color.Black);
                sprite.Texture = chunkRenderer;
                sprite.Origin = new Vector2(chunkPixelSize / 2, chunkPixelSize / 2);
            }

        }
    }
}
