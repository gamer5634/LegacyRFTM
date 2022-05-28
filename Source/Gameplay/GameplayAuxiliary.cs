using MucciArena.Entities;

namespace MucciArena.Gameplay
{
    public partial class GameplayState
    {
        public void SpamEnemies()
        { // funny atse spam reference
            SpammingEnemies = true;
        }

        public Enemy GetNewEnemyType()
        {
            //if (Enemies.Count % GameplayConstant.RateAtWhichYellowSpawn == 0) return new Yellow();
            if (Enemies.Count % GameplayConstant.RateAtWhichBlueSpawn == 0) return new Blue();
            return new Green();
        }

        public void RegisterEnemyAtNewLocation(Enemy newEnemy)
        {
            newEnemy.Load(_contentLibrary);
            if (newEnemy.SpawnsFromQuadrant)
            {
                var quadX = GameplayConstant.Random.Next(2);
                var quadY = GameplayConstant.Random.Next(2);
                newEnemy.SetLocation(quadX * GameplayConstant.MaxXBoundary, quadY * GameplayConstant.MaxXBoundary + 100);
            }
            _collisionManager.Register(newEnemy);
            Enemies.Add(newEnemy);
        }

        public void Reset()
        {
            MainLoad();
        }

        #region IDisposable

        private bool _startedDisposal;

        protected virtual void Dispose(bool disposing)
        {
            if (!_startedDisposal)
            {
                if (disposing)
                {
                    Player = null;
                    Enemies = null;
                }

                _startedDisposal = true;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            System.GC.SuppressFinalize(this);
        }

        #endregion
    }
}
