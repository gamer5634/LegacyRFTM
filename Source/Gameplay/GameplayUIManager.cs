using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MucciArena.Extension;
using MucciArena.Management;
using MucciArena.UserInterface;

namespace MucciArena.Gameplay
{
    public partial class GameplayState
    {
        private Label _healthCounter;
        private Label _cookieCounter;

        private Label _fpsCounter;
        private FramerateManager _framerate;

        private Color _panelColor;
        private Texture2D _uiTex;
        private bool _noStatusSet;

        public void DrawUI(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_uiTex, new Rectangle(0, 0, GameplayConstant.MaxXBoundary, GameplayConstant.MinYBoundary), _panelColor);

            _healthCounter.Draw(spriteBatch);
            _cookieCounter.Draw(spriteBatch);
            _fpsCounter.Draw(spriteBatch);
        }

        private void SetAStatus(string status)
        {
            _noStatusSet = false;
            _cookieCounter.Text = "";
            _healthCounter.Text = status;
        }

        public void LoadUI(ContentLibrary library)
        {
            _noStatusSet = true;
            _healthCounter = new Label(GameplayConstant.DevHealthUI, "Dev/Font", new Point(35, 15));
            _cookieCounter = new Label(GameplayConstant.CookieUI, "Dev/Font", new Point(235, 15));

            _fpsCounter = new Label(GameplayConstant.FPSUI, "Dev/Font", new Point(35, 65));
            _framerate = new FramerateManager(1000);

            _uiTex = library.LoadTexture(ContentLibrary.DevTexture);
            _panelColor = new Color(0, 3, 41);

            _cookieCounter.Load(library);
            _healthCounter.Load(library);
            _fpsCounter.Load(library);
        }

        public void UpdateUI(double secondsElapsed)
        {
            _framerate.Update(secondsElapsed);

            if (_noStatusSet)
            {
                _healthCounter.Text = GameplayConstant.DevHealthUI + Player.Health;
                _cookieCounter.Text = GameplayConstant.CookieUI + CookieCounter;
            }
            _fpsCounter.Text = GameplayConstant.FPSUI + _framerate.Framerate.ToString("0000");
        }
    }
}
