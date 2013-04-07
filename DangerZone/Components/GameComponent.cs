using DangerZone.ScreenManagement;
using Microsoft.Xna.Framework;

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
