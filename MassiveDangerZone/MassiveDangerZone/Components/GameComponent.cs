using System;
using MassiveDangerZone.ScreenManagement;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MassiveDangerZone.Components
{
    class GameComponent
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
