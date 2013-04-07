using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using DangerZone.ScreenManagement;

namespace DangerZone.Components
{
    class Button : DrawableGameComponent
    {
        private static readonly Vector2 tileSize = new Vector2(32, 32);
        private Vector2 size;
        private string textureName;
        private Vector2 position;
        private Sprite border;
        private bool isTiled;
        private int column, row;

        public Button(Vector2 position, Vector2 size, string textureName, GameScreen screen)
            : base(screen)
        {
            this.position = position;
            this.size = size;
            this.textureName = textureName;
        }

        public Button(Vector2 position, Vector2 size, string textureName, int column, int row, GameScreen screen)
            : base(screen)
        {
            this.position = position;
            this.size = size;
            this.textureName = textureName;
            this.column = column;
            this.row = row;
            this.isTiled = true;
        }

        public override void LoadContent(ContentManager contentManager)
        {
            if (isTiled)
            {
                this.Sprite = new TileSprite();
                this.Sprite.Color = Color.White;
                ((TileSprite)this.Sprite).source = Sprite.GetSheetRectangle(tileSize, column, row);
                this.Sprite.Texture = contentManager.Load<Texture2D>(textureName);
                this.Sprite.Origin = new Vector2(tileSize.X / 2, tileSize.Y / 2);
            }
            else
            {
                this.Sprite = new Sprite();
                this.Sprite.Color = Color.White;
                this.Sprite.Texture = contentManager.Load<Texture2D>(textureName);
                this.Sprite.Origin = new Vector2(this.Sprite.Texture.Width / 2, this.Sprite.Texture.Height / 2);
            }
        }

        public override void Update(GameTime gameTime)
        {

        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
        }
    }
}
