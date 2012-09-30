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

    public enum MouseButton
    {
        Left,
        Right,
        Middle,
        X1,
        X2,
    }

    public class MouseEventArgs : EventArgs
    {
        public MouseEventArgs(MouseButton m, Int32 X, Int32 Y, Int32 Scroll)
        {
            mouse = m;
        }
        public MouseButton mouse;
        public Int32 X, Y, Scroll;
    }
    
    public delegate void KeyEventHandler(Object sender, KeyEventArgs e);
    public delegate void MouseEventHandler(Object sender, MouseEventArgs e);

    public class InputEvents
    {
        private event KeyEventHandler KeyDownEvent;
        private Dictionary<Keys, List<KeyEventHandler>> targetedKeyDownEvents;
        private event KeyEventHandler KeyUpEvent;
        private Dictionary<Keys, List<KeyEventHandler>> targetedKeyUpEvents;


        private event MouseEventHandler MouseDownEvent;
        private event MouseEventHandler MouseUpEvent;

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

        protected void RaiseMouseEvent(MouseButton button, MouseState ms, MouseEventHandler mouseEvent)
        {
            MouseEventHandler handler = mouseEvent;
            if (handler != null)
            {
                handler(this, new MouseEventArgs(button, ms.X, ms.Y, ms.ScrollWheelValue));
            }
        }

        protected void RaiseMouseClickEvents(MouseState currentMouseState, MouseState previousMouseState)
        {
            if (currentMouseState.LeftButton == ButtonState.Released && previousMouseState.LeftButton == ButtonState.Pressed)
            {
                RaiseMouseEvent(MouseButton.Left, currentMouseState, MouseUpEvent);
            }
            else if (currentMouseState.LeftButton == ButtonState.Pressed && previousMouseState.LeftButton == ButtonState.Released)
            {
                RaiseMouseEvent(MouseButton.Left, currentMouseState, MouseDownEvent);
            }

            if (currentMouseState.RightButton == ButtonState.Released && previousMouseState.RightButton == ButtonState.Pressed)
            {
                RaiseMouseEvent(MouseButton.Right, currentMouseState, MouseUpEvent);
            }
            else if (currentMouseState.RightButton == ButtonState.Pressed && previousMouseState.RightButton == ButtonState.Released)
            {
                RaiseMouseEvent(MouseButton.Right, currentMouseState, MouseDownEvent);
            }

            if (currentMouseState.MiddleButton == ButtonState.Released && previousMouseState.MiddleButton == ButtonState.Pressed)
            {
                RaiseMouseEvent(MouseButton.Middle, currentMouseState, MouseUpEvent);
            }
            else if (currentMouseState.MiddleButton == ButtonState.Pressed && previousMouseState.MiddleButton == ButtonState.Released)
            {
                RaiseMouseEvent(MouseButton.Middle, currentMouseState, MouseDownEvent);
            }
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
