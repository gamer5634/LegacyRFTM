using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;

namespace MucciArena
{
    public static class InputManager
    {
        private static KeyboardState[] _keyboardStates;

        private static MouseState[] _mouseStates;

        public static void Initialize()
        {
            _keyboardStates = new KeyboardState[2];
            _mouseStates = new MouseState[2];
        }

        public static void Update()
        {
            _keyboardStates[1] = _keyboardStates[0];
            _keyboardStates[0] = Keyboard.GetState();

            _mouseStates[1] = _mouseStates[0];
            _mouseStates[0] = Mouse.GetState();
        }

        #region Input Abstraction

        public static bool IsButtonUp(Keys key, bool usingPresentState = true)
        {
            return _keyboardStates[Convert.ToInt32(usingPresentState)].IsKeyUp(key);
        }

        public static bool IsButtonDown(Keys key, bool usingPresentState = true)
        {
            return _keyboardStates[Convert.ToInt32(usingPresentState)].IsKeyDown(key);
        }

        public static bool IsMouseTwoUp(bool usingPresentState = true)
        {
            return _mouseStates[Convert.ToInt32(usingPresentState)].RightButton == ButtonState.Released;
        }

        public static bool IsMouseOneUp(bool usingPresentState = true)
        {
            return _mouseStates[Convert.ToInt32(usingPresentState)].LeftButton == ButtonState.Released;
        }

        public static bool IsMouseTwoDown(bool usingPresentState = true)
        {
            return _mouseStates[Convert.ToInt32(usingPresentState)].RightButton == ButtonState.Pressed;
        }

        public static bool IsMouseOneDown(bool usingPresentState = true)
        {
            return _mouseStates[Convert.ToInt32(usingPresentState)].LeftButton == ButtonState.Pressed;
        }

        public static bool IsMouseOnePressed()
        {
            return IsMouseOneDown() && IsMouseOneUp(false);
        }

        public static bool IsMouseTwoPressed()
        {
            return IsMouseTwoDown() && IsMouseTwoUp(false);
        }

        public static bool IsButtonPressed(Keys key)
        {
            return IsButtonDown(key) && IsButtonUp(key, false);
        }

        #endregion

        #region Settings + Movement Abstraction

        public static Vector2 GetRawMovementVector()
        {
            var ret = new Vector2(0, 0);

            if (IsButtonDown(Keys.W)) ret.Y--;
            if (IsButtonDown(Keys.S)) ret.Y++;
            if (IsButtonDown(Keys.A)) ret.X--;
            if (IsButtonDown(Keys.D)) ret.X++;

            return ret;
        }

        #endregion
    }
}
