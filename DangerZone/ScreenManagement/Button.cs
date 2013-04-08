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
        protected Rectangle hitBox;

        public Button(Vector2 position, Vector2 size, string textureName, GameScreen screen)
            : base(screen)
        {
            this.position = position;
            this.size = size;
            this.textureName = textureName;
            this.hitBox = new Rectangle((int)(position.X - size.X / 2), (int)(position.Y - size.Y / 2), (int)size.X, (int)size.Y);
        }

        public override void LoadContent(ContentManager contentManager)
        {
            this.Sprite = new Sprite();
            this.Sprite.Color = Color.White;
            this.Sprite.Texture = contentManager.Load<Texture2D>(textureName);
            this.Sprite.Origin = new Vector2(size.X / 2, size.Y / 2);
        }

        public override void Update(GameTime gameTime)
        {

        }

        public virtual void OnMouseMove(object sender, MouseEventArgs e)
        {
        }

        private bool wasDown;
        public virtual void OnMouseDown(object sender, MouseEventArgs e)
        {
            if (hitBox.Contains(e.X, e.Y))
            {
                wasDown = true;
            }
        }

        public virtual void OnMouseUp(object sender, MouseEventArgs e)
        {
            if (hitBox.Contains(e.X, e.Y) && wasDown == true)
            {
                MessageBoxScreen test = new MessageBoxScreen("Wahoo you clicked the button!");

                Screen.ScreenManager.AddScreen(test, PlayerIndex.One);
            }
            wasDown = false;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            Sprite.Draw(spriteBatch, position, size);
        }
    }
}
