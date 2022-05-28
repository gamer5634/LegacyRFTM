using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MucciArena.Management;

namespace MucciArena.UserInterface
{
    public abstract class Component : IContentLoadable, IUpdatable
    {
        public Rectangle Box;

        public virtual void Load(ContentLibrary library)
        {
            
        }

        public virtual void Update(float delta)
        {
            
        }

        public virtual void Draw(SpriteBatch sprb)
        {

        }
    }
}
