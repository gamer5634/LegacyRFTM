using Microsoft.Xna.Framework;
using MucciArena.Entities;
using MucciArena.Events;
using System.Collections.Generic;

namespace MucciArena.Gameplay
{
    public partial class GameplayState
    {
        public Player Player;
        public List<Enemy> Enemies;

        public Collectible Cookie;
        public int CookieCounter;

        private CollisionManager _collisionManager;

        public bool SpammingEnemies;

        private void MiscChecks()
        {
            if (SpammingEnemies)
            {
                RegisterEnemyAtNewLocation(GetNewEnemyType());
                if (Enemies.Count >= GameplayConstant.MaxCollidables) SpammingEnemies = false;
            }
        }

        private void CheckReset()
        {
            if (InputManager.IsButtonPressed(Microsoft.Xna.Framework.Input.Keys.Back))
                EventManager.SendMessage(Loop.ResetCurrentState);
        }

        public void Update(GameTime gameTime)
        {
            float delta = (float)gameTime.ElapsedGameTime.TotalSeconds;

            CheckReset();

            UpdateEnemiesAndPlayer(delta);
            _collisionManager.CheckCollisions();

            UpdateUI(gameTime.ElapsedGameTime.TotalSeconds);//double instead of float

            MiscChecks();
        }

        private void UpdateEnemiesAndPlayer(float delta)
        {
            var p_state = Player.Update(delta);
            if (Enemies.Count > 0)
            {
                UpdateEnemies(delta, p_state);
            }
        }

        private void UpdateEnemies(float delta, PlayerState p_state)
        {
            foreach (Enemy e in Enemies)
            {
                e.Update(delta, p_state, GenerateWorldState());
            }
        }

        private WorldState GenerateWorldState()
        {
            return new WorldState()
            {
                CookieLocation = Cookie.CollisionCircle.Location,
            };
        }
    }
}
