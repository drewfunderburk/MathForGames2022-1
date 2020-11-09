using System;
using System.Collections.Generic;
using System.Text;
using Raylib_cs;
using MathLibrary;

namespace MathForGames
{
    /// <summary>
    /// An actor that moves based on input given by the user
    /// </summary>
    class Player : Actor
    {
        private float _speed = 10;
        private float _fireDelay = 100;

        private System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();
        public float Speed
        {
            get
            {
                return _speed;
            }
            set
            {
                _speed = value;
            }
        }

        /// <param name="x">Position on the x axis</param>
        /// <param name="y">Position on the y axis</param>
        public Player(float x, float y)
            : base(x, y)
        {
            _sprite = new Sprite("Images/player.png");
            stopwatch.Start();
        }

        public override void Update(float deltaTime)
        {
            //Gets the player's input to determine which direction the actor will move in on each axis 
            int xDirection = -Convert.ToInt32(Game.GetKeyDown((int)KeyboardKey.KEY_A))
                + Convert.ToInt32(Game.GetKeyDown((int)KeyboardKey.KEY_D));
            int yDirection = -Convert.ToInt32(Game.GetKeyDown((int)KeyboardKey.KEY_W))
                + Convert.ToInt32(Game.GetKeyDown((int)KeyboardKey.KEY_S));

            //Set the actors current velocity to be the a vector with the direction found scaled by the speed
            Velocity = new Vector2(xDirection, yDirection);
            LocalPosition += Velocity.Normalized * Speed;

            Vector2 mousePos = new Vector2(Raylib.GetMouseX(), Raylib.GetMouseY());
            LookAt(mousePos);

            // Shoot
            if (Raylib.IsMouseButtonDown(MouseButton.MOUSE_LEFT_BUTTON))
            {
                float currentTime = stopwatch.ElapsedMilliseconds;
                if (currentTime > _fireDelay)
                {
                    Bullet bullet = new Bullet(this, GlobalPosition, _rotationAngle);
                    Game.GetCurrentScene().AddActor(bullet);
                    stopwatch.Restart();
                }
            }

            base.Update(deltaTime);
        }
    }
}
