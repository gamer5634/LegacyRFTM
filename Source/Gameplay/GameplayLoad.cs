using System.Collections.Generic;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MucciArena.Entities;
using MucciArena.Events;
using MucciArena.Management;

namespace MucciArena.Gameplay
{
    public partial class GameplayState : IState
    {
        private ContentLibrary _contentLibrary;

        public void LoadContent(ContentManager contentManager, GraphicsDevice graphicsDevice)
        {
            _contentLibrary = new ContentLibrary(contentManager);
            _contentLibrary.GenerateDevTexture(graphicsDevice);

            EventManager.RegisterEventListener(this);

            LoadUI(_contentLibrary);

            MainLoad();
        }

        private void MainLoad()
        {
            CookieCounter = 0;
            SpammingEnemies = false;

            Enemies = new List<Enemy>();
            Player = new Player();
            Cookie = new Collectible();

            Player.Load(_contentLibrary);
            Cookie.Load(_contentLibrary);
            Cookie.ChangeLocation(750, 500);
            CookieCounter = 0;

            _collisionManager = new CollisionManager();
            _collisionManager.Register(Cookie);
            _collisionManager.Register(Player);
        }
    }
}
