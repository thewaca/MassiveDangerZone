using Artemis.Interface;
using DangerZone.Sprites;

namespace DangerZone.Components
{
    public class Drawable:IComponent
    {
        protected Sprite[] actualSprites;
        public Sprite[] sprites { get { return this.actualSprites; } set { this.actualSprites = value; } }
    }
}
