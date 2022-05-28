using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace MucciArena.Management
{
    public class ContentLibrary
    {
        public const string DevTexture = "U%Dev";

        private Dictionary<string, Texture2D> _textures;
        private Dictionary<string, SpriteFont> _fonts;

        private ContentManager _contentManager;

        public ContentLibrary(ContentManager contentManager) 
        { 
            _contentManager = contentManager;
            _textures = new Dictionary<string, Texture2D>();
            _fonts = new Dictionary<string, SpriteFont>();
        }

        public bool GenerateDevTexture(GraphicsDevice graphicsDevice)
        {
            if (_textures != null)
            {
                Texture2D newTex = new Texture2D(graphicsDevice, 1, 1);
                newTex.SetData(new Color[] { Color.White });
                _textures.Add(DevTexture, newTex);

                return true;
            }
            return false;
        }

        public SpriteFont LoadFont(string dir)
        {
            if (_fonts.ContainsKey(dir)) return _fonts[dir];

            _fonts.Add(dir, _contentManager.Load<SpriteFont>(dir));

            return LoadFont(dir);
        }

        public Texture2D LoadTexture(string dir)
        {
            if (_textures.ContainsKey(dir)) return _textures[dir];

            _textures.Add(dir, _contentManager.Load<Texture2D>(dir));

            return LoadTexture(dir);
        }
    }
}
