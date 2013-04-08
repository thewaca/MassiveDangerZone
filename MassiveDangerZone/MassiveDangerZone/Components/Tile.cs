using DangerZone.Components;
using DangerZone.ScreenManagement;
using DangerZone.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using DrawableGameComponent = DangerZone.Components.DrawableGameComponent;

namespace MassiveDangerZone.Components
{
    class Tile : DrawableGameComponent
    {
        public static readonly Vector2 size = new Vector2(32, 32);
        private static readonly int percentPlainGrass = 95;
        private Vector3 position;
        public Vector2 drawPos;
        public Tile(GameScreen screen, Vector3 position)
            : base(screen)
        {
            this.position = position;
            drawPos = new Vector2(position.X * size.X, position.Y * size.Y);
            drawPos.X += size.X / 2;
            drawPos.Y += size.Y / 2;
        }

        public override void LoadContent(ContentManager contentManager)
        {
            int type = MassiveDangerZone.rand.Next(0, 100);
            Rectangle source;
            if (type > percentPlainGrass)
            {
                int tilex = MassiveDangerZone.rand.Next(0, 3);
                source = Sprite.GetSheetRectangle(size, tilex, 5);
            }
            else
            {
                source = Sprite.GetSheetRectangle(size, 1, 3);
            }
            this.Sprite = new TileSprite()
                              {
                                  Color = Color.White,
                                  Origin = new Vector2(size.X/2, size.Y/2),
                                  source = source,
                                  Texture = contentManager.Load<Texture2D>(@"Tiles\grass")
                              };
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            Sprite.Draw(spriteBatch, drawPos);
        }
    }
}
