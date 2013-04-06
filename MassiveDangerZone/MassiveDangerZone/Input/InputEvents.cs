using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;

namespace MassiveDangerZone.Input
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
        None,
        Left,
        Right,
        Middle,
        X1,
        X2,
    }

    public class MouseEventArgs : EventArgs
    {
        public MouseEventArgs(MouseButton m, Int32 x, Int32 y, Int32 scroll)
        {
            mouse = m;
            this.X = x;
            this.Y = y;
            this.Scroll = scroll;
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


        public event MouseEventHandler MouseDownEvent;
        public event MouseEventHandler MouseUpEvent;
        public event MouseEventHandler MouseMoveEvent;

        private KeyboardState currentKeyboardState;
        private MouseState currentMouseState;
        private KeyboardState previousKeyboardState;
        private MouseState previousMouseState;

        public InputEvents(KeyboardState initialKeyboardState, MouseState initialMouseState)
        {
            currentKeyboardState = initialKeyboardState;
            currentMouseState = initialMouseState;
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

        protected void RaiseMouseButtonEvent(ButtonState current, ButtonState previous, MouseState currentState, MouseButton button)
        {
            if (current == ButtonState.Released && previous == ButtonState.Pressed)
            {
                RaiseMouseEvent(button, currentState, MouseUpEvent);
            }
            else if (current == ButtonState.Pressed && previous == ButtonState.Released)
            {
                RaiseMouseEvent(button, currentState, MouseDownEvent);
            }
        }

        protected void RaiseMouseMoveEvents(MouseState currentMouseState, MouseState previousMouseState)
        {
            if ((currentMouseState.X != previousMouseState.X) ||
                (currentMouseState.Y != previousMouseState.Y))
            {
                RaiseMouseEvent(MouseButton.None, currentMouseState, MouseMoveEvent);
            }
        }

        protected void RaiseMouseEvents(MouseState currentMouseState, MouseState previousMouseState)
        {
            RaiseMouseButtonEvent(currentMouseState.LeftButton, previousMouseState.LeftButton, currentMouseState, MouseButton.Left);
            RaiseMouseButtonEvent(currentMouseState.RightButton, previousMouseState.RightButton, currentMouseState, MouseButton.Right);
            RaiseMouseButtonEvent(currentMouseState.MiddleButton, previousMouseState.MiddleButton, currentMouseState, MouseButton.Middle);
            RaiseMouseButtonEvent(currentMouseState.XButton1, previousMouseState.XButton1, currentMouseState, MouseButton.X1);
            RaiseMouseButtonEvent(currentMouseState.XButton2, previousMouseState.XButton2, currentMouseState, MouseButton.X2);

            RaiseMouseMoveEvents(currentMouseState, previousMouseState);
        }

        public void RaiseInputEvents()
        {
            RaiseKeyboardEvents(previousKeyboardState, currentKeyboardState, KeyUpEvent, targetedKeyUpEvents);
            RaiseKeyboardEvents(currentKeyboardState, previousKeyboardState, KeyDownEvent, targetedKeyDownEvents);

            RaiseMouseEvents(currentMouseState, previousMouseState);
        }

        public void UpdateInputState(KeyboardState currKeyState, MouseState currMouseState)
        {
            previousKeyboardState = currentKeyboardState;
            previousMouseState = currentMouseState;

            currentKeyboardState = currKeyState;
            currentMouseState = currMouseState;
        }
    }
}
