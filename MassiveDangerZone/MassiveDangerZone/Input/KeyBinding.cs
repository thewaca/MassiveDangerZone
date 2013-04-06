using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;

namespace MassiveDangerZone.Input
{
    class KeyBinding
    {

        // TODO: implement me
        // public bool repeat;

        public readonly InputEvents source;
        public bool IsRegistered { get; private set; }

        private Keys _binding;
        public Keys Binding
        {
            get { return _binding; }
            set
            {
                var wasRegistered = this.IsRegistered;
                if(wasRegistered) this.UnRegister();
                _binding = value;
                if(wasRegistered) this.Register();
            }
        }

        private KeyEventHandler _target;
        public KeyEventHandler Target
        {
            get { return _target; }
            set
            {
                var wasRegistered = this.IsRegistered;
                if(wasRegistered) this.UnRegister();
                _target = value;
                if(wasRegistered) this.Register();
            }
        }

        private KeyState _type;
        public KeyState Type
        {
            get { return _type; }
            set
            {
                var wasRegistered = this.IsRegistered;
                if(wasRegistered) this.UnRegister();
                _type = value;
                if(wasRegistered) this.Register();
            }
        }

        public KeyBinding(InputEvents source, Keys binding, KeyEventHandler target, KeyState type = KeyState.Up)
        {
            this.source = source;
            this._binding = binding;
            this._target = target;
            this._type = type;
            this.IsRegistered = false;
        }

        public void Register()
        {
            if(this._type == KeyState.Down) source.RegisterForKeyDown(_target, _binding);
            else source.RegisterForKeyUp(_target, _binding);

            this.IsRegistered = true;
        }

        public void UnRegister()
        {
            if(this._type == KeyState.Down) source.UnRegisterForKeyDown(_target, _binding);
            else source.UnRegisterForKeyUp(_target, _binding);

            this.IsRegistered = false;
        }
    }
}
