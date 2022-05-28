using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MucciArena.Events;
using MucciArena.Gameplay;
using System;

namespace MucciArena
{
    public class Loop : Game, IEventListener
    {
        public const string ResetCurrentState = "reset";

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private IState _currentState;

        public Loop()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        public void ChangeState(IState newState)
        {
            if (newState != null)
            {
                _currentState.Dispose();
                _currentState = newState;
                _currentState.LoadContent(Content, GraphicsDevice);
            }
        }

        private void SetWindowProperties(bool uncappedFPS, bool vsync, int width, int height, string title)
        {
            IsFixedTimeStep = uncappedFPS;
            TargetElapsedTime = TimeSpan.FromSeconds(1 / EngineConstant.FramerateCap);
            _graphics.SynchronizeWithVerticalRetrace = vsync;

            _graphics.PreferredBackBufferWidth = width;
            _graphics.PreferredBackBufferHeight = height;

            _graphics.ApplyChanges();

            Window.Title = title;
        }

        protected override void Initialize()
        {
            SetWindowProperties(true, false, 900, 1000, "{Not dated} RFTM Sequel Prototype");

            EventManager.Initialize();
            EventManager.RegisterEventListener(this);

            _currentState = new GameplayState();

            InputManager.Initialize();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _currentState.LoadContent(Content, GraphicsDevice);
        }

        protected override void Update(GameTime gameTime)
        {
            InputManager.Update();
            _currentState.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            _currentState.Draw(_spriteBatch, GraphicsDevice);
            base.Draw(gameTime);
        }

        public void FetchEvent(string message)
        {
            if (message == ResetCurrentState) _currentState.Reset();
        }
    }
}
