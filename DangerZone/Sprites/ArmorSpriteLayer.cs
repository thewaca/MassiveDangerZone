using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Artemis;
using DangerZone.Components;
using Microsoft.Xna.Framework.Content;

namespace DangerZone.Sprites
{
    class ArmorSpriteLayer:EquipmentSpriteLayer<Armor>
    {
        public ArmorSpriteLayer(ContentManager contentManager, Character character, Equipment.Slot slot) : base(contentManager, character, slot)
        {
        }

        protected override string fileNameForEquipment(Character character, Armor equipment)
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

        private Dictionary<Armor.Type, String> genericSlot = new Dictionary<Armor.Type, string>
            {
            };

        private Dictionary<Armor.Type, String> femaleSlot = new Dictionary<Armor.Type, string>
            {
                {Armor.Type.Leather, "sprites\\torso\\leather\\chest_female"}
            };

        private Dictionary<Armor.Type, String> maleSlot = new Dictionary<Armor.Type, string>
            {
                {Armor.Type.Leather, "sprites\\torso\\leather\\chest_male"}
            };
    }
}
