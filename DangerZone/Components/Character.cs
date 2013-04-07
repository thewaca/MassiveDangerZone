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

        uint getFrame(GameTime gameTime, uint frames)
        {
            const float frameLength = (float)1000/30;
            return (uint)(gameTime.TotalGameTime.TotalMilliseconds / frameLength) % frames;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            var frame = getFrame(gameTime, CharacterSprite.frames[this.currentState]);
            this.sprite.Draw(spriteBatch, this.currentState, this.facing, frame, position);
        }

        public CharacterSprite.State currentState = CharacterSprite.State.Walking;
        public Gender gender = Gender.Male;
        public CharacterSprite sprite;
        public CharacterSprite.Facing facing = CharacterSprite.Facing.Down;
        public Vector2 position = new Vector2(0,0);

        public override void LoadContent(ContentManager contentManager)
        {
            string path;
            switch (gender)
            {
                case Gender.Male:
                    path = "sprites\\body\\male\\tanned";
                    break;
                case Gender.Female:
                    path = "sprites\\body\\female\\tanned";
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
