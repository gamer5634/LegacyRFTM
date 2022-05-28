using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MucciArena.Drawing;
using MucciArena.Entities.Tasks;
using MucciArena.Events;
using MucciArena.Extension;
using MucciArena.Gameplay;
using MucciArena.Management;

namespace MucciArena.Entities
{
    public class Player : CollisionObject, IDrawable, IContentLoadable
    {
        private int _maxHealth;
        public int Health;

        private float _vel;

        private Texture2D _texture;

        private Color _hurtTint;
        private Color _defaultTint;
        private Color _activeTint;

        private SecondCounter _invisTimer;

        private float _pushbackForce;

        public bool IsAlive { get => Health > 0; }
        public bool IsDead { get => Health <= 0; }

        private void SendMessage(string message)
        {
            if (IsAlive) EventManager.SendMessage(message);
        }

        public void Load(ContentLibrary library)
        {
            CollisionCircle = new Circle(15, 15, 15);
            CollisionCircle.Location = new Vector2(100, 150);

            _maxHealth = GameplayConstant.StartingMaxHealth;
            Health = _maxHealth;

            _vel = GameplayConstant.Velocity;

            _texture = library.LoadTexture(ContentLibrary.DevTexture);

            _invisTimer = new SecondCounter(1);
            _invisTimer.Update(1); // To make sure tints aren't weird in the beginning

            _pushbackForce = GameplayConstant.StartingForce;
            Mass = 15; // low priority

            _defaultTint = Color.White;
            _hurtTint = Color.Red;

            _activeTint = _defaultTint;
        }

        public override void OnCollision(CollisionEvent collisionEvent)
        {
            if (_invisTimer.IsCapReached())
            {
                if (Health - collisionEvent.Damage <= 0) OnDeath();
                Health -= collisionEvent.Damage;
                SendMessage(GameplayConstant.Event_PlayerHurt);
                _activeTint = _hurtTint;

                _invisTimer.ResetCounter();
            }
        }

        public override CollisionEvent GenerateCollisionEvent()
        {
            return new CollisionEvent()
            {
                StartingForce = _pushbackForce,
                Sender = this,
                ObjectMass = Mass,
            };
        }

        private PlayerState GenerateState()
        {
            return new PlayerState()
            {
                Dead = IsDead,
                Health = Health,
                Location = CollisionCircle.Location,
            };
        }

        private void OnDeath()
        {
            _activeTint = _hurtTint;
            SendMessage(GameplayConstant.Event_PlayerDied);
        }

        public PlayerState Update(float delta)
        {
            if (IsAlive)
            {
                _invisTimer.Update(delta);
                SettingCommonVariables();
                MovePlayer(delta);
                CheckForBounds();
            }

            return GenerateState();
        }

        private void SettingCommonVariables()
        {
            if (_invisTimer.IsCapNotReached())
                _activeTint = Color.Lerp(_hurtTint, _defaultTint, _invisTimer.SecondsElapsed / _invisTimer.SecondsCap);
            else
            {
                _activeTint = _defaultTint;
                _vel = GameplayConstant.Velocity;
            }
        }

        private void MovePlayer(float delta)
        {
            var raw = InputManager.GetRawMovementVector().FluentNormalize().StabilizeVector2();
            var toAdd = raw * (_vel * delta);
            CollisionCircle.Location += toAdd;
        }

        public void AddToStartingForce(float toAdd)
        {
            if (_pushbackForce + toAdd > GameplayConstant.FinalForce)
                _pushbackForce += toAdd;
        }

        public DrawParameters Draw()
        {
            return new DrawParameters()
            {
                Texture = _texture,
                Tint = _activeTint,
                Box = CollisionCircle.ToRectangle(),
                Source = null
            };
        }
    }
}
