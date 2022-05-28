using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MucciArena.Drawing;
using MucciArena.Entities.Tasks;
using MucciArena.Gameplay;
using MucciArena.Management;
using System;

namespace MucciArena.Entities
{
    public abstract class Enemy : CollisionObject, IDrawable, IContentLoadable
    {
        public Texture2D Graphic;

        public Color Tint;
        protected int damage;
        protected float flightStrength;

        public bool SpawnsFromQuadrant = true;

        protected bool notInFlight { get => !_inFlight; }
        private bool _inFlight;
        private SecondCounter _progressInFlight;
        private float _pushBack;
        private Vector2 _flightVelocity;

        protected void MoveToDestination(float delta, float speed, Vector2 dest)
        {
            var dir = dest - CollisionCircle.Location;
            dir.Normalize();
            CollisionCircle.Location += dir * (speed * delta);
        }

        private void DealWithFlight(float delta)
        {
            _progressInFlight.Update(delta);
            var functionReturn = FlightFunction(_progressInFlight.SecondsElapsed);
            CollisionCircle.Location += _flightVelocity * functionReturn * _pushBack * delta;
            _inFlight = functionReturn >= 0;
        }

        private float FlightFunction(float x)
        {
            x += .7f;
            var sqrt = Math.Sqrt(x);
            return (float)(flightStrength * sqrt - x * sqrt);
        }

        public override CollisionEvent GenerateCollisionEvent()
        {
            return new CollisionEvent()
            {
                Damage = damage,
                Sender = this,
                ObjectMass = Mass
            };
        }

        public override void OnCollision(CollisionEvent collisionEvent)
        {
            if (collisionEvent.Sender is Player)
            {
                _inFlight = true;
                collisionEvent.PenetrationAngle += (float)GameplayConstant.Random.NextDouble() * 1.2f - .8f;
                _flightVelocity = new Vector2(-(float)Math.Cos(collisionEvent.PenetrationAngle), -(float)Math.Sin(collisionEvent.PenetrationAngle));
                _pushBack = collisionEvent.StartingForce;
                _progressInFlight = new SecondCounter();
            }
            else
            {
                if (Mass <= collisionEvent.ObjectMass)
                {
                    var toAdd = new Vector2(-(float)Math.Cos(collisionEvent.PenetrationAngle), -(float)Math.Sin(collisionEvent.PenetrationAngle));
                    toAdd.Normalize();
                    CollisionCircle.Location += toAdd * collisionEvent.PenetrationDepth;
                }
            }
        }

        public virtual DrawParameters Draw()
        {
            return new DrawParameters()
            {
                Texture = Graphic,
                Box = CollisionCircle.ToRectangle(),
                Tint = Tint,
                Source = null
            };
        }

        public void SetLocation(float x, float y)
        {
            CollisionCircle.X = x;
            CollisionCircle.Y = y;
        }

        public virtual void Update(float delta, PlayerState playerState, WorldState worldState)
        {
            if (_inFlight)
            {
                DealWithFlight(delta);
            }
        }

        public virtual void Load(ContentLibrary library)
        {
            Graphic = library.LoadTexture(ContentLibrary.DevTexture);
            CollisionCircle = new Circle(500, 500, 7);
        }
    }
}
