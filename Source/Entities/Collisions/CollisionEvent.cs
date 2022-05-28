namespace MucciArena.Entities
{
    public struct CollisionEvent
    {
        public object Sender;
        public int Damage;
        public bool AvoidInvincibility;
        public float StartingForce;
        public float PenetrationDepth;
        public float PenetrationAngle;
        public float ObjectMass;
    }
}
