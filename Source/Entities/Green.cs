using Microsoft.Xna.Framework;
using MucciArena.Gameplay;
using MucciArena.Management;

namespace MucciArena.Entities
{
    public class Green : Enemy
    {
        public override void Update(float delta, PlayerState playerState, WorldState worldState)
        {
            base.Update(delta, playerState, worldState);

            if (notInFlight)
            {
                MoveToDestination(delta, GameplayConstant.GreenSpeed, playerState.Location);
            }

            CheckForBounds();
        }

        public override void Load(ContentLibrary library)
        {
            base.Load(library);

            Tint = Color.Green;
            damage = 1;
            flightStrength = 2.2f;
            Mass = 1;
        }
    }
}
