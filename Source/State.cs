using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MucciArena
{
    public interface IState : IDisposable
    {
        void Update(GameTime gameTime);
        void Draw(SpriteBatch spriteBatch, GraphicsDevice graphicsDevice);
        void LoadContent(ContentManager contentManager, GraphicsDevice graphicsDevice);
        void Reset();
    }
}
