using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MucciArena.Drawing
{
    public struct DrawParameters
    {
        public Texture2D Texture;
        public Color Tint;
        public Rectangle Box;
        public Rectangle? Source;
    }
}
