using System;
using DangerZone.Input;
using DangerZone.ScreenManagement;
using MassiveDangerZone.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MassiveDangerZone.Screens
{
    class GameplayScreen : GameScreen
    {
        #region Fields

        private ContentManager _content;

        private World world;

        #endregion

        #region Initialization
        
        void HandlePause(object sender, KeyEventArgs e) {
            ScreenManager.AddScreen(new PauseMenuScreen(), ControllingPlayer);
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        public GameplayScreen()
        {
            TransitionOnTime = TimeSpan.FromSeconds(1.5);
            TransitionOffTime = TimeSpan.FromSeconds(0.5);
        }


        /// <summary>
        /// Load graphics content for the game.
        /// </summary>
        public override void LoadContent()
        {
            var game = (MassiveDangerZone) ScreenManager.Game;

            bindings.Add(new KeyBinding(inputEvents, Keys.Escape, this.HandlePause, KeyState.Down));

            if (_content == null)
                _content = new ContentManager(game.Services, "Content");

            world = new World(this);
            world.LoadContent(_content);

            // once the load has finished, we use ResetElapsedTime to tell the game's
            // timing mechanism that we have just finished a very long frame, and that
            // it should not try to catch up.
            game.ResetElapsedTime();

            base.LoadContent();
        }


        /// <summary>
        /// Unload graphics content used by the game.
        /// </summary>
        public override void UnloadContent()
        {
            world.UnloadContent();

            _content.Unload();

            base.UnloadContent();
        }


        #endregion

        #region Update and Draw


        /// <summary>
        /// Updates the state of the game. This method checks the GameScreen.IsActive
        /// property, so the game will stop updating when the pause menu is active,
        /// or if you tab away to a different application.
        /// </summary>
        public override void Update(GameTime gameTime, bool otherScreenHasFocus,
                                                       bool coveredByOtherScreen)
        {
            if (IsActive) this.world.Update(gameTime);

            base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);
        }


        /// <summary>
        /// Lets the game respond to player input. Unlike the Update method,
        /// this will only be called when the gameplay screen is active.
        /// </summary>
        public override void HandleInput()
        {
        }


        /// <summary>
        /// Draws the gameplay screen.
        /// </summary>
        public override void Draw(GameTime gameTime)
        {
            this.world.Draw(gameTime, ScreenManager.SpriteBatch);
        }


        #endregion
    }
}
