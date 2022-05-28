using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MucciArena.Drawing;
using MucciArena.Entities;

namespace MucciArena.Gameplay
{
    public partial class GameplayState
    {
        public void Draw(SpriteBatch spriteBatch, GraphicsDevice graphicsDevice)
        {
            graphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();
            ExecuteDraw(spriteBatch);
            spriteBatch.End();
        }

        private void ExecuteDraw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Cookie.Draw());

            foreach (Enemy e in Enemies)
                spriteBatch.Draw(e.Draw());

            spriteBatch.Draw(Player.Draw());
            DrawUI(spriteBatch);
        }
    }
}
