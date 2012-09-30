using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;

namespace Fantasy_Wars.Input
{
    class KeyBindingManager
    {
        private InputEvents eventSource;

        private Keys _pauseBinding = Keys.None;

        public Keys pauseBinding
        {
            get { return _pauseBinding; }
            set
            {
                if (_pauseBinding != Keys.None)
                {
                    this.eventSource.UnRegisterForKeyDown(this.pausedKeyPressed, _pauseBinding);
                }

                this.eventSource.RegisterForKeyDown(this.pausedKeyPressed, value);
                _pauseBinding = value;
            }
        }

        void pausedKeyPressed(object sender, KeyEventArgs e)
        {

        }

        public KeyBindingManager(InputEvents eventSource)
        {
            this.eventSource = eventSource;
        }

        public event EventHandler<EventArgs> Pause;

        public void OnPause(EventArgs e)
        {
            EventHandler<EventArgs> handler = Pause;
            if (handler != null) handler(this, e);
        }

        public event EventHandler<EventArgs> Resume;

        public void OnResume(EventArgs e)
        {
            EventHandler<EventArgs> handler = Resume;
            if (handler != null) handler(this, e);
        }
    }
}
