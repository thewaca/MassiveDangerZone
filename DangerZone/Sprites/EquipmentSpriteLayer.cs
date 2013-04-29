using System;
using System.Collections.Generic;
using System.Diagnostics;
using Artemis;
using DangerZone.Components;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace DangerZone.Sprites
{
    abstract class EquipmentSpriteLayer<T>:CharacterSpriteLayer where T:Equipment
    {
        public EquipmentSpriteLayer(ContentManager contentManager, Character character, Equipment.Slot slot):base(character)
        {
            var equipment = this.equipmentForSlot(character, slot).GetComponent<T>();

            Texture = contentManager.Load<Texture2D>(this.fileNameForEquipment(character, equipment));
        }

        protected virtual Entity equipmentForSlot(Character character, Equipment.Slot slot)
        {
            return character.equipment[slot];
        }

        protected abstract string fileNameForEquipment(Character character, T equipment);
    }
}
