using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Artemis;
using DangerZone.Components;
using Microsoft.Xna.Framework.Content;

namespace DangerZone.Sprites
{
    class WeaponSpriteLayer:EquipmentSpriteLayer<Weapon>
    {
        public WeaponSpriteLayer(ContentManager contentManager, Character character) : base(contentManager, character, Equipment.Slot.Weapon)
        {
        }

        protected override string fileNameForEquipment(Character character, Weapon equipment)
        {
            var type = equipment.type;

            switch (character.gender)
            {
                case Gender.Male:
                    if (this.maleSlot.ContainsKey(type)) return this.maleSlot[type];
                    break;
                case Gender.Female:
                    if (this.femaleSlot.ContainsKey(type)) return this.femaleSlot[type];
                    break;
            }

            return this.genericSlot[type];
        }

        private Dictionary<Weapon.Type, String> genericSlot = new Dictionary<Weapon.Type, string>
            {
                {Weapon.Type.Spear, "sprites\\weapons\\spear"},
                {Weapon.Type.Bow, "sprites\\weapons\\bow"}
            };

        private Dictionary<Weapon.Type, String> femaleSlot = new Dictionary<Weapon.Type, string>
            {
                {Weapon.Type.Sword, "sprites\\weapons\\dagger_male"},
            };

        private Dictionary<Weapon.Type, String> maleSlot = new Dictionary<Weapon.Type, string>
            {
                {Weapon.Type.Sword, "sprites\\weapons\\dagger_male"},
            };
    }
}
