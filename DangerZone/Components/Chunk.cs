using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Artemis;
using Artemis.Interface;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DangerZone.Components
{
    public class Chunk : IComponent
    {
        public Entity[,] tiles = null;
        public Point size = Point.Zero;
        public Chunk right = null, bottom = null, corner = null;
        public bool clean = false;
    }
}
