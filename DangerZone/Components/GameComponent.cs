using System;
using DangerZone.ScreenManagement;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace DangerZone.Components
{
    public class GameComponent
    {
        protected readonly GameScreen Screen;

        public GameComponent(GameScreen screen)
        {
            Screen = screen;
        }
        public virtual void Update(GameTime gameTime)
        {
            
        }
    }
}
