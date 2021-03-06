﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DangerZone.Input;
using DangerZone.Sprites;
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

        private Sprite tileBorder;
        private Vector2 borderPos;

        TileChunk map;
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

            map = new TileChunk(this, 32, 32);
            borderPos = new Vector2(-1, -1);
        }


        /// <summary>
        /// Load graphics content for the game.
        /// </summary>
        public override void LoadContent()
        {
            var game = (MassiveDangerZone) ScreenManager.Game;

            bindings.Add(new KeyBinding(inputEvents, Keys.Escape, this.HandlePause, KeyState.Down));
            inputEvents.MouseMoveEvent += OnMouseMove;

            if (_content == null)
                _content = new ContentManager(ScreenManager.Game.Services, "Content");

            buttons.Add(new TileButton(new Vector2(game.GraphicsDevice.Viewport.Width - 200, 200), new Vector2(96, 96), @"Tiles\grass", 1, 3, this));

            foreach (Button b in buttons)
            {
                b.LoadContent(_content);
                inputEvents.MouseDownEvent += b.OnMouseDown;
                inputEvents.MouseUpEvent += b.OnMouseUp;
                inputEvents.MouseMoveEvent += b.OnMouseMove;
            }

            tileBorder = new Sprite();
            tileBorder.Color = Color.Red;
            tileBorder.Texture = _content.Load<Texture2D>("border");
            tileBorder.Origin = new Vector2(tileBorder.Texture.Width / 2, tileBorder.Texture.Height / 2);

            map.LoadContent(_content);

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
            map.Update(gameTime);
            base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);
        }


        /// <summary>
        /// Lets the game respond to player input. Unlike the Update method,
        /// this will only be called when the gameplay screen is active.
        /// </summary>
        public override void HandleInput()
        {
        }

        protected void OnMouseMove(object sender, MouseEventArgs e)
        {
            Tile selected = map.GetTileAt(e.X, e.Y);
            if (selected != null)
            {
                borderPos = selected.drawPos;
            }
            else
            {
                borderPos.X = -1;
                borderPos.Y = -1;
            }
        }


        /// <summary>
        /// Draws the gameplay screen.
        /// </summary>
        public override void Draw(GameTime gameTime)
        {
            map.Draw(gameTime, ScreenManager.SpriteBatch);
            ScreenManager.SpriteBatch.Begin();
            if ((borderPos.X >= 0) && (borderPos.Y >= 0))
            {
                tileBorder.Draw(ScreenManager.SpriteBatch, borderPos);
            }
            ScreenManager.SpriteBatch.End();

            ScreenManager.SpriteBatch.Begin();
            foreach (Button b in buttons)
            {
                b.Draw(gameTime, ScreenManager.SpriteBatch);
            }
            ScreenManager.SpriteBatch.End();
        }

        #endregion
    }
}
