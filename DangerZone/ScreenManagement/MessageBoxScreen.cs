using System;
using DangerZone.Input;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace DangerZone.ScreenManagement
{
    public class MessageBoxScreen:GameScreen
    {
        #region Fields

        string message;
        Texture2D gradientTexture;

        #endregion

        #region Events

        public event EventHandler Accepted;
        public event EventHandler Cancelled;

        #endregion

        #region Initialization


        /// <summary>
        /// Constructor automatically includes the standard "A=ok, B=cancel"
        /// usage text prompt.
        /// </summary>
        public MessageBoxScreen(string message)
            : this(message, true)
        { }


        /// <summary>
        /// Constructor lets the caller specify whether to include the standard
        /// "A=ok, B=cancel" usage text prompt.
        /// </summary>
        public MessageBoxScreen(string message, bool includeUsageText)
        {
            const string usageText = "\nSpace or Enter = ok" +
                                     "\nBackspace or Esc = cancel"; 
            
            if (includeUsageText)
                this.message = message + usageText;
            else
                this.message = message;

            IsPopup = true;

            TransitionOnTime = TimeSpan.FromSeconds(0.2);
            TransitionOffTime = TimeSpan.FromSeconds(0.2);
        }


        /// <summary>
        /// Loads graphics content for this screen. This uses the shared ContentManager
        /// provided by the Game class, so the content will remain loaded forever.
        /// Whenever a subsequent MessageBoxScreen tries to load this same content,
        /// it will just get back another reference to the already loaded data.
        /// </summary>
        public override void LoadContent()
        {
            ContentManager content = ScreenManager.Game.Content;

            gradientTexture = content.Load<Texture2D>("gradient");

            bindings.Add(new KeyBinding(inputEvents, Keys.Enter, this.OnAccept, KeyState.Down));
            bindings.Add(new KeyBinding(inputEvents, Keys.Space, this.OnAccept, KeyState.Down));
            bindings.Add(new KeyBinding(inputEvents, Keys.Escape, this.OnCancel, KeyState.Down));
            bindings.Add(new KeyBinding(inputEvents, Keys.Back, this.OnCancel, KeyState.Down));

            base.LoadContent();
        }


        #endregion

        #region Handle Input

        protected void OnAccept(object sender, KeyEventArgs e)
        {
            if (Accepted != null)
            {
                Accepted(this, new EventArgs());
            }

            ExitScreen();
        }

        protected void OnCancel(object sender, KeyEventArgs e)
        {

            if (Cancelled != null)
            {
                Cancelled(this, new EventArgs());
            }

            ExitScreen();
        }

        /// <summary>
        /// Responds to user input, accepting or cancelling the message box.
        /// </summary>
        public override void HandleInput()
        {
        }


        #endregion

        #region Draw


        /// <summary>
        /// Draws the message box.
        /// </summary>
        public override void Draw(GameTime gameTime)
        {
            SpriteBatch spriteBatch = ScreenManager.SpriteBatch;
            SpriteFont font = ScreenManager.Font;

            // Darken down any other screens that were drawn beneath the popup.
            ScreenManager.FadeBackBufferToBlack(TransitionAlpha * 2 / 3);

            // Center the message text in the viewport.
            Viewport viewport = ScreenManager.GraphicsDevice.Viewport;
            Vector2 viewportSize = new Vector2(viewport.Width, viewport.Height);
            Vector2 textSize = font.MeasureString(message);
            Vector2 textPosition = (viewportSize - textSize) / 2;

            // The background includes a border somewhat larger than the text itself.
            const int hPad = 32;
            const int vPad = 16;

            Rectangle backgroundRectangle = new Rectangle((int)textPosition.X - hPad,
                                                          (int)textPosition.Y - vPad,
                                                          (int)textSize.X + hPad * 2,
                                                          (int)textSize.Y + vPad * 2);

            // Fade the popup alpha during transitions.
            Color color = Color.White * TransitionAlpha;

            spriteBatch.Begin();

            // Draw the background rectangle.
            spriteBatch.Draw(gradientTexture, backgroundRectangle, color);

            // Draw the message box text.
            spriteBatch.DrawString(font, message, textPosition, color);

            spriteBatch.End();
        }


        #endregion
    }
}
