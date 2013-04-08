using DangerZone.Sprites;

namespace DangerZone.Components
{
    public class DrawableCharacter:Drawable
    {
        public new CharacterSprite[] sprites { get { return (CharacterSprite[])this.actualSprites; } set { this.actualSprites = value; } }
    }
}
