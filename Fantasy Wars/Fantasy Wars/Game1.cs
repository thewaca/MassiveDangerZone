using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Fantasy_Wars
{
    public class KeyEventArgs : EventArgs
    {
        public KeyEventArgs(Keys k)
        {
            key = k;
        }
        public Keys key;
    }

    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        KeyboardState currentKeyboardState;
        KeyboardState previousKeyboardState;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            previousKeyboardState = Keyboard.GetState();
            this.KeyUpEvent += HandleKeyUp;

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        public event EventHandler KeyDownEvent;
        public event EventHandler KeyUpEvent;

        protected void RaiseKeyEvent(Keys key, EventHandler keyEvent)
        {
            EventHandler handler = keyEvent;
            if (handler != null)
            {
                handler(this, new KeyEventArgs(key));
            }
        }

        protected void RaiseKeyboardEvents(KeyboardState downKeys, KeyboardState upKeys, EventHandler eventToRaise)
        {
            foreach (Keys k in downKeys.GetPressedKeys())
            {
                if (upKeys.IsKeyUp(k))
                {
                    RaiseKeyEvent(k, eventToRaise);
                }
            }
        }

        protected void RaiseInputEvents()
        {
            RaiseKeyboardEvents(previousKeyboardState, currentKeyboardState, KeyUpEvent);
            RaiseKeyboardEvents(currentKeyboardState, previousKeyboardState, KeyDownEvent);
        }

        void HandleKeyUp(object sender, EventArgs e)
        {
            switch(((KeyEventArgs)e).key)
            {
                case Keys.Escape:
                    this.Exit();
                    break;
            }
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            currentKeyboardState = Keyboard.GetState();

            RaiseInputEvents();

            // TODO: Add your update logic here
            previousKeyboardState = currentKeyboardState;
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            SpriteBatch batch = new SpriteBatch(GraphicsDevice);

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
