using System;
using System.Collections.Generic;
using System.Diagnostics;
using Artemis;
using DangerZone.Components;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace DangerZone.Sprites
{
    class EquipmentSpriteLayer<T>:CharacterSpriteLayer where T:Equipment
    {
        public EquipmentSpriteLayer(ContentManager contentManager, Character character, Entity weapon):base(character)
        {
            Dictionary<Equipment.Slot, String> genderFileNames;
            switch (character.gender)
            {
                case Gender.Male:
                    genderFileNames = this.maleSlot;
                    break;
                case Gender.Female:
                    genderFileNames = this.femaleSlot;
                    break;
                default: throw new Exception("fuck you");
            }

            var slot = weapon.GetComponent<T>().slot;
            var fileName = genderFileNames.ContainsKey(slot) ? genderFileNames[slot] : this.genericSlot[slot];

            Debug.WriteLine("loading " + fileName + " for " + slot.ToString());

            Texture = contentManager.Load<Texture2D>(fileName);
        }

        private Dictionary<Equipment.Slot, String> genericSlot = new Dictionary<Equipment.Slot, string>
            {
                {Equipment.Slot.Spear, "sprites\\weapons\\spear"},
                {Equipment.Slot.Bow, "sprites\\weapons\\bow"}
            };

        private Dictionary<Equipment.Slot, String> femaleSlot = new Dictionary<Equipment.Slot, string>
            {
                {Equipment.Slot.Sword, "sprites\\weapons\\dagger_male"},
                {Equipment.Slot.Chest, "sprites\\torso\\leather\\chest_female"}
            };

        private Dictionary<Equipment.Slot, String> maleSlot = new Dictionary<Equipment.Slot, string>
            {
                {Equipment.Slot.Sword, "sprites\\weapons\\dagger_male"},
                {Equipment.Slot.Chest, "sprites\\torso\\leather\\chest_male"}
            };
    }
}
