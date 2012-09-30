using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;

namespace Fantasy_Wars.Input
{
    public class KeyEventArgs : EventArgs
    {
        public KeyEventArgs(Keys k)
        {
            key = k;
        }
        public Keys key;
    }

    public delegate void KeyEventHandler(Object sender, KeyEventArgs e);

    public class InputEvents
    {
        private event KeyEventHandler KeyDownEvent;
        private Dictionary<Keys, List<KeyEventHandler>> targetedKeyDownEvents;
        private event KeyEventHandler KeyUpEvent;
        private Dictionary<Keys, List<KeyEventHandler>> targetedKeyUpEvents;

        private KeyboardState previousKeyboardState;
        private MouseState previousMouseState;

        public InputEvents(KeyboardState initialKeyboardState, MouseState initialMouseState)
        {
            previousKeyboardState = initialKeyboardState;
            previousMouseState = initialMouseState;
            targetedKeyDownEvents = new Dictionary<Keys,List<KeyEventHandler>>();
            targetedKeyUpEvents = new Dictionary<Keys, List<KeyEventHandler>>();
        }

        public void RegisterForKeyDown(KeyEventHandler eventHandler, Keys target)
        {
            if (!targetedKeyDownEvents.ContainsKey(target))
            {
                targetedKeyDownEvents.Add(target, new List<KeyEventHandler>());
            }
            targetedKeyDownEvents[target].Add(eventHandler);
        }

        public void RegisterForKeyDown(KeyEventHandler eventHandler)
        {
            KeyDownEvent += eventHandler;
        }

        public void UnRegisterForKeyDown(KeyEventHandler eventHandler, Keys target)
        {
            if (targetedKeyDownEvents.ContainsKey(target))
            {
                targetedKeyDownEvents[target].Remove(eventHandler);
            }
        }

        public void UnRegisterForKeyDown(KeyEventHandler eventHandler)
        {
            KeyDownEvent -= eventHandler;
        }

        public void RegisterForKeyUp(KeyEventHandler eventHandler, Keys target)
        {
            if (!targetedKeyUpEvents.ContainsKey(target))
            {
                targetedKeyUpEvents.Add(target, new List<KeyEventHandler>());
            }
            targetedKeyUpEvents[target].Add(eventHandler);
        }

        public void RegisterForKeyUp(KeyEventHandler eventHandler)
        {
            KeyUpEvent += eventHandler;
        }

        public void UnRegisterForKeyUp(KeyEventHandler eventHandler, Keys target)
        {
            if (targetedKeyUpEvents.ContainsKey(target))
            {
                targetedKeyUpEvents[target].Remove(eventHandler);
            }
        }

        public void UnRegisterForKeyUp(KeyEventHandler eventHandler)
        {
            KeyUpEvent -= eventHandler;
        }

        protected void RaiseKeyEvent(Keys key, KeyEventHandler keyEvent)
        {
            KeyEventHandler handler = keyEvent;
            if (handler != null)
            {
                handler(this, new KeyEventArgs(key));
            }
        }

        protected void RaiseKeyboardEvents(KeyboardState downKeys, KeyboardState upKeys, KeyEventHandler eventToRaise, Dictionary<Keys, List<KeyEventHandler>> targetEvents)
        {
            foreach (Keys k in downKeys.GetPressedKeys())
            {
                if (upKeys.IsKeyUp(k))
                {
                    RaiseKeyEvent(k, eventToRaise);
                    if (targetEvents.ContainsKey(k))
                    {
                        foreach (KeyEventHandler handler in targetEvents[k])
                        {
                            handler(this, new KeyEventArgs(k));
                        }
                    }
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
