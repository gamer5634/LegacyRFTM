using Microsoft.Xna.Framework;

namespace MucciArena.Entities
{
    public struct Circle // only used on entities
    {
        public float X { get => Location.X; set => Location.X = value; }
        public float Y { get => Location.Y; set => Location.Y = value; }

        public float Radius;
        public Vector2 Location;

        public Circle(float x, float y, float radius)
        {
            Location = new Vector2(x, y);
            Radius = radius;
        }

        public static bool CircleVCircle(Circle x1, Circle x2, out float penetrationDepth)
        {
            penetrationDepth = x1.Radius + x2.Radius - Vector2.Distance(x1.Location, x2.Location);

            return penetrationDepth >= 0;
        }

        // for drawing
        public Rectangle ToRectangle()
        {
            return new Rectangle((int)(X - Radius), (int)(Y - Radius), (int)(Radius + Radius), (int)(Radius + Radius));
        }

        public static Circle IncreaseRadius(Circle original, float toIncreaseBy)
        {
            original.Radius += toIncreaseBy;
            return original;
        }
    }
}
