using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Artemis;
using Artemis.Interface;
using Microsoft.Xna.Framework;

namespace DangerZone.Components
{
    class Chunk : IComponent
    {
        public Entity[,] tiles = null;
    }
}
