using System;
using DangerZone.ScreenManagement;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace DangerZone.Components
{
    public class Character:DrawableGameComponent
    {
        public enum Gender
        {
            Male, Female
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            var frame = ((uint)gameTime.TotalGameTime.Milliseconds * 10) % CharacterSprite.frames[this.currentState];
            this.sprite.Draw(spriteBatch, this.currentState, this.facing, frame, position);
        }

        public CharacterSprite.State currentState = CharacterSprite.State.Idle;
        public Gender gender = Gender.Female;
        public CharacterSprite sprite;
        public CharacterSprite.Facing facing = CharacterSprite.Facing.Down;
        public Vector2 position;

        public override void LoadContent(ContentManager contentManager)
        {
            string path;
            switch (gender)
            {
                case Gender.Male:
                    path = "sprites\\body\\male\\tanned.png";
                    break;
                case Gender.Female:
                    path = "sprites\\body\\female\\tanned.png";
                    break;
                default:
                    throw new Exception("fuck you");
            }

            this.sprite = new CharacterSprite
                {
                    Texture = contentManager.Load<Texture2D>(path)
                };
        }

        public Character(GameScreen screen) : base(screen)
        {
        }
    }
}
