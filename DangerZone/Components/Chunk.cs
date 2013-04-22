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
        public int size = 0;
        public Chunk right = null, bottom = null, corner = null;
        public bool hasLeft = false, hasTop = false;
        public bool clean = false;
    }
}
