using System;
using Artemis.Interface;
using DangerZone.ScreenManagement;
using DangerZone.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace DangerZone.Components
{
    public class Tile : IComponent
    {
        public enum Type
        {
            Grass, Dirt, DarkDirt, Water
        }

        public Type type;
        public int zLevel;
    }
}
