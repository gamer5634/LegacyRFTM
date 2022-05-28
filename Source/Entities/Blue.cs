using Microsoft.Xna.Framework;
using MucciArena.Gameplay;
using MucciArena.Management;

namespace MucciArena.Entities
{
    public class Blue : Enemy
    {
        public override void Update(float delta, PlayerState playerState, WorldState worldState)
        {
            base.Update(delta, playerState, worldState);

            if (notInFlight)
            {
                MoveToDestination(delta, GameplayConstant.BlueSpeed, playerState.Location);
            }

            CheckForBounds();
        }

        public override void Load(ContentLibrary library)
        {
            base.Load(library);

            CollisionCircle.Radius = 12f;

            Tint = Color.Blue;
            damage = 2;
            flightStrength = 2.5f;
            Mass = 2;
        }
    }
}
