using System.Collections.Generic;
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

#region LoadContent
        public override void LoadContent()
        {
            CreateComponentMappers();
            LoadBlackBoardObjects();
            LoadGroundTextures();
        }

        private void LoadBlackBoardObjects()
        {
            contentManager = BlackBoard.GetEntry<ContentManager>("ContentManager");
            graphics = BlackBoard.GetEntry<GraphicsDevice>("GraphicsDevice");
            spriteBatch = BlackBoard.GetEntry<SpriteBatch>("SpriteBatch");
        }

        private void CreateComponentMappers()
        {
            chunkMapper = new ComponentMapper<Chunk>(EntityWorld);
            drawableMapper = new ComponentMapper<Drawable>(EntityWorld);
            tileMapper = new ComponentMapper<Tile>(EntityWorld);
            chunkPositionMapper = new ComponentMapper<ChunkPosition>(EntityWorld);
        }

        private void LoadGroundTextures()
        {
            groundTextures.Add(Tile.Type.Grass, contentManager.Load<Texture2D>(@"Tiles\grass"));
            groundTextures.Add(Tile.Type.Dirt, contentManager.Load<Texture2D>(@"Tiles\dirt"));
        }
#endregion

#region RenderTargetHelpers
        private void SetRenderTargetAndBegin(RenderTarget2D renderTarget)
        {
            graphics.SetRenderTarget(renderTarget);
            graphics.Clear(Color.Transparent);
            spriteBatch.Begin();
        }

        private void ClearRenderTargetAndEnd()
        {
            spriteBatch.End();
            graphics.SetRenderTarget(null);
            graphics.Clear(Color.Black);
        }

        private RenderTarget2D CreateRenderTargetForChunk(Chunk chunk)
        {
            return new RenderTarget2D(graphics,
                    (chunk.size.X * MassiveDangerZone.tileSize),
                    (chunk.size.Y * MassiveDangerZone.tileSize),
                    false,
                    SurfaceFormat.Color,
                    DepthFormat.None);
        }
#endregion

        public override void Process(Entity e)
        {
            var chunk = chunkMapper.Get(e);
            var drawable = drawableMapper.Get(e);

            if (drawable.sprite == null)
            {
                drawable.sprite = new Sprite();
            }

            if (!chunk.clean)
            {
                var chunkRenderer = CreateRenderTargetForChunk(chunk);
                SetRenderTargetAndBegin(chunkRenderer);

                ComposeCenterTiles(chunk);
                ComposeBottomTiles(chunk);
                ComposeRightTiles(chunk);
                ComposeBottomRightCornerTile(chunk);

                ClearRenderTargetAndEnd();
                drawable.sprite.SetSprite(chunkRenderer);
            }
        }

#region ComposeTilesHelpers
        private void ComposeCenterTiles(Chunk chunk)
        {
            for (int x = 0; x < chunk.size.X - 1; x++)
            {
                for (int y = 0; y < chunk.size.Y - 1; y++)
                {
                    Tile topLeft = tileMapper.Get(chunk.tiles[x, y]);
                    Tile topRight = tileMapper.Get(chunk.tiles[x + 1, y]);
                    Tile bottomLeft = tileMapper.Get(chunk.tiles[x, y + 1]);
                    Tile bottomRight = tileMapper.Get(chunk.tiles[x + 1, y + 1]);

                    ChunkPosition pos = chunkPositionMapper.Get(chunk.tiles[x, y]);
                    ComposeTile(topLeft, topRight, bottomLeft, bottomRight, pos.position);
                }
            }
        }

        private void ComposeBottomTiles(Chunk chunk)
        {
            int y = chunk.size.Y - 1;
            for (int x = 0; x < chunk.size.X - 1; x++)
            {
                Tile topLeft = tileMapper.Get(chunk.tiles[x, y]);
                Tile topRight = tileMapper.Get(chunk.tiles[x + 1, y]);
                Tile bottomLeft = (chunk.bottom != null ? tileMapper.Get(chunk.bottom.tiles[x, 0]) : Tile.emptyTile);
                Tile bottomRight = (chunk.bottom != null ? tileMapper.Get(chunk.bottom.tiles[x + 1, 0]) : Tile.emptyTile);

                ChunkPosition pos = chunkPositionMapper.Get(chunk.tiles[x, y]);
                ComposeTile(topLeft, topRight, bottomLeft, bottomRight, pos.position);
            }
        }

        private void ComposeRightTiles(Chunk chunk)
        {
            int x = chunk.size.X - 1;
            for (int y = 0; y < chunk.size.Y - 1; y++)
            {
                Tile topLeft = tileMapper.Get(chunk.tiles[x, y]);
                Tile topRight = (chunk.right != null ? tileMapper.Get(chunk.right.tiles[0, y]) : Tile.emptyTile);
                Tile bottomLeft = tileMapper.Get(chunk.tiles[x, y + 1]);
                Tile bottomRight = (chunk.right != null ? tileMapper.Get(chunk.right.tiles[0, y + 1]) : Tile.emptyTile);

                ChunkPosition pos = chunkPositionMapper.Get(chunk.tiles[x, y]);
                ComposeTile(topLeft, topRight, bottomLeft, bottomRight, pos.position);
            }
        }

        private void ComposeBottomRightCornerTile(Chunk chunk)
        {
            int x = chunk.size.X - 1;
            int y = chunk.size.Y - 1;
            Tile topLeft = tileMapper.Get(chunk.tiles[x, y]);
            Tile topRight = (chunk.right != null ? tileMapper.Get(chunk.right.tiles[0, y]) : Tile.emptyTile);
            Tile bottomLeft = (chunk.bottom != null ? tileMapper.Get(chunk.bottom.tiles[x, 0]) : Tile.emptyTile);
            Tile bottomRight = (chunk.corner != null ? tileMapper.Get(chunk.corner.tiles[0, 0]) : Tile.emptyTile);

            ChunkPosition pos = chunkPositionMapper.Get(chunk.tiles[x, y]);
            ComposeTile(topLeft, topRight, bottomLeft, bottomRight, pos.position);
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

        private Dictionary<Tile.Type, int> CreateCountsDictionary(Tile topLeft, Tile topRight, Tile bottomLeft, Tile bottomRight)
        {
            var counts = new Dictionary<Tile.Type, int>();
            AddTileToCountDictionary(topLeft.type, 1, counts);
            AddTileToCountDictionary(topRight.type, 2, counts);
            AddTileToCountDictionary(bottomRight.type, 4, counts);
            AddTileToCountDictionary(bottomLeft.type, 8, counts);
            return counts;
        }

        private void DrawType(Dictionary<Tile.Type, int> counts, Tile.Type type, Vector2 pos)
        {
            if (counts.ContainsKey(type))
            {
                Point p = tileMap[counts[type]];
                Rectangle source = Sprite.GetSheetRectangle(new Vector2(MassiveDangerZone.tileSize, MassiveDangerZone.tileSize), (uint)p.X, (uint)p.Y);
                spriteBatch.Draw(groundTextures[type], pos, source, Color.White);
            }
        }
#endregion

        private void ComposeTile(Tile topLeft, Tile topRight, Tile bottomLeft, Tile bottomRight, Vector2 pos)
        {
            var counts = CreateCountsDictionary(topLeft, topRight, bottomLeft, bottomRight);

            DrawType(counts, Tile.Type.Dirt, pos);
            DrawType(counts, Tile.Type.Grass, pos);
        }
    }
}
