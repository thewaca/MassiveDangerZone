using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DangerZone.Components;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace DangerZone.Sprites
{
    class WeaponSpriteLayer:CharacterSpriteLayer
    {
        public WeaponSpriteLayer(ContentManager contentManager, Character character, Weapon.Type weaponType):base(character)
        {
            Dictionary<Weapon.Type, String> fileNames;
            switch (character.gender)
            {
                case Gender.Male:
                    fileNames = this.maleWeapons;
                    break;
                case Gender.Female:
                    fileNames = this.femaleWeapons;
                    break;
                default: throw new Exception("fuck you");
            }
            
            Texture = contentManager.Load<Texture2D>(fileNames[weaponType]);
        }

        private Dictionary<Weapon.Type, String> femaleWeapons = new Dictionary<Weapon.Type, string>
            {
                {Weapon.Type.Sword, "sprites\\weapons\\dagger_male"},
                {Weapon.Type.Spear, "sprites\\body\\female\\spear"},
                {Weapon.Type.Bow, "sprites\\body\\female\\bow"}
            };

        private Dictionary<Weapon.Type, String> maleWeapons = new Dictionary<Weapon.Type, string>
            {
                {Weapon.Type.Sword, "sprites\\weapons\\dagger_male"},
                {Weapon.Type.Spear, "sprites\\body\\female\\spear"},
                {Weapon.Type.Bow, "sprites\\body\\female\\bow"}
            };
    }
}
