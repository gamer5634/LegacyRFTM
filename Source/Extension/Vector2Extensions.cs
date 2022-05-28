using Microsoft.Xna.Framework;

namespace MucciArena.Extension
{
    public static class Vector2Extensions
    {
        public static Vector2 FluentNormalize(this Vector2 raw)
        {
            raw.Normalize();
            return raw;
        }

        public static Vector2 StabilizeVector2(this Vector2 raw)
        {
            var newX = raw.X;
            var newY = raw.Y;
            if (float.IsNaN(newX)) newX = 0;
            if (float.IsNaN(newY)) newY = 0;
            return new Vector2(newX, newY);
        }
    }
}
