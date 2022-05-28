using MucciArena.Events;

namespace MucciArena.Gameplay
{
    public partial class GameplayState : IEventListener
    {
        private void OnCollectibleGrabbed()
        {
            var newX = NewCollectibleAxisChange(Cookie.CollisionCircle.X, GameplayConstant.MinXBoundary);
            var newY = NewCollectibleAxisChange(Cookie.CollisionCircle.Y);
            Cookie.ChangeLocation(newX, newY);

            CookieCounter++;

            RegisterEnemyAtNewLocation(GetNewEnemyType());
            Player.AddToStartingForce(GameplayConstant.PushbackAdd);

            if (CookieCounter >= GameplayConstant.CookieCounterLimit) EventManager.SendMessage(GameplayConstant.Event_SuccessGame);
        }

        private float NewCollectibleAxisChange(float firstPos, int minBoundary=GameplayConstant.MinYBoundary)
        {
            var newPos = firstPos;
            newPos -= GameplayConstant.Random.Next(minBoundary+100, minBoundary + 600);
            if (newPos <= minBoundary) newPos += GameplayConstant.MaxXBoundary;
            if (newPos >= GameplayConstant.MaxXBoundary) newPos -= GameplayConstant.MaxXBoundary;

            if (newPos <= minBoundary) newPos += minBoundary;

            return newPos;
        }

        public void FetchEvent(string message)
        {
#if DEBUG
            System.Diagnostics.Debug.WriteLine("Event: " + message);
#endif
            if (message == GameplayConstant.Event_PlayerDied) SpamEnemies();
            if (message == GameplayConstant.Event_CollectibleGrabbed) OnCollectibleGrabbed();
            if (message == GameplayConstant.Event_SuccessGame) SetAStatus(GameplayConstant.SuccessMessage);
            if (message == GameplayConstant.Event_PlayerDied) SetAStatus(GameplayConstant.DeathMessage);
            if (message == Loop.ResetCurrentState) _noStatusSet = true;
        }
    }
}
