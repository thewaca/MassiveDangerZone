using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Fantasy_Wars.ScreenManagement
{
    class GameComponent
    {
        private readonly GameScreen _screen;

        public GameComponent(GameScreen screen)
        {
            _screen = screen;
        }
        
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

        public virtual void Update(GameTime gameTime)
        {
            
        }
    }
}
