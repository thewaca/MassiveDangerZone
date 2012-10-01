using Fantasy_Wars.Components;
using Fantasy_Wars.ScreenManagement;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Fantasy_Wars
{
    class Tile : WorldObject
    {
        public Tile(GameScreen screen, Vector3 position) : base(screen, position)
        {
        }

        public override void LoadContent(ContentManager contentManager)
        {
            this.Sprite = new Sprite()
                              {
                                  Color = Color.White,
                                  Origin = new Vector2(32, 16),
                                  Texture = contentManager.Load<Texture2D>("grass")
                              };
        }
    }
}
