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
using DrawableGameComponent = DangerZone.Components.DrawableGameComponent;

namespace DangerZone.ScreenManagement
{
    public class Button : DrawableGameComponent
    {
        protected Vector2 size;
        protected string textureName;
        protected Vector2 position;

        public Button(Vector2 position, Vector2 size, string textureName, GameScreen screen)
            : base(screen)
        {
            this.position = position;
            this.size = size;
            this.textureName = textureName;
        }

        public override void LoadContent(ContentManager contentManager)
        {
            this.Sprite = new SpriteOld();
            this.Sprite.Color = Color.White;
            this.Sprite.Texture = contentManager.Load<Texture2D>(textureName);
            this.Sprite.Origin = new Vector2(this.Sprite.Texture.Width / 2, this.Sprite.Texture.Height / 2);
        }

        public override void Update(GameTime gameTime)
        {

        }

        public virtual void OnMouseMove(object sender, MouseEventArgs e)
        {
        }

        public virtual void OnMouseUp(object sender, MouseEventArgs e)
        {
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            Sprite.Draw(spriteBatch, position, size);
        }
    }
}
