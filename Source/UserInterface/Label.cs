using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MucciArena.Management;

namespace MucciArena.UserInterface
{
    public class Label : Component
    {
        public string Text;
        private string _fontDir;
        public SpriteFont Font;

        public Label(string text, string font, Point location)
        {
            Text = text;
            _fontDir = font;
            Box = new Rectangle(location, new Point(0, 0));
        }

        public override void Load(ContentLibrary library)
        {
            base.Load(library);
            Font = library.LoadFont(_fontDir);
        }

        public override void Draw(SpriteBatch sprb)
        {
            base.Draw(sprb);
            sprb.DrawString(Font, Text, new Vector2(Box.X, Box.Y), Color.White);
        }
    }
}
