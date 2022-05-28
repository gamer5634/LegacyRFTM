using MucciArena.Gameplay;
using System;
using System.Collections.Generic;

namespace MucciArena.Entities
{
    public class CollisionManager
    {
        private List<CollisionObject> _collisionGroup;
        private List<CollisionObject> _queuedCollisions;
        private bool _notCheckingForCollisions;

        public CollisionManager()
        {
            _collisionGroup = new List<CollisionObject>();
            _queuedCollisions = new List<CollisionObject>();
            _notCheckingForCollisions = true;
        }

        public bool Register(CollisionObject newCollisionObject)
        {
            if (_notCheckingForCollisions)
                return AddToList(ref _collisionGroup, newCollisionObject);
            else
                return AddToList(ref _queuedCollisions, newCollisionObject);
        }

        private bool AddToList(ref List<CollisionObject> toAdd, CollisionObject obj)
        {
            if (_collisionGroup.Count + _queuedCollisions.Count < GameplayConstant.MaxCollidables)
            {
                toAdd.Add(obj);
                return true;
            }
            return false;
        }

        private void CheckForNewQueuedCollisionObjects()
        {
            if (_queuedCollisions.Count > 0)
            {
                foreach (CollisionObject c in _queuedCollisions)
                    _collisionGroup.Add(c);
                _queuedCollisions.Clear();
            }
        }

        public void CheckCollisions()
        {
            CheckForNewQueuedCollisionObjects();

            _notCheckingForCollisions = false;

            foreach (CollisionObject c1 in _collisionGroup)
            {
                foreach (CollisionObject c2 in _collisionGroup)
                {
                    OnCollision(c1, c2);
                }
            }

            _notCheckingForCollisions = true;
        }

        private void OnCollision(CollisionObject collisionObject1, CollisionObject collisionObject2)
        {
            var x1 = collisionObject1.CollisionCircle;
            var x2 = collisionObject2.CollisionCircle;

            if (collisionObject1 != collisionObject2 && 
                Circle.CircleVCircle(collisionObject1.CollisionCircle, collisionObject2.CollisionCircle, out float penetrationDepth))
            {
                var collisionEvent = collisionObject2.GenerateCollisionEvent();

                collisionEvent.PenetrationAngle = (float)Math.Atan2(x2.Y - x1.Y, x2.X - x1.X);
                collisionEvent.PenetrationDepth = penetrationDepth;

                collisionObject1.OnCollision(collisionEvent);
            }
        }
    }
}
