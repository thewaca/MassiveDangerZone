using System;
using DangerZone.ScreenManagement;
using DangerZone.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace DangerZone.Components
{
    public class DrawableGameComponent: GameComponent
    {
        public DrawableGameComponent(GameScreen screen) : base(screen)
        {
        }

        public SpriteOld Sprite { get; protected set; }

        public virtual void LoadContent(ContentManager contentManager)
        {
            throw new NotImplementedException();
        }

        public virtual void UnloadContent()
        {
        }

        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            throw new NotImplementedException();
        }

    }
}
