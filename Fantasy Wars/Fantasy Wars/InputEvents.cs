using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

    public class InputEvents
    {
        private event EventHandler KeyDownEvent;
        private Dictionary<Keys, List<EventHandler>> targetedKeyDownEvents;
        private event EventHandler KeyUpEvent;
        private Dictionary<Keys, List<EventHandler>> targetedKeyUpEvents;

        private KeyboardState previousKeyboardState;
        private MouseState previousMouseState;

        public InputEvents(KeyboardState initialKeyboardState, MouseState initialMouseState)
        {
            previousKeyboardState = initialKeyboardState;
            previousMouseState = initialMouseState;
        }

        public void RegisterForKeyDown(EventHandler eventHandler, Keys target)
        {
            if (targetedKeyDownEvents[target] == null)
            {
                targetedKeyDownEvents[target] = new List<EventHandler>();
            }
            targetedKeyDownEvents[target].Add(eventHandler);
        }

        public void RegisterForKeyDown(EventHandler eventHandler)
        {
            KeyDownEvent += eventHandler;
        }

        public void UnRegisterForKeyDown(EventHandler eventHandler, Keys target)
        {
            if (targetedKeyDownEvents[target] != null)
            {
                targetedKeyDownEvents[target].Remove(eventHandler);
            }
        }

        public void UnRegisterForKeyDown(EventHandler eventHandler)
        {
            KeyDownEvent -= eventHandler;
        }

        public void RegisterForKeyUp(EventHandler eventHandler, Keys target)
        {
            if (targetedKeyUpEvents[target] == null)
            {
                targetedKeyUpEvents[target] = new List<EventHandler>();
            }
            targetedKeyUpEvents[target].Add(eventHandler);
        }

        public void RegisterForKeyUp(EventHandler eventHandler)
        {
            KeyUpEvent += eventHandler;
        }

        public void UnRegisterForKeyUp(EventHandler eventHandler, Keys target)
        {
            if (targetedKeyUpEvents[target] != null)
            {
                targetedKeyUpEvents[target].Remove(eventHandler);
            }
        }

        public void UnRegisterForKeyUp(EventHandler eventHandler)
        {
            KeyUpEvent -= eventHandler;
        }

        protected void RaiseKeyEvent(Keys key, EventHandler keyEvent)
        {
            EventHandler handler = keyEvent;
            if (handler != null)
            {
                handler(this, new KeyEventArgs(key));
            }
        }

        protected void RaiseKeyboardEvents(KeyboardState downKeys, KeyboardState upKeys, EventHandler eventToRaise, Dictionary<Keys, List<EventHandler>> targetEvents)
        {
            foreach (Keys k in downKeys.GetPressedKeys())
            {
                if (upKeys.IsKeyUp(k))
                {
                    RaiseKeyEvent(k, eventToRaise);
                }
            }
        }

        protected void RaiseMouseClickEvents(MouseState currentMouseState, MouseState previousMouseState)
        {

        }

        public void RaiseInputEvents(KeyboardState currentKeyboardState, MouseState currentMouseState)
        {
            RaiseKeyboardEvents(previousKeyboardState, currentKeyboardState, KeyUpEvent, targetedKeyUpEvents);
            RaiseKeyboardEvents(currentKeyboardState, previousKeyboardState, KeyDownEvent, targetedKeyDownEvents);
            previousKeyboardState = currentKeyboardState;

            RaiseMouseClickEvents(currentMouseState, previousMouseState);
            previousMouseState = currentMouseState;
        }
    }
}
