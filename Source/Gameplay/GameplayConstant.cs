using System;

namespace MucciArena.Gameplay
{
    public static class GameplayConstant
    {
        public static Random Random = new Random();

        public const string Event_PlayerDied = "p_D";
        public const string Event_PlayerHurt = "p_H";
        public const string Event_CollectibleGrabbed = "c_G";
        public const string Event_SuccessGame = "s_G";

        public const int MinXBoundary = 0;
        public const int MinYBoundary = 100;
        public const int MaxXBoundary = 900;
        public const int MaxYBoundary = 1000;

        public const int MaxCollidables = 700;

        public const float GreenSpeed = 300f;
        public const float BlueSpeed = 75f;
        public const float Velocity = 350f;

        public const int CookieCounterLimit = 150;
        public const int RateAtWhichBlueSpawn = 3;
        public const int RateAtWhichYellowSpawn = 23;

        public const float PushbackAdd = 50f;
        public const float StartingForce = 500f;
        public const float FinalForce = 850f;

        public const int StartingMaxHealth = 4;

        public const string SuccessMessage = "Oh you won, cookie?";
        public const string DeathMessage = "Maybe if you were better, this wouldn't of happened.";
        public const string FPSUI = "FPS: ";
        public const string CookieUI = "Cookies: ";
        public const string DevHealthUI = "Health: ";
    }
}
