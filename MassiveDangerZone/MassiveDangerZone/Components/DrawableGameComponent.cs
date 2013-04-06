using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MassiveDangerZone.ScreenManagement;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MassiveDangerZone.Components
{
    class DrawableGameComponent: GameComponent
    {
        public DrawableGameComponent(GameScreen screen) : base(screen)
        {
        }

        public Sprite Sprite { get; protected set; }

        public virtual void LoadContent(ContentManager contentManager)
        {
            throw new NotImplementedException();
        }

        public virtual void UnLoadContent()
        {
        }

        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            throw new NotImplementedException();
        }

    }
}
