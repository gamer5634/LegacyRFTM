using Microsoft.Xna.Framework.Graphics;

namespace MucciArena.Drawing
{
    public static class SpritebatchExtension
    {
        public static void Draw(this SpriteBatch spriteBatch, DrawParameters drawParams)
        {
            spriteBatch.Draw(drawParams.Texture, drawParams.Box, drawParams.Source, drawParams.Tint);
        }
    }
}
