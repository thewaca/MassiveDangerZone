using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DangerZone.Input;
using DangerZone.ScreenManagement;
using MassiveDangerZone.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MassiveDangerZone.Screens
{
    class CreativeplayScreen : GameScreen
    {
        #region Fields

        private ContentManager _content;

        List<Button> buttons;

        #endregion

        #region Initialization
        
        void HandlePause(object sender, KeyEventArgs e) {
            ScreenManager.AddScreen(new PauseMenuScreen(), ControllingPlayer);
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        public CreativeplayScreen()
        {
            TransitionOnTime = TimeSpan.FromSeconds(1.5);
            TransitionOffTime = TimeSpan.FromSeconds(0.5);

            buttons = new List<Button>();
            buttons.Add(new TileButton(new Vector2(), new Vector2(), @"", 0, 0, this));
        }


        /// <summary>
        /// Load graphics content for the game.
        /// </summary>
        public override void LoadContent()
        {
            var game = (MassiveDangerZone) ScreenManager.Game;

            bindings.Add(new KeyBinding(inputEvents, Keys.Escape, this.HandlePause, KeyState.Down));

            if (_content == null)
                _content = new ContentManager(ScreenManager.Game.Services, "Content");
            
            // once the load has finished, we use ResetElapsedTime to tell the game's
            // timing mechanism that we have just finished a very long frame, and that
            // it should not try to catch up.
            ScreenManager.Game.ResetElapsedTime();

            base.LoadContent();
        }


        /// <summary>
        /// Unload graphics content used by the game.
        /// </summary>
        public override void UnloadContent()
        {
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
        }

        #endregion
    }
}
