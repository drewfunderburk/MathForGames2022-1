using System;
using System.Collections.Generic;
using System.Text;
using MathLibrary;

namespace MathForGames
{
    class Bullet : Actor
    {
        private float _lifeTime = 2000;

        private Actor _spawner;

        private System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();

        public float Speed { get; set; } = 1000;

        public Bullet(Actor spawner, float x, float y, float angle)
            :base(x, y)
        {
            _sprite = new Sprite("Images/bullet.png");
            _spawner = spawner;
            _collisionRadius = 5;
            SetRotation(angle);
        }

        public Bullet(Actor spawner, Vector2 position, float angle) : this(spawner, position.X, position.Y, angle) { }

        public override void OnCollision(Actor other)
        {
            if (other == _spawner)
                return;

            if (other is Bullet)
                return;

            base.OnCollision(other);

            WillDestroy = true;
        }

        public override void Start()
        {
            stopwatch.Start();
            base.Start();
        }

        public override void Update(float deltaTime)
        {
            Velocity = Forward * Speed;
            float currentTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();
            if (stopwatch.ElapsedMilliseconds > _lifeTime)
                WillDestroy = true;
            base.Update(deltaTime);
        }
    }
}
