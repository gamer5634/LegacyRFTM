using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MucciArena.Drawing;
using MucciArena.Events;
using MucciArena.Gameplay;
using MucciArena.Management;

namespace MucciArena.Entities
{
    public class Collectible : CollisionObject, IDrawable, IContentLoadable
    {
        private const int _size = 20;

        private Texture2D _texture;

        public DrawParameters Draw()
        {
            return new DrawParameters()
            {
                Texture = _texture,
                Tint = new Color(230, 206, 160),
                Box = CollisionCircle.ToRectangle(),
                Source = null
            };
        }

        public void ChangeLocation(float x, float y)
        {
            CollisionCircle = new Circle(x, y, _size);
        }

        public override void OnCollision(CollisionEvent collisionEvent)
        {
            if (collisionEvent.Sender is Player) EventManager.SendMessage(GameplayConstant.Event_CollectibleGrabbed);
        }

        public void Load(ContentLibrary library)
        {
            _texture = library.LoadTexture(ContentLibrary.DevTexture);
            Mass = 5000;
            ChangeLocation(0,0);
        }
    }
}
