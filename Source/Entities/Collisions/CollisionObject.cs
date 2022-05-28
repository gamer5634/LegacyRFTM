using MucciArena.Gameplay;

namespace MucciArena.Entities
{
    public abstract class CollisionObject
    {
        public Circle CollisionCircle;
        public float Mass;

        protected void CheckForBounds()
        {
            var l = CollisionCircle.Location;
            var r = CollisionCircle.Radius;
            if (l.X - r <= GameplayConstant.MinXBoundary) l.X = r;
            if (l.X + r >= GameplayConstant.MaxXBoundary) l.X = GameplayConstant.MaxXBoundary - r;
            if (l.Y - r <= GameplayConstant.MinYBoundary) l.Y = r + GameplayConstant.MinYBoundary;
            if (l.Y + r >= GameplayConstant.MaxYBoundary) l.Y = GameplayConstant.MaxYBoundary - r;
            CollisionCircle.Location = l;
        }

        public virtual CollisionEvent GenerateCollisionEvent()
        {
            return new CollisionEvent()
            {
                Sender = this,
                ObjectMass = Mass,
            };
        }

        public virtual void OnCollision(CollisionEvent collisionEvent)
        {

        }
    }
}
