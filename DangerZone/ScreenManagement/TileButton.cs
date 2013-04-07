using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using DangerZone.Components;
using DangerZone.Sprites;
using DangerZone.Input;

namespace DangerZone.ScreenManagement
{
    public class TileButton : Button
    {
        private static readonly Vector2 tileSize = new Vector2(32, 32);
        private Sprite border;
        private int column, row;

        public TileButton(Vector2 position, Vector2 size, string textureName, int column, int row, GameScreen screen)
            : base(position, size, textureName, screen)
        {
            this.column = column;
            this.row = row;
        }

        public override void LoadContent(ContentManager contentManager)
        {
            this.Sprite = new TileSprite();
            this.Sprite.Color = Color.White;
            ((TileSprite)this.Sprite).source = Sprite.GetSheetRectangle(tileSize, column, row);
            this.Sprite.Texture = contentManager.Load<Texture2D>(this.textureName);
            this.Sprite.Origin = new Vector2(tileSize.X / 2, tileSize.Y / 2);

            border = new Sprite();
            this.border.Color = Color.Gray;
            this.border.Texture = contentManager.Load<Texture2D>("border");
            this.border.Origin = new Vector2(this.border.Texture.Width / 2, this.border.Texture.Height / 2);
        }

        public override void Update(GameTime gameTime)
        {

        }

        public override void OnMouseMove(object sender, MouseEventArgs e)
        {
        }

        public override void OnMouseUp(object sender, MouseEventArgs e)
        {
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            Sprite.Draw(spriteBatch, position, size);
            border.Draw(spriteBatch, position, size);
        }
    }
}
