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

        private readonly Dictionary<int, Point> tileMap = new Dictionary<int, Point>
            {
                {1 , new Point(2,4)},
                {2 , new Point(0,4)},
                {4 , new Point(0,2)},
                {8 , new Point(2,2)},
                {3 , new Point(1,4)},
                {12 , new Point(1,2)},
                {6 , new Point(0,3)},
                {9 , new Point(2,3)},
                {11 , new Point(1,0)},
                {7 , new Point(2,0)},
                {13 , new Point(1,1)},
                {14 , new Point(2,1)},
                {15 , new Point(1,3)},
            };

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
                for (int x = 0; x < chunk.size - 1; x++)
                {
                    for (int y = 0; y < chunk.size - 1; y++)
                    {
                        Tile topLeft = tileMapper.Get(chunk.tiles[x, y]);
                        Tile topRight = tileMapper.Get(chunk.tiles[x + 1, y]);
                        Tile bottomLeft = tileMapper.Get(chunk.tiles[x, y + 1]);
                        Tile bottomRight = tileMapper.Get(chunk.tiles[x + 1, y + 1]);

                        ChunkPosition pos = chunkPositionMapper.Get(chunk.tiles[x, y]);
                        ComposeTile(topLeft, topRight, bottomLeft, bottomRight, pos.position, spriteBatch);
                    }
                }
                spriteBatch.End();
                graphics.SetRenderTarget(null);
                graphics.Clear(Color.Black);
                sprite.Texture = chunkRenderer;
                sprite.Origin = new Vector2(chunkPixelSize / 2, chunkPixelSize / 2);
            }
        }

        private void ComposeTile(Tile topLeft, Tile topRight, Tile bottomLeft, Tile bottomRight, Vector2 pos, SpriteBatch spriteBatch)
        {
            var counts = new Dictionary<Tile.Type, int>();
            AddTileToCountDictionary(topLeft.type, 1, counts);
            AddTileToCountDictionary(topRight.type, 2, counts);
            AddTileToCountDictionary(bottomRight.type, 4, counts);
            AddTileToCountDictionary(bottomLeft.type, 8, counts);

            Tile.Type type = Tile.Type.Dirt;
            if (counts.ContainsKey(type))//foreach (Tile.Type type in counts.Keys)
            {
                Point p = tileMap[counts[type]];
                Rectangle source = Sprite.GetSheetRectangle(new Vector2(MassiveDangerZone.tileSize, MassiveDangerZone.tileSize), p.X, p.Y);
                spriteBatch.Draw(groundTextures[type], pos, source, Color.White);
            }
            type = Tile.Type.Grass;
            if (counts.ContainsKey(type))//foreach (Tile.Type type in counts.Keys)
            {
                Point p = tileMap[counts[type]];
                Rectangle source = Sprite.GetSheetRectangle(new Vector2(MassiveDangerZone.tileSize, MassiveDangerZone.tileSize), p.X, p.Y);
                spriteBatch.Draw(groundTextures[type], pos, source, Color.White);
            }
        }

        private void AddTileToCountDictionary(Tile.Type tile, int pos, Dictionary<Tile.Type, int> counts)
        {
            if (counts.ContainsKey(tile))
            {
                counts[tile] += pos;
            }
            else
            {
                counts[tile] = pos;
            }
        }
    }
}
